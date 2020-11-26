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
    public partial class FormCaiDatThamSo : Form
    {
        private Form activeForm;

        public FormCaiDatThamSo()
        {
            InitializeComponent();
            foreach (var btns in panel1.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }



        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormCaiDatThamSo_Load(object sender, EventArgs e)
        {
            OpenChildForm(new CaiDatThamSo.LoaiPhuThu(), sender);
        }

        private void btnLoaiPhuThu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CaiDatThamSo.LoaiPhuThu(), sender);
        }

        private void btn_ThongSoLuong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new View.CaiDatThamSo.ThongSoLuong(), sender);
        }

        private void btn_HinhThucAn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new View.CaiDatThamSo.HinhThucAn(), sender);
        }

        private void btn_HocPhi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new View.CaiDatThamSo.HocPhi(), sender);
        }

        private void btn_HDNK_Click(object sender, EventArgs e)
        {
            OpenChildForm(new View.CaiDatThamSo.HDNK(), sender);
        }

        private void btn_Kho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new View.CaiDatThamSo.Kho(), sender);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_ChucVu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new View.CaiDatThamSo.ChucVu1(), sender);
        }
    }
}
