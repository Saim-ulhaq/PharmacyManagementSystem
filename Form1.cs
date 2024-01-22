using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace PharmacyManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void gunaLabel4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gunaLabel3_Click(object sender, EventArgs e)
        {
        }
        int startP = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 1;
            gunaCircleProgressBar1.Value = startP;
            Percentage.Text = startP + "%";
            if (gunaCircleProgressBar1.Value == 100)
            {
                gunaCircleProgressBar1.Value = 0;
                LoginPage login = new LoginPage();
                login.Show();
                this.Hide();
                timer1.Stop();
            }

        }
    }
}
