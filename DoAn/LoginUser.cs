using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn
{
    class LoginUser
    {
        public static int manv = 1;
        public static DataRow UserData()
        {
            return DataController.ExecTable("select * from nhanvien where manv = " + manv.ToString()).Rows[0];
        }
    }
}
