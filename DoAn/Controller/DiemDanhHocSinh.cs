using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class DiemDanhHocSinh
    {
        static public DataTable fetch(string khoi, string day)
        {
            string query = "select theodoixuatan.mahocsinh, " +
                "hocsinh.tenhocsinh, " +
                "hinhthucan.tenhinhthucan, " +
                "theodoixuatan.vangmat " +
                "from hocsinh, theodoixuatan, hinhthucan, khoi " +
                "where theodoixuatan.mahocsinh = hocsinh.mahocsinh " +
                "and theodoixuatan.mahinhthucan = hinhthucan.mahinhthucan " +
                "and theodoixuatan.tg = '" + day + "' " +
                "and hocsinh.makhoi = khoi.makhoi " +
                "and khoi.tenkhoi = N'" + khoi + "'";
            return DataController.ExecTable(query);
        }

        static public void DiemDanhVang(string mahocsinh)
        {
            string query = "update theodoixuatan set vangmat = 1, mahinhthucan = 3 where mahocsinh = " + mahocsinh +
                "and tg = '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
            DataController.Execute(query);
        }

        static public void DiemDanhCoMat(string mahocsinh)
        {
            string query = "update theodoixuatan set vangmat = 0 where mahocsinh = " + mahocsinh +
                "and tg = '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
            DataController.Execute(query);
        }
    }
}
