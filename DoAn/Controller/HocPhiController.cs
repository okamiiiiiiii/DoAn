using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class HocPhiController
    {
        static public DataTable fetchKhoi()
        {
            return DataController.ExecTable("select makhoi, tenkhoi, tenphanloai from khoi, phanloai where khoi.maphanloai = phanloai.maphanloai");
        }

        static public DataTable fetchPhanLoai()
        {
            return DataController.ExecTable("select * from phanLoai");
        }

        static public DataRow findKhoi(string id)
        {
            DataTable dt = DataController.ExecTable("select * from khoi, phanloai where makhoi = " + id + " " +
                "and khoi.maphanloai = phanloai.maphanloai");
            return dt.Rows[0];
        }

        static public DataRow findPhanLoai(string id)
        {
            DataTable dt = DataController.ExecTable("select * from phanLoai where maphanloai = " + id);
            return dt.Rows[0];
        }

        static public void addKhoi(string ten, int maphanloai)
        {
            DataController.Execute("insert into khoi values(N'" + ten + "', N'', " + maphanloai + ")");
        }

        static public void updateKhoi(string id, string ten, int maphanloai)
        {
            DataController.Execute("update khoi set tenkhoi = N'" + ten +
                "', maphanloai = " + maphanloai +
                " where makhoi = " + id);
        }

        static public void deleteKhoi(string id)
        {
            DataController.Execute("delete khoi where makhoi = " + id);
        }

        static public void addPhanLoai(string ten, string gia)
        {
            DataController.Execute("insert into khoi values(N'" + ten + "', " + gia + ")");
        }

        static public void updatePhanLoai(string id, string ten, string gia)
        {
            DataController.Execute("update khoi set " +
                "tenphanloai = N'" + ten +
                "', hocphi = " + gia +
                " where makhoi = " + id);
        }

        static public void deletePhanLkoai(string id)
        {
            DataController.Execute("delete phanloai where maphanloai = " + id);
        }
    }
}
