using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class DuLieuController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select * from dulieu");
        }

        static public void updateDuLieu(string id, string ten, string giatri)
        {
            DataController.Execute("update dulieu set " +
                "tendulieu = N'" + ten + "', " +
                "giatri = " + giatri + " " +
                "where iddulieu = " + id);
        }
    }
}
