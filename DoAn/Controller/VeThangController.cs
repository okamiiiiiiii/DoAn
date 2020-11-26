using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class VeThangController
    {
        static public DataTable fetchByMaKhoi(int makhoi)
        {
            return DataController.ExecTable("select " +
                "mavethang, " +
                "tenhocsinh, " +
                "ngaydangky, " +
                "tenloaivethang " +
                "from vethang, hocsinh, loaivethang " +
                "where hocsinh.mahocsinh = vethang.mahocsinh " +
                "and vethang.maloaivethang = loaivethang.maloaivethang " +
                "and hocsinh.makhoi = " + makhoi);
        }

        static public DataRow findOneById(string id)
        {
            DataTable dt = DataController.ExecTable("select * " +
                "from hocsinh, vethang, loaivethang " +
                "where vethang.mahocsinh = hocsinh.mahocsinh " +
                "and vethang.maloaivethang = loaivethang.maloaivethang " +
                "and vethang.mavethang = " + id);
            return dt.Rows[0];
        }

        static public DataTable fetchHSnoVT()
        {
            return DataController.ExecTable("select hocsinh.mahocsinh from hocsinh " +
                "left join vethang " +
                "on vethang.mahocsinh = hocsinh.mahocsinh " +
                "where vethang.mahocsinh IS NULL");
        }

        static public void addVeThang(string gia, string mahocsinh, string ngaydangki)
        {
            DataController.Execute("insert into vethang values(" +
                "" + gia + ", " +
                "" + mahocsinh + ", " +
                "'" + DateTime.Today.ToString("yyyy-MM-dd") + "', " +
                "" + "dbo.laymahoadonthangtheomahocsinh(" + mahocsinh +")" + ")");
        }

        static public void deleteVeThang(string id)
        {
            DataController.Execute("delete vethang where mavethang = " + id);
        }
    }
}
