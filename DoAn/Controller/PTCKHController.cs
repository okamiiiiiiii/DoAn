using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DoAn.Controller
{
    class PTCKHController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select maphuthucokehoach, tenphuthucokehoach, tenkhoi, tienmoihocsinh from phuthucokehoach, khoi where phuthucokehoach.makhoiapdung = khoi.makhoi");
        }

        static public DataRow findbyID(string id)
        {
            return DataController.ExecTable("select * from khoi, phuthucokehoach where phuthucokehoach.makhoiapdung = khoi.makhoi and maphuthucokehoach = " + id).Rows[0];
        }

        static public void addPTCKH(string ten, int makhoi, string tien)
        {
            DataController.Execute("insert phuthucokehoach values(" +
                "N'" + ten + "'," +
                makhoi + ", " +
                tien + ", " +
                "dbo.getmabctt2())");
        }

        static public void updatePTCKH(string id, string ten, int makhoi, string tien)
        {
            DataController.Execute("update phuthucokehoach set " +
                "tenphuthucokehoach = N'" + ten + "', " +
                "makhoiapdung = " +
                makhoi + ", " +
                "tienmoihocsinh = " + tien + 
                " where maphuthucokehoach = " + id);
        }

        static public void deletePTCKH(string id)
        {
            DataController.Execute("delete from phuthucokehoach where maphuthucokehoach = " + id);
        }
    }
}
