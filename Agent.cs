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
    public partial class Agent : Form
    {
        public Agent()
        {
            InitializeComponent();
            DisplayAgent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D7LVLIA\SQLEXPRESS;Initial Catalog=PharmacyProject;Integrated Security=True;Encrypt=False");

         private void DisplayAgent()
        {
            con.Open();
            string Query = "Select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ADGV.DataSource = ds.Tables[0];
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

        private void Reset()
        {
            txtAN.Clear();
            txtAP.Clear();
            txtAA.Clear();
            txtAS.Clear();
            txtSA.Clear();
        }

        private void btnAA_Click(object sender, EventArgs e)
        {

            if (txtAN.Text == "" || txtAP.Text == "" || txtAA.Text == "" || txtAS.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl (EmpName,EmpPhone,EmpAddress,EmpSalery) values (@EN,@EP,@EA,@ES)", con);
                    cmd.Parameters.AddWithValue("@EN", txtAN.Text);
                    cmd.Parameters.AddWithValue("@EP", txtAP.Text);
                    cmd.Parameters.AddWithValue("@EA", txtAA.Text);
                    cmd.Parameters.AddWithValue("@ES", txtAS.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee added Successfully");
                    con.Close();
                    DisplayAgent();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void btnDA_Click(object sender, EventArgs e)
        {


            if (txtAP.Text == "")
            {
                MessageBox.Show("Please select Employee phone number which you would like to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from EmployeeTbl where EmpPhone=@EP", con);
                    cmd.Parameters.AddWithValue("@EP", txtAP.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted Successfully");
                    con.Close();
                    DisplayAgent();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void btnEA_Click(object sender, EventArgs e)
        {

            if (txtAN.Text == "" || txtAP.Text == "" || txtAA.Text == "" || txtAS.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update EmployeeTbl set EmpName=@EN,EmpPhone=@EP,EmpAddress=@EA,EmpSalery=@ES where EmpPhone=@EP", con);
                    cmd.Parameters.AddWithValue("@EN", txtAN.Text);
                    cmd.Parameters.AddWithValue("@EP", txtAP.Text);
                    cmd.Parameters.AddWithValue("@EA", txtAA.Text);
                    cmd.Parameters.AddWithValue("@ES", txtAS.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee updated Successfully");
                    con.Close();
                    DisplayAgent();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;

        private void ADGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAN.Text = ADGV.SelectedRows[0].Cells[1].Value.ToString();
            txtAP.Text = ADGV.SelectedRows[0].Cells[2].Value.ToString();
            txtAA.Text = ADGV.SelectedRows[0].Cells[3].Value.ToString();
            txtAS.Text = ADGV.SelectedRows[0].Cells[4].Value.ToString();


            if (txtAN.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ADGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnSA_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "Select * from EmployeeTbl where EmpName=@EN";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@EN", txtSA.Text);
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ADGV.DataSource = ds.Tables[0];
            con.Close();
            Reset();
        }

        private void txtSA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select EmpID, EmpName,EmpPhone,EmpAddress,EmpSalery from EmployeeTbl " +
                                "where EmpName LIKE '%" + txtSA.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                ADGV.DataSource = ds.Tables[0];
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
