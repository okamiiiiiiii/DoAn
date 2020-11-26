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
    class baocaodoanhchi
    {
        static public void inphieuchi(string id)
        {
            DocX gDoc;

            try
            {
                if (File.Exists(@"mauphieuchi.docx"))
                {
                    gDoc = CreateInvoiceFromTemplate(DocX.Load(@"mauphieuchi.docx"),id);
                    gDoc.SaveAs(@"newFilephieuchi.docx");
                }
                else
                {
                    MessageBox.Show("Không có file mauphieuchi.docx");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Không có khoản chi trong phiếu chi này");
            }
        }
        //tạo hóa đơn từ mẫu
        static private DocX CreateInvoiceFromTemplate(DocX template,string id)
        {
            DataTable dt = DataController.ExecTable(" select phieuchi.maphieuchi,tenphieuchi,tg,manv,sum(thanhtien) as tongtien from phieuchi,tttienchi where phieuchi.maphieuchi=tttienchi.maphieuchi  and  phieuchi.maphieuchi = "+ id+" group by phieuchi.maphieuchi,tg,manv,tenphieuchi " );
            DataRow data = dt.Rows[0];

            DataTable dt2 = DataController.ExecTable(" select tennv,diachi from nhanvien where manv = " + data["manv"].ToString());
            DataRow data2 = dt2.Rows[0];

            template.AddCustomProperty(new CustomProperty("angay", Convert.ToDateTime(data["tg"].ToString()).ToString("dd")));
            template.AddCustomProperty(new CustomProperty("athang", Convert.ToDateTime(data["tg"].ToString()).ToString("MM")));
            template.AddCustomProperty(new CustomProperty("anam", Convert.ToDateTime(data["tg"].ToString()).ToString("yyyy")));
            template.AddCustomProperty(new CustomProperty("atongtien", data["tongtien"].ToString()));
            template.AddCustomProperty(new CustomProperty("adiachi", data2["diachi"].ToString()));
            template.AddCustomProperty(new CustomProperty("atennhanvien", data2["tennv"].ToString()));
            template.AddCustomProperty(new CustomProperty("anguoilap", LoginUser.UserData()["tennv"].ToString()));
            template.AddCustomProperty(new CustomProperty("atenphieuchi", data["tenphieuchi"].ToString()));

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

            var t3 = template.Tables[2];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter3(t3, ref template,id);
            t3.Remove();// xóa giữ liệu table
            return template; // lưu vào template
        }

        static private Table CreateAndInsertInvoiceTableAfter(Table t, ref DocX document,string id)
        {
            var data = DataController.ExecTable(" select tentttc,tennguyenlieu,soluong,thanhtien from tttienchi,phieuchi,nguyenlieu" +
                " where tttienchi.maphieuchi =phieuchi.maphieuchi and tttienchi.manguyenlieu=nguyenlieu.manguyenlieu and  phieuchi.maphieuchi= "+id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);
            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên tên tiền chi", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tên nguyên liệu", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("Số lượng", false, tableTitle);
            invoiceTable.Rows[0].Cells[3].InsertParagraph("Thành tiền", false, tableTitle);
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
            var data = DataController.ExecTable(" select tentttc,tenhoclieu,soluong,thanhtien from tttienchi,phieuchi,hoclieu" +
                " where tttienchi.maphieuchi =phieuchi.maphieuchi " +
                "and tttienchi.mahoclieu=hoclieu.mahoclieu " +
                "and  phieuchi.maphieuchi= "+id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên tên tiền chi", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tên học liệu", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("Số lượng", false, tableTitle);
            invoiceTable.Rows[0].Cells[3].InsertParagraph("Thành tiền", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }

        static private Table CreateAndInsertInvoiceTableAfter3(Table t, ref DocX document,string id)
        {
            var data = DataController.ExecTable(" select tentttc,tenkhoanchikhac,soluong,thanhtien from tttienchi,phieuchi,khoanchikhac where phieuchi.maphieuchi =tttienchi.maphieuchi and tttienchi.makhoanchikhac=khoanchikhac.makhoanchikhac and  phieuchi.maphieuchi="+id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên tiền chi", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tên khoản chi khác", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("Số lượng", false, tableTitle);
            invoiceTable.Rows[0].Cells[3].InsertParagraph("Thành tiền", false, tableTitle);
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
