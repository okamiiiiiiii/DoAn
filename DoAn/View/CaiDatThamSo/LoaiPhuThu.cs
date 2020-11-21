using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.CaiDatThamSo
{
    
    public partial class LoaiPhuThu : Form
    {
        private bool editMode = false;
        private bool addMode = false;
        public LoaiPhuThu()
        {
            InitializeComponent();
        }

        private void getInfo()
        {
            if (addMode) addMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;
            txt_ten.Enabled = false;

            string id = guna2DataGridView1.CurrentRow.Cells["maloai"].Value.ToString();
            DataRow data = Controller.LoaiPhuThuController.findOneById(id);
            txt_ten.Text = data["tenloaiphuthu"].ToString();
            if (addMode) addMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;

            btn_Them.Enabled = true;
            btn_Xoa.Enabled = true;
        }

        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.LoaiPhuThuController.index();
            getInfo();
        }

        private void LoaiPhuThu_Load(object sender, EventArgs e)
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
            ViewLoad();

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_ten.Enabled = false;
            txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xoá", "Xoá loại phụ thu", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string id = guna2DataGridView1.CurrentRow.Cells["maloai"].Value.ToString();
                Controller.LoaiPhuThuController.deleteLoaiPhuThu(id);
            }
            ViewLoad();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if(!addMode)
            {
                addMode = true;
                btn_HuyThem.Visible = true;
                txt_ten.Enabled = true;
                txt_ten.Clear();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa tên loại phụ thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Controller.LoaiPhuThuController.addLoaiPhuThu(txt_ten.Text);
                }
                ViewLoad();
            }
        }
    }
}
