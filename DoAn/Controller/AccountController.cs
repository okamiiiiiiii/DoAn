using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class AccountController
    {
        static public DataTable Accounts()
        {
            return DataController.ExecTable("select " +
                "manv, " +
                "tennv, " +
                "tentaikhoan, " +
                "matkhau " +
                "from nhanvien " +
                "where nhanvien.tentaikhoan IS NOT NULL");
        }

        static public DataTable fetchAll()
        {
            return DataController.ExecTable("select * from nhanvien,chucvu " +
                "where nhanvien.machucvu = chucvu.machucvu");
        }

        static public void UpdateAccount(string manv, string taikhoan, string matkhau)
        {
            DataController.Execute("Update nhanvien set " +
                "tentaikhoan = '" + taikhoan + "', " +
                "matkhau = '" + matkhau + "' " +
                "where manv = " + manv);
        }

        static public void DeleteAccount(string manv)
        {
            DataController.Execute("Update nhanvien set " +
                "tentaikhoan = NULL , " +
                "matkhau = NULL " +
                "where manv = " + manv);
        }
    }
}
