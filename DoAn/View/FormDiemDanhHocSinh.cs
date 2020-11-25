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

        private void Disable()
        {
            guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = true;
            guna2DataGridView1.Columns["vangmat"].ReadOnly = true;
        }

        private void Enable()
        {
            guna2DataGridView1.Columns["diemdanhvean"].ReadOnly = false;
            guna2DataGridView1.Columns["vangmat"].ReadOnly = false;
        }

        private void ViewLoad()
        {
            string tenKhoiLop = listBox1.SelectedItem.ToString();
            string day = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            guna2DataGridView1.DataSource = Controller.DiemDanhHocSinh.fetch(tenKhoiLop, day);
        }

        private void FormQuanLyVeAn_Load(object sender, EventArgs e)
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
            timer1.Start();
            Enable();
            
            groupBox1.ForeColor = ThemeColor.PrimaryColor;
            groupBox2.ForeColor = ThemeColor.PrimaryColor;
            label1.ForeColor = ThemeColor.PrimaryColor;
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
            string day = DateTime.Today.ToString("yyyy-MM-dd");
            guna2DataGridView1.DataSource =  Controller.DiemDanhHocSinh.fetch(tenKhoiLop, day);
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
                ViewLoad();
                guna2DataGridView1.Rows[e.RowIndex].Selected = true;
            }

            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "vangmat" && guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "True")
            {
                Console.WriteLine(guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                DataGridViewRow dataRow = guna2DataGridView1.Rows[e.RowIndex];
                string mahocsinh = dataRow.Cells["mahocsinh"].Value.ToString();
                Controller.DiemDanhHocSinh.DiemDanhVang(mahocsinh);
                ViewLoad();
                guna2DataGridView1.Rows[e.RowIndex].Selected = true;
            }
            else if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "vangmat" && guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "False")
            {
                DataGridViewRow dataRow = guna2DataGridView1.Rows[e.RowIndex];
                string mahocsinh = dataRow.Cells["mahocsinh"].Value.ToString();
                Controller.DiemDanhHocSinh.DiemDanhCoMat(mahocsinh);
                ViewLoad();
                guna2DataGridView1.Rows[e.RowIndex].Selected = true;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int hour = DateTime.Now.Hour;
            Console.WriteLine(hour);
            if (hour >= 6 && hour <= 9)
            {
                label4.Text = "Đang trong thời gian điểm danh";
            }
            else
            {
                Disable();
                label4.Text = "Đã hết thời gian điểm danh";
                timer1.Stop();
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}
