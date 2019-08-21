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
    public class HiawathaSocketClient
    {
        private Screen m_screen;
        IPAddress mServerIPAddress;
        int mServerPort;
        TcpClient mClient;


        public HiawathaSocketClient()
        {
            mClient = null;
            mServerPort = -1;
            mServerIPAddress = null;
        }

        public HiawathaSocketClient(Screen a_screen) : this()
        {
            m_screen = a_screen;
        }

        public bool SetServerIPAddress(string _IPAddressServer)
        {
            IPAddress ipaddr;
            if (!IPAddress.TryParse(_IPAddressServer, out ipaddr))
            {
                Console.WriteLine("Invalid IP Address supplied.");
                return false;
            }

            mServerIPAddress = ipaddr;

            return true;           
        }

        public bool SetPortNumber(string a_serverPort)
        {
            if(!int.TryParse(a_serverPort, out var portNumber))
            {
                Console.WriteLine(@"Invalid server port number.");
                return false;
            }

            if(portNumber <= 0 || portNumber > 65535)
            {
                Console.WriteLine(@"Port number must be between 0 and 65535");
                return false;
            }

            mServerPort = portNumber;
            return true;
        }

        public void CloseAndDisconnect()
        {
            if (mClient != null)
            {
                if (mClient.Connected)
                {
                    mClient.Close();
                }
            }
        }

        public async Task SendToServer(string a_strInputUser)
        {
            if (string.IsNullOrEmpty(a_strInputUser))
            {
                Console.WriteLine("Empty string supplied to send");
                return;
            }
            if(mClient != null)
            {
                if (mClient.Connected)
                {
                    StreamWriter clientStreamWriter = new StreamWriter(mClient.GetStream());
                    clientStreamWriter.AutoFlush = true;

                    await clientStreamWriter.WriteAsync(a_strInputUser);
                    Console.WriteLine(@"Data sent....");
                }
            }
        }

        public async Task ConnectToServer()
        {
            if(mClient == null)
            {
                mClient = new TcpClient();
            }

            try
            {
                await mClient.ConnectAsync(mServerIPAddress, mServerPort);
                Console.WriteLine(@"Connected to server IP/Port: {0} / {1}", mServerIPAddress, mServerPort);

                await ReadDataAsync(mClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                
            }
        }

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

                    if(readByteCount <= 0)
                    {
                        Console.WriteLine(@"Connection lost...");
                        mClient.Close();
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
        }

        public bool IsConnected()
        {
            return mClient.Connected;
        }
    }
}
