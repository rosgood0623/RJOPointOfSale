using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;


namespace CustomPoSElements
{
    class TabButton : Control
    {
        private static int xAxisLocation = 0;
        private System.Windows.Size btnSize;
        private System.Windows.Point btnLocation;
        private Color CurrentBackColor;

        public TabButton()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            btnSize = new System.Windows.Size(35, 35);
        }

        public int setXAxis
        {
            set
            {
                xAxisLocation = value;
            }
        }

        public System.Windows.Point setLocation
        {
            set
            {
                btnLocation = new System.Windows.Point(xAxisLocation, 10);
            }
        }
    }
}
