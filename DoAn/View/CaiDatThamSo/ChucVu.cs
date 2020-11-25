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
    public partial class ChucVu1 : Form
    {
        public ChucVu1()
        {
            InitializeComponent();
        }

        private void getInfo()
        {
            string id = guna2DataGridView1.CurrentRow.Cells["machucvu"].Value.ToString();
            DataRow data = Controller.ChucVuController.findOneByID(id);
            textBox1.Text = data["tenchucvu"].ToString();

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ChucVu1_Load(object sender, EventArgs e)
        {
            label1.Text = "*Tất cả các chức năng mới sẽ thêm vào đều không " +
                            "\n dụng được chức năng gì của chương trình ngoài" +
                            "\n những chức năng ban đầu";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
