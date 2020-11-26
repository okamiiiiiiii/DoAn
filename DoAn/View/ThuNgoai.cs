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
    public partial class ThuNgoai : Form
    {
        private bool addMode = false, editMode = false;
        public ThuNgoai()
        {
            InitializeComponent();
        }

        private void Enable()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

        private void Disable()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }

        private void getInfo()
        {
            if(guna2DataGridView1.Rows.Count != 0)
            {
                string id = guna2DataGridView1.CurrentRow.Cells["mathungoai"].Value.ToString();
                DataRow data = Controller.ThuNgoaiController.findById(id);
                textBox1.Text = data["tenthungoai"].ToString();
                textBox2.Text = data["sotienthu"].ToString();
            }
            Disable();

            if (addMode) addMode = false;
            if (editMode) editMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;

            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
            btn_Them.Enabled = true;
        }

        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.ThuNgoaiController.index();
            getInfo();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (!addMode)
            {
                addMode = true;
                Enable();
                textBox1.Clear();
                textBox2.Clear();
                btn_HuyThem.Visible = true;
                btn_Sua.Enabled = false;
                btn_Xoa.Enabled = false;
            }
            else {
                if(string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Chưa nhập tên khoản thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Chưa nhập số tiền thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = textBox1.Text;
                    string tien = textBox2.Text;
                    Controller.ThuNgoaiController.addTN(ten, tien);
                }
                ViewLoad();
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (!editMode)
            {
                editMode = true;
                Enable();

                btn_Them.Enabled = false;
                btn_Xoa.Enabled = false;
            }
            else
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Chưa nhập tên khoản thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Chưa nhập số tiền thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string id = guna2DataGridView1.CurrentRow.Cells["mathungoai"].Value.ToString();
                    string ten = textBox1.Text;
                    string tien = textBox2.Text;
                    Controller.ThuNgoaiController.editTN(id, ten, tien);
                }
                ViewLoad();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["mathungoai"].Value.ToString();
            Controller.ThuNgoaiController.deleteTN(id);
            ViewLoad();
        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["mathungoai"].Value.ToString();
            FormWord frm = new FormWord(id, 5);
            frm.Show();
        }

        private void ThuNgoai_Load(object sender, EventArgs e)
        {
            ViewLoad();
        }
    }
}
