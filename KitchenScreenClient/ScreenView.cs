using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitchenScreenClient
{
    class ScreenView
    {
        private FlowLayoutPanel m_screen;
        public int CursorPosition { get; set; }
        public ScreenView(FlowLayoutPanel a_screen)
        {
            m_screen = a_screen;
        }

        public void AddElementToScreen(OnScreenOrder a_order, Color a_textBoxColor)
        {
            TextBox element = new TextBox{ Multiline = true, ReadOnly = true, BackColor = a_textBoxColor};

            for (int i = 0; i < a_order.SizeOfOrder; i++)
            {
                element.Text += a_order.GetElementAtIndex(i) + Environment.NewLine;
            }

            Size renderSize = TextRenderer.MeasureText(element.Text, element.Font);
            element.ClientSize = new Size(200, renderSize.Height);

            m_screen.Controls.Add(element);
        }

        public void ClearScreenForRefresh()
        {
            m_screen.Controls.Clear();
        }
       
    }
}
