using System;
using System.Windows.Forms;

namespace PosSystem
{
    public partial class Form1 : Form
    {
        public string _pass, _user;

        public Form1()
        {
            InitializeComponent();

            // SQL Server disabled while migrating to Laravel API
            // NotifyCriticalItems();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public void NotifyCriticalItems()
        {
            // Disabled while replacing SQL Server with Laravel API
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            frmBrandList frm = new frmBrandList();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmCategoryList frm = new frmCategoryList();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();

            // Disabled while SQL Server is being removed
            // frm.LoadCategory();

            frm.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProduct_List frm = new frmProduct_List();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();

            // Disabled while SQL Server is being removed
            // frm.LoadRecords();

            frm.Show();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            frmStockin frm = new frmStockin();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmUserAccount frm = new frmUserAccount(this);
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.txtU.Text = _user;
            frm.BringToFront();
            frm.Show();
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmRecords frm = new frmRecords();
            frm.TopLevel = false;

            // Disabled while SQL Server is being removed
            // frm.LoadCriticalItems();
            // frm.LoadInventory();
            // frm.LoadStockHistory();
            // frm.CancelledOrder();

            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmStoreSetting frm = new frmStoreSetting();

            // Disabled while SQL Server is being removed
            // frm.LoadRecord();

            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyDashbord();
        }

        public void MyDashbord()
        {
            frmDashbord f = new frmDashbord();
            f.TopLevel = false;
            panel3.Controls.Add(f);

            // Temporary values while Laravel API dashboard is not yet connected
            f.lblDailySales.Text = "0.00";
            f.lblProduct.Text = "0";
            f.lblStock.Text = "0";
            f.lblCriticalItems.Text = "0";

            f.BringToFront();
            f.Show();
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            frm_VendorList f = new frm_VendorList();
            f.TopLevel = false;
            panel3.Controls.Add(f);

            // Disabled while SQL Server is being removed
            // f.LoadRecords();

            f.BringToFront();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAdjustment f = new frmAdjustment(this);

            // Disabled while SQL Server is being removed
            // f.LoadRecords();
            // f.referenceNo();

            f.txtUser.Text = lblUser.Text;
            f.ShowDialog();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout Application?", "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frmUserLogin frm = new frmUserLogin();
                frm.ShowDialog();
            }
        }
    }
}