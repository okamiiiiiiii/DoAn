using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.View
{
    public partial class FormKhoanChi : Form
    {
        private bool addMode1 = false, addMode2 = false, addMode3 = false;
        private bool editMode1 = false, editMode2 = false, editMode3 = false;
        private string idPC;
        
        public FormKhoanChi()
        {
            InitializeComponent();
        }

        public FormKhoanChi(string id) : this()
        {
            idPC = id;
        }

        private void Disable1()
        {
            foreach(Control txt in groupBox1.Controls)
            {
                if(txt.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)txt;
                    tb.Enabled = false;
                }

                if (txt.GetType() == typeof(ComboBox))
                {
                    ComboBox cbb = (ComboBox)txt;
                    cbb.Enabled = false;
                }
            }
        }

        private void Disable2()
        {
            foreach (Control txt in groupBox2.Controls)
            {
                if (txt.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)txt;
                    tb.Enabled = false;
                }

                if (txt.GetType() == typeof(ComboBox))
                {
                    ComboBox cbb = (ComboBox)txt;
                    cbb.Enabled = false;
                }
            }
        }

        private void Disable3()
        {
            foreach (Control txt in groupBox3.Controls)
            {
                if (txt.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)txt;
                    tb.Enabled = false;
                }

                if (txt.GetType() == typeof(ComboBox))
                {
                    ComboBox cbb = (ComboBox)txt;
                    cbb.Enabled = false;
                }
            }
        }

        private void Enable1()
        {
            foreach (Control txt in groupBox1.Controls)
            {
                if (txt.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)txt;
                    tb.Enabled = true;
                }

                if (txt.GetType() == typeof(ComboBox))
                {
                    ComboBox cbb = (ComboBox)txt;
                    cbb.Enabled = true;
                }
            }
        }

        private void Enable2()
        {
            foreach (Control txt in groupBox2.Controls)
            {
                if (txt.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)txt;
                    tb.Enabled = true;
                }

                if (txt.GetType() == typeof(ComboBox))
                {
                    ComboBox cbb = (ComboBox)txt;
                    cbb.Enabled = true;
                }
            }
        }

        private void Enable3()
        {
            foreach (Control txt in groupBox3.Controls)
            {
                if (txt.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)txt;
                    tb.Enabled = true;
                }

                if (txt.GetType() == typeof(ComboBox))
                {
                    ComboBox cbb = (ComboBox)txt;
                    cbb.Enabled = true;
                }
            }
        }

        public void getInfo1()
        {
            cbb_tennl.Items.Clear();
            foreach (DataRow row in Controller.Kho.fetchNguyenLieu().Rows)
            {
                cbb_tennl.Items.Add(row["tennguyenlieu"].ToString());
            }
            if (guna2DataGridView1.Rows.Count != 0) {
                cbb_tennl.DropDownStyle = ComboBoxStyle.DropDown;
                string id = guna2DataGridView1.CurrentRow.Cells["matttchi"].Value.ToString();
                DataRow data = Controller.KhoanChiController.findTienChiNguyenLieuByID(id);
                txt_ten1.Text = data["tentttc"].ToString();
                txt_soluong1.Text = data["soluong"].ToString();
                cbb_tennl.Text = data["tennguyenlieu"].ToString();
                txt_tien1.Text = data["thanhtien"].ToString();
                btn_Them1.Enabled = true;
                btn_Sua1.Enabled = true;
                btn_Xoa1.Enabled = true;
            }
            else
            {
                btn_Them1.Enabled = true;
                btn_Sua1.Enabled = false;
                btn_Xoa1.Enabled = false;
                txt_ten1.Clear();
                txt_soluong1.Clear();
                txt_tien1.Clear();
                cbb_tennl.SelectedIndex = -1;
            }

            if (addMode1) addMode1 = false;
            if (editMode1) editMode1 = false;
            if (btn_HuyThem1.Visible == true) btn_HuyThem1.Visible = false;

            

            Disable1();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo1();
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo2();
        }

        private void guna2DataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo3();
        }

        private void btn_Them1_Click(object sender, EventArgs e)
        {
            if(!addMode1)
            {
                addMode1 = true;
                Enable1();
                txt_soluong1.Clear();
                txt_ten1.Clear();
                txt_tien1.Clear();
                cbb_tennl.DropDownStyle = ComboBoxStyle.DropDownList;
                cbb_tennl.SelectedIndex = -1;
                btn_HuyThem1.Visible = true;

                btn_Sua1.Enabled = false;
                btn_Xoa1.Enabled = false;
            }
            else
            {
                if(string.IsNullOrWhiteSpace(txt_soluong1.Text))
                    MessageBox.Show("Chưa nhập số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_ten1.Text))
                    MessageBox.Show("Chưa nhập tên khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_tien1.Text))
                    MessageBox.Show("Chưa nhập thành tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (cbb_tennl.SelectedIndex == -1)
                    MessageBox.Show("Chưa chọn tên nguyên liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string ten = txt_ten1.Text;
                    string soluong = txt_soluong1.Text;
                    string tien = txt_tien1.Text;
                    string maphieuchi = idPC;
                    int manl = cbb_tennl.SelectedIndex + 1;
                    Controller.KhoanChiController.addTienChiNguyenLieu(ten, soluong, tien, maphieuchi, manl);
                }
                ViewLoad1();
            }
        }

        private void btn_Sua1_Click(object sender, EventArgs e)
        {
            if (!editMode1)
            {
                editMode1 = true;
                Enable1();
                cbb_tennl.DropDownStyle = ComboBoxStyle.DropDownList;
                btn_Them1.Enabled = false;
                btn_Xoa1.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_soluong1.Text))
                    MessageBox.Show("Chưa nhập số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_ten1.Text))
                    MessageBox.Show("Chưa nhập tên khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_tien1.Text))
                    MessageBox.Show("Chưa nhập thành tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (cbb_tennl.SelectedIndex == -1)
                    MessageBox.Show("Chưa chọn tên nguyên liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string id = guna2DataGridView1.CurrentRow.Cells["matttchi"].Value.ToString();
                    string ten = txt_ten1.Text;
                    string soluong = txt_soluong1.Text;
                    string tien = txt_tien1.Text;
                    string maphieuchi = idPC;
                    int manl = cbb_tennl.SelectedIndex + 1;
                    Controller.KhoanChiController.updateTienChiNguyenLieu(id, ten, soluong, tien, maphieuchi, manl);
                }
                ViewLoad1();
            }
        }

        private void btn_Xoa1_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["matttchi"].Value.ToString();
            Controller.KhoanChiController.deleteTienChi(id);
            ViewLoad1();
        }

        public void getInfo2()
        {
            cbb_tenhl.Items.Clear();
            foreach (DataRow row in Controller.Kho.fetchHocLieu().Rows)
            {
                cbb_tenhl.Items.Add(row["tenhoclieu"].ToString());
            }
            if (guna2DataGridView2.Rows.Count != 0)
            {
                cbb_tenhl.DropDownStyle = ComboBoxStyle.DropDown;
                string id = guna2DataGridView2.CurrentRow.Cells["matienchihoclieu"].Value.ToString();
                DataRow data = Controller.KhoanChiController.findTienChiHocLieuByID(id);
                txt_ten2.Text = data["tentttc"].ToString();
                txt_soluong2.Text = data["soluong"].ToString();
                cbb_tenhl.Text = data["tenhoclieu"].ToString();
                txt_tien2.Text = data["thanhtien"].ToString();
                btn_them2.Enabled = true;
                btn_sua2.Enabled = true;
                btn_xoa2.Enabled = true;
            }
            else
            {
                btn_them2.Enabled = true;
                btn_sua2.Enabled = false;
                btn_xoa2.Enabled = false;
                txt_ten2.Clear();
                txt_soluong2.Clear();
                txt_tien2.Clear();
                cbb_tenhl.SelectedIndex = -1;
            }

            if (addMode2) addMode2 = false;
            if (editMode2) editMode2 = false;
            if (btn_huythem2.Visible == true) btn_huythem2.Visible = false;

            

            Disable2();
        }

        public void getInfo3()
        {
            if (guna2DataGridView3.Rows.Count != 0)
            {
                string id = guna2DataGridView3.CurrentRow.Cells["tienkhoanchikhac"].Value.ToString();
                DataRow data = Controller.KhoanChiController.findTienChiKhacByID(id);
                txt_ten3.Text = data["tentttc"].ToString();
                txt_soluong3.Text = data["soluong"].ToString();
                txt_tien3.Text = data["thanhtien"].ToString();
                btn_them3.Enabled = true;
                btn_sua3.Enabled = true;
                btn_xoa3.Enabled = true;
            }
            else
            {
                btn_them3.Enabled = true;
                btn_sua3.Enabled = false;
                btn_xoa3.Enabled = false;
                txt_ten3.Clear();
                txt_soluong3.Clear();
                txt_tien3.Clear();
            }

            if (addMode3) addMode3 = false;
            if (editMode3) editMode3 = false;
            if (btn_huythem3.Visible == true) btn_huythem3.Visible = false;

            Disable3();
        }

        private void btn_HuyThem1_Click(object sender, EventArgs e)
        {
            ViewLoad1();
        }

        private void btn_them2_Click(object sender, EventArgs e)
        {
            if (!addMode2)
            {
                addMode2 = true;
                Enable2();
                txt_soluong2.Clear();
                txt_ten2.Clear();
                txt_tien2.Clear();
                cbb_tenhl.DropDownStyle = ComboBoxStyle.DropDownList;
                cbb_tenhl.SelectedIndex = -1;
                btn_huythem2.Visible = true;
                btn_xoa2.Enabled = false;
                btn_sua2.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_soluong2.Text))
                    MessageBox.Show("Chưa nhập số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_ten2.Text))
                    MessageBox.Show("Chưa nhập tên khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_tien2.Text))
                    MessageBox.Show("Chưa nhập thành tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (cbb_tenhl.SelectedIndex == -1)
                    MessageBox.Show("Chưa chọn tên học liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string ten = txt_ten2.Text;
                    string soluong = txt_soluong2.Text;
                    string tien = txt_tien2.Text;
                    string maphieuchi = idPC;
                    int mahl = cbb_tenhl.SelectedIndex + 1;
                    Controller.KhoanChiController.addTienChiHocLieu(ten, soluong, tien, maphieuchi, mahl);
                }
                ViewLoad2();
            }
        }

        private void btn_sua2_Click(object sender, EventArgs e)
        {
            if (!editMode2)
            {
                editMode2 = true;
                Enable2();
                cbb_tenhl.DropDownStyle = ComboBoxStyle.DropDownList;
                btn_them2.Enabled = false;
                btn_xoa2.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_soluong2.Text))
                    MessageBox.Show("Chưa nhập số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_ten2.Text))
                    MessageBox.Show("Chưa nhập tên khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_tien2.Text))
                    MessageBox.Show("Chưa nhập thành tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (cbb_tenhl.SelectedIndex == -1)
                    MessageBox.Show("Chưa chọn tên học liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string id = guna2DataGridView2.CurrentRow.Cells["matienchihoclieu"].Value.ToString();
                    string ten = txt_ten2.Text;
                    string soluong = txt_soluong2.Text;
                    string tien = txt_tien2.Text;
                    string maphieuchi = idPC;
                    int mahl = cbb_tenhl.SelectedIndex + 1;
                    Controller.KhoanChiController.updateTienChiHocLieu(id, ten, soluong, tien, maphieuchi, mahl);
                }
                ViewLoad2();
            }
        }

        private void btn_xoa2_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView2.CurrentRow.Cells["matienchihoclieu"].Value.ToString();
            Controller.KhoanChiController.deleteTienChi(id);
            ViewLoad2();
        }

        private void btn_huythem2_Click(object sender, EventArgs e)
        {
            ViewLoad2();
        }

        private void btn_them3_Click(object sender, EventArgs e)
        {
            if (!addMode3)
            {
                addMode3 = true;
                Enable3();
                txt_soluong3.Clear();
                txt_ten3.Clear();
                txt_tien3.Clear();
                btn_huythem3.Visible = true;
                btn_xoa3.Enabled = false;
                btn_sua3.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_soluong3.Text))
                    MessageBox.Show("Chưa nhập số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_ten3.Text))
                    MessageBox.Show("Chưa nhập tên khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_tien3.Text))
                    MessageBox.Show("Chưa nhập thành tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string ten = txt_ten3.Text;
                    string soluong = txt_soluong3.Text;
                    string tien = txt_tien3.Text;
                    string maphieuchi = idPC;
                    Controller.KhoanChiController.addTienChiKhac(ten, soluong, tien, maphieuchi);
                }
                ViewLoad3();
            }
        }

        private void btn_sua3_Click(object sender, EventArgs e)
        {
            if (!editMode3)
            {
                editMode3 = true;
                Enable3();
                btn_them3.Enabled = false;
                btn_xoa3.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_soluong3.Text))
                    MessageBox.Show("Chưa nhập số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_ten3.Text))
                    MessageBox.Show("Chưa nhập tên khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (string.IsNullOrWhiteSpace(txt_tien3.Text))
                    MessageBox.Show("Chưa nhập thành tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string id = guna2DataGridView3.CurrentRow.Cells["tienkhoanchikhac"].Value.ToString();
                    string ten = txt_ten3.Text;
                    string soluong = txt_soluong3.Text;
                    string tien = txt_tien3.Text;
                    string maphieuchi = idPC;
                    Controller.KhoanChiController.updateTienChiKhac(id, ten, soluong, tien, maphieuchi);
                }
                ViewLoad3();
            }
        }

        private void btn_xoa3_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView3.CurrentRow.Cells["tienkhoanchikhac"].Value.ToString();
            Controller.KhoanChiController.deleteTienChi(id);
            ViewLoad3();
        }

        private void btn_huythem3_Click(object sender, EventArgs e)
        {
            ViewLoad3();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        public void ViewLoad1()
        {
            guna2DataGridView1.DataSource = Controller.KhoanChiController.fetchTienChiNguyenLieu(idPC);

            getInfo1();
        }

        public void ViewLoad2()
        {
            guna2DataGridView2.DataSource = Controller.KhoanChiController.fetchTienChiHocLieu(idPC);
            getInfo2();
        }

        public void ViewLoad3()
        {
            guna2DataGridView3.DataSource = Controller.KhoanChiController.fetchTienChiKhac(idPC);

            getInfo3();
        }

        private void FormKhoanChi_Load(object sender, EventArgs e)
        {
            ViewLoad1();
            ViewLoad2();
            ViewLoad3();
            Disable1();
            Disable2();
            Disable3();

            cbb_tenhl.Items.Clear();
            foreach (DataRow row in Controller.Kho.fetchHocLieu().Rows)
            {
                cbb_tenhl.Items.Add(row["tenhoclieu"].ToString());
            }

            cbb_tennl.Items.Clear();
            foreach (DataRow row in Controller.Kho.fetchNguyenLieu().Rows)
            {
                cbb_tennl.Items.Add(row["tennguyenlieu"].ToString());
            }
        }
    }
}
