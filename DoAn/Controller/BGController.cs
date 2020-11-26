using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class BGController
    {
        static public DataTable indexNL(string idPBG)
        {
            return DataController.ExecTable(" select mattbangiao, ttbangiao.soluong, tennguyenlieu from ttbangiao, nguyenlieu where ttbangiao.manguyenlieu = nguyenlieu.manguyenlieu and maphieubangiao = " + idPBG);
        }

        static public DataTable indexHL(string idPBG)
        {
            return DataController.ExecTable(" select mattbangiao, ttbangiao.soluong, tenhoclieu from ttbangiao, hoclieu where ttbangiao.mahoclieu = hoclieu.mahoclieu and maphieubangiao = " + idPBG);
        }

        static public DataRow findByID(string id)
        {
            return DataController.ExecTable("select * from ttbangiao, nguyenlieu where mattbangiao = " + id).Rows[0];
        }
        
        static public void addBGNL(string soluongbangiao, int manguyenlieu, string maPBG)
        {
            DataController.Execute("insert into ttbangiao values(" +
                soluongbangiao + ", " +
                +manguyenlieu + ", " +
                "null, " +
                maPBG + ")");
        }

        static public void addBGHL(string soluongbangiao, int mahoclieu, string maPBG)
        {
            DataController.Execute("insert into ttbangiao values(" +
                soluongbangiao + ", " +
                "null, " +
                + mahoclieu + ", " +
                maPBG + ")");
        }
    }
}
