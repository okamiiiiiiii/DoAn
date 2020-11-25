using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DoAn.Controller
{
    class HDNKController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select mahdnk, tenhdnk, CONVERT(varchar, CAST( chiphidukien AS money),1), ghichu, tgthuchien from hoatdongngoaikhoa");
        }

        static public DataRow findOneById(string id)
        {
            DataTable dt = DataController.ExecTable("select * from hoatdongngoaikhoa where mahdnk = " + id);
            return dt.Rows[0];
        }

        static public void addHDNK(string ten, string tien, string ghichu, string tg)
        {
            DataController.Execute("insert into hoatdongngoaikhoa(" +
                "tenhdnk," +
                "chiphidukien," +
                "ghichu," +
                "lopthamgia1," +
                "lopthamgia2," +
                "tgthuchien) " +
                "values(" +
                "N'" + ten + "', " +
                tien + ", " +
                "N'" + ghichu + "', " +
                "N'', " +
                "'', " +
                "'" + tg + "')");
        }

        static public void updateHDNK(string id, string ten, string tien, string ghichu, string tg)
        {
            DataController.Execute("update hoatdongngoaikhoa set " +
                "tenhdnk = N'" + ten + "', " +
                "chiphidukien = " + tien + ", " +
                "ghichu = N'" + ghichu + "' " +
                "where mahdnk = " + id);
        }

        static public void deleteHDNK(string id)
        {
            DataTable dt = DataController.ExecTable("select * from tttienchi where mahdnk = " + id);
            if(dt.Rows.Count == 0)
            {
                DataController.Execute("delete from hoatdongngoaikhoa where mahdnk = " + id);
            }
            else
                MessageBox.Show("Vẫn còn phiếu chi thuộc loại hoạt động ngoại khóa này, không thể xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
