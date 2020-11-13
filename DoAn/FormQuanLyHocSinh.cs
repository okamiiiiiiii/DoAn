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
    public partial class FormQuanLyHocSinh : Form
    {
        static bool editMode = false;
        static bool addMode = false;
        public FormQuanLyHocSinh()
        {
            InitializeComponent();
        }

        private void FormQuanLyChi_Load(object sender, EventArgs e)
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
            groupBox1.ForeColor = ThemeColor.PrimaryColor;
            label1.ForeColor = ThemeColor.PrimaryColor;
            label2.ForeColor = ThemeColor.PrimaryColor;
            label3.ForeColor = ThemeColor.PrimaryColor;
            label4.ForeColor = ThemeColor.PrimaryColor;
            label5.ForeColor = ThemeColor.PrimaryColor;
            label6.ForeColor = ThemeColor.PrimaryColor;
            label7.ForeColor = ThemeColor.PrimaryColor;
            label8.ForeColor = ThemeColor.PrimaryColor;
            guna2DataGridView1.DataSource = DataController.ExecTable("SELECT mahocsinh, tenhocsinh, gioitinh, ngaysinh, makhoi FROM hocsinh");

            txt_mahs.Enabled = false;
            txt_ten.Enabled = false;
            cbb_GT.Enabled = false;
            cbb_makhoi.Enabled = false;
            dtP_Birth.Enabled = false;
            txt_tenPhuHuynh.Enabled = false;
            txt_sdtPhuHuynh.Enabled = false;
            txt_DiaChi.Enabled = false;

            dtP_Birth.MaxDate = DateTime.Now;
            btn_HuyThem.Visible = false;

            DataTable dtKhoiLop1 = DataController.ExecTable("select * from khoi");
            foreach (DataRow row in dtKhoiLop1.Rows)
            {
                cbb_makhoi.Items.Add(row["makhoi"].ToString() + " - " + row["tenkhoi"].ToString());
            }

            txt_mahs.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            cbb_GT.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            dtP_Birth.Value = Convert.ToDateTime(guna2DataGridView1.CurrentRow.Cells[3].Value.ToString());
            DataTable dtKhoiLop = DataController.ExecTable("select * from khoi where makhoi = " + guna2DataGridView1.CurrentRow.Cells[4].Value.ToString());
            cbb_makhoi.Text = dtKhoiLop.Rows[0]["makhoi"].ToString() + " - " + dtKhoiLop.Rows[0]["tenkhoi"].ToString();

            DataTable dt = DataController.ExecTable("select * from hocsinh where mahocsinh = '" + txt_mahs.Text + "'");
            DataRow data = dt.Rows[0];
            txt_tenPhuHuynh.Text = data["tenphuhuynh"].ToString();
            txt_DiaChi.Text = data["diachi"].ToString();
            txt_sdtPhuHuynh.Text = data["sdtphuhuynh"].ToString();
            cbb_GT.DropDownStyle = ComboBoxStyle.DropDownList;
            cbb_makhoi.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_mahs.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            cbb_GT.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            dtP_Birth.Value = Convert.ToDateTime(guna2DataGridView1.CurrentRow.Cells[3].Value.ToString());
            DataTable dtKhoiLop = DataController.ExecTable("select * from khoi where makhoi = " + guna2DataGridView1.CurrentRow.Cells[4].Value.ToString());
            cbb_makhoi.Text = dtKhoiLop.Rows[0]["makhoi"].ToString() + " - " + dtKhoiLop.Rows[0]["tenkhoi"].ToString();

            DataTable dt = DataController.ExecTable("select * from hocsinh where mahocsinh = '" + txt_mahs.Text + "'");
            DataRow data = dt.Rows[0];
            txt_tenPhuHuynh.Text = data["tenphuhuynh"].ToString();
            txt_DiaChi.Text = data["diachi"].ToString();
            txt_sdtPhuHuynh.Text = data["sdtphuhuynh"].ToString();

            txt_mahs.Enabled = false;
            txt_ten.Enabled = false;
            cbb_GT.Enabled = false;
            cbb_makhoi.Enabled = false;
            dtP_Birth.Enabled = false;
            txt_tenPhuHuynh.Enabled = false;
            txt_sdtPhuHuynh.Enabled = false;
            txt_DiaChi.Enabled = false;

            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (!addMode)
            {

                txt_mahs.Text = guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 1].Cells[0].Value.ToString();
                txt_ten.Clear();
                txt_tenPhuHuynh.Clear();
                txt_DiaChi.Clear();
                txt_sdtPhuHuynh.Clear();
                cbb_GT.SelectedIndex = -1;
                cbb_makhoi.SelectedIndex = -1;
                dtP_Birth.Value = dtP_Birth.MaxDate;

                txt_mahs.Enabled = false;
                txt_ten.Enabled = true;
                cbb_GT.Enabled = true;
                cbb_makhoi.Enabled = true;
                dtP_Birth.Enabled = true;
                txt_tenPhuHuynh.Enabled = true;
                txt_sdtPhuHuynh.Enabled = true;
                txt_DiaChi.Enabled = true;

                btn_Sua.Enabled = false;
                btn_Xoa.Enabled = false;
                btn_HuyThem.Visible = true;
                addMode = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên học sinh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_tenPhuHuynh.Text))
                {
                    MessageBox.Show("Chưa nhập tên phụ huynh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_sdtPhuHuynh.Text))
                {
                    MessageBox.Show("Chưa nhập sđt phụ huynh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_DiaChi.Text))
                {
                    MessageBox.Show("Chưa nhập tên địa chỉ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(cbb_GT.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập giới tính", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_makhoi.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập giới tính", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string query = "insert into hocsinh values (" +
                        "N'" + txt_ten.Text + "', " +
                        "N'" + txt_DiaChi.Text + "', " +
                        "N'" + cbb_GT.Text + "', " +
                        "'" + dtP_Birth.Value.ToString("yyyy-MM-dd") + "', " +
                        "N'" + txt_tenPhuHuynh.Text + "', " +
                        "'" + txt_sdtPhuHuynh.Text + "', " +
                        "'" + cbb_makhoi.Text.Split(' ')[0] + "'" +
                        ")";

                    DataController.Execute(query);
                    guna2DataGridView1.DataSource = DataController.ExecTable("SELECT mahocsinh, tenhocsinh, gioitinh, ngaysinh, makhoi FROM hocsinh");
                    addMode = false;
                    btn_HuyThem.Visible = false;
                }
            }
        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            txt_mahs.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            cbb_GT.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            dtP_Birth.Value = Convert.ToDateTime(guna2DataGridView1.CurrentRow.Cells[3].Value.ToString());
            DataTable dtKhoiLop = DataController.ExecTable("select * from khoi where makhoi = " + guna2DataGridView1.CurrentRow.Cells[4].Value.ToString());
            cbb_makhoi.Text = dtKhoiLop.Rows[0]["makhoi"].ToString() + " - " + dtKhoiLop.Rows[0]["tenkhoi"].ToString();

            DataTable dt = DataController.ExecTable("select * from hocsinh where mahocsinh = '" + txt_mahs.Text + "'");
            DataRow data = dt.Rows[0];
            txt_tenPhuHuynh.Text = data["tenphuhuynh"].ToString();
            txt_DiaChi.Text = data["diachi"].ToString();
            txt_sdtPhuHuynh.Text = data["sdtphuhuynh"].ToString();

            txt_mahs.Enabled = false;
            txt_ten.Enabled = false;
            cbb_GT.Enabled = false;
            cbb_makhoi.Enabled = false;
            dtP_Birth.Enabled = false;
            txt_tenPhuHuynh.Enabled = false;
            txt_sdtPhuHuynh.Enabled = false;
            txt_DiaChi.Enabled = false;

            btn_HuyThem.Visible = false;
            addMode = false;
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (!editMode)
            {
                txt_mahs.Enabled = false;
                txt_ten.Enabled = true;
                cbb_GT.Enabled = true;
                cbb_makhoi.Enabled = true;
                dtP_Birth.Enabled = true;
                txt_tenPhuHuynh.Enabled = true;
                txt_sdtPhuHuynh.Enabled = true;
                txt_DiaChi.Enabled = true;

                btn_Them.Enabled = false;
                btn_Xoa.Enabled = false;
                editMode = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên học sinh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_tenPhuHuynh.Text))
                {
                    MessageBox.Show("Chưa nhập tên phụ huynh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_sdtPhuHuynh.Text))
                {
                    MessageBox.Show("Chưa nhập sđt phụ huynh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_DiaChi.Text))
                {
                    MessageBox.Show("Chưa nhập tên địa chỉ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_GT.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập giới tính", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_makhoi.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập giới tính", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string query = "update hocsinh set " +
                        "tenhocsinh = N'" + txt_ten.Text + "', " +
                        "diachi = N'" + txt_DiaChi.Text + "', " +
                        "gioitinh = N'" + cbb_GT.Text + "', " +
                        "ngaysinh = '" + dtP_Birth.Value.ToString("yyyy-MM-dd") + "', " +
                        "tenphuhuynh = N'" + txt_tenPhuHuynh.Text + "', " +
                        "sdtphuhuynh = '" + txt_sdtPhuHuynh.Text + "', " +
                        "makhoi = '" + cbb_makhoi.Text.Split(' ')[0] + "'" +
                        "where mahocsinh = " + txt_mahs.Text;

                    DataController.Execute(query);

                    txt_mahs.Enabled = false;
                    txt_ten.Enabled = false;
                    cbb_GT.Enabled = false;
                    cbb_makhoi.Enabled = false;
                    dtP_Birth.Enabled = false;
                    txt_tenPhuHuynh.Enabled = false;
                    txt_sdtPhuHuynh.Enabled = false;
                    txt_DiaChi.Enabled = false;

                    guna2DataGridView1.DataSource = DataController.ExecTable("SELECT mahocsinh, tenhocsinh, gioitinh, ngaysinh, makhoi FROM hocsinh");
                    editMode = false;
                    btn_HuyThem.Visible = false;
                    btn_Them.Enabled = true;
                    btn_Xoa.Enabled = true;
                }
            }
        }
    }
}
