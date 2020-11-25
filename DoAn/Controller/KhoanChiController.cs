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
        static public DataTable fetchTienChiNguyenLieu()
        {
            return DataController.ExecTable("select matttc, tentttc, soluong, tennguyenlieu, thanhtien from tttienchi, nguyenlieu where tttienchi.manguyenlieu = nguyenlieu.manguyenlieu");
        }

        static public DataRow findTienChiNguyenLieuByID(string id)
        {
            DataTable dt = DataController.ExecTable("select matttc, tentttc, soluong, tennguyenlieu, thanhtien from tttienchi, nguyenlieu where tttienchi.manguyenlieu = nguyenlieu.manguyenlieu where matttc = " + id);
            return dt.Rows[0];
        }
    }
}
