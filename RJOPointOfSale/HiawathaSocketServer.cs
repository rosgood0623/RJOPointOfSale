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

        public HiawathaSocketServer()
        {
            mClients = new List<TcpClient>();
        }

        protected virtual void OnRaiseClientConnectedEvent(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = RaiseClientConnectedEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public async void StartListeningForIncomingConnection(IPAddress a_ipaddr = null, int a_port = 23000)
        {
            if(a_ipaddr == null)
            {
                a_ipaddr = IPAddress.Any;
            }

            if(a_port <= 0)
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

        }

        public void StopServer()
        {
            try
            {
                if(m_TCPListener != null)
                {
                    m_TCPListener.Stop();
                }

                foreach(TcpClient c in mClients)
                {
                    c.Close();
                }

                mClients.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

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

                    if(nRet == 0)
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
            catch(Exception ex)
            {
                RemoveClient(a_paramClient);
                Debug.WriteLine(ex.ToString());
            }
        }

        private void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient))
            {
                mClients.Remove(paramClient);
                Debug.WriteLine("Client removed, count: {0}", mClients.Count);
            }
        }

        public async void SendToAll(string leMessage)
        {
            if(string.IsNullOrEmpty(leMessage))
            {
                return;
            }

            try
            {
                byte[] buffMessage = Encoding.ASCII.GetBytes(leMessage);

                foreach(TcpClient c in mClients)
                {
                    await c.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
