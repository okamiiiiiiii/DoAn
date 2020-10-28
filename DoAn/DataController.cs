using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DoAn
{
    class DataController
    {
        private static string connStr = @"Data Source=MINHTHIEN\SQLEXPRESS;Initial Catalog=mamnonsaoviet;Integrated Security=True";

        public static DataTable ExecTable(string query)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            conn.Close();
            return dt;
        }
    }
}
