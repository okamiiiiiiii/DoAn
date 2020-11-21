using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class LoaiPhuThuController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select * from loaiphuthu");
        }

        static public DataRow findOneById(string id)
        {
            DataTable dt = DataController.ExecTable("select * from loaiphuthu where maloai = " + id);
            return dt.Rows[0];
        }

        static public void addLoaiPhuThu(string ten)
        {
            DataController.Execute("insert into loaiphuthu values (N'" + ten + "')");
        }

        static public void deleteLoaiPhuThu(string id)
        {
            DataController.Execute("delete loaiphuthu where maloai = " + id);
        }
    }
}
