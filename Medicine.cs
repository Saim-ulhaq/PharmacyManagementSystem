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
using static System.Net.Mime.MediaTypeNames;

namespace PharmacyManagementSystem
{
    public partial class Medicine : Form
    {
        public Medicine()
        {
            InitializeComponent();
            DisplayMedicine();
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
            MDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            txtMN.Clear();
            txtMP.Clear(); 
            txtMQ.Clear();
            txtSM.Clear();
            txtMD.Clear();

            Key = 0;
        }


        //add medicine//
        private void btnSM_Click(object sender, EventArgs e)
        {

            if (txtMN.Text == "" || txtMP.Text == "" || txtMQ.Text == "" || txtMD.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into MedicineTbl (MedName,MedPrice,MedQuantity,MedDescription) values (@MN,@MP,@MQ,@MD)", con);
                    cmd.Parameters.AddWithValue("@MN", txtMN.Text);
                    cmd.Parameters.AddWithValue("@MP", txtMP.Text);
                    cmd.Parameters.AddWithValue("@MQ", txtMQ.Text);
                    cmd.Parameters.AddWithValue("@MD", txtMD.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine added Successfully");
                    con.Close();
                    DisplayMedicine();
                   Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        //Delete medicine //
        private void btnDM_Click(object sender, EventArgs e)
        {
            if (txtMN.Text == "")
            {
                MessageBox.Show("Please select any ID of Medicine which you would like to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from MedicineTbl where MedName=@MN", con);
                    cmd.Parameters.AddWithValue("@MN", txtMN.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Deleted Successfully");
                    con.Close();
                    DisplayMedicine();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btnEM_Click(object sender, EventArgs e)
        {

            if (txtMN.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update MedicineTbl set MedPrice=@MP,MedQuantity=@MQ,MedDescription=@MD where MedName=@MN", con);
                    cmd.Parameters.AddWithValue("@MN", txtMN.Text);
                    cmd.Parameters.AddWithValue("@MP", txtMP.Text);
                    cmd.Parameters.AddWithValue("@MQ", txtMQ.Text);
                    cmd.Parameters.AddWithValue("@MD", txtMD.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Updated Successfully");
                    con.Close();
                    DisplayMedicine() ;
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void btnSMM_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from MedicineTbl where MedName=@MN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@MN", txtSM.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MDGV.DataSource = ds.Tables[0];
            con.Close();
            Reset();
        }

        private void MDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtMN.Text = MDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtMP.Text = MDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtMQ.Text = MDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtMD.Text = MDGV.SelectedRows[0].Cells[4].Value.ToString();
            


            if (txtMN.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(MDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void txtSM_TextChanged(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                string querry = "select MedID, MedName,MedPrice,MedQuantity,MedDescription from MedicineTbl " +
                                "where MedName LIKE '%" + txtSM.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                MDGV.DataSource = ds.Tables[0];
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

        private void gunaLabel1_Click(object sender, EventArgs e)
        {
          //  Application.Exit();
        }

        private void gunaLabel1_Click_1(object sender, EventArgs e)
        {
       //     Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
