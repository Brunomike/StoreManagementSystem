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
    public partial class frmSellers : Form
    {
        SqlConnection Con = new SqlConnection("Data Source=BRUNO;Initial Catalog=SUPERMARKET_C_SHARP;Integrated Security=True");
        public frmSellers()
        {
            InitializeComponent();
        }

        private void frmSellers_Load(object sender, EventArgs e)
        {
            loadSellerData();
        }
        private void loadSellerData()
        {
            try
            {
                Con.Open();
                string readQuery = "Select * FROM SellerTable";
                SqlDataAdapter da = new SqlDataAdapter(readQuery, Con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgSeller.DataSource = dt;



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

        private void clearTextBoxes()
        {
            txtSellerId.Text = "";
            txtSellerName.Text = "";
            txtSellerAge.Text = "";
            txtSellerPhone.Text = "";
            txtSellerPassword.Text = "";

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int i;
            try
            {
                Con.Open();
                string query = "INSERT into SellerTable VALUES(" + txtSellerId.Text + ",'" + txtSellerName.Text + "','" + txtSellerAge.Text + "','" + txtSellerPhone.Text + "','" + txtSellerPassword.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Seller Added Successfully!");
                }
                else
                {
                    MessageBox.Show("Seller not Added!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();

                loadSellerData();
                clearTextBoxes();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSellerId.Text == "" || txtSellerName.Text == "" || txtSellerAge.Text == "" || txtSellerPhone.Text == "" || txtSellerPassword.Text == "")
                {
                    MessageBox.Show("Please provide the Missing Information!");
                }
                else
                {
                    Con.Open();
                    int id = Convert.ToInt32(txtSellerId.Text);
                    string updateQuery = "UPDATE SellerTable SET SellerName='" + txtSellerName.Text + "',SellerAge='" + txtSellerAge.Text + "',SellerPhone='" + txtSellerPhone.Text + "',SellerPassword='" + txtSellerPassword.Text + "' WHERE SellerId=" + id + ";";
                    int i;
                    SqlCommand cmd = new SqlCommand(updateQuery, Con);
                    //cmd.Connection = Con;
                    //cmd.CommandText = updateQuery;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Seller record has been UPDATED successfully!");
                    }
                    else
                    {
                        MessageBox.Show("No record has been UPDATED!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
                clearTextBoxes();
                loadSellerData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtSellerId.Text == "")
                {
                    MessageBox.Show("Select the Seller record to be deleted!");
                }
                else
                {
                    Con.Open();
                    int id = Convert.ToInt32(txtSellerId.Text);
                    string deleteQuery = "DELETE FROM SellerTable WHERE SellerId=" + id + "";

                    SqlCommand cmd = new SqlCommand();
                    int i;
                    cmd.Connection = Con;
                    cmd.CommandText = deleteQuery;

                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Seller Record hes been DELETED successfuly!");
                    }
                    else
                    {
                        MessageBox.Show("No record has been DELETED1");
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
                clearTextBoxes();
                loadSellerData();
            }
        }

        private void dtgSeller_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSellerId.Text = dtgSeller.SelectedRows[0].Cells[0].Value.ToString();
            txtSellerName.Text = dtgSeller.SelectedRows[0].Cells[1].Value.ToString();
            txtSellerAge.Text = dtgSeller.SelectedRows[0].Cells[2].Value.ToString();
            txtSellerPhone.Text = dtgSeller.SelectedRows[0].Cells[3].Value.ToString();
            txtSellerPassword.Text = dtgSeller.SelectedRows[0].Cells[4].Value.ToString();
        }


    }
}
