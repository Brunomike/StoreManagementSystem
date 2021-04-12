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
    public partial class frmCategories : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=BRUNO;Initial Catalog=SUPERMARKET_C_SHARP;Integrated Security=True");

        public frmCategories()
        {
            InitializeComponent();
        }

        private void btnAddCatetgory_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                string query = "INSERT into CategoryTable VALUES(" + txtCategoryId.Text + ",'" + txtCategoryName.Text + "','" + txtCategoryDesc.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();

                loadCategories();
                clearTextBoxes();
            }

        }

        private void loadCategories()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Con.Open();
                string query = "Select * from CategoryTable";

                da = new SqlDataAdapter(query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                //da.Fill(ds);
                //dtgCategories.DataSource = ds.Tables[0];

                da.Fill(dt);
                dtgCategories.DataSource = dt;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
                da.Dispose();

            }
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            loadCategories();
        }

        private void dtgCategories_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategoryId.Text = dtgCategories.SelectedRows[0].Cells[0].Value.ToString();
            txtCategoryName.Text = dtgCategories.SelectedRows[0].Cells[1].Value.ToString();
            txtCategoryDesc.Text = dtgCategories.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtCategoryId.Text == "" || txtCategoryName.Text == "" || txtCategoryDesc.Text == "")
                {
                    MessageBox.Show("Please provide the Missing Information!");
                }
                else
                {
                    Con.Open();
                    int id = Convert.ToInt32(txtCategoryId.Text);
                    string updateQuery = "UPDATE CategoryTable SET CategoryName='" + txtCategoryName.Text + "',CategoryDescription='" + txtCategoryDesc.Text + "' WHERE CategoryId=" + id + ";";
                    int i;
                    SqlCommand cmd = new SqlCommand(updateQuery, Con);
                    //cmd.Connection = Con;
                    //cmd.CommandText = updateQuery;
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Category record has been UPDATED successfully!");
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
                loadCategories();
            }

        }

        private void clearTextBoxes()
        {
            txtCategoryDesc.Text = "";
            txtCategoryId.Text = "";
            txtCategoryName.Text = "";
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtCategoryId.Text == "")
                {
                    MessageBox.Show("Select the Category record to be deleted!");
                }
                else
                {
                    Con.Open();
                    int id = Convert.ToInt32(txtCategoryId.Text);
                    string deleteQuery = "DELETE  FROM CategoryTable WHERE CategoryId=" + id + "";

                    SqlCommand cmd = new SqlCommand();
                    int i;
                    cmd.Connection = Con;
                    cmd.CommandText = deleteQuery;

                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Category Record hes been DELETED successfuly!");
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
                loadCategories();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearTextBoxes();
        }
    }
}
