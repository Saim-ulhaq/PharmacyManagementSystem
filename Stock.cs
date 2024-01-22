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
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
            DisplayStockMedicine();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D7LVLIA\SQLEXPRESS;Initial Catalog=PharmacyProject;Integrated Security=True;Encrypt=False");


        private void DisplayStockMedicine()
        {
            con.Open();
            string Query = "Select * from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SMDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void btnDMr_Click(object sender, EventArgs e)
        {

        }

        private void btnAMr_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SMDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSMN.Text = SMDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtSMP.Text = SMDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtSLMQ.Text = SMDGV.SelectedRows[0].Cells[3].Value.ToString();


            if (txtSMN.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(SMDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnMH_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void btnMM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Medicine medicine = new Medicine(); 
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
            Manufactures manufactures = new Manufactures(); 
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

        int Key = 0;
        private void Reset()
        {
            txtASMQ.Clear();
            txtSLMQ.Clear();
            txtSMN.Clear();
            txtSMP.Clear();
            txtSSM.Clear();
        }

        private void btnSSM_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from MedicineTbl where MedName=@MN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@MN", txtSSM.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SMDGV.DataSource = ds.Tables[0];
            con.Close();
            Reset();

        }

        private void txtSSM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select MedID, MedName,MedPrice,MedQuantity from MedicineTbl " +
                                "where MedName LIKE '%" + txtSSM.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                SMDGV.DataSource = ds.Tables[0];
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

        private void btnSAM_Click(object sender, EventArgs e)
        {
            try
            {
                int newQty = Convert.ToInt32(txtSLMQ.Text) + Convert.ToInt32(txtASMQ.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("Update MedicineTbl set MedQuantity=@MQ where MedID=@MI", con);
                cmd.Parameters.AddWithValue("@MQ", newQty);
                cmd.Parameters.AddWithValue("@MI", Key);
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayStockMedicine();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
