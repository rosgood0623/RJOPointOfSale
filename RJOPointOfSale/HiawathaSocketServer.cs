using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HiawathaSocketAsync
{
    public class HiawathaSocketServer
    {
        IPAddress m_ip;
        int m_port;
        TcpListener m_TCPListener;

        List<TcpClient> mClients;

        public EventHandler<ClientConnectedEventArgs> RaiseClientConnectedEvent;

        public bool KeepRunning { get; set; }

        /// <summary>
        /// The default constructor of the HiawathaSocketServer object.
        /// Inits the clients list.
        /// </summary>
        /// <remarks>
        /// NAME: HiawathaSocketServer
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        public HiawathaSocketServer()
        {
            mClients = new List<TcpClient>();
        }/*HiawathaSocketServer()*/

        /// <summary>
        /// Raises a customer event for when a Client has connected to the server.
        /// Used mostly for debugging.
        /// </summary>
        /// <remarks>
        /// NAME: OnRaiseClientConnectedEvent
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="e">The custom ClientConnectEventArgs associated with this event.</param>
        protected virtual void OnRaiseClientConnectedEvent(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = RaiseClientConnectedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }/*OnRaiseClientConnectedEvent(ClientConnectedEventArgs e)*/

        /// <summary>
        /// An asynchronous method to handle and allow the connection of any Clients
        /// that ping the server.
        /// </summary>
        /// <remarks>
        /// NAME: StartListeningForIncomingConnection
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_ipaddr">The IP Address to listen on.</param>
        /// <param name="a_port"> The port number to listen on.</param>
        public async void StartListeningForIncomingConnection(IPAddress a_ipaddr = null, int a_port = 23000)
        {
            if (a_ipaddr == null)
            {
                a_ipaddr = IPAddress.Any;
            }

            if (a_port <= 0)
            {
                a_port = 23000;
            }

            m_ip = a_ipaddr;
            m_port = a_port;

            Debug.WriteLine("IP Address: {0} - Port: {1}", m_ip, m_port);

            m_TCPListener = new TcpListener(m_ip, m_port);

            try
            {
                m_TCPListener.Start();

                KeepRunning = true;
                while (KeepRunning)
                {
                    var returnedByAccept = await m_TCPListener.AcceptTcpClientAsync();

                    mClients.Add(returnedByAccept);

                    Debug.WriteLine("Client connected successfully, count: {0} - {1}", mClients.Count, returnedByAccept.Client.RemoteEndPoint);

                    TakeCareOfTCPClient(returnedByAccept);

                    ClientConnectedEventArgs eaClientConnected = new ClientConnectedEventArgs(returnedByAccept.Client.RemoteEndPoint.ToString());
                    OnRaiseClientConnectedEvent(eaClientConnected);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        } /*StartListeningForIncomingConnection(IPAddress a_ipaddr = null, int a_port = 23000)*/

        /// <summary>
        /// This asynchronous method handles any data that might be sent from
        /// the specified client. 
        /// </summary>
        /// <remarks>
        /// NAME: TakeCareOfTCPClient
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_paramClient">The client the server is receiving data from.</param>
        private async void TakeCareOfTCPClient(TcpClient a_paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            try
            {
                stream = a_paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buff = new char[1024];

                while (KeepRunning)
                {
                    Debug.WriteLine("*** Ready to read");

                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);

                    Debug.WriteLine("Returned: " + nRet);

                    if (nRet == 0)
                    {
                        RemoveClient(a_paramClient);
                        Debug.WriteLine("Socket disconnected");
                        break;
                    }

                    string receivedText = new string(buff);

                    Debug.WriteLine("*** RECEIVED: " + receivedText);

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (Exception ex)
            {
                RemoveClient(a_paramClient);
                Debug.WriteLine(ex.ToString());
            }
        } /*TakeCareOfTCPClient(TcpClient a_paramClient)*/

        /// <summary>
        /// Removes the target client from the client list. 
        /// </summary>
        /// <remarks>
        /// NAME: RemoveClient
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="paramClient">The client to be removed.</param>
        private void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient))
            {
                mClients.Remove(paramClient);
                Debug.WriteLine("Client removed, count: {0}", mClients.Count);
            }
        }/*RemoveClient(TcpClient paramClient)*/

        /// <summary>
        /// An asynchronous method to send any data to all connected clients.
        /// </summary>
        /// <remarks>
        /// NAME: SendToAll
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="leMessage">The raw data to send to the clients.</param>
        public async void SendToAll(string leMessage)
        {
            if (string.IsNullOrEmpty(leMessage))
            {
                return;
            }

            try
            {
                byte[] buffMessage = Encoding.ASCII.GetBytes(leMessage);

                foreach (TcpClient c in mClients)
                {
                    await c.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }/*SendToAll(string leMessage)*/

        /// <summary>
        /// A boolean method to determine if the TCPListener has any clients for it to send to.
        /// </summary>
        /// <remarks>
        /// NAME: NoClients
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <returns>
        /// Returns true if there are no clients, false otherwise.
        /// </returns>
        public bool NoClients()
        {
            return mClients.Count == 0;
        }
    }
}
