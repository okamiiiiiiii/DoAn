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
    public partial class PhuThuKeHoach : Form
    {
        private bool addMode = false;
        private bool editMode = false;

        public PhuThuKeHoach()
        {
            InitializeComponent();
        }

        private void Enable()
        {
            textBox1.Enabled = true;
            textBox3.Enabled = true;
            comboBox1.Enabled = true;
        }

        private void Disable()
        {
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void getInfo()
        {
            comboBox1.Items.Clear();
            foreach (DataRow row in Controller.HocSinhController.fetchKhoi().Rows)
            {
                comboBox1.Items.Add(row["tenkhoi"].ToString());
            }
            if(guna2DataGridView1.Rows.Count != 0)
            {
                string id = guna2DataGridView1.CurrentRow.Cells["maphuthucokehoach"].Value.ToString();
                DataRow data = Controller.PTCKHController.findbyID(id);
                textBox1.Text = data["tenphuthucokehoach"].ToString();
                textBox3.Text = data["tienmoihocsinh"].ToString();
                comboBox1.Text = data["tenkhoi"].ToString();
            }

            textBox1.Enabled = false;
            textBox3.Enabled = false;
            comboBox1.Enabled = false;

            if (addMode) addMode = false;
            if (editMode) editMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;

            btn_Them.Enabled = true;
            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
        }

        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.PTCKHController.index();
            getInfo();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PhuThuKeHoach_Load(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (!addMode)
            {
                Enable();
                addMode = true;
                comboBox1.SelectedIndex = -1;
                textBox1.Clear();
                textBox3.Clear();

                btn_HuyThem.Visible = true;
                btn_Sua.Enabled = false;
                btn_Xoa.Enabled = false;
            }
            else
            {
                if(string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Chưa nhập tên phụ thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Chưa nhập số tiền thu của mỗi học sinh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn khối áp dụng phụ thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = textBox1.Text;
                    int makhoi = comboBox1.SelectedIndex + 1;
                    string tien = textBox3.Text;
                    Controller.PTCKHController.addPTCKH(ten, makhoi, tien);
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
                Enable();
                editMode = true;

                btn_Them.Enabled = false;
                btn_Xoa.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Chưa nhập tên phụ thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Chưa nhập số tiền thu của mỗi học sinh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn khối áp dụng phụ thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string id = guna2DataGridView1.CurrentRow.Cells["maphuthucokehoach"].Value.ToString();
                    string ten = textBox1.Text;
                    int makhoi = comboBox1.SelectedIndex + 1;
                    string tien = textBox3.Text;
                    Controller.PTCKHController.updatePTCKH(id, ten, makhoi, tien);
                }
                ViewLoad();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["maphuthucokehoach"].Value.ToString();
            Controller.PTCKHController.deletePTCKH(id);
            ViewLoad();
        }
    }
}
