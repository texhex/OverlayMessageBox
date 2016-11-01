using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OverlayMessageBox
{
    internal partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            
            int height = 0;
            int width = 0;
            int left = 0;
            int top = 0;
            
            foreach (Screen screen in Screen.AllScreens)
            {
                height = (screen.Bounds.Height > height) ? screen.Bounds.Height : height;
                left = (screen.Bounds.X < left) ? screen.Bounds.X : left;
                top = (screen.Bounds.Y < top) ? screen.Bounds.Y : top;
                width += screen.Bounds.Width;
            }

            Left = left;
            Top = top;
            Size = new System.Drawing.Size(width, height);
             
        }
    }
}
