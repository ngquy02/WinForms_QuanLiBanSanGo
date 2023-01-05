using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_QuanLiBanSanGo.Class
{
    internal class DBconfig
    {
        private SqlDataAdapter sqlDataAdapter;
        private SqlCommand sqlCommand;
        public string connectString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CSharp_QuanLiBanSanGo;Integrated Security=True";

        public DataTable getTable(string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(connectString))
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }

            return dataTable;
        }

        public void getExcute(string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
