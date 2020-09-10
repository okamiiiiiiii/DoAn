using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class MainForm : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        private bool isDroped = false;


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public MainForm()
        {
            InitializeComponent();
            random = new Random();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            btnCloseChildForm.Visible = false;
            
            this.Text = string.Empty;
            this.ControlBox = false;

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")] private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")] private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelTitleBar.ForeColor = Color.White;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    panelLogo.ForeColor = Color.White;
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    btnCloseChildForm.Visible = true;
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.SteelBlue;
                    previousBtn.ForeColor = Color.White;
                    previousBtn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            labelTitle.Text = childForm.Text;
            AccountPanel.BringToFront();
        }

        private void btnQuanLyChi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLyChi(), sender);
        }

        private void btnQuanLyThu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLyThu(), sender);
        }

        private void btnQuanLyHeThong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLyTaiKhoan(), sender);
        }

        private void btnQuanLyHocSinh_Click(object sender, EventArgs e)
        {
            
            OpenChildForm(new FormQuanLyHocSinh(), sender);
        }

        private void btnQuanLyDanhMuc_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btnQuanLyVeAn_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btnBaoCaoThongKe_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            labelTitle.Text = "HOME";
            panelTitleBar.BackColor = Color.DodgerBlue;
            panelLogo.BackColor = Color.RoyalBlue;
            currentButton = null;
            btnCloseChildForm.Visible = false;
            panelLogo.ForeColor = Color.Black;
            panelTitleBar.ForeColor = Color.Black;
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bntMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất ?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form Login = new FormLogin();
                Login.Show();
                this.Hide();
            }
        }

        private void controlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            avtButton.BackgroundImageLayout = ImageLayout.Stretch;
            //guna2CircleButton1.BackgroundImageLayout = ImageLayout.Stretch;
            AccountPanel.Height = 0;
            AccountPanel.BringToFront();
            timer2.Start();
            label1.Font = new Font("Kids", 14, FontStyle.Bold);
        }

        private void avtButton_Click(object sender, EventArgs e)
        {
            if (!isDroped)
            {
                avtButton.Width += 10;
                avtButton.Height += 10;
            }
            else
            {
                avtButton.Width -= 10;
                avtButton.Height -= 10;
            }
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isDroped)
            {
                
                AccountPanel.Height += 10;
                if(AccountPanel.Size == AccountPanel.MaximumSize)
                {
                    isDroped = true;
                    timer1.Stop();
                }
            }
            else
            {
                AccountPanel.Height -= 10;
                if (AccountPanel.Height == 0)
                {
                    isDroped = false;
                    timer1.Stop();
                }
            }
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void panelDesktopPane_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLyChi(), sender);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ActivateButton(btnQuanLyThu);
            Form childForm = new FormQuanLyThu();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            labelTitle.Text = childForm.Text;
            btnCloseChildForm.Visible = true;
        }
    }

}
