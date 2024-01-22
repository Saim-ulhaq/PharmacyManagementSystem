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
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D7LVLIA\SQLEXPRESS;Initial Catalog=PharmacyProject;Integrated Security=True;Encrypt=False");


        //create new account for login of multiple users//
        private void gunaLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateNewAccount createNewAccount = new CreateNewAccount();
            createNewAccount.Show();
            this.Hide();
        }

        private void gunaLabel5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //login of user only those which are create their account //

        private void btnlogin_Click(object sender, EventArgs e)
        {

            if (txtUName.Text == "" || txtUP.Text == "")
            {
                MessageBox.Show("Missing login data");
            }

            else
            {


                String userName, userPasward;
                userName = txtUName.Text;
                userPasward = txtUP.Text;
                try
                {

                    String Query = "Select * from LoginTbl where UserName= '" + txtUName.Text + "' AND Passward= '" + txtUP.Text + "'";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        userName = txtUName.Text;
                        userPasward = txtUP.Text;

                        HomePage homepage = new HomePage();
                        homepage.Show();
                        this.Hide();
                    }
                }
                catch
                {
                    MessageBox.Show("invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUName.Clear();
                    txtUP.Clear();

                    txtUName.Focus();
                }
                finally
                {
                    con.Close();
                }
                Reset();
            }

        }


        //delete the user data from database//

        private void btnReset_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("Delete * from Table", con);

            //  EDLogOut eDLogOut = new EDLogOut();
            //  eDLogOut.Show();
            //  this.Hide();
        }

        private void btnL_Click(object sender, EventArgs e)
        {

            if (txtUName.Text == "" || txtUP.Text == "")
            {
                MessageBox.Show("Missing login data");
            }

            else
            {


                String userName, userPasward;
                userName = txtUName.Text;
                userPasward = txtUP.Text;
                try
                {

                    String Query = "Select * from LoginTbl where UserName= '" + txtUName.Text + "' AND Passward= '" + txtUP.Text + "'";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        userName = txtUName.Text;
                        userPasward = txtUP.Text;

                        HomePage homePage = new HomePage();
                        homePage.Show();
                        this.Hide();
                    }
                }
                catch
                {
                    MessageBox.Show("invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUName.Clear();
                    txtUP.Clear();

                    txtUName.Focus();
                }
                finally
                {
                    con.Close();
                }
            }

        }

        private void Reset()
        {
            txtUName.Clear();
            txtUP.Clear();
            txtUName.Focus();

        }
    }
}
