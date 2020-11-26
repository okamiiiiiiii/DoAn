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
    class thungoai
    {
        static public void inphieuthungoai(string id)
        {
            DocX gDoc;

            try
            {
                if (File.Exists(@"mauthungoai.docx"))
                {
                    gDoc = CreateInvoiceFromTemplate(DocX.Load(@"mauthungoai.docx"), id);
                    gDoc.SaveAs(@"newFilemauthungoai.docx");
                }
                else
                {
                    MessageBox.Show("Không có file mauthungoai.docx");
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
            DataTable dt = DataController.ExecTable(" select manv,mathungoai,tenthungoai,tg,sotienthu from thungoai where mathungoai=" + id);
            DataRow data = dt.Rows[0];

            DataTable dt2 = DataController.ExecTable(" select tennv,diachi from nhanvien where manv = " + data["manv"].ToString());
            DataRow data2 = dt2.Rows[0];

            template.AddCustomProperty(new CustomProperty("angay", Convert.ToDateTime(data["tg"].ToString()).ToString("dd")));
            template.AddCustomProperty(new CustomProperty("athang", Convert.ToDateTime(data["tg"].ToString()).ToString("MM")));
            template.AddCustomProperty(new CustomProperty("anam", Convert.ToDateTime(data["tg"].ToString()).ToString("yyyy")));
            template.AddCustomProperty(new CustomProperty("asotienthu", data["sotienthu"].ToString()));
            template.AddCustomProperty(new CustomProperty("adiachi", data2["diachi"].ToString()));
            template.AddCustomProperty(new CustomProperty("atennv", data2["tennv"].ToString()));
            template.AddCustomProperty(new CustomProperty("anguoilap", LoginUser.UserData()["tennv"].ToString()));
            template.AddCustomProperty(new CustomProperty("atenthungoai", data["tenthungoai"].ToString()));

            
            return template; // lưu vào template
        }


    }
}
