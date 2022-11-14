using CSharp_QuanLiBanSanGo.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CSharp_QuanLiBanSanGo
{
    public partial class frmKhachHang : Form
    {
        DBconfig dtBase = new DBconfig();
        string queryLoad = "SELECT * FROM tKhachHang";

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private bool isCheck()
        {
            if (txtMaKhach.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mã khách");
                txtMaKhach.Focus();

                return false;
            }

            if (txtTenKhach.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên khách hàng");
                txtTenKhach.Focus();

                return false;
            }

            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ");
                txtDiaChi.Focus();

                return false;
            }

            if (txtDienThoai.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại");
                txtDienThoai.Focus();

                return false;
            }

            return true;
        }

        private void CleanInput()
        {
            txtMaKhach.Text = "";
            txtTenKhach.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            DataTable dtKhachHang = dtBase.table(queryLoad);
            dgvKhachHang.DataSource = dtKhachHang;

            dgvKhachHang.Columns[0].HeaderText = "Mã khách hàng";
            dgvKhachHang.Columns[1].HeaderText = "Tên khách hàng";
            dgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns[3].HeaderText = "Điện thoại";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            CleanInput();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKhach.Text = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
            txtTenKhach.Text = dgvKhachHang.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells[2].Value.ToString();
            txtDienThoai.Text = dgvKhachHang.CurrentRow.Cells[3].Value.ToString();

            txtMaKhach.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnTimKiem.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            CleanInput();

            btnThem.Enabled = true;
            btnTimKiem.Enabled = true;
            txtMaKhach.Enabled = true;

            dgvKhachHang.DataSource = dtBase.table(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                DataTable dtKhachHang = dtBase.table("SELECT * FROM tKhachHang WHERE" + " MaKhach ='" + (txtMaKhach.Text).Trim() + "'");
                if (dtKhachHang.Rows.Count > 0)
                {
                    MessageBox.Show("Mã khách này đã có, hãy nhập mã khách hàng khác");
                    txtMaKhach.Focus();
                }
                else
                {
                    try
                    {
                        if (MessageBox.Show("Bạn có muốn thêm khách hàng này vào danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            dtBase.Excute($"INSERT INTO tKhachHang VALUES(N'" + txtMaKhach.Text + "', N'" + txtTenKhach.Text + "', N'" + txtDiaChi.Text + "', N'" + txtDienThoai.Text + "')");
                            MessageBox.Show("Bạn đã thêm khách hàng thành công");

                            dgvKhachHang.DataSource = dtBase.table(queryLoad);

                            CleanInput();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                try
                {
                    if (MessageBox.Show("Bạn có sửa thông tin nhà khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        dtBase.Excute($"UPDATE tKhachHang SET TenKhach = N'" + txtTenKhach.Text + "' WHERE MaNCC = '" + txtMaKhach.Text + "'");
                        dtBase.Excute($"UPDATE tKhachHang SET DiaChi = N'" + txtDiaChi.Text + "' WHERE MaNCC = '" + txtMaKhach.Text + "'");
                        dtBase.Excute($"UPDATE tKhachHang SET DienThoai = N'" + txtDienThoai.Text + "' WHERE MaNCC = '" + txtMaKhach.Text + "'");

                        CleanInput();

                        dgvKhachHang.DataSource = dtBase.table(queryLoad);
                        MessageBox.Show("Bạn đã sửa thông tin thành công");
                        txtMaKhach.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa nhà khách hàng có mã là: " + txtMaKhach.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dtBase.Excute($"DELETE tKhachHang WHERE MaKhach ='" + txtMaKhach.Text + "'");
                dgvKhachHang.DataSource = dtBase.table(queryLoad);

                CleanInput();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string dk = "";

            if (txtMaKhach.Text.Trim() != "")
            {
                dk += " AND MaKhach LIKE N'%" + txtMaKhach.Text + "%'";
            }

            if (txtTenKhach.Text.Trim() != "")
            {
                dk += " AND TenKhach LIKE N'%" + txtTenKhach.Text + "%'";
            }

            if (txtDiaChi.Text.Trim() != "")
            {
                dk += " AND DiaChi LIKE N'%" + txtDiaChi.Text + "%'";
            }

            if (txtDienThoai.Text.Trim() != "")
            {
                dk += " AND DienThoai LIKE N'%" + txtDienThoai.Text + "%'";
            }

            if (dk != "")
            {
                string find = queryLoad + " WHERE MaKhach LIKE N'%KH%'" + dk;
                dgvKhachHang.DataSource = dtBase.table(find);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];
                exSheet.StandardWidth = 12;
                exSheet.get_Range("B2").Font.Bold = true;
                exSheet.get_Range("B2").Value = "Danh sách khách hàng";
                exSheet.get_Range("A3").Value = "Số thứ tự";
                exSheet.get_Range("B3").Value = "Mã khách hàng";
                exSheet.get_Range("C3").Value = "Tên khách hàng";
                exSheet.get_Range("D3").Value = "Địa chỉ";
                exSheet.get_Range("E3").Value = "Điện thoại";

                int n = dgvKhachHang.Rows.Count;

                for (int i = 0; i < n - 1; i++)
                {
                    exSheet.get_Range("A" + (i + 4).ToString()).Value = (i + 1).ToString();
                    exSheet.get_Range("B" + (i + 4).ToString()).Value = dgvKhachHang.Rows[i].Cells[0].Value;
                    exSheet.get_Range("C" + (i + 4).ToString()).Value = dgvKhachHang.Rows[i].Cells[1].Value;
                    exSheet.get_Range("D" + (i + 4).ToString()).Value = dgvKhachHang.Rows[i].Cells[2].Value;
                    exSheet.get_Range("E" + (i + 4).ToString()).Value = dgvKhachHang.Rows[i].Cells[3].Value;
                }

                exBook.Activate();
                SaveFileDialog excelFile = new SaveFileDialog();
                excelFile.Title = "Lưu Excel";
                excelFile.Filter = "Sổ làm việc Excel|*.xlsx|Tất cả tệp|*.*";
                excelFile.FileName = "DanhSachKhachHang";
                excelFile.ShowDialog();
                exBook.SaveAs(excelFile.FileName.ToString());
                exApp.Quit();
            }
            catch
            {

            }
        }
    }
}
