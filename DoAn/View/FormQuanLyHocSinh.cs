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

        private void EnableAllControl()
        {
            txt_DiaChi.Enabled = true;
            txt_GhiChu.Enabled = true;
            txt_mahs.Enabled = false;
            txt_sdtPhuHuynh.Enabled = true;
            txt_ten.Enabled = true;
            txt_tenPhuHuynh.Enabled = true;
            txt_TienMienGiam.Enabled = true;
            cbb_GT.Enabled = true;
            cbb_makhoi.Enabled = true;
            dtP_Birth.Enabled = true;
        }

        private void DisableAllControl()
        {
            txt_DiaChi.Enabled = false;
            txt_GhiChu.Enabled = false;
            txt_mahs.Enabled = false;
            txt_sdtPhuHuynh.Enabled = false;
            txt_ten.Enabled = false;
            txt_tenPhuHuynh.Enabled = false;
            txt_TienMienGiam.Enabled = false;
            cbb_GT.Enabled = false;
            cbb_makhoi.Enabled = false;
            dtP_Birth.Enabled = false;
        }

        private void getInfo()
        {
            string id = guna2DataGridView1.CurrentRow.Cells["mahocsinh"].Value.ToString();
            DataRow data = Controller.HocSinhController.findOneById(id);
            txt_mahs.Text = id;
            txt_ten.Text = data["tenhocsinh"].ToString();
            cbb_GT.Text = data["gioitinh"].ToString();
            dtP_Birth.Value = Convert.ToDateTime(data["ngaysinh"].ToString());
            cbb_makhoi.Text = data["tenkhoi"].ToString();
            txt_tenPhuHuynh.Text = data["tenphuhuynh"].ToString();
            txt_DiaChi.Text = data["diachi"].ToString();
            txt_sdtPhuHuynh.Text = data["sdtphuhuynh"].ToString();
            txt_GhiChu.Text = data["ghichumiengiam"].ToString();
            txt_TienMienGiam.Text = data["tienmiengiam"].ToString();
            textBox1.Text = data["namnhaphoc"].ToString();
            

            DisableAllControl();

            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
            btn_Them.Enabled = true;

            if (addMode) addMode = false;
            if (editMode) editMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;
        }

        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.HocSinhController.index();
            getInfo();
        }

        private void ActiveAddMode()
        {
            if (!addMode)
            {
                txt_mahs.Text = (int.Parse(guna2DataGridView1.Rows[guna2DataGridView1.Rows.Count - 1].Cells[0].Value.ToString()) + 1).ToString();
                txt_ten.Clear();
                txt_tenPhuHuynh.Clear();
                txt_DiaChi.Clear();
                txt_sdtPhuHuynh.Clear();
                cbb_GT.SelectedIndex = -1;
                cbb_makhoi.SelectedIndex = -1;
                dtP_Birth.Value = dtP_Birth.MaxDate;
                txt_TienMienGiam.Clear();
                txt_GhiChu.Clear();

                EnableAllControl();

                btn_Sua.Enabled = false;
                btn_Xoa.Enabled = false;
                btn_HuyThem.Visible = true;
            }
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
            ViewLoad();

            dtP_Birth.MaxDate = DateTime.Now;

            foreach (DataRow row in Controller.HocSinhController.fetchKhoi().Rows)
            {
                cbb_makhoi.Items.Add(row["tenkhoi"].ToString());
            }

            ViewLoad();
            cbb_GT.DropDownStyle = ComboBoxStyle.DropDownList;
            cbb_makhoi.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (!addMode)
            {
                ActiveAddMode();
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
                else if (string.IsNullOrWhiteSpace(txt_TienMienGiam.Text))
                {
                    MessageBox.Show("Chưa nhập tiền miễn giảm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = txt_ten.Text;
                    string diachi = txt_DiaChi.Text;
                    string gioitinh = cbb_GT.Text;
                    string ngaysinh = dtP_Birth.Value.ToString("yyyy-MM-dd");
                    string tenphuhuynh = txt_tenPhuHuynh.Text;
                    string sdtphuhuynh = txt_sdtPhuHuynh.Text;
                    string tienmiengiam = txt_TienMienGiam.Text;
                    string ghichu = txt_GhiChu.Text;
                    int makhoi = cbb_makhoi.SelectedIndex + 1;
                    Controller.HocSinhController.addHocsinh(ten, diachi, gioitinh, ngaysinh, tenphuhuynh, sdtphuhuynh, tienmiengiam, ghichu, makhoi);
                }
                ViewLoad();
            }
        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (!editMode)
            {
                EnableAllControl();

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
                else if (string.IsNullOrWhiteSpace(txt_TienMienGiam.Text))
                {
                    MessageBox.Show("Chưa nhập tiền miễn giảm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = txt_ten.Text;
                    string diachi = txt_DiaChi.Text;
                    string gioitinh = cbb_GT.Text;
                    string ngaysinh = dtP_Birth.Value.ToString("yyyy-MM-dd");
                    string tenphuhuynh = txt_tenPhuHuynh.Text;
                    string sdtphuhuynh = txt_sdtPhuHuynh.Text;
                    int makhoi = cbb_makhoi.SelectedIndex + 1;
                    string tienmiengiam = txt_TienMienGiam.Text;
                    string ghichu = txt_GhiChu.Text;
                    string id = txt_mahs.Text;

                    Controller.HocSinhController.updateHocSinh(id, ten, diachi, gioitinh, ngaysinh, tenphuhuynh, sdtphuhuynh, tienmiengiam, ghichu, makhoi);
                }
                ViewLoad();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DataTable dt = DataController.ExecTable("select * from hoadonthang where mahocsinh = " + txt_mahs.Text + "" +
                "and matrangthai=1 or matrangthai=2");
            if (dt.Rows.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Học sinh này vẫn còn chưa đóng học phí hoặc nợ, vẫn muốn xóa chứ ?", "Xoá tài khoản", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Controller.HocSinhController.deleteHocSinh(txt_mahs.Text);
                }
            }
            else
            {
                Controller.HocSinhController.deleteHocSinh(txt_mahs.Text);
            }
            ViewLoad();
        }
    }
}
