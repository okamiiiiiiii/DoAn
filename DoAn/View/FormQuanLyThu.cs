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
    public partial class FormQuanLyThu : Form
    {
        private Form activeForm;
        public FormQuanLyThu()
        {
            InitializeComponent();
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel4.Controls.Add(childForm);
            this.panel4.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void FormQuanLyThu_Load(object sender, EventArgs e)
        {
            button4.BackColor = ThemeColor.PrimaryColor;
            button4.ForeColor = Color.White;
            button4.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;

            button5.BackColor = ThemeColor.PrimaryColor;
            button5.ForeColor = Color.White;
            button5.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;

            button1.BackColor = ThemeColor.PrimaryColor;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;

            button2.BackColor = ThemeColor.PrimaryColor;
            button2.ForeColor = Color.White;
            button2.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;

            OpenChildForm(new PhuThu(), sender);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PhuThu(), sender);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TienAn(), sender);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new View.PhuThuKeHoach(), sender);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new View.ThuNgoai(), sender);
        }
    }
}
