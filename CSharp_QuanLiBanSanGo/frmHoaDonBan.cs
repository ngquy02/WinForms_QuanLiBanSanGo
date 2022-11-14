using CSharp_QuanLiBanSanGo.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CSharp_QuanLiBanSanGo
{
    public partial class frmHoaDonBan : Form
    {
        DBconfig dtBase = new DBconfig();
        string queryLoad = "SELECT SoHDB, TenNV, NgayBan, TenKhach, TongTien FROM tHoaDonBan JOIN tNhanVien ON tHoaDonBan.MaNV = tNhanVien.MaNV JOIN tKhachHang ON tHoaDonBan.MaKhach = tKhachHang.MaKhach";

        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private bool isCheck()
        {
            if (txtSoHDB.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số hoá đơn");
                txtSoHDB.Focus();

                return false;
            }

            if (cboNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn nhân viên");
                cboNhanVien.Focus();

                return false;
            }

            if (cboKhachHang.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn khách hàng");
                cboKhachHang.Focus();

                return false;
            }

            return true;
        }

        private void CleanInput()
        {
            txtSoHDB.Text = "";
            cboNhanVien.Text = "";
            dtpNgayBan.Text = "";
            cboKhachHang.Text = "";
            txtTongTien.Text = "";
        }

        private void load_NhanVien()
        {
            cboNhanVien.DataSource = dtBase.table("SELECT * FROM tNhanVien");
            cboNhanVien.ValueMember = "MaNV";
            cboNhanVien.DisplayMember = "TenNV";
        }

        private void load_KhachHang()
        {
            cboKhachHang.DataSource = dtBase.table("SELECT * FROM tKhachHang");
            cboKhachHang.ValueMember = "MaKhach";
            cboKhachHang.DisplayMember = "TenKhach";
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            DataTable dtHDB = dtBase.table(queryLoad);
            dgvHoaDonBan.DataSource = dtHDB;

            dgvHoaDonBan.Columns[0].HeaderText = "Số hoá đơn bán";
            dgvHoaDonBan.Columns[1].HeaderText = "Nhân viên";
            dgvHoaDonBan.Columns[2].HeaderText = "Ngày bán";
            dgvHoaDonBan.Columns[3].HeaderText = "Khách hàng";
            dgvHoaDonBan.Columns[4].HeaderText = "Tổng tiền";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            load_NhanVien();
            load_KhachHang();

            CleanInput();
        }

        private void dgvHoaDonBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSoHDB.Text = dgvHoaDonBan.CurrentRow.Cells[0].Value.ToString();
            cboNhanVien.Text = dgvHoaDonBan.CurrentRow.Cells[1].Value.ToString();
            dtpNgayBan.Text = dgvHoaDonBan.CurrentRow.Cells[2].Value.ToString();
            cboKhachHang.Text = dgvHoaDonBan.CurrentRow.Cells[3].Value.ToString();
            txtTongTien.Text = dgvHoaDonBan.CurrentRow.Cells[4].Value.ToString();

            txtSoHDB.Enabled = false;
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
            txtSoHDB.Enabled = true;

            dgvHoaDonBan.DataSource = dtBase.table(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                DataTable dtNhanVien = dtBase.table("SELECT * FROM tHoaDonBan WHERE" + " SoHDB ='" + (txtSoHDB.Text).Trim() + "'");

                if (dtNhanVien.Rows.Count > 0)
                {
                    MessageBox.Show("Số hoá đơn này đã có, hãy nhập số hoá đơn khác");
                    txtSoHDB.Focus();
                }
                else
                {
                    try
                    {
                        if (MessageBox.Show("Bạn có muốn thêm hoá đơn này vào không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            dtBase.Excute($"INSERT INTO tHoaDonBan VALUES(N'" + txtSoHDB.Text + "', N'" + cboNhanVien.SelectedValue.ToString() + "', N'" + dtpNgayBan.Text + "', N'" + cboKhachHang.SelectedValue.ToString() + "', NULL)");
                            MessageBox.Show("Bạn đã thêm hoá đơn thành công");

                            dgvHoaDonBan.DataSource = dtBase.table(queryLoad);

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
                    if (MessageBox.Show("Bạn có sửa thông tin hoá đơn này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        dtBase.Excute($"UPDATE tHoaDonBan SET MaNV = N'" + cboNhanVien.SelectedValue.ToString() + "' WHERE SoHDB = '" + txtSoHDB.Text + "'");
                        dtBase.Excute($"UPDATE tHoaDonBan SET NgayBan = N'" + dtpNgayBan.Text + "' WHERE SoHDB = '" + txtSoHDB.Text + "'");
                        dtBase.Excute($"UPDATE tHoaDonBan SET NgaySinh = N'" + cboKhachHang.SelectedValue.ToString() + "' WHERE SoHDB = '" + txtSoHDB.Text + "'");

                        CleanInput();

                        dgvHoaDonBan.DataSource = dtBase.table(queryLoad);
                        MessageBox.Show("Bạn đã sửa thông tin thành công");
                        txtSoHDB.Enabled = true;
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
            if (MessageBox.Show("Bạn có muốn xóa hoá đơn mã là: " + txtSoHDB.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dtBase.Excute($"DELETE tHoaDonBan WHERE SoHDB ='" + txtSoHDB.Text + "'");
                dgvHoaDonBan.DataSource = dtBase.table(queryLoad);

                CleanInput();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string dk = "";

            if (txtSoHDB.Text.Trim() != "")
            {
                dk += " AND SoHDB LIKE N'%" + txtSoHDB.Text + "%'";
            }

            if (cboKhachHang.Text.Trim() != "")
            {
                dk += " AND TenKhach LIKE N'%" + cboKhachHang.Text + "%'";
            }

            if (dk != "")
            {
                string find = queryLoad + " WHERE SoHDB LIKE N'%HDB%'" + dk;
                dgvHoaDonBan.DataSource = dtBase.table(find);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];
                exSheet.get_Range("B2").Font.Bold = true;
                exSheet.get_Range("B2").Value = "Danh sách sản phẩm";
                exSheet.get_Range("A3").Value = "Số thứ tự";
                exSheet.get_Range("B3").Value = "Số hoá đơn bán";
                exSheet.get_Range("C3").Value = "Nhân viên";
                exSheet.get_Range("D3").Value = "Ngày bán";
                exSheet.get_Range("E3").Value = "Khách hàng";
                exSheet.get_Range("F3").Value = "Tổng tiền";

                int n = dgvHoaDonBan.Rows.Count;

                for (int i = 0; i < n - 1; i++)
                {
                    exSheet.get_Range("A" + (i + 4).ToString()).Value = (i + 1).ToString();
                    exSheet.get_Range("B" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[0].Value;
                    exSheet.get_Range("C" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[1].Value;
                    exSheet.get_Range("D" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[2].Value;
                    exSheet.get_Range("E" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[3].Value;
                    exSheet.get_Range("F" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[4].Value;
                }

                exSheet.Columns.AutoFit();
                SaveFileDialog excelFile = new SaveFileDialog();
                excelFile.Title = "Lưu Excel";
                excelFile.Filter = "Sổ làm việc Excel|*.xlsx|Tất cả tệp|*.*";
                excelFile.FileName = "DanhSachHDB";
                excelFile.ShowDialog();
                exBook.SaveAs(excelFile.FileName.ToString());

                MessageBox.Show("Đã xuất Excel thành công");
            }
            catch
            {

            }
        }
    }
}
