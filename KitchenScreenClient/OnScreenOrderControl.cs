using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KitchenScreenClient
{
    public class OnScreenOrderControl : FlowLayoutPanel
    {
        private readonly List<Label> m_labels;
        private readonly List<Label> m_voidedLabels;

        public OnScreenOrderControl()
        {
            BorderStyle = BorderStyle.Fixed3D;
            FlowDirection = FlowDirection.TopDown;
            AutoSize = true;
            Padding = new Padding(0);
            m_labels = new List<Label>();
            m_voidedLabels = new List<Label>();
        }

        public void PostRegularElementsToControl(OnScreenOrder a_screenOrder)
        {
            
            for (int i = 0; i < a_screenOrder.GetSizeOfRegularOrders(); i++)
            {
                Label onScreenElement = new Label();

                string element = a_screenOrder.GetElementAtIndex(i);
                if (element.Contains('\t'))
                {
                    element = element.Replace("\t", string.Empty);
                    onScreenElement.Text = "    " + element;
                }
                else
                {
                    onScreenElement.Text = element;
                }

                m_labels.Add(onScreenElement);
            }
            
        }

        public void PostVoidedElementsToControl(OnScreenOrder a_screenOrder)
        {
            for (int i = 0; i < a_screenOrder.GetSizeOfVoidedOrders(); i++)
            {
                Label onScreenElement = new Label();
                string element = a_screenOrder.GetVoidedElementAtIndex(i);
                if (element.Contains('\t'))
                {
                    element = element.Replace("\t", string.Empty);
                    onScreenElement.Text = "    " + element;
                }
                else
                {
                    onScreenElement.Text = element;
                }
                onScreenElement.Font = new Font(onScreenElement.Font, FontStyle.Strikeout);
                m_voidedLabels.Add(onScreenElement);
            }

        }

        public void FinalizeOnScreenInfo()
        {
            foreach (Label l in m_voidedLabels)
            {
                l.AutoSize = true;
                Controls.Add(l);
            }

            foreach (Label l in m_labels)
            {
                l.AutoSize = true;
                Controls.Add(l);
            }
        }
    }
}
