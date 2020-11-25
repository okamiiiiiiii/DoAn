using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class BaoCaoThongKe
    {
        public DataTable DiemDanh(string tg, string makhoi)
        {
            return DataController.ExecTable("select " +
                "tg, " +
                "vangmat, " +
                "hocsinh.mahocsinh, " +
                "tenhocsinh, " +
                "tenhinhthucan, " +
                "tennv, " +
                "tenkhoi " +
                "from " +
                "theodoixuatan, hocsinh, hinhthucan, nhanvien, khoi, phutrach " +
                "where hocsinh.mahocsinh = theodoixuatan.mahocsinh " +
                "and hinhthucan.mahinhthucan = theodoixuatan.mahinhthucan " +
                "and hocsinh.makhoi = khoi.makhoi " +
                "and phutrach.makhoi = khoi.makhoi " +
                "and nhanvien.manv = phutrach.manv" +
                "and tg = " + tg +
                " and makhoi = " + makhoi);
        }
    }
}
