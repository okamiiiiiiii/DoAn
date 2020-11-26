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
    public partial class FormPhieuBanGiao : Form
    {
        private bool addMode = false;
        private bool editMode = false;
        public FormPhieuBanGiao()
        {
            InitializeComponent();
        }

        private void Disable()
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void Enable()
        {
            textBox1.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void getInfo()
        {
            comboBox1.Items.Clear();
            foreach(DataRow data in Controller.HocSinhController.fetchKhoi().Rows)
            {
                comboBox1.Items.Add(data["tenkhoi"].ToString());
            }
            if(guna2DataGridView1.Rows.Count !=0)
            {
                string id = guna2DataGridView1.CurrentRow.Cells["maphieubangiao"].Value.ToString();
                DataRow data = Controller.PhieuBanGiaoController.findById(id);
                textBox1.Text = data["tenphieubangiao"].ToString();
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox1.Text = data["tenkhoi"].ToString();
            }
            Disable();

            btn_detail.Enabled = true;
            btn_Sua.Enabled = true;
            btn_Them.Enabled = true;
            btn_Xoa.Enabled = true;

            if (addMode) addMode = false;
            if (editMode) editMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;
        }

        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.PhieuBanGiaoController.index();
            getInfo();
        }


        private void FormPhieuBanGiao_Load(object sender, EventArgs e)
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
                btn_detail.Enabled = false;
                btn_HuyThem.Visible = true;
                btn_Sua.Enabled = false;
                btn_Xoa.Enabled = false;
                button1.Enabled = false;
                addMode = true;
                Enable();
                textBox1.Clear();
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.SelectedIndex = -1;
            }
            else
            {
                if(string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Chưa nhập tên phiếu bàn giao", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn khối để bàn giao", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = textBox1.Text;
                    int makhoi = comboBox1.SelectedIndex + 1;
                    Controller.PhieuBanGiaoController.addPBG(ten, makhoi);
                }
                ViewLoad();
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (!editMode)
            {
                btn_detail.Enabled = false;
                btn_Them.Enabled = false;
                btn_Xoa.Enabled = false;
                editMode = true;
                Enable();
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Chưa nhập tên phiếu bàn giao", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn khối để bàn giao", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string id = guna2DataGridView1.CurrentRow.Cells["maphieubangiao"].Value.ToString();
                    string ten = textBox1.Text;
                    int makhoi = comboBox1.SelectedIndex + 1;
                    Controller.PhieuBanGiaoController.updatePBG(id, ten, makhoi);
                }
                ViewLoad();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["maphieubangiao"].Value.ToString();
            Controller.PhieuBanGiaoController.deletePBG(id);
            ViewLoad();
        }

        private void btn_detail_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["maphieubangiao"].Value.ToString();
            View.FormBanGiao f = new View.FormBanGiao(id);
            f.Show();
        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["maphieubangiao"].Value.ToString();
            FormWord frm = new FormWord(id, 3);
            frm.Show();
        }
    }
}
