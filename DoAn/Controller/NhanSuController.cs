using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DoAn.Controller
{
    class NhanSuController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select manv, tennv, gioitinh, tenchucvu, namsinh from nhanvien chucvu where nhanvien.manv = chucvu.machucvu");
        }

        static public DataRow findByID(string id)
        {
            return DataController.ExecTable("select * from nhanvien chucvu where nhanvien.manv = chucvu.machucvu and manv = " + id).Rows[0];
        }
    }
}
