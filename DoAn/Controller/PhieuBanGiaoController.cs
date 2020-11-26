using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class PhieuBanGiaoController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select " +
                "maphieubangiao, " +
                "tenphieubangiao, " +
                "tg, " +
                "tenkhoi, " +
                "tennv " +
                "from phieubangiao, khoi, nhanvien " +
                "where phieubangiao.makhoi = khoi.makhoi " +
                "and phieubangiao.manv = nhanvien.manv");
        }

        static public DataRow findById(string id)
        {
            return DataController.ExecTable("select * from phieubangiao, khoi, nhanvien where phieubangiao.makhoi = khoi.makhoi and phieubangiao.manv = nhanvien.manv and maphieubangiao = " + id).Rows[0];
        }

        static public void addPBG(string ten, int makhoi)
        {
            DataController.Execute("insert into phieubangiao values(" +
                "N'" + ten + "', " +
                makhoi +", " +
                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                LoginUser.UserData()["manv"] + ")");
        }

        static public void updatePBG(string id, string ten, int makhoi)
        {
            DataController.Execute("update phieubangiao set " +
                "tenphieubangiao = N'" + ten + "', " +
                "makhoi = " + makhoi + " " +
                "where maphieubangiao = " + id);
        }

        static public void deletePBG(string id)
        {
            DataController.Execute("delete from phieubangiao where maphieubangiao = " + id);
        }
    }
}
