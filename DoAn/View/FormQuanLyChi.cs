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
    public partial class FormQuanLyChi : Form
    {
        bool addMode = false;
        bool editMode = false;
        public FormQuanLyChi()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            btn_Them.Enabled = true;
            btn_Sua.Enabled = false;
            btn_Xoa.Enabled = false;
        }

        private void FormQuanLyChi_Load(object sender, EventArgs e)
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
            groupBox1.ForeColor = ThemeColor.PrimaryColor;
            txt_ten.Enabled = false;
            txt_tien.Enabled = false;
            txt_SoLuong.Enabled = false;
            cbb_loaikhoanchi.Enabled = false;
            txt_GhiChu.Enabled = false;
            cbb_loaikhoanchi.Enabled = false;
            cbb_type.Enabled = false;

            guna2DataGridView1.DataSource = DataController.ExecTable("select " +
                "matttc, " +
                "tentienchi, " +
                "soluong, " +
                "thanhtien, " +
                "thoigian, " +
                "tennv " +
                "from tttienchi " +
                "inner join nhanvien on tttienchi.manv = nhanvien.manv");
            cbb_type.Visible = false;
            lbl_type.Visible = false;
            txt_GhiChu.Enabled = false;
            cbb_type.DropDownStyle = ComboBoxStyle.DropDownList;
            cbb_loaikhoanchi.DropDownStyle = ComboBoxStyle.DropDownList;

            DataTable dt = DataController.ExecTable("select * from tttienchi where matttc = " + guna2DataGridView1.Rows[0].Cells[0].Value.ToString());
            DataRow data = dt.Rows[0];

            txt_ten.Text = data["tentienchi"].ToString();
            txt_tien.Text = data["thanhtien"].ToString();
            txt_SoLuong.Text = data["soluong"].ToString();
            if (!data.IsNull("manguyenlieu"))
            {
                cbb_loaikhoanchi.SelectedIndex = 0;
                cbb_type.SelectedIndex = int.Parse(data["manguyenlieu"].ToString()) - 1;
            }
            else if (!data.IsNull("mahoclieu"))
            {
                cbb_loaikhoanchi.SelectedIndex = 1;
                cbb_type.SelectedIndex = int.Parse(data["mahoclieu"].ToString()) - 1;
            }
            else if (!data.IsNull("mahdnk"))
            {
                cbb_loaikhoanchi.SelectedIndex = 2;
                cbb_type.SelectedIndex = int.Parse(data["mahdnk"].ToString()) - 1;
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_ten.Enabled = false;
            txt_tien.Enabled = false;
            txt_SoLuong.Enabled = false;
            cbb_loaikhoanchi.Enabled = false;
            txt_GhiChu.Enabled = false;
            cbb_loaikhoanchi.Enabled = false;
            cbb_type.Enabled = false;

            if (addMode)
            {
                addMode = false;
                btn_HuyThem.Visible = false;
                btn_Sua.Enabled = true;
                btn_Xoa.Enabled = true;
            }

            string mathu = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            DataTable dt = DataController.ExecTable("select * from tttienchi where matttc = " + mathu);
            DataRow data = dt.Rows[0];

            txt_ten.Text = data["tentienchi"].ToString();
            txt_tien.Text = data["thanhtien"].ToString();
            txt_SoLuong.Text = data["soluong"].ToString();
            if (!data.IsNull("manguyenlieu"))
            {
                cbb_loaikhoanchi.SelectedIndex = 0;
                cbb_type.SelectedIndex = int.Parse(data["manguyenlieu"].ToString()) - 1;
            }
            else if (!data.IsNull("mahoclieu"))
            {
                cbb_loaikhoanchi.SelectedIndex = 1;
                cbb_type.SelectedIndex = int.Parse(data["mahoclieu"].ToString()) - 1;
            }
            else if (!data.IsNull("mahdnk"))
            {
                cbb_loaikhoanchi.SelectedIndex = 2;
                cbb_type.SelectedIndex = int.Parse(data["mahdnk"].ToString()) - 1;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_loaikhoanchi.SelectedIndex == -1)
            {
                cbb_type.Visible = false;
                lbl_type.Visible = false;
            }
            else
            {
                cbb_type.Visible = true;
                lbl_type.Visible = true;
            }

            if (cbb_loaikhoanchi.SelectedIndex == 0)
            {
                cbb_type.Text = "";
                cbb_type.Items.Clear();
                lbl_type.Text = "Mã nguyên liệu";
                DataTable dt = DataController.ExecTable("select * from nguyenlieu");
                foreach (DataRow row in dt.Rows)
                {
                    cbb_type.Items.Add(row["manguyenlieu"].ToString() + " - " + row["tennguyenlieu"].ToString());
                }
                if (addMode) cbb_type.SelectedIndex = -1;
            }
            else if (cbb_loaikhoanchi.SelectedIndex == 1)
            {
                cbb_type.Text = "";
                cbb_type.Items.Clear();
                lbl_type.Text = "Mã học liệu";
                DataTable dt = DataController.ExecTable("select * from hoclieu");
                foreach (DataRow row in dt.Rows)
                {
                    cbb_type.Items.Add(row["mahoclieu"].ToString() + " - " + row["tenhoclieu"].ToString());
                }
                if (addMode) cbb_type.SelectedIndex = -1;
            }
            else if (cbb_loaikhoanchi.SelectedIndex == 2)
            {
                cbb_type.Text = "";
                cbb_type.Items.Clear();
                lbl_type.Text = "Mã hoạt động ngoại khoá";
                DataTable dt = DataController.ExecTable("select * from hoatdongngoaikhoa");
                foreach (DataRow row in dt.Rows)
                {
                    cbb_type.Items.Add(row["mahdnk"].ToString() + " - " + row["tenhdnk"].ToString());
                }
                if (addMode) cbb_type.SelectedIndex = -1;
            }
            Console.WriteLine(cbb_type.SelectedIndex);
        }

        private void lbl_type_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_loaikhoanchi.SelectedIndex == 0)
            {
                DataTable dt = DataController.ExecTable("select ghichu from nguyenlieu where manguyenlieu = " + cbb_type.Text.Split(' ')[0]);
                txt_GhiChu.Text = dt.Rows[0]["ghichu"].ToString();
            }
            else if (cbb_loaikhoanchi.SelectedIndex == 1)
            {
                DataTable dt = DataController.ExecTable("select ghichu from nguyenlieu where manguyenlieu = " + cbb_type.Text.Split(' ')[0]);
                txt_GhiChu.Text = dt.Rows[0]["ghichu"].ToString();
            }
            else if (cbb_loaikhoanchi.SelectedIndex == 2)
            {
                DataTable dt = DataController.ExecTable("select ghichu from hoatdongngoaikhoa where mahdnk = " + cbb_type.Text.Split(' ')[0]);
                txt_GhiChu.Text = dt.Rows[0]["ghichu"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!addMode)
            {
                addMode = true;
                txt_ten.Enabled = true;
                txt_tien.Enabled = true;
                txt_SoLuong.Enabled = true;
                cbb_loaikhoanchi.Enabled = true;
                cbb_type.Enabled = true;

                txt_ten.Clear();
                txt_tien.Clear();
                txt_GhiChu.Clear();
                txt_SoLuong.Clear();
                cbb_loaikhoanchi.SelectedIndex = -1;
                cbb_type.Visible = false;
                lbl_type.Visible = false;

                btn_HuyThem.Visible = true;
                btn_Sua.Enabled = false;
                btn_Xoa.Enabled = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_tien.Text))
                {
                    MessageBox.Show("Chưa nhập số tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_SoLuong.Text))
                {
                    MessageBox.Show("Chưa nhập số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_loaikhoanchi.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn loại khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_type.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập mã khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string loai = (cbb_loaikhoanchi.SelectedIndex + 1).ToString();
                    string magiaovien = LoginUser.UserData()["manv"].ToString();
                    DataController.Execute("inserttttienchi " +
                        txt_SoLuong.Text + "," +
                        "N'" + txt_ten.Text + "'," +
                        "" + txt_tien.Text + "," +
                        "" + magiaovien + "," +
                        "" + cbb_type.Text.Split(' ')[0] + "," +
                        "" + loai);

                    guna2DataGridView1.DataSource = DataController.ExecTable("select " +
                    "matttc, " +
                    "tentienchi, " +
                    "soluong, " +
                    "thanhtien, " +
                    "thoigian, " +
                    "tennv " +
                    "from tttienchi " +
                    "inner join nhanvien on tttienchi.manv = nhanvien.manv");

                    addMode = false;

                    addMode = false;
                    btn_HuyThem.Visible = false;
                    btn_Sua.Enabled = true;
                    btn_Xoa.Enabled = true;

                    txt_ten.Enabled = false;
                    txt_tien.Enabled = false;
                    txt_SoLuong.Enabled = false;
                    cbb_loaikhoanchi.Enabled = false;
                    txt_GhiChu.Enabled = false;
                    cbb_loaikhoanchi.Enabled = false;
                    cbb_type.Enabled = false;

                    string mathu = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                    DataTable dt = DataController.ExecTable("select * from tttienchi where matttc = " + mathu);
                    DataRow data = dt.Rows[0];

                    txt_ten.Text = data["tentienchi"].ToString();
                    txt_tien.Text = data["thanhtien"].ToString();
                    txt_SoLuong.Text = data["soluong"].ToString();
                    if (!data.IsNull("manguyenlieu"))
                    {
                        cbb_loaikhoanchi.SelectedIndex = 0;
                        cbb_type.SelectedIndex = int.Parse(data["manguyenlieu"].ToString()) - 1;
                    }
                    else if (!data.IsNull("mahoclieu"))
                    {
                        cbb_loaikhoanchi.SelectedIndex = 1;
                        cbb_type.SelectedIndex = int.Parse(data["mahoclieu"].ToString()) - 1;
                    }
                    else if (!data.IsNull("mahdnk"))
                    {
                        cbb_loaikhoanchi.SelectedIndex = 2;
                        cbb_type.SelectedIndex = int.Parse(data["mahdnk"].ToString()) - 1;
                    }
                }
            }
        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            addMode = false;
            btn_HuyThem.Visible = false;
            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;

            txt_ten.Enabled = false;
            txt_tien.Enabled = false;
            txt_SoLuong.Enabled = false;
            cbb_loaikhoanchi.Enabled = false;
            txt_GhiChu.Enabled = false;
            cbb_loaikhoanchi.Enabled = false;
            cbb_type.Enabled = false;

            DataTable dt = DataController.ExecTable("select * from tttienchi where matttc = " + guna2DataGridView1.Rows[0].Cells[0].Value.ToString());
            DataRow data = dt.Rows[0];

            txt_ten.Text = data["tentienchi"].ToString();
            txt_tien.Text = data["thanhtien"].ToString();
            txt_SoLuong.Text = data["soluong"].ToString();
            if (!data.IsNull("manguyenlieu"))
            {
                cbb_loaikhoanchi.SelectedIndex = 0;
                cbb_type.SelectedIndex = int.Parse(data["manguyenlieu"].ToString()) - 1;
            }
            else if (!data.IsNull("mahoclieu"))
            {
                cbb_loaikhoanchi.SelectedIndex = 1;
                cbb_type.SelectedIndex = int.Parse(data["mahoclieu"].ToString()) - 1;
            }
            else if (!data.IsNull("mahdnk"))
            {
                cbb_loaikhoanchi.SelectedIndex = 2;
                cbb_type.SelectedIndex = int.Parse(data["mahdnk"].ToString()) - 1;
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (!editMode)
            {
                editMode = true;
                txt_ten.Enabled = true;
                txt_tien.Enabled = true;
                txt_SoLuong.Enabled = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txt_ten.Text))
                {
                    MessageBox.Show("Chưa nhập tên khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_tien.Text))
                {
                    MessageBox.Show("Chưa nhập số tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (string.IsNullOrWhiteSpace(txt_SoLuong.Text))
                {
                    MessageBox.Show("Chưa nhập số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_loaikhoanchi.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa chọn loại khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cbb_type.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập mã khoản chi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string mathu1 = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string loai = (cbb_loaikhoanchi.SelectedIndex + 1).ToString();
                    string magiaovien = LoginUser.UserData()["manv"].ToString();

                    DataController.Execute("update tttienchi set " +
                        "soluong = " + txt_SoLuong.Text +
                        ", thanhtien = " + txt_tien.Text +
                        ", tentienchi = N'" + txt_ten.Text + "'" +
                        "where matttc = " + mathu1);

                    guna2DataGridView1.DataSource = DataController.ExecTable("select " +
                    "matttc, " +
                    "tentienchi, " +
                    "soluong, " +
                    "thanhtien, " +
                    "thoigian, " +
                    "tennv " +
                    "from tttienchi " +
                    "inner join nhanvien on tttienchi.manv = nhanvien.manv");


                    editMode = false;
                    btn_Sua.Enabled = true;
                    btn_Xoa.Enabled = true;
                    btn_HuyThem.Visible = false;

                    txt_ten.Enabled = false;
                    txt_tien.Enabled = false;
                    txt_SoLuong.Enabled = false;
                    cbb_loaikhoanchi.Enabled = false;
                    txt_GhiChu.Enabled = false;
                    cbb_loaikhoanchi.Enabled = false;
                    cbb_type.Enabled = false;


                    DataTable dt = DataController.ExecTable("select * from tttienchi where matttc = " + mathu1);
                    DataRow data = dt.Rows[0];

                    txt_ten.Text = data["tentienchi"].ToString();
                    txt_tien.Text = data["thanhtien"].ToString();
                    txt_SoLuong.Text = data["soluong"].ToString();
                    if (!data.IsNull("manguyenlieu"))
                    {
                        cbb_loaikhoanchi.SelectedIndex = 0;
                        cbb_type.SelectedIndex = int.Parse(data["manguyenlieu"].ToString()) - 1;
                    }
                    else if (!data.IsNull("mahoclieu"))
                    {
                        cbb_loaikhoanchi.SelectedIndex = 1;
                        cbb_type.SelectedIndex = int.Parse(data["mahoclieu"].ToString()) - 1;
                    }
                    else if (!data.IsNull("mahdnk"))
                    {
                        cbb_loaikhoanchi.SelectedIndex = 2;
                        cbb_type.SelectedIndex = int.Parse(data["mahdnk"].ToString()) - 1;
                    }
                }
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string mathu = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
            DataController.Execute("Delete from tttienchi where matttc = " + mathu);

            guna2DataGridView1.DataSource = DataController.ExecTable("select " +
                    "matttc, " +
                    "tentienchi, " +
                    "soluong, " +
                    "thanhtien, " +
                    "thoigian, " +
                    "tennv " +
                    "from tttienchi " +
                    "inner join nhanvien on tttienchi.manv = nhanvien.manv");
            DataTable dt = DataController.ExecTable("select * from tttienchi where matttc = " + guna2DataGridView1.CurrentRow.Cells[0].Value.ToString());
            DataRow data = dt.Rows[0];

            txt_ten.Text = data["tentienchi"].ToString();
            txt_tien.Text = data["thanhtien"].ToString();
            txt_SoLuong.Text = data["soluong"].ToString();
            if (!data.IsNull("manguyenlieu"))
            {
                cbb_loaikhoanchi.SelectedIndex = 0;
                cbb_type.SelectedIndex = int.Parse(data["manguyenlieu"].ToString()) - 1;
            }
            else if (!data.IsNull("mahoclieu"))
            {
                cbb_loaikhoanchi.SelectedIndex = 1;
                cbb_type.SelectedIndex = int.Parse(data["mahoclieu"].ToString()) - 1;
            }
            else if (!data.IsNull("mahdnk"))
            {
                cbb_loaikhoanchi.SelectedIndex = 2;
                cbb_type.SelectedIndex = int.Parse(data["mahdnk"].ToString()) - 1;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
