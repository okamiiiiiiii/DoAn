using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class ChucVuController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select * from chucvu");
        }

        static public DataRow findOneByID(string id)
        {
            return DataController.ExecTable("select * from chucvu where machucvu = " + id).Rows[0];
        }
    }
}
