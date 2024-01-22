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
    public partial class Manufactures : Form
    {
        public Manufactures()
        {
            InitializeComponent();
            DisplayManfacturers();

        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D7LVLIA\SQLEXPRESS;Initial Catalog=PharmacyProject;Integrated Security=True;Encrypt=False");


        private void DisplayManfacturers()
        {
            con.Open();
            string Query = "Select * from ManfacturesTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MrDGV.DataSource = ds.Tables[0];
            con.Close();

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

        private void gunaShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMMr_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        int Key = 0;
        private void Reset()
        {
            txtMName.Clear();
            txtMP.Clear();
            txtPC.Clear();
            txtSMr.Clear();
        }

        private void btnAMr_Click(object sender, EventArgs e)
        {

            if (txtMName.Text == "" || txtMP.Text == "" || txtPC.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ManfacturesTbl (ManName,ManPhone,ManCompany) values (@MN,@MP,@MC)", con);
                    cmd.Parameters.AddWithValue("@MN", txtMName.Text);
                    cmd.Parameters.AddWithValue("@MP", txtMP.Text);
                    cmd.Parameters.AddWithValue("@MC", txtPC.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manfacturer added Successfully");
                    con.Close();
                    DisplayManfacturers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void btnDMr_Click(object sender, EventArgs e)
        {



            if (txtMP.Text == "")
            {
                MessageBox.Show("Please select manufacturer phone number which you would like to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ManfacturesTbl where ManPhone=@MP", con);
                    cmd.Parameters.AddWithValue("@MP", txtMP.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Deleted Successfully");
                    con.Close();
                    DisplayManfacturers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void btnEMr_Click(object sender, EventArgs e)
        {

            if (txtMName.Text == "" || txtMP.Text == "" || txtPC.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update ManfacturesTbl set ManName=@MN,ManPhone=@MP,ManCompany=@MC where ManPhone=@MP", con);
                    cmd.Parameters.AddWithValue("@MN", txtMName.Text);
                    cmd.Parameters.AddWithValue("@MP", txtMP.Text);
                    cmd.Parameters.AddWithValue("@MC", txtPC.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer updated Successfully");
                    con.Close();
                    DisplayManfacturers();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void MrDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtMName.Text = MrDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtMP.Text = MrDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtPC.Text = MrDGV.SelectedRows[0].Cells[3].Value.ToString();


            if (txtMName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(MrDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void btnSMr_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from ManfacturesTbl where ManName=@MN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@MN", txtSMr.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MrDGV.DataSource = ds.Tables[0];
            con.Close();
            Reset();
        }

        private void txtSMr_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select ManID, ManName,ManPhone,ManCompany from ManfacturesTbl " +
                                "where ManName LIKE '%" + txtSMr.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                MrDGV.DataSource = ds.Tables[0];
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
