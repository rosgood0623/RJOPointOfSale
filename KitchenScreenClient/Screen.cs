using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HiawathaSocketAsync;

namespace KitchenScreenClient
{
    public partial class Screen : Form
    {
        private readonly HiawathaSocketClient m_client;
        private readonly List<OnScreenOrder> m_customerOrders = new List<OnScreenOrder>();
        private readonly ScreenView m_screenManager;
        private int m_cursorSelectionPostion = 0;

        public Screen()
        {
            InitializeComponent();
            m_client = new HiawathaSocketClient(this);
            m_screenManager = new ScreenView(flpScreenFlow);
        }

        private void Btn_BumpViaNumbers(object sender, EventArgs e)
        {
            Button positionToBump = sender as Button;
            int position = (int)positionToBump.Tag;
            flpScreenFlow.Controls.RemoveAt(position);
            m_customerOrders.RemoveAt(position);
        }


        private void Btn_BumpViaBump(object sender, EventArgs e)
        {
            flpScreenFlow.Controls.RemoveAt(m_cursorSelectionPostion);
        }

        private void Btn_EstablishConnectionWithPoS_Click(object sender, EventArgs e)
        {
            const string strIPAddress = "127.0.0.1";
            const string strPortNumber = "23000";

            if (!m_client.SetServerIPAddress(strIPAddress)
                || !m_client.SetPortNumber(strPortNumber))
            {
                Console.WriteLine(@"Wrong IP Address or port number supplied: {0} - {1}. Press any key to exit", strIPAddress, strPortNumber);
                return;
            }

            m_client.ConnectToServer();
            ConfirmConnectionSuccession();
        }

        private void ConfirmConnectionSuccession()
        {
            lbConnectionEstablished.Text = m_client.IsConnected() ? @"Connection Established!" : @"Connection Failed to Establish....";
        }
        
        public void ConvertDataFromPoSIntoScreenElement(string a_message)
        {
            OnScreenOrder newOnScreenOrder = new OnScreenOrder();
            newOnScreenOrder.ParseSentStringIntoOrder(a_message);
            m_customerOrders.Add(newOnScreenOrder);
            UpdateScreenView();
        }

        private void BtnUpArrow_Click(object sender, EventArgs e)
        {
            m_cursorSelectionPostion--;
            m_screenManager.CursorPosition = m_cursorSelectionPostion;
            UpdateScreenView();
        }

        private void BtnDownArrow_Click(object sender, EventArgs e)
        {
            m_cursorSelectionPostion++;
            m_screenManager.CursorPosition = m_cursorSelectionPostion;
            UpdateScreenView();
        }

        private void UpdateScreenView()
        {
            m_screenManager.ClearScreenForRefresh();

            for (int i = 0; i < m_customerOrders.Count; i++)
            {
                if (i == m_screenManager.CursorPosition)
                {
                    m_screenManager.AddElementToScreen(m_customerOrders[i], Color.Tan);
                }
                else
                {
                    m_screenManager.AddElementToScreen(m_customerOrders[i], Color.Empty);
                }

            }

        }
    }

}
