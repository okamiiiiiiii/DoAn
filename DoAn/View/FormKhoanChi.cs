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
    public partial class FormKhoanChi : Form
    {
        private int id;
        
        public FormKhoanChi()
        {
            InitializeComponent();
        }

        public FormKhoanChi(string idPC) : this()
        {
            id = int.Parse(idPC);
        }

        public void getInfo()
        {

        }

        public void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.KhoanChiController.fetchTienChiNguyenLieu();
        }

        private void FormKhoanChi_Load(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
