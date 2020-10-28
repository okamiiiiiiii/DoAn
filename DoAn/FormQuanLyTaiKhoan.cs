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
    public partial class FormQuanLyTaiKhoan : Form
    {
        static bool editMode = false;
        static bool addMode = false;
        public FormQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
            label2.ForeColor = ThemeColor.PrimaryColor;
            label3.ForeColor = ThemeColor.PrimaryColor;
            label4.ForeColor = ThemeColor.PrimaryColor;
            label5.ForeColor = ThemeColor.PrimaryColor;
        }

        private void FormQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadTheme();
            guna2DataGridView1.DataSource = DataController.ExecTable("SELECT manv, tentaikhoan, matkhau, phanquyen FROM nhanvien");
            bt_Sua.Enabled = false;
            bt_Xoa.Enabled = false;

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_TK.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_MK.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            cbb_VT.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();

            textBox1.Enabled = false;
            txt_MK.Enabled = false;
            txt_TK.Enabled = false;
            cbb_VT.Enabled = false;

            bt_Sua.Enabled = true;
            bt_Xoa.Enabled = true;
        }

        private void bt_Them_Click(object sender, EventArgs e)
        {
            if (!addMode)
            {
                textBox1.Clear();
                txt_TK.Clear();
                txt_MK.Clear();
                cbb_VT.SelectedIndex = -1;

                textBox1.Enabled = true;
                txt_MK.Enabled = true;
                txt_TK.Enabled = true;
                cbb_VT.Enabled = true;

                bt_Sua.Enabled = false;
                bt_Xoa.Enabled = false;
            }
            else { 
                if(string.IsNullOrWhiteSpace(textBox1.Text))
                    MessageBox.Show("Chưa nhập mã tài khoản", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else if (string.IsNullOrWhiteSpace(txt_TK.Text))
                    MessageBox.Show("Chưa nhập tên tài khoản", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else if (string.IsNullOrWhiteSpace(txt_MK.Text))
                    MessageBox.Show("Chưa nhập mật khẩu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else if(cbb_VT.SelectedIndex == -1)
                    MessageBox.Show("Chưa nhập vị trí", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string query = "insert into taikhoan ()";
                    DataController.ExecTable(query);

                    textBox1.Clear();
                    txt_TK.Clear();
                    txt_MK.Clear();
                    cbb_VT.SelectedIndex = -1;

                    textBox1.Enabled = false;
                    txt_MK.Enabled = false;
                    txt_TK.Enabled = false;
                    cbb_VT.Enabled = false;

                    bt_Sua.Enabled = true;
                    bt_Xoa.Enabled = true;
                }
                guna2DataGridView1.DataSource = DataController.ExecTable("SELECT manv, tentaikhoan, matkhau, phanquyen FROM nhanvien");
            }
        }

            private void bt_Xoa_Click(object sender, EventArgs e)
        {
            string query = "delete from nhanvien where manv = '" + textBox1.Text + "'";
            DataController.ExecTable(query);
            guna2DataGridView1.DataSource = DataController.ExecTable("SELECT manv, tentaikhoan, matkhau, phanquyen FROM nhanvien");
        }

        private void bt_Sua_Click(object sender, EventArgs e)
        {
            if (!editMode)
            {
                txt_MK.Enabled = true;
                txt_TK.Enabled = true;
                cbb_VT.Enabled = true;

                bt_Them.Enabled = false;
                bt_Xoa.Enabled = false;

                editMode = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_TK.Text))
                    MessageBox.Show("Chưa nhập tên tài khoản", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else if (string.IsNullOrWhiteSpace(txt_MK.Text))
                    MessageBox.Show("Chưa nhập mật khẩu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else if (cbb_VT.SelectedIndex == -1)
                    MessageBox.Show("Chưa chọn vị trí", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string query = "update nhanvien set tentaikhoan = '" + txt_TK.Text + "', matkhau = '" + txt_MK.Text + "', phanquyen = '" + cbb_VT.Text + "' where manv = " + textBox1.Text;
                    DataController.ExecTable(query);
                    bt_Them.Enabled = true;
                    bt_Xoa.Enabled = true;

                    txt_MK.Enabled = false;
                    txt_TK.Enabled = false;
                    cbb_VT.Enabled = false;
                }
                guna2DataGridView1.DataSource = DataController.ExecTable("SELECT manv, tentaikhoan, matkhau, phanquyen FROM nhanvien");
            }
        }
    }
}
