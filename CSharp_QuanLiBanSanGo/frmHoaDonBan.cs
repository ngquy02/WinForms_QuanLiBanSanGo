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
        string queryLoad = "SELECT * FROM View_HoaDonBan";

        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private bool checkValidation()
        {
            if (txtSoHDB.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số hoá đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSoHDB.Focus();

                return false;
            }

            if (cboNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboNhanVien.Focus();

                return false;
            }

            if (cboKhachHang.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboKhachHang.Focus();

                return false;
            }

            return true;
        }

        private void refreshInput()
        {
            txtSoHDB.Text = "";
            cboNhanVien.Text = "";
            dtpNgayBan.Text = "";
            cboKhachHang.Text = "";
            txtTongTien.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void load_NhanVien()
        {
            cboNhanVien.DataSource = dtBase.getTable("SELECT * FROM tNhanVien");
            cboNhanVien.ValueMember = "MaNV";
            cboNhanVien.DisplayMember = "TenNV";
        }

        private void load_KhachHang()
        {
            cboKhachHang.DataSource = dtBase.getTable("SELECT * FROM tKhachHang");
            cboKhachHang.ValueMember = "MaKhach";
            cboKhachHang.DisplayMember = "TenKhach";
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            DataTable dtHDB = dtBase.getTable(queryLoad);
            dgvHoaDonBan.DataSource = dtHDB;

            dgvHoaDonBan.Columns[0].HeaderText = "Số hoá đơn bán";
            dgvHoaDonBan.Columns[1].HeaderText = "Nhân viên";
            dgvHoaDonBan.Columns[2].HeaderText = "Ngày bán";
            dgvHoaDonBan.Columns[3].HeaderText = "Khách hàng";
            dgvHoaDonBan.Columns[4].HeaderText = "Tổng tiền";

            load_NhanVien();
            load_KhachHang();

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            refreshInput();
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

        private void dgvHoaDonBan_DoubleClick(object sender, EventArgs e)
        {
            string soHDB = dgvHoaDonBan.CurrentRow.Cells[0].Value.ToString();
            string query = "SELECT tChiTietHoaDonBan.SoHDB, TenHangHoa, tChiTietHoaDonBan.SoLuong, GiamGia, ThanhTien FROM tChiTietHoaDonBan JOIN tHoaDonBan ON tChiTietHoaDonBan.SoHDB = tHoaDonBan.SoHDB JOIN tDMHangHoa ON tChiTietHoaDonBan.MaHang = tDMHangHoa.MaHang WHERE tChiTietHoaDonBan.SoHDB = N'" + txtSoHDB.Text + "'";

            frmChiTietHDB chiTietHDB = new frmChiTietHDB(soHDB, query);
            chiTietHDB.ShowDialog();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refreshInput();

            btnThem.Enabled = true;
            btnTimKiem.Enabled = true;
            txtSoHDB.Enabled = true;

            dgvHoaDonBan.DataSource = dtBase.getTable(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtNhanVien = dtBase.getTable($"SELECT * FROM tHoaDonBan WHERE SoHDB = N'{txtSoHDB.Text}'");

                if (dtNhanVien.Rows.Count > 0)
                {
                    MessageBox.Show("Số hoá đơn này đã có, hãy nhập số hoá đơn khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSoHDB.Focus();
                }
                else
                {
                    try
                    {
                        if (MessageBox.Show("Bạn có muốn thêm hoá đơn này vào không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dtBase.getExcute($"INSERT INTO tHoaDonBan(SoHDB, MaNV, NgayBan, MaKhach) VALUES(N'{txtSoHDB.Text}', N'{cboNhanVien.SelectedValue}', N'{dtpNgayBan.Text} ', N'{cboKhachHang.SelectedValue}')");
                            MessageBox.Show("Bạn đã thêm hoá đơn thành công");

                            dgvHoaDonBan.DataSource = dtBase.getTable(queryLoad);

                            refreshInput();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                if (MessageBox.Show("Bạn có sửa thông tin hoá đơn này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dtBase.getExcute($"UPDATE tHoaDonBan SET MaNV = N'{cboNhanVien.SelectedValue}' WHERE SoHDB = '{txtSoHDB.Text}'");
                        dtBase.getExcute($"UPDATE tHoaDonBan SET NgayBan = N'{dtpNgayBan.Text}' WHERE SoHDB = '{txtSoHDB.Text}'");
                        dtBase.getExcute($"UPDATE tHoaDonBan SET MaKhach = N'{cboKhachHang.SelectedValue}' WHERE SoHDB = '{txtSoHDB.Text}'");

                        refreshInput();

                        dgvHoaDonBan.DataSource = dtBase.getTable(queryLoad);
                        MessageBox.Show("Bạn đã sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSoHDB.Enabled = true;
                    }
                    catch(Exception ex )
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa hoá đơn mã là: " + txtSoHDB.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    dtBase.getExcute($"DELETE tHoaDonBan WHERE SoHDB ='{txtSoHDB.Text}'");
                    dgvHoaDonBan.DataSource = dtBase.getTable(queryLoad);
                    MessageBox.Show("Đã xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    refreshInput();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string dk = "";

            if (txtSoHDB.Text.Trim() != "")
            {
                dk += $" AND SoHDB LIKE N'{txtSoHDB.Text}%'";
            }

            if (cboKhachHang.Text.Trim() != "")
            {
                dk += $" AND TenKhach LIKE N'%{cboKhachHang.Text}%'";
            }

            if (dk != "")
            {
                string find = queryLoad + " WHERE SoHDB LIKE N'%HDB%'" + dk;
                dgvHoaDonBan.DataSource = dtBase.getTable(find);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog excelFile = new SaveFileDialog();
            excelFile.Title = "Lưu Excel";
            excelFile.Filter = "Sổ làm việc Excel|*.xlsx|Tất cả tệp|*.*";
            excelFile.FileName = "DanhSachHDB";

            if(excelFile.ShowDialog() == DialogResult.OK)
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

                    for (int i = 0; i < dgvHoaDonBan.Rows.Count; i++)
                    {
                        exSheet.get_Range("A" + (i + 4).ToString()).Value = (i + 1).ToString();
                        exSheet.get_Range("B" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[0].Value;
                        exSheet.get_Range("C" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[1].Value;
                        exSheet.get_Range("D" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[2].Value;
                        exSheet.get_Range("E" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[3].Value;
                        exSheet.get_Range("F" + (i + 4).ToString()).Value = dgvHoaDonBan.Rows[i].Cells[4].Value;
                    }

                    exSheet.Columns.AutoFit();
                    exBook.SaveAs(excelFile.FileName.ToString());
                    exApp.ActiveWorkbook.Saved = true;
                    MessageBox.Show("Đã xuất Excel thành công");
                }
                catch
                {

                }
            }
        }
    }
}
