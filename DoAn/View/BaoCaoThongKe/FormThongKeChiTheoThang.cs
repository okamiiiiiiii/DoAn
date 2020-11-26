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

        private void clearDataGridView()
        {
            guna2DataGridView1.DataSource = DataController.ExecTable("select mabctt, sotienchi, sotienthu, thang from baocao where nam = " + comboBox1.Text);
            DataTable dt = DataController.ExecTable("select SUM(sotienchi) as tongchi, SUM(sotienthu) as tongthu from baocao where nam =" + comboBox1.Text);
            label3.Text = int.Parse(dt.Rows[0]["tongchi"].ToString()).ToString("C");
            label4.Text = int.Parse(dt.Rows[0]["tongthu"].ToString()).ToString("C");
            label6.Text = (int.Parse(dt.Rows[0]["tongthu"].ToString()) - int.Parse(dt.Rows[0]["tongchi"].ToString())).ToString("C");
        }

        private void FormThongKeChiTheoThang_Load(object sender, EventArgs e)
        {
            DataTable namDT = DataController.ExecTable("select DISTINCT nam from baocao");
            foreach(DataRow data in namDT.Rows)
            {
                comboBox1.Items.Add(data["nam"].ToString());
            }

            comboBox1.Text = DateTime.Now.Year.ToString();
            guna2DataGridView1.DataSource = DataController.ExecTable("select mabctt, sotienchi, sotienthu, thang from baocao where nam = " + comboBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = guna2DataGridView1.CurrentRow.Cells["td"].Value.ToString();
            FormWord frm = new FormWord(id, 4);
            frm.Show();
        }
    }
}
