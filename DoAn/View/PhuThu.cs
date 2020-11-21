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
        bool editMode = false;
        bool addMode = false;
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
                "phuthu.maphuthu," +
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
                cbb_hs.Items.Add(row["mahocsinh"].ToString() + " - " + row["tenhocsinh"].ToString());
            }
            DataTable dtLPT = DataController.ExecTable("select * from loaiphuthu");

            foreach (DataRow row in dtLPT.Rows)
            {
                cbb_type.Items.Add(row["maloai"].ToString() + " - " + row["tenloaiphuthu"].ToString());
            }

            txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_tien.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();

            txt_ten.Enabled = false;
            txt_tien.Enabled = false;
            cbb_hs.Enabled = false;
            cbb_type.Enabled = false;

            txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_tien.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
            cbb_hs.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[1].Value.ToString()) - 1;
            cbb_type.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()) - 1;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_ten.Enabled = false;
            txt_tien.Enabled = false;
            cbb_hs.Enabled = false;
            cbb_type.Enabled = false;

            txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_tien.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
            cbb_hs.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[1].Value.ToString()) - 1;
            cbb_type.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()) - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!addMode)
            {
                addMode = true;

                txt_ten.Enabled = true;
                txt_tien.Enabled = true;
                cbb_hs.Enabled = true;
                cbb_type.Enabled = true;

                txt_tien.Clear();
                txt_ten.Clear();
                cbb_hs.SelectedIndex = -1;
                cbb_type.SelectedIndex = -1;
                btn_HuyThem.Visible = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên phụ thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_tien.Text))
                {
                    MessageBox.Show("Chưa nhập thành tiền tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_hs.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn học sinh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_type.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập loại phụ thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string mhs = (cbb_hs.SelectedIndex + 1).ToString();
                    string maloai = (cbb_type.SelectedIndex + 1).ToString();
                    DataController.Execute("insertphuthu N'" +
                        txt_ten.Text + "' ," +
                        txt_tien.Text + ", N'" +
                        LoginUser.UserData()["tennv"] + "', " +
                        maloai + ", " + 
                        mhs);

                    txt_ten.Enabled = false;
                    txt_tien.Enabled = false;
                    cbb_hs.Enabled = false;
                    cbb_type.Enabled = false;

                    txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txt_tien.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
                    cbb_hs.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[1].Value.ToString()) - 1;
                    cbb_type.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()) - 1;

                    guna2DataGridView1.DataSource = DataController.ExecTable("select " +
                    "phuthu.maphuthu," +
                    "hocsinh.mahocsinh, " +
                    "hocsinh.tenhocsinh, " +
                    "phuthu.tenphuthu, " +
                    "phuthu.thanhtien, " +
                    "phuthu.maloai, " +
                    "phuthu.nguoithu " +
                    "from phuthu inner join hocsinh on " +
                    "hocsinh.mahocsinh = phuthu.mahocsinh; ");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!editMode)
            {
                editMode = true;

                txt_ten.Enabled = true;
                txt_tien.Enabled = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên phụ thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_tien.Text))
                {
                    MessageBox.Show("Chưa nhập thành tiền tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_hs.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn học sinh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_type.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập loại phụ thu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string mhs = (cbb_hs.SelectedIndex + 1).ToString();
                    string maloai = (cbb_type.SelectedIndex + 1).ToString();
                    DataController.Execute("update phuthu set " +
                        "tenphuthu = N'"+ txt_ten.Text +"'" +
                        ", thanhtien = " + txt_tien.Text +
                        " where maphuthu = " + guna2DataGridView1.CurrentRow.Cells[0].Value.ToString());

                    guna2DataGridView1.DataSource = DataController.ExecTable("select " +
                    "phuthu.maphuthu," +
                    "hocsinh.mahocsinh, " +
                    "hocsinh.tenhocsinh, " +
                    "phuthu.tenphuthu, " +
                    "phuthu.thanhtien, " +
                    "phuthu.maloai, " +
                    "phuthu.nguoithu " +
                    "from phuthu inner join hocsinh on " +
                    "hocsinh.mahocsinh = phuthu.mahocsinh; ");

                    txt_ten.Enabled = false;
                    txt_tien.Enabled = false;
                    cbb_hs.Enabled = false;
                    cbb_type.Enabled = false;

                    txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txt_tien.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
                    cbb_hs.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[1].Value.ToString()) - 1;
                    cbb_type.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()) - 1;

                    editMode = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataController.Execute("Delete from phuthu where maphuthu =" + guna2DataGridView1.CurrentRow.Cells[0].Value.ToString());
            guna2DataGridView1.DataSource = DataController.ExecTable("select " +
                    "phuthu.maphuthu," +
                    "hocsinh.mahocsinh, " +
                    "hocsinh.tenhocsinh, " +
                    "phuthu.tenphuthu, " +
                    "phuthu.thanhtien, " +
                    "phuthu.maloai, " +
                    "phuthu.nguoithu " +
                    "from phuthu inner join hocsinh on " +
                    "hocsinh.mahocsinh = phuthu.mahocsinh; ");

            txt_ten.Enabled = false;
            txt_tien.Enabled = false;
            cbb_hs.Enabled = false;
            cbb_type.Enabled = false;

            txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
            txt_tien.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
            cbb_hs.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[1].Value.ToString()) - 1;
            cbb_type.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()) - 1;
        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            if (addMode)
            {
                btn_HuyThem.Visible = false;
                guna2DataGridView1.DataSource = DataController.ExecTable("select " +
                    "phuthu.maphuthu," +
                    "hocsinh.mahocsinh, " +
                    "hocsinh.tenhocsinh, " +
                    "phuthu.tenphuthu, " +
                    "phuthu.thanhtien, " +
                    "phuthu.maloai, " +
                    "phuthu.nguoithu " +
                    "from phuthu inner join hocsinh on " +
                    "hocsinh.mahocsinh = phuthu.mahocsinh; ");

                txt_ten.Enabled = false;
                txt_tien.Enabled = false;
                cbb_hs.Enabled = false;
                cbb_type.Enabled = false;

                txt_ten.Text = guna2DataGridView1.CurrentRow.Cells[2].Value.ToString();
                txt_tien.Text = guna2DataGridView1.CurrentRow.Cells[4].Value.ToString();
                cbb_hs.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[1].Value.ToString()) - 1;
                cbb_type.SelectedIndex = int.Parse(guna2DataGridView1.CurrentRow.Cells[5].Value.ToString()) - 1;
                addMode = false;
            }
        }
    }
}
