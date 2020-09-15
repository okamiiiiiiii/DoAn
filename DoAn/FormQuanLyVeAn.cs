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
    public partial class FormQuanLyVeAn : Form
    {
        public FormQuanLyVeAn()
        {
            InitializeComponent();
        }

        private void FormQuanLyVeAn_Load(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
            button1.BackColor = ThemeColor.PrimaryColor;
            groupBox1.ForeColor = ThemeColor.PrimaryColor;
            groupBox2.ForeColor = ThemeColor.PrimaryColor;
            label1.ForeColor = ThemeColor.PrimaryColor;

            guna2DataGridView1.DataSource = DataController.ExecTable("select mahocsinh, tenhocsinh, gioitinh from hocsinh");

        }
    }
}
