using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Films
{
    public partial class WebBrowserForm : Form
    {

        const int WM_NCHITTEST = 0x84;
        const int HTCAPTION = 2;
        const int HTCLIENT = 1;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }

        public WebBrowserForm(string linkMovie)
        {
            InitializeComponent();
            wbPlayer.Navigate(linkMovie);
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            wbPlayer.Dispose();
            wbPlayer = null;
            GC.Collect();
            Close();
        }
    }
}

