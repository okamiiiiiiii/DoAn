using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Novacode;

namespace DoAn
{
    class InBaoCaoDiemDanh
    {
        static public void output()
        {
            DocX gDoc;

            try
            {
                if (File.Exists(@"Test.docx"))
                {
                    gDoc = CreateInvoiceFromTemplate(DocX.Load(@"test.docx"));
                    gDoc.SaveAs(@"newFile.docx");
                }
                else
                {
                    MessageBox.Show("Không có file test.docx");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                //throw;
            }
        }

        static private DocX CreateInvoiceFromTemplate(DocX template)
        {
            template.AddCustomProperty(new CustomProperty("Ngay", DateTime.Today.ToString("yyyy-MM-dd")));
            template.AddCustomProperty(new CustomProperty("Lop", "khoi A"));
            template.AddCustomProperty(new CustomProperty("GiaoVien", LoginUser.UserData()["tennv"].ToString()));


            //table
            var t = template.Tables[0];
            CreateAndInsertInvoiceTableAfter(t, ref template);
            t.Remove();
            return template;
        }

        static private Table CreateAndInsertInvoiceTableAfter(Table t, ref DocX document)
        {
            var data = DataController.ExecTable("select DISTINCT hocsinh.mahocsinh, tenhocsinh, tenhinhthucan, vangmat from theodoixuatan, hocsinh, hinhthucan, nhanvien, khoi, phutrach where hocsinh.mahocsinh = theodoixuatan.mahocsinh and hinhthucan.mahinhthucan = theodoixuatan.mahinhthucan and hocsinh.makhoi = khoi.makhoi and phutrach.makhoi = khoi.makhoi and nhanvien.manv = phutrach.manv and khoi.makhoi =1 and tg = '2020-11-24'");

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);
            invoiceTable.Design = TableDesign.LightGridAccent1;

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Mã học sinh", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tên học sinh", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("Hình thức ăn", false, tableTitle);
            invoiceTable.Rows[0].Cells[3].InsertParagraph("Vắng mặt", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }
    }
}
