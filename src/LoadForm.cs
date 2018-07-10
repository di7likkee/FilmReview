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

namespace Films
{
    public partial class LoadForm : Form
    {

        public LoadForm()
        {
            InitializeComponent();
        }

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        const uint WM_SYSCOMMAND = 0x0112;
        const uint DOMOVE = 0xF012;
        const uint DOSIZE = 0xF008;

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            Thread.Sleep(2000);
            ShowInTaskbar = false;
            Hide();
            NoveltiesOfFilmsForm noveltiesOfFilmsForm = new NoveltiesOfFilmsForm();
            noveltiesOfFilmsForm.ShowDialog(this);
            Close();
            //new Thread(() => Application.Run(new NoveltiesOfFilmsForm())).Start();
        }
    }
}
