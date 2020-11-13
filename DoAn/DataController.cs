using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAn
{
    class DataController
    {
        private static string connStr = @"Data Source=21AK22-COM;Initial Catalog=mamnonsaoviet2;Integrated Security=True";

        public static DataTable ExecTable(string query)
        {
            Console.WriteLine(query);
            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            conn.Close();
            return dt;
        }

        public static void Execute(string query)
        {
            Console.WriteLine(query);
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
