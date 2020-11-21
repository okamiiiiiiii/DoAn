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
    public partial class HinhThucAn : Form
    {
        private bool addMode = false;
        private bool editMode = false;
        public HinhThucAn()
        {
            InitializeComponent();
        }

        private void DisableAllControls()
        {
            txt_Gia.Enabled = false;
            txt_ten.Enabled = false;
        }

        private void EnableAllControls()
        {
            txt_Gia.Enabled = true;
            txt_ten.Enabled = true;
        }

        private void getInfo()
        {
            string id = guna2DataGridView1.CurrentRow.Cells["mahinhthucan"].Value.ToString();
            DataRow data = Controller.HinhThucAnController.findOneById(id);
            txt_Gia.Text = data["giatien"].ToString();
            txt_ten.Text = data["tenhinhthucan"].ToString();

            txt_ten.Enabled = false;
            txt_Gia.Enabled = false;

            if (addMode) addMode = false;
            if (editMode) editMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;

            btn_Them.Enabled = true;
            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
        }

        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.HinhThucAnController.index();
            getInfo();
        }

        private void HinhThucAn_Load(object sender, EventArgs e)
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
                ViewLoad();
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if(!addMode)
            {
                txt_Gia.Clear();
                txt_ten.Clear();
                EnableAllControls();
                addMode = true;
                btn_HuyThem.Visible = true;
                btn_Sua.Enabled = false;
                btn_Xoa.Enabled = false;
            }
            else
            {
                if(string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên hình thức ăn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(string.IsNullOrWhiteSpace(txt_Gia.Text))
                {
                    MessageBox.Show("Chưa nhập giá tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = txt_ten.Text;
                    string gia = txt_Gia.Text;
                    Controller.HinhThucAnController.addHinhThucAn(ten, gia);
                }
                ViewLoad();
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if(!editMode)
            {
                EnableAllControls();
                editMode = true;
                btn_Them.Enabled = false;
                btn_Xoa.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên hình thức ăn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_Gia.Text))
                {
                    MessageBox.Show("Chưa nhập giá tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string id = guna2DataGridView1.CurrentRow.Cells["mahinhthucan"].Value.ToString();
                    string ten = txt_ten.Text;
                    string gia = txt_Gia.Text;
                    Controller.HinhThucAnController.updateHinhThucAn(id, ten, gia);
                }
                ViewLoad();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xoá", "Xoá hình thức ăn", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string id = guna2DataGridView1.CurrentRow.Cells["mahinhthucan"].Value.ToString();
                Controller.HinhThucAnController.deleteHinhThucAn(id);
            }
            ViewLoad();
        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            getInfo();
        }
    }
}
