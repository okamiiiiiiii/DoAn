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
    public partial class FormQuanLyNhanSu : Form
    {
        public FormQuanLyNhanSu()
        {
            InitializeComponent();
        }

        private void getInfo()
        {
            string id = guna2DataGridView1.CurrentRow.Cells["manv"].Value.ToString();
            DataRow data = Controller.NhanSuController.findByID(id);
            txt_ten.Text = data["tennv"].ToString();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDown;

            comboBox1.Text = data["gioitinh"].ToString();
            comboBox2.Text = data["tenchucvu"].ToString();

            dateTimePicker1.Value = Convert.ToDateTime(data["namsinh"].ToString());

            textBox1.Text = data["diachi"].ToString();
            textBox2.Text = data["hesoluong"].ToString();
            textBox3.Text = data["dienthoai"].ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void FormQuanLyNhanSu_Load(object sender, EventArgs e)
        {
            guna2DataGridView2.DataSource = Controller.DuLieuController.index();
        }
    }
}
