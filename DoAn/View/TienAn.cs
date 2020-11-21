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
    public partial class TienAn : Form
    {
        public TienAn()
        {
            InitializeComponent();
        }

        private void TienAn_Load(object sender, EventArgs e)
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
            for (int i = 1; i <= 12; i++)
            {
                cbb_Thang.Items.Add(i.ToString());
            }

            for (int i = 2000; i <= 2020; i++)
            {
                cbb_Nam.Items.Add(i.ToString());
            }
            cbb_Nam.Text = DateTime.Now.Year.ToString();
            cbb_Thang.Text = DateTime.Now.Month.ToString();
            

            DataTable dtKhoiLop = DataController.ExecTable("select tenkhoi from khoi");
            foreach (DataRow row in dtKhoiLop.Rows)
            {
                listBox1.Items.Add(row["tenkhoi"].ToString());
            }
            listBox1.SelectedIndex = 0;

            

            Console.WriteLine(cbb_Nam.SelectedIndex);
            Console.WriteLine(cbb_Thang.SelectedIndex);
            string str = "select" +
                   " hoadonthang.mahoadonthang," +
                   " hocsinh.mahocsinh," +
                   " hocsinh.tenhocsinh," +
                   " hoadonthang.tonghocphi," +
                   " baocao.thang," +
                   " baocao.nam," +
                   " trangthai.tentrangthai" +
                   " from hoadonthang inner join hocsinh " +
                   "on hocsinh.mahocsinh = hoadonthang.mahocsinh " +
                   "inner join baocao " +
                   "on baocao.mabctt = hoadonthang.mabctt " +
                   "inner join trangthai " +
                   "on hoadonthang.matrangthai = trangthai.matrangthai" +
                   " where hocsinh.makhoi = " + (listBox1.SelectedIndex + 1).ToString() +
                   " and baocao.thang = " + cbb_Thang.Text +
                   " and baocao.nam = " + cbb_Nam.Text;
            guna2DataGridView1.DataSource = DataController.ExecTable(str);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "select" +
                   " hoadonthang.mahoadonthang," +
                   " hocsinh.mahocsinh," +
                   " hocsinh.tenhocsinh," +
                   " hoadonthang.tonghocphi," +
                   " baocao.thang," +
                   " baocao.nam," +
                   " trangthai.tentrangthai" +
                   " from hoadonthang inner join hocsinh " +
                   "on hocsinh.mahocsinh = hoadonthang.mahocsinh " +
                   "inner join baocao " +
                   "on baocao.mabctt = hoadonthang.mabctt " +
                   "inner join trangthai " +
                   "on hoadonthang.matrangthai = trangthai.matrangthai" +
                   " where hocsinh.makhoi = " + (listBox1.SelectedIndex + 1).ToString() +
                   " and baocao.thang = " + cbb_Thang.Text +
                   " and baocao.nam = " + cbb_Nam.Text;

            Console.WriteLine(str);
            guna2DataGridView1.DataSource = DataController.ExecTable(str);
        }

        private void cbb_Thang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "select" +
                   " hoadonthang.mahoadonthang," +
                   " hocsinh.mahocsinh," +
                   " hocsinh.tenhocsinh," +
                   " hoadonthang.tonghocphi," +
                   " baocao.thang," +
                   " baocao.nam," +
                   " trangthai.tentrangthai" +
                   " from hoadonthang inner join hocsinh " +
                   "on hocsinh.mahocsinh = hoadonthang.mahocsinh " +
                   "inner join baocao " +
                   "on baocao.mabctt = hoadonthang.mabctt " +
                   "inner join trangthai " +
                   "on hoadonthang.matrangthai = trangthai.matrangthai" +
                   " where hocsinh.makhoi = " + (listBox1.SelectedIndex + 1).ToString() +
                   " and baocao.thang = " + cbb_Thang.Text +
                   " and baocao.nam = " + cbb_Nam.Text;
            guna2DataGridView1.DataSource = DataController.ExecTable(str);
        }

        private void cbb_Nam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_Thang.SelectedIndex != -1)
            {
                string str = "select" +
                       " hoadonthang.mahoadonthang," +
                       " hocsinh.mahocsinh," +
                       " hocsinh.tenhocsinh," +
                       " hoadonthang.tonghocphi," +
                       " baocao.thang," +
                       " baocao.nam," +
                       " trangthai.tentrangthai" +
                       " from hoadonthang inner join hocsinh " +
                       "on hocsinh.mahocsinh = hoadonthang.mahocsinh " +
                       "inner join baocao " +
                       "on baocao.mabctt = hoadonthang.mabctt " +
                       "inner join trangthai " +
                       "on hoadonthang.matrangthai = trangthai.matrangthai" +
                       " where hocsinh.makhoi = " + (listBox1.SelectedIndex + 1).ToString() +
                       " and baocao.thang = " + cbb_Thang.Text +
                       " and baocao.nam = " + cbb_Nam.Text;
                guna2DataGridView1.DataSource = DataController.ExecTable(str);
            }
        }
    }
}
