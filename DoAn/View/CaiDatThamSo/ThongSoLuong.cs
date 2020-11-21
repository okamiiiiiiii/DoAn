using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.View.CaiDatThamSo
{
    public partial class ThongSoLuong : Form
    {
        private bool init = false;
        public ThongSoLuong()
        {
            InitializeComponent();
        }
        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.DuLieuController.index();
            init = true;
        }

        private void ThongSoLuong_Load(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (init)
            {
                string id = guna2DataGridView1.CurrentRow.Cells["iddulieu"].Value.ToString();
                string ten = guna2DataGridView1.CurrentRow.Cells["tendulieu"].Value.ToString();
                string giatri = guna2DataGridView1.CurrentRow.Cells["giatri"].Value.ToString();
                Controller.DuLieuController.updateDuLieu(id, ten, giatri);
            }
            //ViewLoad();
        }
    }
}
