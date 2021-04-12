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
    public partial class frmMain : Form
    {
        public static string userName = "";
        public frmMain()
        {
            InitializeComponent();
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            timerCurrent.Start();


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);

            lblCategoryTitle.Text = "Dashboard";
            this.pnlFormLoader.Controls.Clear();
            frmDashboard dashboard = new frmDashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dashboard.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(dashboard);
            dashboard.Show();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnCategories.Height;
            pnlNav.Top = btnCategories.Top;
            pnlNav.Left = btnCategories.Left;
            btnCategories.BackColor = Color.FromArgb(46, 51, 73);

            lblCategoryTitle.Text = "Manage Categories";
            this.pnlFormLoader.Controls.Clear();
            frmCategories categories = new frmCategories() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            categories.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(categories);
            categories.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnProducts.Height;
            pnlNav.Top = btnProducts.Top;
            pnlNav.Left = btnProducts.Left;
            btnProducts.BackColor = Color.FromArgb(46, 51, 73);

            lblCategoryTitle.Text = "Manage Products";
            this.pnlFormLoader.Controls.Clear();
            frmProducts products = new frmProducts() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            products.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(products);
            products.Show();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnSales.Height;
            pnlNav.Top = btnSales.Top;
            pnlNav.Left = btnSales.Left;
            btnSales.BackColor = Color.FromArgb(46, 51, 73);

            lblCategoryTitle.Text = "Sell Devices";
            this.pnlFormLoader.Controls.Clear();
            frmSales sales = new frmSales() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            sales.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(sales);
            sales.Show();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnSellers.Height;
            pnlNav.Top = btnSellers.Top;
            pnlNav.Left = btnSellers.Left;
            btnSellers.BackColor = Color.FromArgb(46, 51, 73);

            lblCategoryTitle.Text = "Manage Sellers";
            this.pnlFormLoader.Controls.Clear();
            frmSellers sellers = new frmSellers() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            sellers.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(sellers);
            sellers.Show();
        }

        private void btnDashboard_Leave(object sender, EventArgs e)
        {
            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnCategories_Leave(object sender, EventArgs e)
        {
            btnCategories.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnProducts_Leave(object sender, EventArgs e)
        {
            btnProducts.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnSales_Leave(object sender, EventArgs e)
        {
            btnSales.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnSellers_Leave(object sender, EventArgs e)
        {
            btnSellers.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void frm_Load(object sender, EventArgs e)
        {
            lblUserName.Text = LoginForm.sellerName;
            lblTime.Text = DateTime.Now.ToLongTimeString();

        }

        private void lblTime_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void timerCurrent_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
            
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
