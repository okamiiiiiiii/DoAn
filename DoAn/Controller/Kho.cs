using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DoAn.Controller
{
    class Kho
    {
        static public DataTable fetchNguyenLieu()
        {
            return DataController.ExecTable("select * from nguyenlieu");
        }

        static public DataTable fetchHocLieu()
        {
            return DataController.ExecTable("select * from hoclieu");
        }

        static public DataRow hoclieu(string id)
        {
            return DataController.ExecTable("select * from hoclieu where mahoclieu = " + id).Rows[0];
        }
        static public DataRow nguyenlieu(string id)
        {
            return DataController.ExecTable("select * from nguyenlieu where manguyenlieu = " + id).Rows[0];
        }

        static public void addHocLieu(string ten, string soluong, string ghichu)
        {
            DataController.Execute("insert into hoclieu values(" +
                "N'" + ten +"', " +
                "N'" + ghichu + "', " +
                "" + soluong + ")");
        }

        static public void addNguyenLieu(string ten, string soluong, string ghichu)
        {
            DataController.Execute("insert into nguyenlieu values(" +
                "N'" + ten + "', " +
                "N'" + ghichu + "', " +
                "" + soluong + ")");
        }
        
        static public void updateNguyenLieu(string id, string ten, string soluong, string ghichu)
        {
            DataController.Execute("update nguyenlieu set " +
                "tennguyenlieu = N'" + ten +"', " +
                "soluong = " + soluong + ", " +
                "ghichu = N'" + ghichu + "' " +
                "where manguyenlieu = " + id);
        }

        static public void updateHocLieu(string id, string ten, string soluong, string ghichu)
        {
            DataController.Execute("update hoclieu set " +
                "tenhoclieu = N'" + ten + "', " +
                "soluong = " + soluong + ", " +
                "ghichu = N'" + ghichu + "' " +
                "where mahoclieu = " + id);
        }

        static public void deleteNguyenLieu(string id)
        {
            DataTable dt = DataController.ExecTable("select * from tttienchi where manguyenlieu = " + id);
            if (dt.Rows.Count == 0)
            {
                DataController.Execute("delete from nguyenlieu where manguyenlieu = " + id);
            }
            else
                MessageBox.Show("Vẫn còn phiếu chi thuộc loại nguyên liệu này, không thể xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static public void deleteHocLieu(string id)
        {
            DataTable dt = DataController.ExecTable("select * from tttienchi where mahoclieu = " + id);
            if (dt.Rows.Count == 0)
            {
                DataController.Execute("delete from hoclieu where mahoclieu = " + id);
            }
            else
                MessageBox.Show("Vẫn còn phiếu chi thuộc loại học liệu này, không thể xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


    }
}
