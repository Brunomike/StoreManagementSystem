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
    public partial class frmSales : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=BRUNO;Initial Catalog=SUPERMARKET_C_SHARP;Integrated Security=True");
        public frmSales()
        {
            InitializeComponent();
        }

        private void frmSales_Load(object sender, EventArgs e)
        {
            lblSellerName.Text = LoginForm.sellerName;
            loadProducts();
            loadBills();
            populateCategoryComboBox();
        }

        private void loadBills()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Con.Open();
                string query = "Select BillId,SellerName,BillDate,TotAmt from BillTable";

                da = new SqlDataAdapter(query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                //da.Fill(ds);
                //dtgCategories.DataSource = ds.Tables[0];

                da.Fill(dt);
                dtgBill.DataSource = dt;



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


        private void loadProducts()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Con.Open();
                string query = "Select ProdName,ProdPrice from ProductTable";

                da = new SqlDataAdapter(query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                //da.Fill(ds);
                //dtgCategories.DataSource = ds.Tables[0];

                da.Fill(dt);
                dtgProduct.DataSource = dt;



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

        private void dtgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductName.Text = dtgProduct.SelectedRows[0].Cells[0].Value.ToString();
            txtPrice.Text = dtgProduct.SelectedRows[0].Cells[1].Value.ToString();
        }
        int GrandTotal = 0, n = 0;
        
        private void btnAddProd_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text == "" || txtProductQuantity.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                int total = Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(txtProductQuantity.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dtgOrder);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = txtProductName.Text;
                newRow.Cells[2].Value = txtPrice.Text;
                newRow.Cells[3].Value = txtProductQuantity.Text;
                newRow.Cells[4].Value = Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(txtProductQuantity.Text);
                dtgOrder.Rows.Add(newRow);
                n++;
                GrandTotal += total;
                lblAmount.Text = "" + GrandTotal;
                loadBills();
                clearTextBoxes();
            }


        }
        private void clearTextBoxes()
        {
            txtBillId.Text = "";
            txtPrice.Text = "";
            txtProductName.Text = "";
            txtProductQuantity.Text = "";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("BRUNO TECHSTORE", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Bill ID: " + dtgBill.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));
            e.Graphics.DrawString("Seller Name: " + dtgBill.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
            e.Graphics.DrawString("Date: " + dtgBill.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
            e.Graphics.DrawString("Total Amount: " + dtgBill.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
            e.Graphics.DrawString("Developer Michael Bruno.", new Font("Century Gothic", 20, FontStyle.Italic), Brushes.Red, new Point(230, 230));
        }

        private void comboBoxCategories_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Con.Open();
                //string query = "Select ProdName,ProdQuantity from ProductTable where ProdCategory="+comboBoxCategories.SelectedValue.ToString()+"";
                string query = "Select ProdName,ProdQuantity from ProductTable WHERE ProdCategory='" + comboBoxCategories.SelectedValue.ToString() + "'";

                da = new SqlDataAdapter(query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                dtgProduct.DataSource = ds.Tables[0];

                // da.Fill(dt);
                //dtgProduct.DataSource = dt;



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
                comboBoxCategories.ValueMember = "categoryName";
                comboBoxCategories.DataSource = dt;

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

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                int id = Convert.ToInt32(dtgBill.SelectedRows[0].Cells[0].Value.ToString());
                string deleteQuery = "DELETE FROM BillTable WHERE BillId='" + id.ToString() + "';";

                SqlCommand cmd = new SqlCommand(deleteQuery, Con);
                int i;
                //cmd.Connection = Con;
                //cmd.CommandText = deleteQuery;

                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Bill Record has been DELETED successfuly!");
                }
                else
                {
                    MessageBox.Show("No record has been DELETED");
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
                loadBills();
            }
        }

        private void btnAddSellsDtg_Click(object sender, EventArgs e)
        {

            if (txtBillId.Text == "")
            {
                MessageBox.Show("Missing Bill ID");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "INSERT INTO BillTable VALUES(" + txtBillId.Text + ",'" + lblSellerName.Text + "','" + lblCurrentDate.Text + "','" + lblAmount.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order added successfully");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                    loadBills();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            lblCurrentDate.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadProducts();
        }
    }
}
