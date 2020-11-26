using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data;
using Novacode;

namespace DoAn.IO
{
    class phieubangiao
    {
        static public void inphieubangiao(string id)
        {
            DocX gDoc;

            try
            {
                if (File.Exists(@"mauphieubangiao.docx"))
                {
                    gDoc = CreateInvoiceFromTemplate(DocX.Load(@"mauphieubangiao.docx"), id);
                    gDoc.SaveAs(@"newFilephieubangiao.docx");
                }
                else
                {
                    MessageBox.Show("Không có file mauphieubangiao.docx");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        //tạo hóa đơn từ mẫu
        static private DocX CreateInvoiceFromTemplate(DocX template, string id)
        {
            DataTable dt = DataController.ExecTable(" select phieubangiao.maphieubangiao,tenphieubangiao,makhoi,tg,manv from ttbangiao,phieubangiao where  phieubangiao.maphieubangiao=ttbangiao.maphieubangiao and phieubangiao.maphieubangiao= "+1 +" group by phieubangiao.maphieubangiao,tenphieubangiao,makhoi,tg,manv ");
            DataRow data = dt.Rows[0];

            DataTable dt2 = DataController.ExecTable(" select tennv,diachi from nhanvien where manv = " + data["manv"].ToString());
            DataRow data2 = dt2.Rows[0];

            DataTable dt3 = DataController.ExecTable(" select tenkhoi from khoi where makhoi= " + data["makhoi"].ToString());
            DataRow data3 = dt3.Rows[0];

            template.AddCustomProperty(new CustomProperty("angay", Convert.ToDateTime(data["tg"].ToString()).ToString("dd")));
            template.AddCustomProperty(new CustomProperty("athang", Convert.ToDateTime(data["tg"].ToString()).ToString("MM")));
            template.AddCustomProperty(new CustomProperty("anam", Convert.ToDateTime(data["tg"].ToString()).ToString("yyyy")));
            template.AddCustomProperty(new CustomProperty("atenkhoi", data3["tenkhoi"].ToString()));
            template.AddCustomProperty(new CustomProperty("adiachi", data2["diachi"].ToString()));
            template.AddCustomProperty(new CustomProperty("atennv", data2["tennv"].ToString()));
            template.AddCustomProperty(new CustomProperty("anguoilap", LoginUser.UserData()["tennv"].ToString()));
            template.AddCustomProperty(new CustomProperty("atenphieubangiao", data["tenphieubangiao"].ToString()));

            //table
            var t = template.Tables[0];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter(t, ref template,id);
            t.Remove();// xóa giữ liệu table
            //return template; // lưu vào template
            //
            var t2 = template.Tables[1];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter2(t2, ref template,id);
            t2.Remove();// xóa giữ liệu table

            return template; // lưu vào template
        }

        static private Table CreateAndInsertInvoiceTableAfter(Table t, ref DocX document,string id)
        {
            var data = DataController.ExecTable(" select tennguyenlieu,soluong from ttbangiao,phieubangiao,nguyenlieu where phieubangiao.maphieubangiao=ttbangiao.maphieubangiao and ttbangiao.manguyenlieu=nguyenlieu.manguyenlieu and phieubangiao.maphieubangiao=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);
            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên nguyên liệu", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Số lượng", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }

        static private Table CreateAndInsertInvoiceTableAfter2(Table t, ref DocX document,string id)
        {
            var data = DataController.ExecTable("select tenhoclieu,soluong from ttbangiao,phieubangiao,hoclieu where phieubangiao.maphieubangiao=ttbangiao.maphieubangiao and ttbangiao.mahoclieu=hoclieu.mahoclieu and phieubangiao.maphieubangiao= " + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;
            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên học liệu", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Số lượng", false, tableTitle);
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
