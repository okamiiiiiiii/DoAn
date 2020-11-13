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
            btn_HuyThem.Visible = false;
            dtP_Birth.MaxDate = DateTime.Now;
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_TK.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_MK.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            cbb_VT.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();

            DataTable dt = DataController.ExecTable("select * from nhanvien where manv = '" + textBox1.Text + "'");
            DataRow data = dt.Rows[0];

            txt_ten.Text = data["tennv"].ToString();
            cbb_GT.Text = data["gioitinh"].ToString();
            dtP_Birth.Value = Convert.ToDateTime(data["namsinh"].ToString());
            txt_SDT.Text = data["dienthoai"].ToString();
            txt_DiaChi.Text = data["diachi"].ToString();

            textBox1.Enabled = false;
            txt_MK.Enabled = false;
            txt_TK.Enabled = false;
            cbb_VT.Enabled = false;
            txt_ten.Enabled = false;
            cbb_GT.Enabled = false;
            dtP_Birth.Enabled = false;
            txt_SDT.Enabled = false;
            txt_DiaChi.Enabled = false;

            bt_Sua.Enabled = true;
            bt_Xoa.Enabled = true;
            bt_Them.Enabled = true;
        }

        private void bt_Them_Click(object sender, EventArgs e)
        {
            if (!addMode)
            {
                textBox1.Clear();
                txt_TK.Clear();
                txt_MK.Clear();
                txt_DiaChi.Clear();
                txt_SDT.Clear();
                txt_ten.Clear();
                dtP_Birth.Value = dtP_Birth.MaxDate;
                cbb_GT.SelectedIndex = -1;
                cbb_VT.SelectedIndex = -1;

                textBox1.Text = guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 1].Cells[0].Value.ToString();
                textBox1.Enabled = false;
                txt_MK.Enabled = true;
                txt_TK.Enabled = true;
                cbb_VT.Enabled = true;
                txt_ten.Enabled = true;
                cbb_GT.Enabled = true;
                dtP_Birth.Enabled = true;
                txt_SDT.Enabled = true;
                txt_DiaChi.Enabled = true;

                bt_Sua.Enabled = false;
                bt_Xoa.Enabled = false;
                btn_HuyThem.Visible = true;
                addMode = true;
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
                    string query = "insert into nhanvien (" + 
                        "N'" + txt_ten.Text + "'" + 
                        "N'" + cbb_GT.Text + "'" + 
                        "'" + dtP_Birth.Value.ToString("yyyy-MM-dd") + "'" +
                        "N'" + txt_DiaChi.Text + "'" +
                        "'" + txt_SDT.Text + "'" +
                        "'" + txt_TK.Text + "'" +
                        "'" + txt_MK.Text + "'" +
                        "N'" +cbb_VT.Text + "'" +
                        ")";
                    DataController.Execute(query);

                    textBox1.Clear();
                    txt_TK.Clear();
                    txt_MK.Clear();
                    cbb_VT.SelectedIndex = -1;

                    textBox1.Enabled = false;
                    txt_MK.Enabled = false;
                    txt_TK.Enabled = false;
                    cbb_VT.Enabled = false;
                    txt_ten.Enabled = false;
                    cbb_GT.Enabled = false;
                    dtP_Birth.Enabled = false;
                    txt_SDT.Enabled = false;
                    txt_DiaChi.Enabled = false;

                    bt_Sua.Enabled = true;
                    bt_Xoa.Enabled = true;  
                }
                guna2DataGridView1.DataSource = DataController.ExecTable("SELECT manv, tentaikhoan, matkhau, phanquyen FROM nhanvien");
                addMode = false;
                btn_HuyThem.Visible = false;
            }
        }

            private void bt_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xoá", "Xoá tài khoản", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string query = "deletenhanvien " + guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                DataController.Execute(query);
                guna2DataGridView1.DataSource = DataController.ExecTable("SELECT manv, tentaikhoan, matkhau, phanquyen FROM nhanvien");
                textBox1.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                txt_TK.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
                txt_MK.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                cbb_VT.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();

                DataTable dt = DataController.ExecTable("select * from nhanvien where manv = '" + textBox1.Text + "'");
                DataRow data = dt.Rows[0];

                txt_ten.Text = data["tennv"].ToString();
                cbb_GT.Text = data["gioitinh"].ToString();
                dtP_Birth.Value = Convert.ToDateTime(data["namsinh"].ToString());
                txt_SDT.Text = data["dienthoai"].ToString();
                txt_DiaChi.Text = data["diachi"].ToString();
            }
        }

        private void bt_Sua_Click(object sender, EventArgs e)
        {
            if (!editMode)
            {
                txt_MK.Enabled = true;
                txt_TK.Enabled = true;
                cbb_VT.Enabled = true;
                txt_ten.Enabled = true;
                cbb_GT.Enabled = true;
                dtP_Birth.Enabled = true;
                txt_SDT.Enabled = true;
                txt_DiaChi.Enabled = true;

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
                    string query = "update nhanvien set tentaikhoan = '" + txt_TK.Text +
                        "', matkhau = '" + txt_MK.Text + 
                        "', phanquyen = N'" + cbb_VT.Text +
                        "', gioitinh = N'" + cbb_GT.Text +
                        "', tennv = N'" + txt_ten.Text +
                        "', namsinh = '" + dtP_Birth.Value.ToString("yyyy-MM-dd") +
                        "', diachi = N'" + txt_DiaChi.Text +
                        "', dienthoai = N'" + txt_SDT.Text +
                        "' where manv = " + textBox1.Text;
                    DataController.Execute(query);
                    bt_Them.Enabled = true;
                    bt_Xoa.Enabled = true;

                    txt_MK.Enabled = false;
                    txt_TK.Enabled = false;
                    cbb_VT.Enabled = false;
                }
                guna2DataGridView1.DataSource = DataController.ExecTable("SELECT manv, tentaikhoan, matkhau, phanquyen FROM nhanvien");
                editMode = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txt_MK_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            txt_TK.Clear();
            txt_MK.Clear();
            cbb_VT.SelectedIndex = -1;

            textBox1.Enabled = false;
            txt_MK.Enabled = false;
            txt_TK.Enabled = false;
            cbb_VT.Enabled = false;
            txt_ten.Enabled = false;
            cbb_GT.Enabled = false;
            dtP_Birth.Enabled = false;
            txt_SDT.Enabled = false;
            txt_DiaChi.Enabled = false;

            bt_Sua.Enabled = true;
            bt_Xoa.Enabled = true;
            guna2DataGridView1.DataSource = DataController.ExecTable("SELECT manv, tentaikhoan, matkhau, phanquyen FROM nhanvien");
            addMode = false;
            btn_HuyThem.Visible = false;

            textBox1.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_TK.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_MK.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            cbb_VT.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();

            DataTable dt = DataController.ExecTable("select * from nhanvien where manv = '" + textBox1.Text + "'");
            DataRow data = dt.Rows[0];

            txt_ten.Text = data["tennv"].ToString();
            cbb_GT.Text = data["gioitinh"].ToString();
            dtP_Birth.Value = Convert.ToDateTime(data["namsinh"].ToString());
            txt_SDT.Text = data["dienthoai"].ToString();
            txt_DiaChi.Text = data["diachi"].ToString();

            btn_HuyThem.Visible = false;
        }
    }
}
