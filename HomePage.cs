using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            Medicine medicine = new Medicine();
            medicine.Show();
            this.Hide();
        }

        private void gunaLabel6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gunaPictureBox2_Click(object sender, EventArgs e)
        {
            Agent agent = new Agent();
            agent.Show();
            this.Hide();
        }

        private void gunaPictureBox5_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void gunaPictureBox4_Click(object sender, EventArgs e)
        {
            Manufactures manufactures = new Manufactures();
            manufactures.Show();
            this.Hide();
        }

        private void gunaPictureBox3_Click(object sender, EventArgs e)
        {
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }

        private void gunaCirclePictureBox1_Click(object sender, EventArgs e)
        {
            LoginPage loginPage = new LoginPage(); 
            loginPage.Show();
            this.Hide();
        }
    }
}
