using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Dota2AdInfo.Win32.User32Wrappers;

namespace Dota2AdInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            ResizeRedraw = true;
            SetFormToTransparent();

            var th = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1);
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            Refresh();
                        }));
                    }
                    else
                    {
                        Refresh();
                    }
                }
            });
            th.Start();
        }

        private void SetFormToTransparent()
        {
            SetWindowLong(Handle, (int)GWL.ExStyle, (int)WS_EX.Layered | (int)WS_EX.Transparent);

            //SetLayeredWindowAttributes(Handle, 0, 255 * (0 / 100), LWA.Alpha);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            e.Graphics.DrawString("Текст", new Font(FontFamily.Families[0].Name, 40), Brushes.Red, 0, 0);
        }
    }
}
