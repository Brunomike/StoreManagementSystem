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
    public partial class frmProducts : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=BRUNO;Initial Catalog=SUPERMARKET_C_SHARP;Integrated Security=True");

        public frmProducts()
        {
            InitializeComponent();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            populateCategoryComboBox();
            populateCategoryComboBox1();
            loadProducts();
        }

        private void populateCategoryComboBox()
        {
            //This Method will bind the Combo box with the Database
            try
            {
                Con.Open();
                string query = "SELECT CategoryName FROM CategoryTable";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("CategoryName", typeof(string));
                dt.Load(dr);
                comboBoxCategory.ValueMember = "categoryName";
                comboBoxCategory.DataSource = dt;

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
        private void populateCategoryComboBox1()
        {
            //This Method will bind the Combo box with the Database
            try
            {
                Con.Open();
                string query = "SELECT CategoryName FROM CategoryTable";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("CategoryName", typeof(string));
                dt.Load(dr);

                comboBoxProductCategory.ValueMember = "categoryName";
                comboBoxProductCategory.DataSource = dt;
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

        private void btnAddProduct_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                string query = "INSERT into ProductTable VALUES(" + txtProductId.Text + ",'" + txtProductName.Text + "','" + txtProductPrice.Text + "','" + comboBoxCategory.SelectedValue.ToString() + "','" + txtProductPrice.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
                clearTextBoxes();
                loadProducts();

            }

        }


        private void clearTextBoxes()
        {
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtProductPrice.Text = "";
            txtProductQuantity.Text = "";
        }


        private void loadProducts()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Con.Open();
                string query = "Select * from ProductTable";

                da = new SqlDataAdapter(query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                //da.Fill(ds);
                //dtgCategories.DataSource = ds.Tables[0];

                da.Fill(dt);
                dtgProducts.DataSource = dt;



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

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductId.Text == "" || txtProductName.Text == "" || txtProductPrice.Text == "")
                {
                    MessageBox.Show("Please provide the Missing Information!");
                }
                else
                {
                    Con.Open();
                    int id = Convert.ToInt32(txtProductId.Text);
                    string updateQuery = "UPDATE ProductTable SET ProdId='" + txtProductId.Text + "',ProdName='" + txtProductName.Text + "',ProdQuantity='" + txtProductQuantity.Text + "',ProdCategory='" + comboBoxCategory.SelectedValue.ToString() + "',ProdPrice='" + txtProductPrice.Text + "' WHERE ProdId=" + id + ";";
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
                loadProducts();
            }
        }

        private void dtgProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductId.Text = dtgProducts.SelectedRows[0].Cells[0].Value.ToString();
            txtProductName.Text = dtgProducts.SelectedRows[0].Cells[1].Value.ToString();
            txtProductQuantity.Text = dtgProducts.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxCategory.Text = dtgProducts.SelectedRows[0].Cells[3].Value.ToString();
            txtProductPrice.Text = dtgProducts.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtProductId.Text == "" || txtProductName.Text == "" || txtProductPrice.Text == "" || txtProductQuantity.Text == "")
                {
                    MessageBox.Show("Select the Product to be deleted!");
                }
                else
                {
                    Con.Open();
                    int id = Convert.ToInt32(txtProductId.Text);
                    string deleteQuery = "DELETE  FROM ProductTable WHERE ProdId=" + id + "";

                    SqlCommand cmd = new SqlCommand();
                    int i;
                    cmd.Connection = Con;
                    cmd.CommandText = deleteQuery;

                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Product Record hes been DELETED successfuly!");
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
                loadProducts();
            }
        }

        private void comboBoxProductCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Con.Open();
                string query = "Select * from ProductTable WHERE ProdCategory='" + comboBoxProductCategory.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                //da.Fill(ds);
                //dtgCategories.DataSource = ds.Tables[0];

                da.Fill(dt);
                dtgProducts.DataSource = dt;



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
    }




}

