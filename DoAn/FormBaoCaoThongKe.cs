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
    public partial class FormBaoCaoThongKe : Form
    {
        private Form activeForm;
        private void OpenChildForm(Form childForm, object btnSender)
        {
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel6.Controls.Add(childForm);
            this.panel6.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            panel2.BringToFront();
            panel3.BringToFront();
            panel4.BringToFront();
            panel5.BringToFront();
        }

        private bool isDropped1 = false;
        private bool isDropped2 = false;
        private bool isDropped3 = false;
        private bool isDropped4 = false;

        public FormBaoCaoThongKe()
        {
            InitializeComponent();
        }

        private void FormBaoCaoThongKe_Load(object sender, EventArgs e)
        {
            panel5.Size = button3.Size;

            button1.BackColor = ThemeColor.SecondaryColor;
            button2.BackColor = ThemeColor.SecondaryColor;
            button3.BackColor = ThemeColor.SecondaryColor;
            button4.BackColor = ThemeColor.SecondaryColor;

            button5.BackColor = ThemeColor.PrimaryColor;
            button6.BackColor = ThemeColor.PrimaryColor;
            button7.BackColor = ThemeColor.PrimaryColor;
            button8.BackColor = ThemeColor.PrimaryColor;
            button9.BackColor = ThemeColor.PrimaryColor;
            button10.BackColor = ThemeColor.PrimaryColor;
            button11.BackColor = ThemeColor.PrimaryColor;
            button12.BackColor = ThemeColor.PrimaryColor;
            button13.BackColor = ThemeColor.PrimaryColor;
            button14.BackColor = ThemeColor.PrimaryColor;
            button15.BackColor = ThemeColor.PrimaryColor;
            button16.BackColor = ThemeColor.PrimaryColor;

            panel2.Size = panel2.MinimumSize;
            panel3.Size = panel3.MinimumSize;
            panel4.Size = panel4.MinimumSize;
            panel5.Size = panel5.MinimumSize;

            OpenChildForm(new FormThongKeChiTheoThang(), sender);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isDropped1)
            {
                panel2.Height += 5;
                if (panel2.Height == panel2.MaximumSize.Height)
                {
                    isDropped1 = true;
                    timer1.Stop();
                }
            }
            else
            {
                panel2.Height -= 5;
                if (panel2.Height == panel2.MinimumSize.Height)
                {
                    isDropped1 = false;
                    timer1.Stop();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!isDropped2)
            {
                panel3.Height += 5;
                if (panel3.Height == panel3.MaximumSize.Height)
                {
                    isDropped2 = true;
                    timer2.Stop();
                }
            }
            else
            {
                panel3.Height -= 5;
                if (panel3.Height == panel3.MinimumSize.Height)
                {
                    isDropped2 = false;
                    timer2.Stop();
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!isDropped3)
            {
                panel4.Height += 5;
                if (panel4.Height == panel4.MaximumSize.Height)
                {
                    isDropped3 = true;
                    timer3.Stop();
                }
            }
            else
            {
                panel4.Height -= 5;
                if (panel4.Height == panel4.MinimumSize.Height)
                {
                    isDropped3 = false;
                    timer3.Stop();
                }
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (!isDropped4)
            {
                panel5.Height += 5;
                if (panel5.Height == panel5.MaximumSize.Height)
                {
                    isDropped4 = true;
                    timer4.Stop();
                }
            }
            else
            {
                panel5.Height -= 5;
                if (panel5.Height == panel5.MinimumSize.Height)
                {
                    isDropped4 = false;
                    timer4.Stop();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            if (isDropped2) timer2.Start();
            if (isDropped3) timer3.Start();
            if (isDropped4) timer4.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Start();
            if (isDropped1) timer1.Start();
            if (isDropped3) timer3.Start();
            if (isDropped4) timer4.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer3.Start();
            if (isDropped1) timer1.Start();
            if (isDropped2) timer2.Start();
            if (isDropped4) timer4.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer4.Start();
            if (isDropped1) timer1.Start();
            if (isDropped2) timer2.Start();
            if (isDropped3) timer3.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Size = panel2.MinimumSize;
            OpenChildForm(new FormThongKeChiTheoThang(), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Size = panel2.MinimumSize;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Size = panel2.MinimumSize;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel3.Size = panel3.MinimumSize;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel3.Size = panel3.MinimumSize;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel3.Size = panel3.MinimumSize;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel4.Size = panel4.MinimumSize;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel4.Size = panel4.MinimumSize;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel4.Size = panel4.MinimumSize;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            panel5.Size = panel5.MinimumSize;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            panel5.Size = panel5.MinimumSize;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel5.Size = panel5.MinimumSize;
        }
    }
}
