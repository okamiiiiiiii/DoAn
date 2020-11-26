using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DoAn.Controller
{
    class ThuNgoaiController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select mathungoai, tenthungoai, tennv, sotienthu, tg from thungoai, nhanvien where thungoai.manv = nhanvien.manv");
        }

        static public DataRow findById(string id)
        {
            return DataController.ExecTable("select * from thungoai, nhanvien where thungoai.manv = nhanvien.manv and mathungoai = " + id).Rows[0];
        }

        static public void addTN(string ten, string tien)
        {
            DataController.Execute("insert into thungoai values(" +
                "N'" + ten + "', " +
                LoginUser.UserData()["manv"].ToString() + ", " +
                "'" + DateTime.Today.ToString("yyyy/MM/dd") + "', " +
                tien + ", " +
                "1, " +
                "dbo.getmabctt2())");
        }

        static public void editTN(string id, string ten, string tien)
        {
            DataController.Execute("update thungoai set " +
                "tenthungoai = N'" + ten + "', " +
                "sotienthu = " + tien +
                "where mathungoai = " + id);
        }

        static public void deleteTN(string id)
        {
            DataController.Execute("delete from thungoai where mathungoai = " + id);
        }
    }
}
