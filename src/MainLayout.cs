using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Films
{
    public partial class MainLayout : Form
    {
        public MainLayout()
        {
            InitializeComponent();
        }

        private void MainLayout_Load(object sender, EventArgs e)
        {
            Activate();
        }

        private void lExpectedFilms_Click(object sender, EventArgs e)
        {
            NoveltiesOfFilmsForm expectedFilmsForm = new NoveltiesOfFilmsForm();
            expectedFilmsForm.ShowDialog(this);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
