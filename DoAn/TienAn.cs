﻿using System;
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
    public partial class TienAn : Form
    {
        public TienAn()
        {
            InitializeComponent();
        }

        private void TienAn_Load(object sender, EventArgs e)
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
                guna2DataGridView1.DataSource = DataController.ExecTable("select hocsinh.mahocsinh, hocsinh.tenhocsinh, hoadonthang.tonghocphi, baocao.thang, baocao.nam from hoadonthang inner join hocsinh on hocsinh.mahocsinh = hoadonthang.mahocsinh inner join baocao on baocao.mabctt = hoadonthang.mabctt");
            }
        }
    }
}
