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
    public partial class CreateNewAccount : Form
    {
        public CreateNewAccount()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D7LVLIA\SQLEXPRESS;Initial Catalog=PharmacyProject;Integrated Security=True;Encrypt=False");

        //for Add new user for using thuis application and login this //
        private void btnSCA_Click(object sender, EventArgs e)
        {
            if (txtUN.Text == "" || txtUP.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into LoginTbl (UserName,Passward) values (@UN,@UP)", con);
                    cmd.Parameters.AddWithValue("@UN", txtUN.Text);
                    cmd.Parameters.AddWithValue("@UP", txtUP.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New account created Successfully");
                    con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        // delete specific user which can not login & remove it from database//

        private void btnDCA_Click(object sender, EventArgs e)
        {
            if (txtUN.Text == "")
            {
                MessageBox.Show("Please select any User Name which you would like to delete");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from LoginTbl where UserName=@UN", con);
                    cmd.Parameters.AddWithValue("@UN", txtUN.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully");
                    con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Hide();
        }

        private void Reset()
        {
            txtUN.Clear();
            txtUP.Clear();
        }
    }
}
