using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace PosSystem
{
    public partial class frmProduct_List : Form
    {
        ApiService api = new ApiService();

        public frmProduct_List()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(this);
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;

            // Still uses old SQL forms for now
            frm.LocalBrand();
            frm.LocalCategory();

            frm.ShowDialog();
        }

        public async void LoadRecords()
        {
            try
            {
                int i = 0;
                dataGridView1.Rows.Clear();

                string productsJson = await api.GetProducts();
                JArray products = JArray.Parse(productsJson);

                foreach (JObject product in products)
                {
                    string pcode = product["id"]?.ToString() ?? "";
                    string barcode = product["barcode"]?.ToString() ?? "";
                    string description =
                        product["name"]?.ToString()
                        ?? product["pdesc"]?.ToString()
                        ?? product["description"]?.ToString()
                        ?? "";

                    string brand =
                        product["brand"]?.ToString()
                        ?? product["brand_name"]?.ToString()
                        ?? "";

                    string category =
                        product["category"]?.ToString()
                        ?? product["category_name"]?.ToString()
                        ?? product["gender"]?.ToString()
                        ?? "";

                    string price = product["price"]?.ToString() ?? "0";
                    string reorder =
                        product["reorder"]?.ToString()
                        ?? product["stock"]?.ToString()
                        ?? product["quantity"]?.ToString()
                        ?? "0";

                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        if (!description.ToLower().StartsWith(txtSearch.Text.ToLower()))
                        {
                            continue;
                        }
                    }

                    i++;
                    dataGridView1.Rows.Add(
                        i,
                        pcode,
                        barcode,
                        description,
                        brand,
                        category,
                        price,
                        reorder
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load products from Laravel API.\n\n" + ex.Message,
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string colName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colName == "Edit")
            {
                frmProduct frm = new frmProduct(this);
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;

                frm.TxtPcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                frm.txtBarcode.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();
                frm.txtPdesc.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();
                frm.comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value?.ToString();
                frm.comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value?.ToString();
                frm.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value?.ToString();
                frm.txtReOrder.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value?.ToString();

                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show(
                    "Are you sure you want to delete this product?",
                    "Delete Product",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                        await api.DeleteProduct(id);

                        MessageBox.Show(
                            "Product removed successfully.",
                            "Done",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        LoadRecords();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Unable to delete product from Laravel API.\n\n" + ex.Message,
                            "API Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
        }
    }
}