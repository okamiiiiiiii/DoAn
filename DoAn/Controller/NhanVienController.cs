using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class NhanVienController
    {
        static public DataRow findOneByID(string id)
        {
            DataTable dt = DataController.ExecTable("select * from nhanvien, chucvu where manv = " + id + " and nhanvien.machucvu = chucvu.machucvu");
            return dt.Rows[0];
        }

        static public DataTable fetchChucvu()
        {
            return DataController.ExecTable("select * from chucvu");
        }
    }
}
