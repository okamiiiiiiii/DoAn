using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class HinhThucAnController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select * from hinhthucan");
        }

        static public DataRow findOneById(string id)
        {
            DataTable dt = DataController.ExecTable("select * from hinhthucan where mahinhthucan = " + id);
            return dt.Rows[0];
        }

        static public void deleteHinhThucAn(string id)
        {
            DataController.Execute("delete from hinhthucan where mahinhthucan = " + id);
        }

        static public void updateHinhThucAn(string id, string ten, string gia)
        {
            DataController.Execute("update hinhthucan set " +
                "tenhinhthucan = N'" + ten + "', " +
                "giatien = " + gia + " " +
                "where mahinhthucan = " + id);
        }

        static public void addHinhThucAn(string ten, string gia)
        {
            DataController.Execute("insert into hinhthucan values" +
                "(N'" + ten + "', " +
                "" + gia + ")");
        }
    }
}
