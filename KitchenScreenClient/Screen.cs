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
    /// <summary>
    /// The GUI that visually contains the customer order information. This is what
    /// an employee would interact to assist in communication with the kitchen.
    /// </summary>
    /// <remarks>
    /// NAME: Screen
    /// AUTHOR: Ryan Osgood
    /// DATE: 9/4/2019
    /// </remarks> 
    public partial class Screen : Form
    {
        private readonly HiawathaSocketClient m_client;
        private readonly List<OnScreenOrder> m_customerOrders = new List<OnScreenOrder>();
        private readonly ScreenView m_screenManager;
        private int m_cursorSelectionPosition;

        private const string m_voidCommand = "VOID";
        /// <summary>
        /// The default constructor for the Screen object. 
        /// Initializes the components and it's members.
        /// </summary>
        /// <remarks>
        /// NAME: Screen
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        public Screen()
        {
            InitializeComponent();
            m_client = new HiawathaSocketClient(this);
            m_screenManager = new ScreenView(flpScreenFlow);
        }/*public Screen()*/

        /// <summary>
        /// This event is triggered when the user presses the number buttons
        /// to bump an order off the screen. The user bumps an order when
        /// the item isn't needed on the screen anymore. 
        /// </summary>
        /// <remarks>
        /// NAME: Btn_BumpViaNumbers
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event.</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void Btn_BumpViaNumbers(object sender, EventArgs e)
        {
            Button positionToBump = sender as Button;
            int position = Convert.ToInt32(positionToBump.Tag);

            if (position >= m_customerOrders.Count) { return; }

            flpScreenFlow.Controls.RemoveAt(position);
            m_customerOrders.RemoveAt(position);
            UpdateScreenView();
        }/*private void Btn_BumpViaNumbers(object sender, EventArgs e)*/

        /// <summary>
        /// This event is triggered when the user wishes to remove an
        /// order from the screen using the Bump button. This will
        /// remove any order on the screen that is currently selected
        /// by the cursor (i.e. the Tan colored order.)
        /// </summary>
        /// <remarks>
        /// NAME: Btn_BumpViaBump
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the Bump button</param>
        /// <param name="e"></param>
        private void Btn_BumpViaBump(object sender, EventArgs e)
        {
            flpScreenFlow.Controls.RemoveAt(m_cursorSelectionPosition);
            m_customerOrders.RemoveAt(m_cursorSelectionPosition);
            UpdateScreenView();
        }/*private void Btn_BumpViaBump(object sender, EventArgs e)*/

        /// <summary>
        /// This event establishes the connection with the database MainPoSMenu. 
        /// </summary>
        /// <remarks>
        /// NAME: Btn_EstablishConnectionWithPoS_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - 
        /// the Establish Connection with Database button</param>
        /// <param name="e">The EventArgs associated with this event.</param>
        private void Btn_EstablishConnectionWithPoS_Click(object sender, EventArgs e)
        {
            const string strIPAddress = "127.0.0.1";
            const string strPortNumber = "23000";

            if (!m_client.SetServerIPAddress(strIPAddress)
                || !m_client.SetPortNumber(strPortNumber))
            {
                Console.WriteLine(@"FATAL ERROR: Wrong IP Address or port number supplied: {0} - {1}.", strIPAddress, strPortNumber);
                return;
            }

            m_client.ConnectToServer();
            ConfirmConnectionSuccession();
        }/*private void Btn_EstablishConnectionWithPoS_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This displays on-screen confirmation that the KitchenScreens and the MainPoSMenu 
        /// have successfully or unsuccessfully connected to each other. 
        /// </summary>
        /// <remarks>
        /// NAME: ConfirmConnectionSuccession
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        private void ConfirmConnectionSuccession()
        {
            lbConnectionEstablished.Text = string.Empty;
            lbConnectionEstablished.Text = m_client.IsConnected() ? @"Connection Established!" : @"Connection Failed to Establish....";
        }/*private void ConfirmConnectionSuccession()*/

        /// <summary>
        /// This method invalidates the screen view to update it with the proper
        /// information. 
        /// </summary>
        /// <remarks>
        /// NAME: UpdateScreenView
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
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
        }/*private void UpdateScreenView()*/

        /// <summary>
        /// This method is responsible for adding new data to the 
        /// screen's model. 
        /// </summary>
        /// <remarks>
        /// NAME: ConvertDataFromPoSIntoScreenElement 
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="a_message">The raw data from the server - the MainPoSMenu</param>
        public void ConvertDataFromPoSIntoScreenElement(string a_message)
        {
            a_message = a_message.Trim('\0');
            
            if (a_message.Contains(m_voidCommand))
            {
                SearchForVoidCandidate(a_message);
            }
            else
            {
                OnScreenOrder newOnScreenOrder = new OnScreenOrder();
                newOnScreenOrder.ParseSentStringIntoOrder(a_message);
                m_customerOrders.Add(newOnScreenOrder);
            }

            UpdateScreenView();

        }/*public void ConvertDataFromPoSIntoScreenElement(string a_message)*/

        /// <summary>
        /// Determines which OnScreenOrder is void applies to using the check ID and order existence
        /// </summary>
        /// <remarks>
        /// NAME: SearchForVoidCandidate 
        /// AUTHOR: Ryan Osgood
        /// DATE: 9/1/2019
        /// </remarks>
        /// <param name="a_voidMessage">The void command sent from the main PoS menu</param>
        private void SearchForVoidCandidate(string a_voidMessage)
        {
            a_voidMessage = a_voidMessage.Replace("VOID\n", string.Empty);
            string[] parsed = a_voidMessage.Split('\n');

            foreach (OnScreenOrder o in m_customerOrders)
            {
                if (o.CompareCheckGUID(parsed[1]) && o.DetermineIfVoidBelongs(parsed))
                {
                    o.AddVoidElements(a_voidMessage);
                    return;
                }
            }
        }

        /// <summary>
        /// This event is triggered when the user wishes to move the screen's 
        /// cursor up. 
        /// </summary>
        /// <remarks>
        /// NAME: BtnUpArrow_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the up arrow button.</param>
        /// <param name="e">The EventArgs associated with this object.</param>
        private void BtnUpArrow_Click(object sender, EventArgs e)
        {
            m_cursorSelectionPosition--;
            m_screenManager.CursorPosition = m_cursorSelectionPosition;
            UpdateScreenView();
        }/*private void BtnUpArrow_Click(object sender, EventArgs e)*/

        /// <summary>
        /// This event is fired when the user wishes to move the screen's 
        /// cursor down.
        /// </summary>
        /// <remarks>
        /// NAME: BtnDownArrow_Click
        /// AUTHOR: Ryan Osgood
        /// DATE: 8/17/2019
        /// </remarks>
        /// <param name="sender">The button that triggered this event - the down arrow button.</param>
        /// <param name="e">The EventArgs Associated with this event.</param>
        private void BtnDownArrow_Click(object sender, EventArgs e)
        {
            m_cursorSelectionPosition++;
            m_screenManager.CursorPosition = m_cursorSelectionPosition;
            UpdateScreenView();
        }/*private void BtnDownArrow_Click(object sender, EventArgs e)*/
    }
}
