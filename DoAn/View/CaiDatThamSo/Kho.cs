using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.View.CaiDatThamSo
{


    public partial class Kho : Form
    {
        private bool addMode1 = false;
        private bool editMode1 = false;
        private bool addMode2 = false;
        private bool editMode2 = false;
        public Kho()
        {
            InitializeComponent();
        }

        public void Enable1()
        {
            txt_ghichu1.Enabled = true;
            txt_soluong1.Enabled = true;
            txt_ten1.Enabled = true;
        }

        public void Enable2()
        {
            txt_GhiChu2.Enabled = true;
            txt_SoLuong2.Enabled = true;
            txt_ten2.Enabled = true;
        }

        public void Disable1()
        {
            txt_ghichu1.Enabled = false;
            txt_soluong1.Enabled = false;
            txt_ten1.Enabled = false;
        }

        public void Disable2()
        {
            txt_GhiChu2.Enabled = false;
            txt_SoLuong2.Enabled = false;
            txt_ten2.Enabled = false;
        }

        private void getInfo1()
        {
            string id = guna2DataGridView1.CurrentRow.Cells["manguyenlieu"].Value.ToString();
            DataRow data = Controller.Kho.nguyenlieu(id);

            txt_ten1.Text = data["tennguyenlieu"].ToString();
            txt_soluong1.Text = data["soluong"].ToString();
            txt_ghichu1.Text = data["ghichu"].ToString();

            Disable1();

            if (editMode1) editMode1 = false;
            if (addMode1) addMode1 = false;
            if (btn_HuyThem1.Visible == true) btn_HuyThem1.Visible = false;

            btn_Them1.Enabled = true;
            btn_Sua1.Enabled = true;
            btn_Xoa1.Enabled = true;
        }

        private void getInfo2()
        {
            string id = guna2DataGridView2.CurrentRow.Cells["mahoclieu"].Value.ToString();
            DataRow data = Controller.Kho.hoclieu(id);

            txt_ten2.Text = data["tenhoclieu"].ToString();
            txt_SoLuong2.Text = data["soluong"].ToString();
            txt_GhiChu2.Text = data["ghichu"].ToString();

            Disable2();

            if (editMode2) editMode2 = false;
            if (addMode2) addMode2 = false;
            if (btn_HuyThem2.Visible == true) btn_HuyThem2.Visible = false;

            btn_Them2.Enabled = true;
            btn_Sua2.Enabled = true;
            btn_Xoa2.Enabled = true;
        }

        private void viewLoad()
        {
            guna2DataGridView1.DataSource = Controller.Kho.fetchNguyenLieu();
            getInfo1();

            guna2DataGridView2.DataSource = Controller.Kho.fetchHocLieu();
            getInfo2();
        }

        private void Kho_Load(object sender, EventArgs e)
        {
            viewLoad();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_Them1_Click(object sender, EventArgs e)
        {
            if (!addMode1)
            {
                addMode1 = true;
                txt_ghichu1.Clear();
                txt_soluong1.Clear();
                txt_ten1.Clear();
                Enable1();

                btn_HuyThem1.Visible = true;
                btn_Sua1.Enabled = false;
                btn_Xoa1.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten1.Text))
                {
                    MessageBox.Show("Chưa nhập tên nguyên liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_soluong1.Text))
                {
                    MessageBox.Show("Chưa nhập phân loại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = txt_ten1.Text;
                    string soluong = txt_soluong1.Text;
                    string ghichu = txt_ghichu1.Text;

                    Controller.Kho.addNguyenLieu(ten, soluong, ghichu);
                }
                viewLoad();
            }
            
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo1();
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo2();
        }

        private void btn_Them2_Click(object sender, EventArgs e)
        {
            if (!addMode2)
            {
                addMode2 = true;
                txt_GhiChu2.Clear();
                txt_SoLuong2.Clear();
                txt_ten2.Clear();
                Enable2();

                btn_HuyThem2.Visible = true;
                btn_Sua2.Enabled = false;
                btn_Xoa2.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten1.Text))
                {
                    MessageBox.Show("Chưa nhập tên nguyên liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_soluong1.Text))
                {
                    MessageBox.Show("Chưa nhập phân loại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = txt_ten2.Text;
                    string soluong = txt_SoLuong2.Text;
                    string ghichu = txt_GhiChu2.Text;

                    Controller.Kho.addHocLieu(ten, soluong, ghichu);
                }
                viewLoad();
            }
            
        }

        private void btn_Sua1_Click(object sender, EventArgs e)
        {
            if (!editMode1)
            {
                editMode1 = true;
                Enable1();

                btn_Them1.Enabled = false;
                btn_Xoa1.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten1.Text))
                {
                    MessageBox.Show("Chưa nhập tên nguyên liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_soluong1.Text))
                {
                    MessageBox.Show("Chưa nhập phân loại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string id = guna2DataGridView1.CurrentRow.Cells["manguyenlieu"].Value.ToString();
                    string ten = txt_ten1.Text;
                    string soluong = txt_soluong1.Text;
                    string ghichu = txt_ghichu1.Text;

                    Controller.Kho.updateNguyenLieu(id, ten, soluong, ghichu);
                }
                viewLoad();
            }
        }

        private void btn_Sua2_Click(object sender, EventArgs e)
        {
            if (!editMode2)
            {
                editMode2 = true;
                Enable2();

                btn_Them2.Enabled = false;
                btn_Xoa2.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten2.Text))
                {
                    MessageBox.Show("Chưa nhập tên nguyên liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_SoLuong2.Text))
                {
                    MessageBox.Show("Chưa nhập phân loại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string id = guna2DataGridView2.CurrentRow.Cells["mahoclieu"].Value.ToString();
                    string ten = txt_ten1.Text;
                    string soluong = txt_soluong1.Text;
                    string ghichu = txt_ghichu1.Text;

                    Controller.Kho.updateHocLieu(id, ten, soluong, ghichu);
                }
                viewLoad();

            }
        }

        private void btn_Xoa1_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["manguyenlieu"].Value.ToString();
            Controller.Kho.deleteNguyenLieu(id);
            viewLoad();
        }

        private void btn_Xoa2_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView2.CurrentRow.Cells["mahoclieu"].Value.ToString();
            Controller.Kho.deleteHocLieu(id);
            viewLoad();
        }

        private void btn_HuyThem1_Click(object sender, EventArgs e)
        {
            viewLoad();
        }

        private void btn_HuyThem2_Click(object sender, EventArgs e)
        {
            viewLoad();
        }
    }
}
