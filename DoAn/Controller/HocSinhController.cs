using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn.Controller
{
    class HocSinhController
    {
        static public DataTable index()
        {
            return DataController.ExecTable("select " +
                "mahocsinh, " +
                "tenhocsinh, " +
                "gioitinh, " +
                "ngaysinh, " +
                "khoi.tenkhoi, " +
                "CONVERT(varchar, CAST( tienmiengiam AS money),1) as tienmiengiam " +
                "from hocsinh, khoi " +
                "where hocsinh.makhoi = khoi.makhoi");
        }

        static public DataRow findOneById(string id)
        {
            DataTable dt = DataController.ExecTable("select * from hocsinh, khoi, phanloai " +
                "where hocsinh.makhoi = khoi.makhoi " +
                "and khoi.maphanloai = phanloai.maphanloai " +
                "and hocsinh.mahocsinh = " + id);
            return dt.Rows[0];
        }

        static public DataTable fetchKhoi()
        {
            return DataController.ExecTable("select * from khoi");
        }

        static public void addHocsinh (string ten, string diachi, string gioitinh, string ngaysinh, string tenphuhuynh, string sdtphuhuynh, string tienmiengiam, string ghichu, int makhoi, string namnhaphoc)
        {
            DataController.Execute("insert into hocsinh values (" +
                "N'" + ten + "', " +
                "N'" + diachi + "', " +
                "N'" + gioitinh + "', " +
                "'" + ngaysinh + "', " +
                namnhaphoc + ", " +
                "N'" + tenphuhuynh + "', " +
                "'" + sdtphuhuynh + "', " +
                "" + tienmiengiam + ", " +
                "N'" + ghichu + "', " +
                makhoi + ")");
        }

        static public void updateHocSinh(string id, string ten, string diachi, string gioitinh, string ngaysinh, string tenphuhuynh, string sdtphuhuynh, string tienmiengiam, string ghichu, int makhoi, string namnhaphoc)
        {
            DataController.Execute("update hocsinh set " +
                        "tenhocsinh = N'" + ten + "', " +
                        "diachi = N'" + diachi + "', " +
                        "gioitinh = N'" + gioitinh + "', " +
                        "ngaysinh = '" + ngaysinh + "', " +
                        "tenphuhuynh = N'" + tenphuhuynh + "', " +
                        "sdtphuhuynh = '" + sdtphuhuynh + "', " +
                        "makhoi = " + makhoi + ", " +
                        "tienmiengiam = " + tienmiengiam + ", " +
                        "ghichumiengiam = N'" + ghichu + "', " +
                        "namnhaphoc = " + namnhaphoc + " " +
                        "where mahocsinh = " + id);
        }

        static public void deleteHocSinh(string id)
        {
            DataController.Execute("deletehocsinh " + id);
        }
    }
}
