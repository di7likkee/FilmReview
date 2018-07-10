using Films.Core.KinoPoisk;
using Films.Core;
using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Films
{

    public partial class NoveltiesOfFilmsForm : Form
    {
        int currentPage = 1;
        int shiftPage, amountCriteria, indexMovie = 0;
        bool flag;
        bool firstLoad = true;
        DateTime thisDay = DateTime.Today;
        List<string> listLinksToMovies = new List<string>();

        ParserWorker<string[]> parser;

        const int WM_NCHITTEST = 0x84;
        const int HTCAPTION = 2;
        const int HTCLIENT = 1;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }

        public String GetTime()
        {
            int year = thisDay.Year;
            return year.ToString();
        }

        public NoveltiesOfFilmsForm()
        {
            InitializeComponent();

            parser = new Core.ParserWorker<string[]>(
                    new FilmsParser()
                );

            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
            parser.OnViewMovie += Parser_OnViewMovie;
        }

        private void Parser_OnCompleted(object obj)
        {
            //MessageBox.Show("Список обновлён");
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            dgv.Focus();
            for (int i = 0; i < arg2.Length; i++)
            {
                listLinksToMovies.Add(arg2[i].Substring(arg2[i].IndexOf('_') + 1));
                dgv.Rows.Add(arg2[i].Substring(0, arg2[i].IndexOf('_')).Replace("&nbsp;", " "));
            }
            if (firstLoad)
            {
                parser.Settings = new FilmsParserSettings(listLinksToMovies[0]);
                parser.View();
                firstLoad = false;
            }
        }

        private void Parser_OnViewMovie(object arg1, string[] arg2)
        {
            AddVisibleControl();
            DeleteControl();
            AddScreens(arg2);
            AddNewControl(arg2);
        }

        private void AddVisibleControl()
        {
            lInfoAboutFilm.Visible = true;
            lScreens.Visible = true;
            bWatchMovie.Visible = true;
        }

        private void AddScreens(string[] arg2)
        {
           
            pbImage.ImageLocation = arg2[arg2.Length - 1].Contains("kinogo.cc") ? arg2[arg2.Length - 1] : ""; ;
            pbScreen1.ImageLocation = arg2[arg2.Length - 4].Contains("kinogo.cc") ? arg2[arg2.Length - 4] : "";
            pbScreen2.ImageLocation = arg2[arg2.Length - 3].Contains("kinogo.cc") ? arg2[arg2.Length - 3] : ""; ;
            pbScreen3.ImageLocation = arg2[arg2.Length - 2].Contains("kinogo.cc") ? arg2[arg2.Length - 2] : ""; ;
        }

        private void AddNewControl(string[] arg2)
        {
            amountCriteria = arg2.Length;
            tbDesciption.Text = arg2[0];
            for (int i = 1; i < arg2.Length - 4; i++)
            {
                Label label = new Label
                {
                    Name = "l" + i,
                    Text = arg2[i],
                    Size = new Size(arg2[i].Length * 12, 21),
                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    Left = 665,
                    Top = (i + 4) * 20,
                };
                Controls.Add(label);
            }
        }

        private void NoveltiesOfFilmsForm_Shown(object sender, EventArgs e)
        {
            Activate();
            parser.Settings = new FilmsParserSettings(currentPage, GetTime());
            parser.Start();
        }

        private void bBackPage_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                flag = false;
                UpdateListFilms(flag);
            }
        }

        private void bNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage != 16)
            {
                flag = true;
                UpdateListFilms(flag);
            }
        }

        private void UpdateListFilms(bool flag)
        {
            dgv.Rows.Clear();
            EnabledButtonFalse();
            if (flag)
            {
                parser.Settings = new FilmsParserSettings(++currentPage, GetTime());
                shiftPage += 10;
            }
            else
            {
                parser.Settings = new FilmsParserSettings(--currentPage, GetTime());
                shiftPage -= 10;
            }
            parser.Start();
            lCurrentPage.Text = $"- {currentPage} -";
            Thread.Sleep(500);
            Application.DoEvents();
            EnabledButtonTrue();
            GC.Collect();
        }

        private void EnabledButtonTrue()
        {
            bBackPage.Enabled = true;
            bNextPage.Enabled = true;
        }

        private void EnabledButtonFalse()
        {
            bBackPage.Enabled = false;
            bNextPage.Enabled = false;
        }

        private void DeleteControl()
        {
            for (int i = 1; i < amountCriteria; i++)
            {
                Controls.RemoveByKey("l" + i);
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bWatchMovie_Click(object sender, EventArgs e)
        {
            WebBrowserForm wb = new WebBrowserForm(listLinksToMovies[indexMovie + shiftPage]);
            wb.ShowDialog(this);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection selectedRowCollection = dgv.SelectedRows;
                foreach (DataGridViewRow dataGridViewRow in selectedRowCollection)
                {
                    indexMovie = dataGridViewRow.Index;
                    break;
                }
                parser.Settings = new FilmsParserSettings(listLinksToMovies[indexMovie + shiftPage]);
                parser.View();
                GC.Collect();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Вы ничего не выбрали!");
            }
        }
    }
}
