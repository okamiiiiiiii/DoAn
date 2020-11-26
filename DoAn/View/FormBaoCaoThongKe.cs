using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Novacode;

namespace DoAn
{
    public partial class FormBaoCaoThongKe : Form
    {
        private Form activeForm;

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

        public FormBaoCaoThongKe()
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
        private void FormBaoCaoThongKe_Load(object sender, EventArgs e)
        {
            OpenChildForm(new FormThongKeChiTheoThang(), sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormThongKeChiTheoThang(), sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new View.BaoCaoThongKe.DiemDanh(), sender);
        }

        private void documentViewer1_Load(object sender, EventArgs e)
        {

        }

        private void panelDesktopPane_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
