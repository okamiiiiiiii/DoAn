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
    }
}
