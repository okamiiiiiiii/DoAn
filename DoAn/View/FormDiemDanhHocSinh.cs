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
            if (!ProgramStatusController.getThoiHanDiemDanh()) button1.Enabled = false;
            guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = false;
            button1.ForeColor = Color.White;
            button1.BackColor = ThemeColor.PrimaryColor;
            groupBox1.ForeColor = ThemeColor.PrimaryColor;
            groupBox2.ForeColor = ThemeColor.PrimaryColor;
            label1.ForeColor = ThemeColor.PrimaryColor;
            foreach(DataRow row in Controller.HocSinhController.fetchKhoi().Rows)
            {
                listBox1.Items.Add(row["tenkhoi"].ToString());
            }
            listBox1.SelectedIndex = 0;
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void guna2DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if(e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
            {
                object value = guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if(!((DataGridViewComboBoxColumn)guna2DataGridView1.Columns[e.ColumnIndex]).Items.Contains(value))
                {
                    ((DataGridViewComboBoxColumn)guna2DataGridView1.Columns[e.ColumnIndex]).Items.Add(value);
                    e.ThrowException = false;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenKhoiLop = listBox1.SelectedItem.ToString();
            string day = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string query = "select theodoixuatan.mahocsinh, " +
                "hocsinh.tenhocsinh, " +
                "hinhthucan.tenhinhthucan " +
                "from hocsinh, theodoixuatan, hinhthucan, khoi " +
                "where theodoixuatan.mahocsinh = hocsinh.mahocsinh " +
                "and theodoixuatan.mahinhthucan = hinhthucan.mahinhthucan " +
                "and theodoixuatan.tg = '" + day + "' " +
                "and hocsinh.makhoi = khoi.makhoi " +
                "and khoi.tenkhoi = N'" + tenKhoiLop +"'";
            guna2DataGridView1.DataSource = DataController.ExecTable(query);

            if (dateTimePicker1.Value.ToString("yyyy-MM-dd") != DateTime.Today.ToString("yyyy-MM-dd"))
            {
                guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = false;
                button1.Enabled = false;
            }
            else if (!ProgramStatusController.LopDaDiemDanh.Contains(tenKhoiLop))
            {
                guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = true;
                button1.Enabled = true;
            }
            else
            {
                guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = false;
                button1.Enabled = false;
            }
        }

        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(guna2DataGridView1.Columns[e.ColumnIndex].Name == "diemdanhvean")
            {
                string tenhinhthucan = guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string query1 = "select mahinhthucan from hinhthucan where tenhinhthucan = N'" + tenhinhthucan + "'";
                string mahinhthucan = DataController.ExecTable(query1).Rows[0]["mahinhthucan"].ToString();

                DataGridViewRow dataRow = guna2DataGridView1.Rows[e.RowIndex];
                string day = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string mahocsinh = dataRow.Cells["mahocsinh"].Value.ToString();
                string query2 = "select maxuatan from theodoixuatan " +
                    "WHERE mahocsinh = " + mahocsinh + " " +
                    "and tg = '" + day + "' ";
                string maxuatan = DataController.ExecTable(query2).Rows[0]["maxuatan"].ToString();
                string query3 = "UPDATE theodoixuatan " +
                    "SET mahinhthucan = " + mahinhthucan + " "  +
                    "WHERE maxuatan = " + maxuatan;
                DataController.Execute(query3);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string day = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string tenKhoiLop = listBox1.SelectedItem.ToString();
            if (dateTimePicker1.Value.ToString("yyyy-MM-dd") != DateTime.Today.ToString("yyyy-MM-dd"))
            {
                Console.WriteLine("if 1");
                guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = false;
                button1.Enabled = false;
            }
            else if (ProgramStatusController.LopDaDiemDanh.Contains(tenKhoiLop))
            {
                Console.WriteLine("if 2");
                guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = false;
                button1.Enabled = false;
            }
            else if(!ProgramStatusController.getThoiHanDiemDanh())
            {
                Console.WriteLine("if 3");
                guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = false;
                button1.Enabled = false;
            }
            else
            {
                guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = true;
                button1.Enabled = true;
            }
            string query = "select theodoixuatan.mahocsinh, " +
                "hocsinh.tenhocsinh, " +
                "hinhthucan.tenhinhthucan " +
                "from hocsinh, theodoixuatan, hinhthucan, khoi " +
                "where theodoixuatan.mahocsinh = hocsinh.mahocsinh " +
                "and theodoixuatan.mahinhthucan = hinhthucan.mahinhthucan " +
                "and theodoixuatan.tg = '" + day + "' " +
                "and hocsinh.makhoi = khoi.makhoi " +
                "and khoi.tenkhoi = N'" + tenKhoiLop + "'";

            guna2DataGridView1.DataSource = DataController.ExecTable(query);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = false;
            button1.Enabled = false;
            ProgramStatusController.LopDaDiemDanh.Add(listBox1.SelectedItem.ToString());
        }

        private void button1_EnabledChanged(object sender, EventArgs e)
        {
            if (button1.Enabled == true) label2.Text = "";
            else label2.Text = "* Kết quả hiện tại đã được lưu";
        }
    }
}
