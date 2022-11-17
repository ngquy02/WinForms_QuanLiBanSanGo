using CSharp_QuanLiBanSanGo.Model;
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
                
                try
                {
                    sqlDataAdapter.Fill(dataTable);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

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

        public void Command(HangHoa hangHoa, string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(STRING_CONNECT))
            {
                sqlConnection.Open();
                sqlCommand= new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.Add("@MaHang", hangHoa.MaHang);
                sqlCommand.Parameters.Add("@TenHang", hangHoa.TenHang);
                sqlCommand.Parameters.Add("@MaLoaiGo", hangHoa.MaLoaiGo);
                sqlCommand.Parameters.Add("@MaKichThuoc", hangHoa.MaKichThuoc);
                sqlCommand.Parameters.Add("@MaDacDiem", hangHoa.MaDacDiem);
                sqlCommand.Parameters.Add("@MaCongDung", hangHoa.MaCongDung);
                sqlCommand.Parameters.Add("@MaMau", hangHoa.MaMau);
                sqlCommand.Parameters.Add("@MaNuocSX", hangHoa.MaNuocSX);
                sqlCommand.Parameters.Add("@SoLuong", hangHoa.SoLuong);
                sqlCommand.Parameters.Add("@DonGiaNhap", hangHoa.DonGiaNhap);
                sqlCommand.Parameters.Add("@ThoiGianBaoHanh", hangHoa.ThoiGianBaoHanh);
                sqlCommand.Parameters.Add("@Anh", hangHoa.Anh);
                sqlCommand.Parameters.Add("@GhiChu", hangHoa.GhiChu);
                
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
