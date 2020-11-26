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
    public partial class FormQuanLyVeThang : Form
    {
        private bool editMode = false;
        private bool addMode = false;
        public FormQuanLyVeThang()
        {
            InitializeComponent();
        }

        private void EnableAllControl()
        {
            cbb_maHS.Enabled = true;
            comboBox1.Enabled = true;
            txt_tenHS.Enabled = false;
        }

        private void DisableAllControl()
        {
            cbb_maHS.Enabled = false;
            comboBox1.Enabled = false;
            txt_tenHS.Enabled = false;
        }

        private void getInfo()
        {
            string id = guna2DataGridView1.CurrentRow.Cells["mavethang"].Value.ToString();
            DataRow data = Controller.VeThangController.findOneById(id);
            cbb_maHS.Text = id;
            txt_tenHS.Text = data["tenhocsinh"].ToString();
            comboBox1.Text = data["tenloaivethang"].ToString();

            DisableAllControl();
            btn_Xoa.Enabled = true;
            btn_Them.Enabled = true;
            if (editMode) editMode = false;
            if (addMode) addMode = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;
        }

        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.VeThangController.fetchByMaKhoi(listBox1.SelectedIndex + 1);
            if(guna2DataGridView1.Rows.Count != 0)
                getInfo();
        }



        private void FormQuanLyVeThang_Load(object sender, EventArgs e)
        {
            foreach (DataRow row in Controller.HocSinhController.fetchKhoi().Rows)
            {
                listBox1.Items.Add(row["tenkhoi"].ToString());
            }
            foreach (DataRow row in DataController.ExecTable("select * from loaivethang").Rows)
            {
                comboBox1.Items.Add(row["tenloaivethang"].ToString());
            }
            
            listBox1.SelectedIndex = 0;
            btn_Them.ForeColor = Color.White;
            btn_Them.BackColor = ThemeColor.PrimaryColor;
            btn_Xoa.ForeColor = Color.White;
            btn_Xoa.BackColor = ThemeColor.PrimaryColor;
            btn_HuyThem.ForeColor = Color.White;
            btn_HuyThem.BackColor = ThemeColor.PrimaryColor;
            ViewLoad();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if(!addMode)
            {
                EnableAllControl();
                addMode = true;
                
                cbb_maHS.Items.Clear();
                foreach (DataRow row in Controller.VeThangController.fetchHSnoVT().Rows)
                {
                    cbb_maHS.Items.Add(row["mahocsinh"].ToString());
                }
                cbb_maHS.SelectedIndex = -1;
                txt_tenHS.Clear();
                comboBox1.SelectedIndex = -1;

                btn_Xoa.Enabled = false;
                btn_HuyThem.Visible = true;
            }
            else
            {
                if(cbb_maHS.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập mã học sinh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Chưa nhập vé tháng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string gia = (comboBox1.SelectedIndex +1).ToString();
                    string mahocsinh = cbb_maHS.Text;
                    string ngaydangki = DateTime.Now.ToString("yyyy/MM/dd");
                    Controller.VeThangController.addVeThang(gia, mahocsinh, ngaydangki);
                }
                ViewLoad();
            }
        }

        private void cbb_maHS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbb_maHS.SelectedIndex != -1)
                txt_tenHS.Text = Controller.HocSinhController.findOneById(cbb_maHS.Text)["tenhocsinh"].ToString();
        }

        private void btn_HuyThem_Click(object sender, EventArgs e)
        {
            ViewLoad();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc muốn xoá ?", "Xoá vé tháng", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string id = guna2DataGridView1.CurrentRow.Cells["mavethang"].Value.ToString();
                Controller.VeThangController.deleteVeThang(id);
            }
            ViewLoad();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {




        }
    }
}
