using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                MessageBox.Show("Chưa nhập tài khoản", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Chưa nhập mật khẩu", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataTable dtLogin = DataController.ExecTable("select tentaikhoan, matkhau from nhanvien");
                foreach (DataRow row in dtLogin.Rows)
                {
                    if (txtMatKhau.Text == row["tentaikhoan"].ToString() && txtMatKhau.Text == row["matkhau"].ToString())
                    {
                        LoginUser.manv = int.Parse(row["manv"].ToString());
                        Form main = new MainForm();
                        main.Show();
                        this.Hide();
                        return;
                    }
                }
                MessageBox.Show("Nhập sai tài khoản hoặc mật khẩu", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
