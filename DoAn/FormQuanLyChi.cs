using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class FormQuanLyChi : Form
    {
        public FormQuanLyChi()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Form TKC = new FormThemKhoanChi();
            //TKC.Show();
        }

        private void FormQuanLyChi_Load(object sender, EventArgs e)
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
            groupBox1.ForeColor = ThemeColor.PrimaryColor;

            guna2DataGridView1.DataSource = DataController.ExecTable("SELECT tentienchi, thanhtien from tttienchi");
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = guna2DataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = guna2DataGridView1.CurrentRow.Cells[3].Value.ToString();

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;

            bt_Sua.Enabled = true;
            bt_Xoa.Enabled = true;
        }
    }
}
