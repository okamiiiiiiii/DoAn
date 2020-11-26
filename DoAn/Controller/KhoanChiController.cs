using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    static class KhoanChiController
    {
        static public DataTable fetchTienChiNguyenLieu(string id)
        {
            return DataController.ExecTable("select matttc, tentttc, soluong, tennguyenlieu, thanhtien from tttienchi, nguyenlieu where tttienchi.manguyenlieu = nguyenlieu.manguyenlieu and tttienchi.maphieuchi = " + id);
        }

        static public DataRow findTienChiNguyenLieuByID(string id)
        {
            DataTable dt = DataController.ExecTable("select * from tttienchi, nguyenlieu where tttienchi.manguyenlieu = nguyenlieu.manguyenlieu and matttc = " + id);
            return dt.Rows[0];
        }

        static public DataTable fetchTienChiHocLieu(string id)
        {
            return DataController.ExecTable("select matttc, tentttc, soluong, tenhoclieu, thanhtien from tttienchi, hoclieu where tttienchi.mahoclieu = hoclieu.mahoclieu and tttienchi.maphieuchi = " + id);
        }

        static public DataRow findTienChiHocLieuByID(string id)
        {
            DataTable dt = DataController.ExecTable("select * from tttienchi, hoclieu where tttienchi.mahoclieu = hoclieu.mahoclieu and matttc = " + id);
            return dt.Rows[0];
        }

        static public DataTable fetchTienChiKhac(string id)
        {
            return DataController.ExecTable("select matttc, tentttc, soluong, tongchiphi from tttienchi, khoanchikhac where tttienchi.makhoanchikhac = khoanchikhac.makhoanchikhac and maphieuchi = " + id);
        }

        static public DataRow findTienChiKhacByID(string id)
        {
            DataTable dt = DataController.ExecTable("select * from tttienchi, khoanchikhac where tttienchi.makhoanchikhac = khoanchikhac.makhoanchikhac and matttc = " + id);
            return dt.Rows[0];

        }

        static public void addTienChiNguyenLieu(string ten, string soluong, string thanhtien, string maphieuchi, int manl)
        {
            DataController.Execute("inserttttienchi " +
                "N'" + ten +"', " +
                soluong + ", " +
                thanhtien + ", " +
                maphieuchi + ", " +
                manl + ", " +
                "1");
        }

        static public void updateTienChiNguyenLieu(string id, string ten, string soluong, string thanhtien, string maphieuchi, int manl)
        {
            DataController.Execute("update tttienchi set " +
                "tentttc = N'" + ten + "', " +
                "soluong = " +
                soluong + ", " +
                "thanhtien = " +
                thanhtien + ", " +
                "maphieuchi = " +
                maphieuchi + ", " +
                "manguyenlieu = " +
                manl + " " +
                "where matttc = " + id);
        }

        static public void addTienChiHocLieu(string ten, string soluong, string thanhtien, string maphieuchi, int mahl)
        {
            DataController.Execute("inserttttienchi " +
                "N'" + ten + "', " +
                soluong + ", " +
                thanhtien + ", " +
                maphieuchi + ", " +
                mahl + ", " +
                "2");
        }

        static public void updateTienChiHocLieu(string id, string ten, string soluong, string thanhtien, string maphieuchi, int mahl)
        {
            DataController.Execute("update tttienchi set " +
                "tentttc = N'" + ten + "', " +
                "soluong = " +
                soluong + ", " +
                "thanhtien = " +
                thanhtien + ", " +
                "maphieuchi = " +
                maphieuchi + ", " +
                "mahoclieu = " +
                mahl + " " +
                "where matttc = " + id);
        }

        static public void addTienChiKhac(string ten, string soluong, string thanhtien, string maphieuchi)
        {
            DataController.Execute("inserttttienchi " +
                "N'" + ten + "', " +
                soluong + ", " +
                thanhtien + ", " +
                maphieuchi + ", " +
                1 + ", " +
                "3");
        }

        static public void updateTienChiKhac(string id, string ten, string soluong, string thanhtien, string maphieuchi)
        {
            DataController.Execute("update tttienchi set " +
                "tentttc = N'" + ten + "', " +
                "soluong = " +
                soluong + ", " +
                "thanhtien = " +
                thanhtien + ", " +
                "maphieuchi = " +
                maphieuchi + ", " +
                "makhoanchikhac = " +
                1 + " " +
                "where matttc = " + id);
        }
        
        static public void deleteTienChi(string id)
        {
            DataController.Execute("delete from tttienchi where matttc = " + id);
        }



    }
}
