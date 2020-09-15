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
    public partial class FormThongKeChiTheoThang : Form
    {
        public FormThongKeChiTheoThang()
        {
            InitializeComponent();
        }

        private void FormThongKeChiTheoThang_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.DataSource = DataController.ExecTable("select tennv, tentienchi, thanhtien, thoigian from tttienchi inner join giaovien on tttienchi.magv = giaovien.magv inner join nhanvien on giaovien.manv = nhanvien.manv");
        }
    }
}
