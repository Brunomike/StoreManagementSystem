using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrunoTechStoreManagemantSystem
{
    public partial class frmSellerRole : Form
    {
        public frmSellerRole()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void frmSellerRole_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblUserName.Text = "Seller "+LoginForm.sellerName;

            this.pnlFormLoader.Controls.Clear();
            frmSales sales = new frmSales() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            sales.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(sales);
            sales.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            lblDate.Text = DateTime.Now.ToLongDateString();
        }
    }
}
