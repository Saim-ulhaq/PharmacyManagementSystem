using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            DisplayMedicine();
            DisplayBills();
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
            PBDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void DisplayBills()
        {
            con.Open();
            string Query = "Select * from BillTbl";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            gunaDataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        
        private void UpdateStock()
        {
            try
            {
                int newQty = stock - Convert.ToInt32(txtBMQ.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("Update MedicineTbl set MedQuantity=@MQ where MedID=@MI", con);
                cmd.Parameters.AddWithValue("@MQ", newQty);
                cmd.Parameters.AddWithValue("@MI", Key);
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayMedicine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Billing_Load(object sender, EventArgs e)
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

        int n = 0, grdtotal = 0;
        private void btnATB_Click(object sender, EventArgs e)
        {
            if (txtBMQ.Text == "" || Convert.ToInt32(txtBMQ.Text) > stock)
            {
                MessageBox.Show("No enough");
            }

            else if (txtBMQ.Text == "" || Key == 0)
            {
                MessageBox.Show("Missing information");
            }

            else if (Convert.ToInt32(txtBMQ.Text) >= 10)
            {

                int quantity = Convert.ToInt32(txtBMQ.Text);
                int pricePerUnit = Convert.ToInt32(txtBMP.Text);
                int totalses = quantity * pricePerUnit;

                int total = totalses - totalses / 10;

                // Increment the currentMaxId before using it

                DataGridViewRow newRow = new DataGridViewRow();

                newRow.CreateCells(BPDGV);
                newRow.Cells[0].Value = n + 1; // Set the ID to the currentMaxId
                newRow.Cells[1].Value = txtBMName.Text;
                newRow.Cells[2].Value = txtBMP.Text;
                newRow.Cells[3].Value = txtBMQ.Text;
                newRow.Cells[4].Value = total;

                grdtotal += total;
                BPDGV.Rows.Add(newRow);
                n++;

                totals.Text = grdtotal.ToString();

                UpdateStock();
                Reset();

            }



            else
            {
                int quantity = Convert.ToInt32(txtBMQ.Text);
                int pricePerUnit = Convert.ToInt32(txtBMP.Text);
                int total = quantity * pricePerUnit;

                // Increment the currentMaxId before using it

                DataGridViewRow newRow = new DataGridViewRow();

                newRow.CreateCells(BPDGV);
                newRow.Cells[0].Value = n + 1; // Set the ID to the currentMaxId
                newRow.Cells[1].Value = txtBMName.Text;
                newRow.Cells[2].Value = txtBMP.Text;
                newRow.Cells[3].Value = txtBMQ.Text;
                newRow.Cells[4].Value = total;

                grdtotal += total;
                BPDGV.Rows.Add(newRow);
                n++;

                totals.Text = grdtotal.ToString();

                UpdateStock();
                Reset();

            }
        }


        private void InsertBill()
        {
            
        }

        private void txtSM_TextChanged(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                string querry = "select MedID, MedName,MedPrice,MedQuantity from MedicineTbl " +
                                "where MedName LIKE '%" + txtSM.Text + "%'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                PBDGV.DataSource = ds.Tables[0];
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

        int Key = 0 , stock = 0;

        private void Reset()
        {
            txtBMName.Clear();
            txtBMP.Clear();
            txtBMQ.Clear();
            txtSM.Clear();

            Key = 0;
        
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }


        string MName;
        int Mid, MPrice, MQuantity, Mtotal,pos=60;

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (txtCN.Text == "")
            {
                MessageBox.Show("Missing Customer Name");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl (CustomerName,Date,Amount) values (@CN,@DA,@Amt)", con);

                    cmd.Parameters.AddWithValue("@CN", txtCN.Text);
                    cmd.Parameters.AddWithValue("@DA", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@Amt", totals.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Saved Successfully");
                    con.Close();
                    DisplayBills();
                    txtCN.Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawString("NEW LIFE PHARMACY", new Font("Time new Romens", 14, FontStyle.Bold), Brushes.Green, new Point(80));

            e.Graphics.DrawString("ID  Medicine   Price  Quantity  Total", new Font("Time new Romens", 12, FontStyle.Bold), Brushes.Green, new Point(26, 40));


            foreach (DataGridViewRow row in BPDGV.Rows)
            {
                Mid = Convert.ToInt32(row.Cells["ID"].Value);
                MName = "" + row.Cells["Medicine"].Value;
                MPrice = Convert.ToInt32(row.Cells["Price"].Value);
                MQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                Mtotal = Convert.ToInt32(row.Cells["Total"].Value);

                e.Graphics.DrawString("" + Mid, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Black, new Point(26, pos));
                e.Graphics.DrawString("" + MName, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Black, new Point(45, pos));
                e.Graphics.DrawString("" + MPrice, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Black, new Point(120, pos));
                e.Graphics.DrawString("" + MQuantity, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Black, new Point(170, pos));
                e.Graphics.DrawString("" + Mtotal, new Font("Time new Romens", 8, FontStyle.Bold), Brushes.Black, new Point(235, pos));

            }

            e.Graphics.DrawString("Grand Total: Rs" + grdtotal, new Font("Time new Romens", 10, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("*************Sahiwal*****************", new Font("Time new Romens", 10, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));

            BPDGV.Rows.Clear();
            BPDGV.Refresh();
            pos = 100;
            grdtotal = 0;
            n = 0;

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
            PBDGV.DataSource = ds.Tables[0];
            con.Close();
            Reset();
        }

        private void PBDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtBMName.Text = PBDGV.SelectedRows[0].Cells[1].Value.ToString();
            txtBMP.Text = PBDGV.SelectedRows[0].Cells[2].Value.ToString();
            stock = Convert.ToInt32(PBDGV.SelectedRows[0].Cells[3].Value.ToString());


            if (txtBMName.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PBDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
