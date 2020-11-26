using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Security.Cryptography;
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

        private void EnableAllControl()
        {
            cbb_manv.Enabled = true;
            txt_MK.Enabled = true;
            txt_TK.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void DisableAllControl()
        {
            cbb_manv.Enabled = false;
            txt_MK.Enabled = false;
            txt_ten.Enabled = false;
            txt_TK.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void getInfo()
        {
            string id = guna2DataGridView1.CurrentRow.Cells["manv"].Value.ToString();
            DataRow data = Controller.NhanVienController.findOneByID(id);

            cbb_manv.Text = id;
            txt_TK.Text = data["tentaikhoan"].ToString();

            txt_MK.Text = data["matkhau"].ToString();
            txt_ten.Text = data["tennv"].ToString();
            comboBox1.Text = data["tenchucvu"].ToString();
            

            DisableAllControl();

            bt_Sua.Enabled = true;
            bt_Xoa.Enabled = true;
            bt_Them.Enabled = true;
            if (editMode) editMode = false;
            if (addMode) addMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;
            cbb_manv.Items.Clear();
            foreach (DataRow row in Controller.AccountController.fetchAll().Rows)
            {
                if (string.IsNullOrEmpty(row["tentaikhoan"].ToString()))
                    cbb_manv.Items.Add(row["manv"].ToString());
            }
        } 
        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.AccountController.Accounts();
            getInfo();
            bt_Sua.Enabled = true;
            bt_Xoa.Enabled = true;
            cbb_manv.DropDownStyle = ComboBoxStyle.DropDownList;
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
            comboBox1.Items.Clear();
            foreach(DataRow data in DataController.ExecTable("select * from chucvu").Rows)
            {
                comboBox1.Items.Add(data["tenchucvu"]);
            }
        }

        private void FormQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadTheme();
            ViewLoad();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo();
        }

        private void bt_Them_Click(object sender, EventArgs e)
        {
            if (!addMode)
            {
                addMode = true;
                EnableAllControl();
                btn_HuyThem.Visible = true;
                txt_ten.Clear();
                txt_TK.Clear();
                txt_MK.Clear();
                cbb_manv.SelectedIndex = -1;

                bt_Sua.Enabled = false;
                bt_Xoa.Enabled = false;
            }
            else { 
                if(string.IsNullOrWhiteSpace(cbb_manv.Text))
                    MessageBox.Show("Chưa nhập mã nhân viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_TK.Text))
                    MessageBox.Show("Chưa nhập tài khoản", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_MK.Text))
                    MessageBox.Show("Chưa nhập mật khẩu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string manv = cbb_manv.Text;
                    string taikhoan = txt_TK.Text;
                    string matkhau = txt_MK.Text;
                    Controller.AccountController.UpdateAccount(manv, taikhoan, matkhau);
                }
                ViewLoad();
            }
        }

        private void bt_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xoá", "Xoá tài khoản", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string manv = guna2DataGridView1.CurrentRow.Cells["manv"].Value.ToString();
                Controller.AccountController.DeleteAccount(manv);
            }
            ViewLoad();
        }

        private void bt_Sua_Click(object sender, EventArgs e)
        {
            if (!editMode)
            {
                editMode = true;
                EnableAllControl();
                cbb_manv.Enabled = false;
                txt_ten.Enabled = false;
                bt_Them.Enabled = false;
                bt_Xoa.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_TK.Text))
                    MessageBox.Show("Chưa nhập tên tài khoản", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_MK.Text))
                    MessageBox.Show("Chưa nhập mật khẩu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string manv = guna2DataGridView1.CurrentRow.Cells["manv"].Value.ToString();
                    string taikhoan = txt_TK.Text;
                    string matkhau = txt_MK.Text;

                    Controller.AccountController.UpdateAccount(manv, taikhoan, matkhau);
                }
                ViewLoad();
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void cbb_manv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = cbb_manv.Text;
            DataRow data = Controller.NhanVienController.findOneByID(id);
            txt_ten.Text = data["tennv"].ToString();
        }
    }
}
