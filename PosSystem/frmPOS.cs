using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PosSystem
{
    public partial class frmPOS : Form
    {
        string id;
        string price;

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;

        string stitle = "Pos System";
        int qty;
        frmUserLogin f;

        public frmPOS(frmUserLogin frm)
        {
            InitializeComponent();

            lblDate.Text = DateTime.Now.ToLongDateString();
            this.KeyPreview = true;

            cn = new SqlConnection(dbcon.MyConnection());
            f = frm;

            // Disabled first while Laravel API integration is ongoing
            // NotifyCriticalItems();
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            timer1.Start();

            // Disabled first kay SQL Server removed na
            lblAddress.Text = "";
            lblSname.Text = "Shoe Store POS";
            lblPhone.Text = "";
        }

        public void GetCartTotal()
        {
            double discount = double.Parse(lblDiscount.Text);
            double total = double.Parse(lblTotal.Text);
            double sales = total - discount;
            double vat = sales * dbcon.GetVal();
            double vatable = sales - vat;

            lblDisplayTotal.Text = vatable.ToString("#,##0.00");
            lblVat.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");
        }

        public void NotifyCriticalItems()
        {
            // Temporary disabled to fix build error:
            // await api.GetProducts() cannot be used inside non-async method.
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Temporary disabled while replacing SQL Server cart with Laravel API
        }

        public void GetTransNo()
        {
            string sdate = DateTime.Now.ToString("yyyyMMdd");
            lblTransno.Text = sdate + "1001";
        }

        private void btnTrans_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                return;
            }

            GetTransNo();
            txtSearch.Enabled = true;
            txtSearch.Focus();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Temporary disabled while replacing SQL search with Laravel API
        }

        public void LoadCart()
        {
            try
            {
                double total = 0;
                double discount = 0;

                lblDiscount.Text = discount.ToString("#,##0.00");
                lblTotal.Text = total.ToString("#,##0.00");

                GetCartTotal();

                btnSattle.Enabled = dataGridView1.Rows.Count > 0;
                btnDiscount.Enabled = dataGridView1.Rows.Count > 0;
                btnCancel.Enabled = dataGridView1.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddToCart(string _pcode, double _price, int _qty)
        {
            // Temporary disabled while replacing SQL cart with Laravel API
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (lblTransno.Text == "00000000000000")
            {
                return;
            }

            frmLookUp frm = new frmLookUp(this);

            // Disabled first while SQL Server removed
            // frm.LoadRecords();

            frm.ShowDialog();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            frmDiscount frm = new frmDiscount(this);
            frm.lblID.Text = id;
            frm.txtPrice.Text = price;
            frm.ShowDialog();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            int i = dataGridView1.CurrentRow.Index;

            if (dataGridView1.Rows.Count > 0)
            {
                id = dataGridView1[1, i].Value?.ToString();
                price = dataGridView1[7, i].Value?.ToString();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void label17_Click(object sender, EventArgs e)
        {
        }

        private void label14_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
            label6.Text = DateTime.Now.ToLongDateString();
        }

        private void btnSattle_Click(object sender, EventArgs e)
        {
            frmSettel frm = new frmSettel(this);
            frm.txtSale.Text = lblDisplayTotal.Text;
            frm.ShowDialog();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.dateTimePicker1.Enabled = false;
            frm.dateTimePicker2.Enabled = false;
            frm.suser = LblUser.Text;
            frm.cbCashier.Enabled = false;
            frm.cbCashier.Text = LblUser.Text;
            frm.ShowDialog();
        }

        private void lblName_Click(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Unable to Logout. Please cancel the transaction.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Logout Application?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frmUserLogin frm = new frmUserLogin();
                frm.ShowDialog();
            }
        }

        private void LblUser_Click(object sender, EventArgs e)
        {
        }

        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) btnTrans_Click(sender, e);
            if (e.KeyCode == Keys.F2) btnSearch_Click(sender, e);
            if (e.KeyCode == Keys.F5) btnCancel_Click(sender, e);
            if (e.KeyCode == Keys.F3) btnDiscount_Click(sender, e);
            if (e.KeyCode == Keys.F7) btnChange_Click(sender, e);
            if (e.KeyCode == Keys.F6) btnSales_Click(sender, e);
            if (e.KeyCode == Keys.F10) btnClose_Click(sender, e);
            if (e.KeyCode == Keys.F4) btnSattle_Click(sender, e);

            if (e.KeyCode == Keys.F8)
            {
                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            frmChangePaasword frm = new frmChangePaasword(this);
            frm.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove items from cart?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
                LoadCart();

                MessageBox.Show("All items have been successfully removed!",
                    "Remove",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}