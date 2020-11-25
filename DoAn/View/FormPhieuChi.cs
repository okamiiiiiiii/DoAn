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
            string id = guna2DataGridView1.CurrentRow.Cells["maphieuchi"].Value.ToString();
            DataRow data = Controller.PhieuChiController.findByID(id);
            textBox1.Text = data["tenphieuchi"].ToString();
            textBox2.Text = Controller.PhieuChiController.tongtien(id).ToString();
            comboBox1.Text = data["tentrangthaiphieuchi"].ToString();

            textBox1.Enabled = false;
            textBox2.Enabled = false;
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
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

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
    }
}
