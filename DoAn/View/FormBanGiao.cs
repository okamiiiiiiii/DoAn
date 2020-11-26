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
    public partial class FormBanGiao: Form
    {
        private string idBG;
        public FormBanGiao()
        {
            InitializeComponent();
        }

        public FormBanGiao(string id): this()
        {
            idBG = id;
        }

        private void getInfo1()
        {
            comboBox1.Items.Clear();
            foreach(DataRow data in Controller.Kho.fetchNguyenLieu().Rows)
            {
                comboBox1.Items.Add(data["tennguyenlieu"].ToString());
            }

            comboBox2.Items.Clear();
            foreach (DataRow data in Controller.Kho.fetchHocLieu().Rows)
            {
                comboBox1.Items.Add(data["tennguyenlieu"].ToString());
            }
        }

        private void ViewLoad()
        {
            comboBox1.Items.Clear();
            foreach (DataRow data in Controller.Kho.fetchNguyenLieu().Rows)
            {
                comboBox1.Items.Add(data["tennguyenlieu"].ToString());
            }

            comboBox2.Items.Clear();
            foreach (DataRow data in Controller.Kho.fetchHocLieu().Rows)
            {
                comboBox2.Items.Add(data["tenhoclieu"].ToString());
            }
            guna2DataGridView1.DataSource = Controller.BGController.indexNL(idBG);
            guna2DataGridView2.DataSource = Controller.BGController.indexHL(idBG);
        }

        private void FormBanGiao_Load(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow data = Controller.Kho.nguyenlieu((comboBox1.SelectedIndex + 1).ToString());
            label2.Text = "Số lượng (còn tồn kho " + data["soluongnguyenlieu"] +"):";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow data = Controller.Kho.hoclieu((comboBox2.SelectedIndex + 1).ToString());
            label3.Text = "Số lượng (còn tồn kho " + data["soluonghoclieu"].ToString() + "):";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataRow data = Controller.Kho.nguyenlieu((comboBox1.SelectedIndex + 1).ToString());
            if (int.Parse(textBox1.Text) > int.Parse(data["soluongnguyenlieu"].ToString()))
                textBox1.Text = data["soluongnguyenlieu"].ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataRow data = Controller.Kho.hoclieu((comboBox2.SelectedIndex + 1).ToString());
            if (int.Parse(textBox2.Text) > int.Parse(data["soluonghoclieu"].ToString()))
                textBox2.Text = data["soluonghoclieu"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Chưa nhập số lượng bàn giao", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn nguyên liệu bàn giao", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string soluong = textBox1.Text;
                int maNL = comboBox1.SelectedIndex + 1;
                Controller.BGController.addBGNL(soluong, maNL, idBG);
            }
            ViewLoad();
            DataRow data = Controller.Kho.nguyenlieu((comboBox1.SelectedIndex + 2).ToString());
            label2.Text = "Số lượng (còn tồn kho " + data["soluongnguyenlieu"] + "):";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Chưa nhập số lượng bàn giao", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn nguyên liệu bàn giao", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string soluong = textBox2.Text;
                int maHL = comboBox2.SelectedIndex + 1;
                Controller.BGController.addBGHL(soluong, maHL, idBG);
            }
            ViewLoad();
            DataRow data = Controller.Kho.hoclieu((comboBox2.SelectedIndex + 2).ToString());
            label3.Text = "Số lượng (còn tồn kho " + data["soluonghoclieu"].ToString() + "):";
        }
    }
}
