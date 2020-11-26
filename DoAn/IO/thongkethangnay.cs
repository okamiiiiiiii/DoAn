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
    class thongkethangnay
    {
        static public void inthongkechitiet(string id)
        {
            DocX gDoc;

            try
            {
                if (File.Exists(@"mauthongkebaocaothang.docx"))
                {
                    gDoc = CreateInvoiceFromTemplate(DocX.Load(@"mauthongkebaocaothang.docx"), id);
                    //gDoc.SaveAs(@"..\..\baocaothongke\newFilemauthongkebaocaothang.docx");
                    gDoc.SaveAs(@"newFilemauthongkebaocaothang.docx");
                }
                else
                {
                    MessageBox.Show("Không có file mauthongkebaocaothang.docx");
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
            DataTable dt = DataController.ExecTable("select mabctt,thang,nam from baocao where mabctt="+id);
            DataRow data = dt.Rows[0];


            
            template.AddCustomProperty(new CustomProperty("adatenow", DateTime.Today.ToString("dd-MM-yyyy")));
            template.AddCustomProperty(new CustomProperty("athang", data["thang"].ToString()));
            template.AddCustomProperty(new CustomProperty("anam", data["nam"].ToString()));
            template.AddCustomProperty(new CustomProperty("amabctt", data["mabctt"].ToString()));

            var t = template.Tables[0];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter(t, ref template, id);
            t.Remove();// xóa giữ liệu table
            //return template; // lưu vào template


            var t1 = template.Tables[1];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter1(t1, ref template, id);
            t1.Remove();// xóa giữ liệu table
                        //return template; // lưu vào template

            var t2 = template.Tables[2];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter2(t2, ref template, id);
            t2.Remove();// xóa giữ liệu table
                        //return template; // lưu vào template

            var t3 = template.Tables[3];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter3(t3, ref template, id);
            t3.Remove();// xóa giữ liệu table
                        //return template; // lưu vào template

            var t4 = template.Tables[4];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter4(t4, ref template, id);
            t4.Remove();// xóa giữ liệu table
                        //return template; // lưu vào template


            var t5 = template.Tables[5];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter5(t5, ref template, id);
            t5.Remove();// xóa giữ liệu table
                        //return template; // lưu vào template

            var t6 = template.Tables[6];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter6(t6, ref template, id);
            t6.Remove();// xóa giữ liệu table
                        //return template; // lưu vào template


            return template; // lưu vào template
        }

        static private Table CreateAndInsertInvoiceTableAfter(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tenhocsinh,tonghocphi,tentrangthai from hoadonthang,hocsinh,trangthai,baocao where baocao.mabctt=hoadonthang.mabctt and hoadonthang.mahocsinh=hocsinh.mahocsinh and hoadonthang.matrangthai=trangthai.matrangthai and hoadonthang.matrangthai=2 and baocao.mabctt=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);
            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên học sinh", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tổng học phí", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("trạng thái", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }

        static private Table CreateAndInsertInvoiceTableAfter1(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tenhocsinh,tonghocphi,tentrangthai from hoadonthang,hocsinh,trangthai,baocao where baocao.mabctt=hoadonthang.mabctt and hoadonthang.mahocsinh=hocsinh.mahocsinh and hoadonthang.matrangthai=trangthai.matrangthai and hoadonthang.matrangthai!=2 and baocao.mabctt=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên học sinh", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tổng học phí", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("trạng thái", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }

        static private Table CreateAndInsertInvoiceTableAfter2(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tenphuthucokehoach,(select tenkhoi from khoi where makhoi=makhoiapdung) as makhoiapdung,tienmoihocsinh from phuthucokehoach ptkh where mabctt= " + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên kế hoạch phụ thu", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("khối áp dụng", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("tiền đóng mỗi học sinh", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }

        static private Table CreateAndInsertInvoiceTableAfter3(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tenthungoai,tg,sotienthu	from thungoai where mabctt=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên khoản thu ngoài trường", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Thời gian", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("Số tiền thu", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }

        static private Table CreateAndInsertInvoiceTableAfter4(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tentttc,tennguyenlieu ,soluong,thanhtien from tttienchi,phieuchi,nguyenlieu where tttienchi.manguyenlieu=nguyenlieu.manguyenlieu and tttienchi.maphieuchi=phieuchi.maphieuchi and phieuchi.mabctt=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên thông tin tiền chi nguyên liệu", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tên nguyên liệu", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("Số lượng", false, tableTitle);
            invoiceTable.Rows[0].Cells[3].InsertParagraph("thành tiền", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }

        static private Table CreateAndInsertInvoiceTableAfter5(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tentttc,tenhoclieu ,soluong,thanhtien from tttienchi,phieuchi,hoclieu where tttienchi.mahoclieu=hoclieu.mahoclieu and tttienchi.maphieuchi=phieuchi.maphieuchi and phieuchi.mabctt=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên thông tin tiền chi học liệu", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tên học liệu", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("Số lượng", false, tableTitle);
            invoiceTable.Rows[0].Cells[3].InsertParagraph("thành tiền", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }
        static private Table CreateAndInsertInvoiceTableAfter6(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tentttc,tenkhoanchikhac ,soluong,thanhtien from tttienchi,phieuchi,khoanchikhac where tttienchi.makhoanchikhac=khoanchikhac.makhoanchikhac and tttienchi.maphieuchi=phieuchi.maphieuchi and phieuchi.mabctt=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên thông tin khoản chi khác", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tên khoản chi", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("Số lượng", false, tableTitle);
            invoiceTable.Rows[0].Cells[3].InsertParagraph("thành tiền", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }
        static private Table CreateAndInsertInvoiceTableAfter7(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tenphieubangiao,(select tenkhoi from khoi where phieubangiao.makhoi=khoi.makhoi) as tenkhoi,tg  from ttbangiao,phieubangiao,nguyenlieu where ttbangiao.maphieubangiao=phieubangiao.maphieubangiao and ttbangiao.manguyenlieu=nguyenlieu.manguyenlieu and phieubangiao.tg>dateadd(day,-day(GETDATE())+1,getdate())");

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên học sinh", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tổng học phí", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("trạng thái", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }
        static private Table CreateAndInsertInvoiceTableAfter8(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tenhocsinh,tonghocphi,tentrangthai from hoadonthang,hocsinh,trangthai,baocao where baocao.mabctt=hoadonthang.mabctt and hoadonthang.mahocsinh=hocsinh.mahocsinh and hoadonthang.matrangthai=trangthai.matrangthai and matrangthai!=2 and baocao.mabctt=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên học sinh", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tổng học phí", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("trạng thái", false, tableTitle);
            for (var row = 1; row < invoiceTable.RowCount; row++)
            {
                for (var cell = 0; cell < invoiceTable.ColumnCount; cell++)
                {
                    invoiceTable.Rows[row].Cells[cell].InsertParagraph(data.Rows[row - 1].ItemArray[cell].ToString(), false);
                }
            }
            return invoiceTable;
        }

        static private Table CreateAndInsertInvoiceTableAfter9(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tenhocsinh,tonghocphi,tentrangthai from hoadonthang,hocsinh,trangthai,baocao where baocao.mabctt=hoadonthang.mabctt and hoadonthang.mahocsinh=hocsinh.mahocsinh and hoadonthang.matrangthai=trangthai.matrangthai and matrangthai!=2 and baocao.mabctt=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên học sinh", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tổng học phí", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("trạng thái", false, tableTitle);
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
