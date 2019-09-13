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
    /// <summary>
    /// The event handler for when a client connects to the server.
    /// </summary>
    /// <remarks>
    /// NAME: ClientConnectedEventArgs
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks>
    public class ClientConnectedEventArgs : EventArgs
    {
        public string NewClient { get; set; }

        public ClientConnectedEventArgs(string a_newClient)
        {
            NewClient = a_newClient;
        }
    }
}