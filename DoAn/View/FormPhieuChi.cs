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
    public partial class FormPhieuChi : Form
    {
        private bool addMode = false;
        private bool editMode = false;
        public FormPhieuChi()
        {
            InitializeComponent();
        }



        private void getInfo()
        {
            comboBox1.Items.Clear();
            foreach(DataRow row in Controller.PhieuChiController.fetchTrangThai().Rows)
            {
                comboBox1.Items.Add(row["tentrangthaiphieuchi"].ToString());
            }
            string id = guna2DataGridView1.CurrentRow.Cells["maphieuchi"].Value.ToString();
            DataRow data = Controller.PhieuChiController.findByID(id);
            textBox1.Text = data["tenphieuchi"].ToString();
            comboBox1.Text = data["tentrangthaiphieuchi"].ToString();

            textBox1.Enabled = false;
            comboBox1.Enabled = false;

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;

            if (addMode) addMode = false;
            if (editMode) editMode = false;
            if (button4.Visible == true) button4.Visible = false;
        }

        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.PhieuChiController.index();
            getInfo();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!addMode)
            {
                addMode = true;
                textBox1.Enabled = true;
                comboBox1.Enabled = true;

                button2.Enabled = false;
                button3.Enabled = false;
                button4.Visible = true;
                button5.Enabled = false;
                button6.Enabled = false;

                textBox1.Clear();
                comboBox1.Text = "chưa thanh toán";
                comboBox1.Enabled = false;
            }
            else
            {
                if(string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Chưa nhập tên phiếu chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập trạng thái thanh toán", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ten = textBox1.Text;
                    int matrangthai = comboBox1.SelectedIndex + 1;
                    Controller.PhieuChiController.addPhieuChi(ten, matrangthai);
                }
                ViewLoad();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["maphieuchi"].Value.ToString();
            Controller.PhieuChiController.deletePhieuChi(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!editMode)
            {
                if(guna2DataGridView1.CurrentRow.Cells["tentrangthai"].Value.ToString() == "đã thanh toán")
                {
                    ViewLoad();
                    return;
                }
                editMode = true;
                textBox1.Enabled = true;
                comboBox1.Enabled = true;

                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Chưa nhập tên phiếu chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập trạng thái thanh toán", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string id = guna2DataGridView1.CurrentRow.Cells["maphieuchi"].Value.ToString();
                    string ten = textBox1.Text;
                    int matrangthai = comboBox1.SelectedIndex + 1;
                    Controller.PhieuChiController.updatePhieuChi(id, ten, matrangthai);
                }
                ViewLoad();
            }
        }

        private void FormPhieuChi_Load(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["maphieuchi"].Value.ToString();
            View.FormKhoanChi form = new View.FormKhoanChi(id);
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["maphieuchi"].Value.ToString();
            FormWord frm = new FormWord(id, 1);
            frm.Show();
        }
    }
}
