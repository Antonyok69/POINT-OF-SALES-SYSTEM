using System;
using System.Windows.Forms;

namespace PosSystem
{
    public partial class frmUserLogin : Form
    {
        public string _pass, _username = "";
        public bool _isactive = true;

        public frmUserLogin()
        {
            InitializeComponent();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("EXIT APPLICATION?", "CONFIRM",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser.Text.Trim() == "admin" && txtPass.Text.Trim() == "admin")
                {
                    MessageBox.Show("Welcome Admin!", "ACCESS GRANTED",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtPass.Clear();
                    txtUser.Clear();

                    this.Hide();

                    Form1 frm = new Form1();
                    frm.lblName.Text = "Admin";
                    frm.lblUser.Text = "admin";
                    frm.lblRole.Text = "Admin";
                    frm._pass = "admin";
                    frm._user = "admin";
                  // frm.MyDashbord();
                  //  frm.ShowDialog();

                    return;
                }

                MessageBox.Show("Invalid username or password!",
                    "ACCESS DENIED",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void frmUserLogin_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}