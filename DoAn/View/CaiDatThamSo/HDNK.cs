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
    public partial class HDNK : Form
    {
        private bool addMode = false;
        private bool editMode = false;
        public HDNK()
        {
            InitializeComponent();
        }

        private void Disable()
        {
            txt_GhiChu.Enabled = false;
            txt_ten.Enabled = false;
            txt_tien.Enabled = false;
            dateTimePicker1.Enabled = false;
        }

        private void Enable()
        {
            txt_GhiChu.Enabled = true;
            txt_ten.Enabled = true;
            txt_tien.Enabled = true;
            dateTimePicker1.Enabled = true;
        }

        public void getInfo()
        {
            string id = guna2DataGridView1.CurrentRow.Cells["mahdnk"].Value.ToString();
            DataRow data = Controller.HDNKController.findOneById(id);
            txt_GhiChu.Text = data["ghichu"].ToString();
            txt_ten.Text = data["tenhdnk"].ToString();
            txt_tien.Text = data["chiphidukien"].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(data["tgthuchien"].ToString());

            Disable();

            if (addMode) addMode = false;
            if (editMode) editMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;

            btn_Them.Enabled = true;
            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
        }

        public void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.HDNKController.index();
            getInfo();
            //dateTimePicker1.MinDate = DateTime.Now;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void HDNK_Load(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if(!addMode)
            {
                addMode = true;
                btn_HuyThem.Visible = true;
                btn_Sua.Enabled = false;
                btn_Xoa.Enabled = false;
                Enable();
                txt_GhiChu.Clear();
                txt_ten.Clear();
                txt_tien.Clear();
                dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên hoạt động ngoại khóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_tien.Text))
                {
                    MessageBox.Show("Chưa nhập chi phí dự kiến", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = txt_ten.Text;
                    string tien = txt_tien.Text;
                    string ghichu = txt_GhiChu.Text;
                    string tg = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    Controller.HDNKController.addHDNK(ten, tien, ghichu, tg);
                }
                ViewLoad();
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if(!editMode)
            {
                editMode = true;
                Enable();
                btn_Them.Enabled = false;
                btn_Xoa.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên hoạt động ngoại khóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_tien.Text))
                {
                    MessageBox.Show("Chưa nhập chi phí dự kiến", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string id = guna2DataGridView1.CurrentRow.Cells["mahdnk"].Value.ToString();
                    string ten = txt_ten.Text;
                    string tien = txt_tien.Text;
                    string ghichu = txt_GhiChu.Text;
                    string tg = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    Controller.HDNKController.updateHDNK(id, ten, tien, ghichu, tg);
                }
                ViewLoad();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["mahdnk"].Value.ToString();
            Controller.HDNKController.deleteHDNK(id);
            ViewLoad();
        }
    }
}
