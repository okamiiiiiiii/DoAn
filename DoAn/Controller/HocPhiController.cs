using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DoAn.Controller
{
    class HocPhiController
    {
        static public DataTable fetchKhoi()
        {
            return DataController.ExecTable("select makhoi, tenkhoi, tenphanloai from khoi, phanloai where khoi.maphanloai = phanloai.maphanloai");
        }

        static public DataTable fetchPhanLoai()
        {
            return DataController.ExecTable("select * from phanLoai");
        }

        static public DataRow findKhoi(string id)
        {
            DataTable dt = DataController.ExecTable("select * from khoi, phanloai where makhoi = " + id + " " +
                "and khoi.maphanloai = phanloai.maphanloai");
            return dt.Rows[0];
        }

        static public DataRow findPhanLoai(string id)
        {
            DataTable dt = DataController.ExecTable("select * from phanLoai where maphanloai = " + id);
            return dt.Rows[0];
        }

        static public void addKhoi(string ten, int maphanloai)
        {
            DataController.Execute("insert into khoi values(N'" + ten + "', N'', " + maphanloai + ")");
        }   

        static public void updateKhoi(string id, string ten, int maphanloai)
        {
            DataController.Execute("update khoi set tenkhoi = N'" + ten +
                "', maphanloai = " + maphanloai +
                " where makhoi = " + id);
        }

        static public void deleteKhoi(string id)
        {
            DataTable dt1 = DataController.ExecTable("select * from hocsinh where makhoi = " + id);
            DataTable dt2 = DataController.ExecTable("select * from phutrach where makhoi = " + id);
            Console.WriteLine(dt1.Rows.Count + ", " + dt2.Rows.Count);
            if (dt1.Rows.Count == 0 && dt2.Rows.Count == 0)
            {
                DataController.Execute("delete khoi where makhoi = " + id);
            }
            else
                MessageBox.Show("Vẫn còn học sinh thuộc khối này, không thể xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static public void addPhanLoai(string ten, string gia)
        {
            DataController.Execute("insert into phanloai values(N'" + ten + "', " + gia + ")");
        }

        static public void updatePhanLoai(string id, string ten, string gia)
        {
            DataController.Execute("update phanloai set " +
                "tenphanloai = N'" + ten +
                "', hocphi = " + gia +
                " where maphanloai = " + id);
        }

        static public void deletePhanLkoai(string id)
        {
            DataTable dt = DataController.ExecTable("select * from khoi where maphanloai = " + id);
            if (dt.Rows.Count == 0)
            {
                DataController.Execute("delete phanloai where maphanloai = " + id);
            }
            else
                MessageBox.Show("Vẫn còn khối thuộc phân loại này, không thể xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            
        }
    }
}
