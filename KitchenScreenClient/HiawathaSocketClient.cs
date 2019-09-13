using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using KitchenScreenClient;


namespace HiawathaSocketAsync
{
    /// <summary>
    /// Handles the 'client-side' part of this project. Used to receive customer
    /// order information from the MainMenuPoS.
    /// </summary>
    /// <remarks>
    /// NAME: HiawathaSocketClient
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks>
    public class HiawathaSocketClient
    {
        private readonly Screen m_screen;
        private IPAddress m_serverIPAddress;
        private int m_serverPort;
        private TcpClient m_client;

        /// <summary>
        /// The default constructor for the HiawathaSocketClient object.
        /// Initializes members.
        /// </summary>
        /// <remarks>
        /// NAME: HiawathaSocketClient
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        public HiawathaSocketClient()
        {
            m_client = null;
            m_serverPort = -1;
            m_serverIPAddress = null;
        }/*public HiawathaSocketClient()*/

        /// <summary>
        /// A constructor for the HiawathaSocketClient object. Inherits from the default
        /// constructor and initializes the passed Screen object.
        /// </summary>
        /// <remarks>
        /// NAME: HiawathaSocketClient
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_screen"></param>
        public HiawathaSocketClient(Screen a_screen) : this()
        {
            m_screen = a_screen;
        }/*public HiawathaSocketClient(Screen a_screen) : this()*/

        /// <summary>
        /// Sets the IPAddress of the Server with IP Address validation 
        /// to ensure an appropriate address. 
        /// </summary>
        /// <remarks>
        /// NAME: SetServerIPAddress
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_ipAddressServer">The IP Address entered.</param>
        /// <returns>
        /// Returns true if successfully set, false otherwise.
        /// </returns>
        public bool SetServerIPAddress(string a_ipAddressServer)
        {
            IPAddress ipaddr;
            if (!IPAddress.TryParse(a_ipAddressServer, out ipaddr))
            {
                Console.WriteLine("Invalid IP Address supplied.");
                return false;
            }

            m_serverIPAddress = ipaddr;

            return true;
        }/*public bool SetServerIPAddress(string a_IPAddressServer)*/

        /// <summary>
        /// Sets the server's port number with port number validation to
        /// ensure appropriate port number. Port number must be a number 
        /// that's between 0 and 65535
        /// </summary>
        /// <remarks>
        /// NAME: SetPortNumber
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_serverPort">The entered port number as a string.</param>
        /// <returns>
        /// Returns true if successfully set, false otherwise.
        /// </returns>
        public bool SetPortNumber(string a_serverPort)
        {
            if (!int.TryParse(a_serverPort, out var portNumber))
            {
                Console.WriteLine(@"Invalid server port number.");
                return false;
            }

            if (portNumber <= 0 || portNumber > 65535)
            {
                Console.WriteLine(@"Port number must be between 0 and 65535");
                return false;
            }

            m_serverPort = portNumber;
            return true;
        }/*public bool SetPortNumber(string a_serverPort)*/

        /// <summary>
        /// An asynchronous method that uses the provided IP address and
        /// port number from the above methods to attempt to connect to the
        /// server. 
        /// </summary>
        /// <remarks>
        /// NAME: ConnectToServer
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <returns>
        /// A Task, a special type for handling asynchronous methods
        /// </returns>
        public async Task ConnectToServer()
        {
            if (m_client == null)
            {
                m_client = new TcpClient();
            }

            try
            {
                await m_client.ConnectAsync(m_serverIPAddress, m_serverPort);
                Console.WriteLine(@"Connected to server IP/Port: {0} / {1}", m_serverIPAddress, m_serverPort);

                await ReadDataAsync(m_client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        } /*public async Task ConnectToServer()*/

        /// <summary>
        /// The method used for asynchronously reading data from the server.
        /// </summary>
        /// <remarks>
        /// NAME: ReadDataAsync
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_mClient">The TCPClient object that has an established connection to the server.</param>
        /// <returns>
        /// A Task, a special type for handling asynchronous methods.
        /// </returns>
        private async Task ReadDataAsync(TcpClient a_mClient)
        {
            try
            {
                StreamReader clientStreamReader = new StreamReader(a_mClient.GetStream());
                char[] buff = new char[1024];
                int readByteCount = 0;

                while (true)
                {
                    readByteCount = await clientStreamReader.ReadAsync(buff, 0, buff.Length);

                    if (readByteCount <= 0)
                    {
                        Console.WriteLine(@"Connection lost...");
                        m_client.Close();
                        break;
                    }

                    m_screen.ConvertDataFromPoSIntoScreenElement(new string(buff));
                    //Console.WriteLine("Received bytes: {0} - Message: {1}", readByteCount, new string(buff));

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }/*private async Task ReadDataAsync(TcpClient a_mClient)*/

        /// <summary>
        /// Checks for an established connection.
        /// </summary>
        /// <remarks>
        /// NAME: IsConnected
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <returns>
        /// Returns true if the client is connected, false otherwise.
        /// </returns>
        public bool IsConnected()
        {
            return m_client.Connected;
        }/*public bool IsConnected()*/
    }
}
