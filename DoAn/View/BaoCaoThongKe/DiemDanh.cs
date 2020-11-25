using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.View.BaoCaoThongKe
{
    public partial class DiemDanh : Form
    {
        public DiemDanh()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenKhoiLop = listBox1.SelectedItem.ToString();
            string day = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            guna2DataGridView1.DataSource = Controller.DiemDanhHocSinh.fetch(tenKhoiLop, day);
        }

        private void DiemDanh_Load(object sender, EventArgs e)
        {
            foreach (DataRow row in Controller.HocSinhController.fetchKhoi().Rows)
            {
                listBox1.Items.Add(row["tenkhoi"].ToString());
            }
            listBox1.SelectedIndex = 0;
            foreach (DataRow row in Controller.HinhThucAnController.index().Rows)
            {
                ((DataGridViewComboBoxColumn)guna2DataGridView1.Columns["diemdanhvean"]).Items.Add(row["tenhinhthucan"].ToString());
            }
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string tenKhoiLop = listBox1.SelectedItem.ToString();
            string day = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            guna2DataGridView1.DataSource = Controller.DiemDanhHocSinh.fetch(tenKhoiLop, day);
        }
    }
}
