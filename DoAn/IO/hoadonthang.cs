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
    class hoadonthang
    {
        static public void inhoadonthang(string id)
        {
            DocX gDoc;

            try
            {
                if (File.Exists(@"mauhoadonthang.docx"))
                {
                    gDoc = CreateInvoiceFromTemplate(DocX.Load(@"mauhoadonthang.docx"), id);
                    gDoc.SaveAs(@"newFilemauhoadonthang.docx");
                }
                else
                {
                    MessageBox.Show("Không có file mauhoadonthang.docx");
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
            DataTable dt = DataController.ExecTable("    select tonghocphi,mahocsinh,mabctt from hoadonthang where mahoadonthang= "+id+" group by tonghocphi,mahocsinh,mabctt;");
            DataRow data = dt.Rows[0];


            string B = "Không có vé tháng của tháng này";
            DataTable dt2 = DataController.ExecTable("  select mavethang,maloaivethang,mahocsinh,ngaydangky,mahoadonthang from vethang where vethang.mahoadonthang="+ id +" and mahocsinh= " + data["mahocsinh"].ToString());
            if(dt2.Rows.Count > 0) { 
            DataRow data2 = dt2.Rows[0];
            
            DataTable dt3 = DataController.ExecTable("  select tenloaivethang,gia from loaivethang where maloaivethang= " + data2["maloaivethang"].ToString());
            
            DataRow data3 = dt3.Rows[0];
            B= data3["gia"].ToString();
            }

            DataTable dt4 = DataController.ExecTable("  select thang,nam from hoadonthang,baocao where hoadonthang.mabctt=baocao.mabctt and mahoadonthang= " +id);
            DataRow data4 = dt4.Rows[0];

            DataTable dt5 = DataController.ExecTable("   select tenkhoi,diachi,tenhocsinh,hocphi from khoi,hocsinh,phanloai where khoi.maphanloai=phanloai.maphanloai and hocsinh.makhoi=khoi.makhoi and mahocsinh=" + data["mahocsinh"].ToString());
            DataRow data5 = dt5.Rows[0];

            DataTable dt6 = DataController.ExecTable(" select count(*) as  sobuoidaydu from theodoixuatan,baocao where baocao.mabctt="+ (int.Parse(data["mabctt"].ToString())-1).ToString() +" and mahinhthucan=1 and mahocsinh= " + data["mahocsinh"].ToString());
            DataRow data6 = dt6.Rows[0];

            DataTable dt7 = DataController.ExecTable(" select count(*) as  sobuoikhongdaydu from theodoixuatan,baocao where baocao.mabctt=" + (int.Parse(data["mabctt"].ToString()) - 1).ToString() + " and mahinhthucan=2 and mahocsinh= " + data["mahocsinh"].ToString());
            DataRow data7 = dt7.Rows[0];

            DataTable dt8 = DataController.ExecTable(" select count(*) as  sobuoikhongan from theodoixuatan,baocao where baocao.mabctt=" + (int.Parse(data["mabctt"].ToString()) - 1).ToString() + " and mahinhthucan=3 and mahocsinh= " + data["mahocsinh"].ToString());
            DataRow data8 = dt8.Rows[0];
            String A;
            DataTable dt9 = DataController.ExecTable(" select tenloaivethang from loaivethang,vethang where mahoadonthang=(select mahoadonthang from hoadonthang where mahocsinh="+ data["mahocsinh"].ToString() + "and hoadonthang.mabctt="+ (int.Parse(data["mabctt"].ToString()) - 1).ToString() + ") ");
            if (dt9.Rows.Count == 0) A = "Không có vé tháng của tháng trước";
            else
            {
                DataRow data9 = dt9.Rows[0];
                A= data9["tenloaivethang"].ToString();
            }

            template.AddCustomProperty(new CustomProperty("adatenow", DateTime.Today.ToString("dd-MM-yyyy")));
            template.AddCustomProperty(new CustomProperty("athang", data4["thang"].ToString()));
            template.AddCustomProperty(new CustomProperty("anam", data4["nam"].ToString()));
            template.AddCustomProperty(new CustomProperty("atenkhoi", data5["tenkhoi"].ToString()));
            template.AddCustomProperty(new CustomProperty("atenhocsinh", data5["tenhocsinh"].ToString()));
            template.AddCustomProperty(new CustomProperty("adiachi", data5["diachi"].ToString()));
            template.AddCustomProperty(new CustomProperty("ahocphi", data5["hocphi"].ToString()));
            template.AddCustomProperty(new CustomProperty("agia",B ));
            template.AddCustomProperty(new CustomProperty("atongtientru", data5["diachi"].ToString()));
            template.AddCustomProperty(new CustomProperty("asobuoidaydu", data6["sobuoidaydu"].ToString()));
            template.AddCustomProperty(new CustomProperty("asobuoikhongdaydu", data7["sobuoikhongdaydu"].ToString()));
            template.AddCustomProperty(new CustomProperty("asobuoikhongan", data8["sobuoikhongan"].ToString()));
            template.AddCustomProperty(new CustomProperty("atenvethangtruoc", A));
            template.AddCustomProperty(new CustomProperty("atonghocphi", data["tonghocphi"].ToString()));



            //table
            var t = template.Tables[0];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter(t, ref template, id);
            t.Remove();// xóa giữ liệu table
                       //return template; // lưu vào template
                       //
            var t2 = template.Tables[1];
            //tạo hóa đơn từ template
            CreateAndInsertInvoiceTableAfter2(t2, ref template, id);
            t2.Remove();// xóa giữ liệu table

            return template; // lưu vào template
        }

        static private Table CreateAndInsertInvoiceTableAfter(Table t, ref DocX document, string id)
        {
            var data = DataController.ExecTable("select tenphuthu,thanhtien,tgthu,nguoilapkhoanthu from phuthu where mahoadonthang=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);
            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên phụ thu", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Thành tiền", false, tableTitle);
            invoiceTable.Rows[0].Cells[2].InsertParagraph("Thời gian thu", false, tableTitle);
            invoiceTable.Rows[0].Cells[3].InsertParagraph("người thu", false, tableTitle);
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
            var data = DataController.ExecTable("select tenphuthucokehoach,tienmoihocsinh as tiencannop from phuthucokehoach,baocao,hoadonthang where baocao.mabctt=phuthucokehoach.mabctt and hoadonthang.mabctt=baocao.mabctt and mahoadonthang=" + id);

            var invoiceTable = t.InsertTableAfterSelf(data.Rows.Count + 1, data.Columns.Count);

            var tableTitle = new Formatting();
            tableTitle.Bold = true;

            invoiceTable.Rows[0].Cells[0].InsertParagraph("Tên phụ thu theo kế hoạch", false, tableTitle);
            invoiceTable.Rows[0].Cells[1].InsertParagraph("Tiền cần đóng", false, tableTitle);
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
