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
    public partial class FormBaoCaoThongKeKho : Form
    {
        public FormBaoCaoThongKeKho()
        {
            InitializeComponent();
        }

        private void FormBaoCaoThongKeKho_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.DataSource = DataController.ExecTable("select * from nguyenlieu");
            guna2DataGridView2.DataSource = DataController.ExecTable("select * from hoclieu");
        }
    }
}
