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
        string STRING_CONNECT = @"Data Source=DESKTOP-2TPJP92\SQLEXPRESS;Initial Catalog=QuanLiBanSanGo;Integrated Security=True";
        private SqlDataAdapter sqlDataAdapter;
        private SqlCommand sqlCommand;

        public DataTable table(string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(STRING_CONNECT))
            {
                sqlConnection.Open();
                sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }

            return dataTable;
        }

        public void Excute(string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(STRING_CONNECT))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                sqlConnection.Close();
            }
        }
    }
}
