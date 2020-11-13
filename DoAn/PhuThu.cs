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
    public partial class PhuThu : Form
    {
        public PhuThu()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PhuThu_Load(object sender, EventArgs e)
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

            guna2DataGridView1.DataSource = DataController.ExecTable("select " +
                "hocsinh.mahocsinh, " +
                "hocsinh.tenhocsinh, " +
                "phuthu.tenphuthu, " +
                "phuthu.thanhtien, " +
                "phuthu.maloai, " +
                "phuthu.nguoithu " +
                "from phuthu inner join hocsinh on " +
                "hocsinh.mahocsinh = phuthu.mahocsinh; ");
            DataTable dtHS = DataController.ExecTable("select * from hocsinh");
            foreach(DataRow row in dtHS.Rows)
            {
                comboBox1.Items.Add(row["mahocsinh"].ToString() + " - " + row["tenhocsinh"].ToString());
            }
            DataTable dtLPT = DataController.ExecTable("select * from loaiphuthu");

            foreach (DataRow row in dtLPT.Rows)
            {
                comboBox2.Items.Add(row["maloai"].ToString() + " - " + row["tenloaiphuthu"].ToString());
            }
        }
    }
}
