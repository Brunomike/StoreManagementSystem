using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BrunoTechStoreManagemantSystem
{
    public partial class LoginForm : Form
    {
        SqlConnection Con = new SqlConnection("Data Source=BRUNO;Initial Catalog=SUPERMARKET_C_SHARP;Integrated Security=True");
        public static string sellerName = "";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Plese enter the Username and Password!");
            }
            else
            {
                if (comboBoxSelectRole.SelectedIndex > -1)
                {
                    if (comboBoxSelectRole.SelectedItem.ToString() == "ADMIN")
                    {
                        if (txtUserName.Text == "Admin" && txtPassword.Text == "Admin")
                        {
                            frmMain main = new frmMain();
                            sellerName = txtUserName.Text;
                            main.Show();
                            this.Hide();

                        }
                        else
                        {
                            MessageBox.Show("If you are the Admin,Enter the correct Username and Password!");
                            //clearTextBoxes();
                        }

                    }
                    else if (comboBoxSelectRole.SelectedItem.ToString() == "SELLER")
                    {
                        //MessageBox.Show("You are a Seller!");
                        try
                        {
                            Con.Open();
                            string query = "Select count(8) from SellerTable where SellerName='" + txtUserName.Text + "' and SellerPassword='" + txtPassword.Text + "'";
                            SqlDataAdapter da = new SqlDataAdapter(query, Con);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows[0][0].ToString() == "1")
                            {
                                sellerName = txtUserName.Text;
                                frmSellerRole seller = new frmSellerRole();
                                sellerName = txtUserName.Text;
                                seller.Show();
                                this.Hide();
                                Con.Close();
                            }
                            else
                            {
                                MessageBox.Show("Wrong UserName or Password");
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            Con.Close();
                        }

                    }
                  
                }
                else
                {
                    MessageBox.Show("Select a Role!");
                }
            }
        }
        private void clearTextBoxes()
        {
            txtPassword.Text = "";
            txtUserName.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            clearTextBoxes();
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
