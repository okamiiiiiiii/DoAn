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
    public partial class HocPhi : Form
    {
        private bool edit1 = false, edit2 = false, add1 = false, add2 = false;
        public HocPhi()
        {
            InitializeComponent();
        }

        private void Enable()
        {
            txt_khoi.Enabled = true;
            cbb_phanloai.Enabled = true;
        }

        private void Disable()
        {
            txt_khoi.Enabled = false;
            cbb_phanloai.Enabled = false;
        }

        private void Enable2()
        {
            txt_PhanLoai.Enabled = true;
            txt_HocPhi.Enabled = true;
        }

        private void Disable2()
        {
            txt_PhanLoai.Enabled = false;
            txt_HocPhi.Enabled = false;
        }

        public void getInfo()
        {
            string id = guna2DataGridView1.CurrentRow.Cells["makhoi"].Value.ToString();
            DataRow data = Controller.HocPhiController.findKhoi(id);
            txt_khoi.Text = data["tenkhoi"].ToString();
            cbb_phanloai.Text = data["tenphanloai"].ToString();

            Disable();

            if (add1) add1 = false;
            if (edit1) edit1 = false;
            if (btn_HuyThem.Visible == true) btn_HuyThem.Visible = false;

            btn_Them.Enabled = true;
            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
        }

        private void HocPhi_Load(object sender, EventArgs e)
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
            foreach (DataRow row in Controller.HocPhiController.fetchPhanLoai().Rows)
            {
                cbb_phanloai.Items.Add(row["tenphanloai"].ToString());
            }
            ViewLoad();
            ViewLoad2();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo();
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInfo2();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            if (!add1)
            {
                add1 = true;
                txt_HocPhi.Clear();
                cbb_phanloai.SelectedIndex = - 1;
            }
        }

        public void getInfo2()
        {
            string id = guna2DataGridView2.CurrentRow.Cells["maphanloai"].Value.ToString();
            DataRow data = Controller.HocPhiController.findPhanLoai(id);
            txt_PhanLoai.Text = data["tenphanloai"].ToString();
            txt_HocPhi.Text = data["hocphi"].ToString();

            Disable2();

            if (add2) add2 = false;
            if (edit2) edit2 = false;
            if (btn_HuyThem_2.Visible == true) btn_HuyThem_2.Visible = false;

            btn_Them_2.Enabled = true;
            btn_Sua_2.Enabled = true;
            btn_Xoa_2.Enabled = true;
        }

        private void ViewLoad()
        {
            guna2DataGridView1.DataSource = Controller.HocPhiController.fetchKhoi();
            getInfo();
        }

        private void ViewLoad2()
        {
            guna2DataGridView2.DataSource = Controller.HocPhiController.fetchPhanLoai();
            getInfo2();
        }
    }
}
