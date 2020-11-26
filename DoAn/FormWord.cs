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
    public partial class FormWord : Form
    {
        private string id;
        private int loai;
        public FormWord()
        {
            InitializeComponent();
        }

        public FormWord(string ids, int loais): this()
        {
            id = ids;
            loai = loais;
        }

        private void FormWord_Load(object sender, EventArgs e)
        {
            if(loai == 1)
            {
                IO.baocaodoanhchi.inphieuchi(id);
                documentViewer1.LoadDocument(@"newFilephieuchi.docx");
                return;
            }
            if (loai == 2)
            {
                IO.hoadonthang.inhoadonthang(id);
                documentViewer1.LoadDocument(@"newFilemauhoadonthang.docx");
                return;
            }
            if (loai == 3)
            {
                IO.phieubangiao.inphieubangiao(id);
                documentViewer1.LoadDocument(@"newFilephieubangiao.docx");
                return;
            }
            if (loai == 4)
            {
                IO.thongkethangnay.inthongkechitiet(id);
                documentViewer1.LoadDocument(@"newFilemauthongkebaocaothang.docx");
                return;
            }
            if (loai == 5)
            {
                IO.thungoai.inphieuthungoai(id);
                documentViewer1.LoadDocument(@"newFilemauthungoai.docx");
                return;
            }

        }
    }
}
