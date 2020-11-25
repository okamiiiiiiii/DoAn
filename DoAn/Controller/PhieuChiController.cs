using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DoAn.Controller
{
    class PhieuChiController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select phieuchi.maphieuchi, " +
                "tenphieuchi, " +
                "tg, " +
                "tennv, " +
                "tentrangthaiphieuchi ," +
                "SUM(tttienchi.thanhtien) as tongtien " +
                "from phieuchi, nhanvien, trangthaiphieuchi, tttienchi " +
                "where phieuchi.manv = nhanvien.manv " +
                "and phieuchi.matrangthaiphieuchi = trangthaiphieuchi.matrangthaiphieuchi " +
                "and tttienchi.maphieuchi = phieuchi.maphieuchi " +
                "group by phieuchi.maphieuchi, tenphieuchi, tg, tennv, tentrangthaiphieuchi");
        }

        static public DataRow findByID(string id)
        {
            return DataController.ExecTable("select * " +
                "from phieuchi, nhanvien, trangthaiphieuchi " +
                "where phieuchi.manv = nhanvien.manv " +
                "and phieuchi.matrangthaiphieuchi = trangthaiphieuchi.matrangthaiphieuchi " +
                "and maphieuchi = " + id).Rows[0];
        }

        static public int tongtien(string id)
        {
            string rs = DataController.ExecTable("select SUM(thanhtien) as tongtien from tttienchi, phieuchi where tttienchi.maphieuchi = phieuchi.maphieuchi and phieuchi.maphieuchi = 1").Rows[0]["tongtien"].ToString();
            return int.Parse(rs);
        }

        static public DataTable fetchKhoanChi(string id)
        {
            return DataController.ExecTable("select * from tttienchi where maphieuchi = " + id);
        }

        static public void addPhieuChi(string tenphieuchi, int matrangthai)
        {
            DataController.Execute("insert into phieuchi values(" +
                "N'" + tenphieuchi + "', " +
                "'" + DateTime.Now.ToString("yyyy-MM-dd") +"', " +
                "dbo.getmabctt2(), " +
                LoginUser.manv + ", " +
                + matrangthai +")");
        }

        static public void updatePhieuChi(string id, string tenphieuchi, int matrangthaiphieuchi)
        {
            DataController.Execute("update phieuchi set tenphieuchi = N'" + tenphieuchi + "', matrangthaiphieuchi = " + matrangthaiphieuchi + " where maphieuchi = " + id);
        }

        static public void deletePhieuChi(string id)
        {
            DataController.Execute("delete phieuchi where maphieuchi = " + id);
        }

        static public DataTable fetchTrangThai()
        {
            return DataController.ExecTable("select * from trangthaiphieuchi");
        }
    }
}
