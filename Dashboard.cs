using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            DisplayMedicine();
            CountAntiFungal();
            CountAntiBiotic();
            CountAntiViral();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D7LVLIA\SQLEXPRESS;Initial Catalog=PharmacyProject;Integrated Security=True;Encrypt=False");

        private void DisplayMedicine()
        {
            con.Open();
            string Query = "Select * from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void CountAntiFungal()
        {
            con.Open();
            string AntiFungal = "Antifungal";
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from MedicineTbl where MedDescription ='"+AntiFungal+"'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            antifungal.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void CountAntiBiotic()
        {
            con.Open();
            string AntiBiotic = "Antibiotic";
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from MedicineTbl where MedDescription ='" + AntiBiotic + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            antibiotic.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void CountAntiViral()
        {
            con.Open();
            string AntiViral = "Antiviral";
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from MedicineTbl where MedDescription ='" + AntiViral + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            antiviral.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void DDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMH_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void btnMM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Medicine medicine   = new Medicine();
            medicine.Show();
            this.Hide();
        }

        private void btnMA_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Agent agent = new Agent();
            agent.Show();
            this.Hide();
        }

        private void btnMB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Billing billing = new Billing();
            billing.Show();
            this.Hide();
        }

        private void btnMMr_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Manufactures manufactures   = new Manufactures();
            manufactures.Show();
            this.Hide();
        }

        private void gunaLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void gunaLinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stock stock = new Stock();
            stock.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                string querry = "select MedID, MedName,MedPrice,MedQuantity,MedDescription from MedicineTbl " +
                                "where MedName LIKE '%" + gunaTextBox1.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                DDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
