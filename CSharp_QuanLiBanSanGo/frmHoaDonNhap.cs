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
    public partial class frmHoaDonNhap : Form
    {
        DBconfig dtBase = new DBconfig();
        string queryLoad = "SELECT * FROM View_HoaDonNhap";

        public frmHoaDonNhap()
        {
            InitializeComponent();
        }

        private bool isCheck()
        {
            if (txtSoHDN.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số hoá đơn nhập");
                txtSoHDN.Focus();

                return false;
            }

            if (cboNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn nhân viên");
                cboNhanVien.Focus();

                return false;
            }

            if (cboNCC.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn nhà cung cấp");
                cboNCC.Focus();

                return false;
            }

            return true;
        }

        private void CleanInput()
        {
            txtSoHDN.Text = "";
            cboNhanVien.Text = "";
            cboNCC.Text = "";
            txtTongTien.Text = "";
        }

        private void load_NhanVien()
        {
            cboNhanVien.DataSource = dtBase.table("SELECT * FROM tNhanVien");
            cboNhanVien.ValueMember = "MaNV";
            cboNhanVien.DisplayMember = "TenNV";
        }

        private void load_NCC()
        {
            cboNCC.DataSource = dtBase.table("SELECT * FROM tNhaCungCap");
            cboNCC.ValueMember = "MaNCC";
            cboNCC.DisplayMember = "TenNCC";
        }

        private void frmHoaDonNhap_Load(object sender, EventArgs e)
        {
            DataTable dtHDB = dtBase.table(queryLoad);
            dgvHoaDonNhap.DataSource = dtHDB;

            dgvHoaDonNhap.Columns[0].HeaderText = "Số hoá đơn nhập";
            dgvHoaDonNhap.Columns[1].HeaderText = "Nhân viên";
            dgvHoaDonNhap.Columns[2].HeaderText = "Ngày nhập";
            dgvHoaDonNhap.Columns[3].HeaderText = "Nhà cung cấp";
            dgvHoaDonNhap.Columns[4].HeaderText = "Tổng tiền";

            load_NhanVien();
            load_NCC();

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            CleanInput();
        }

        private void dgvHoaDonNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSoHDN.Text = dgvHoaDonNhap.CurrentRow.Cells[0].Value.ToString();
            cboNhanVien.Text = dgvHoaDonNhap.CurrentRow.Cells[1].Value.ToString();
            dtpNgayNhap.Text = dgvHoaDonNhap.CurrentRow.Cells[2].Value.ToString();
            cboNCC.Text = dgvHoaDonNhap.CurrentRow.Cells[3].Value.ToString();
            txtTongTien.Text = dgvHoaDonNhap.CurrentRow.Cells[4].Value.ToString();

            txtSoHDN.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnTimKiem.Enabled = false;
        }

        private void dgvHoaDonNhap_DoubleClick(object sender, EventArgs e)
        {
            string soHDN = dgvHoaDonNhap.CurrentRow.Cells[0].Value.ToString();
            string query = "SELECT tChiTietHoaDonNhap.SoHDN, TenHangHoa, tChiTietHoaDonNhap.SoLuong, DonGia, GiamGia, ThanhTien FROM tChiTietHoaDonNhap JOIN tHoaDonNhap ON tChiTietHoaDonNhap.SoHDN = tHoaDonNhap.SoHDN JOIN tDMHangHoa ON tChiTietHoaDonNhap.MaHang = tDMHangHoa.MaHang WHERE tChiTietHoaDonNhap.SoHDN = N'" + txtSoHDN.Text + "'";

            frmChiTietHDN chiTietHDB = new frmChiTietHDN(soHDN, query);
            chiTietHDB.ShowDialog();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            CleanInput();

            btnThem.Enabled = true;
            btnTimKiem.Enabled = true;
            txtSoHDN.Enabled = true;

            dgvHoaDonNhap.DataSource = dtBase.table(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                DataTable dtHDN = dtBase.table($"SELECT * FROM tHoaDonNhap WHERE SoHDN ='{txtSoHDN.Text}'");

                if (dtHDN.Rows.Count > 0)
                {
                    MessageBox.Show("Số hoá đơn này đã có, hãy nhập số hoá đơn khác");
                    txtSoHDN.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm hoá đơn này vào không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        dtBase.Excute($"INSERT INTO tHoaDonNhap(SoHDN, MaNV, NgayNhap, MaNCC) VALUES(N'{txtSoHDN.Text}', N'{cboNhanVien.SelectedValue.ToString()}', N'{dtpNgayNhap.Text}', N'{cboNCC.SelectedValue.ToString()}')");
                        MessageBox.Show("Bạn đã thêm hoá đơn thành công");

                        dgvHoaDonNhap.DataSource = dtBase.table(queryLoad);

                        CleanInput();
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                if (MessageBox.Show("Bạn có sửa thông tin hoá đơn này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    dtBase.Excute($"UPDATE tHoaDonNhap SET MaNV = N'{cboNhanVien.SelectedValue.ToString()}' WHERE SoHDN = '{txtSoHDN.Text}'");
                    dtBase.Excute($"UPDATE tHoaDonNhap SET NgayNhap = N'{dtpNgayNhap.Text}' WHERE SoHDN = '{txtSoHDN.Text}'");
                    dtBase.Excute($"UPDATE tHoaDonNhap SET MaNCC = N'{cboNCC.SelectedValue.ToString()}' WHERE SoHDN = '{txtSoHDN.Text}'");

                    CleanInput();

                    dgvHoaDonNhap.DataSource = dtBase.table(queryLoad);
                    MessageBox.Show("Bạn đã sửa thông tin thành công");
                    txtSoHDN.Enabled = true;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa hoá đơn có mã là: " + txtSoHDN.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dtBase.Excute($"DELETE tHoaDonNhap WHERE SoHDN ='{txtSoHDN.Text}'");
                dgvHoaDonNhap.DataSource = dtBase.table(queryLoad);

                CleanInput();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string dk = "";

            if (txtSoHDN.Text.Trim() != "")
            {
                dk += $" AND SoHDB LIKE N'{txtSoHDN.Text}%'";
            }

            if (cboNCC.Text.Trim() != "")
            {
                dk += $" AND SoHDB LIKE N'{cboNCC.Text}%'";
            }

            if (dk != "")
            {
                string find = queryLoad + " WHERE SoHDN LIKE N'%HDN%'" + dk;
                dgvHoaDonNhap.DataSource = dtBase.table(find);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];
                exSheet.get_Range("B2").Font.Bold = true;
                exSheet.get_Range("B2").Value = "Danh sách hoá đơn nhập";
                exSheet.get_Range("A3").Value = "Số thứ tự";
                exSheet.get_Range("B3").Value = "Số hoá đơn nhập";
                exSheet.get_Range("C3").Value = "Nhân viên";
                exSheet.get_Range("D3").Value = "Ngày nhập";
                exSheet.get_Range("E3").Value = "Nhà cung cấp";
                exSheet.get_Range("F3").Value = "Tồng tiền";

                int n = dgvHoaDonNhap.Rows.Count;

                for (int i = 0; i < n - 1; i++)
                {
                    exSheet.get_Range("A" + (i + 4).ToString()).Value = (i + 1).ToString();
                    exSheet.get_Range("B" + (i + 4).ToString()).Value = dgvHoaDonNhap.Rows[i].Cells[0].Value;
                    exSheet.get_Range("C" + (i + 4).ToString()).Value = dgvHoaDonNhap.Rows[i].Cells[1].Value;
                    exSheet.get_Range("D" + (i + 4).ToString()).Value = dgvHoaDonNhap.Rows[i].Cells[2].Value;
                    exSheet.get_Range("E" + (i + 4).ToString()).Value = dgvHoaDonNhap.Rows[i].Cells[3].Value;
                    exSheet.get_Range("F" + (i + 4).ToString()).Value = dgvHoaDonNhap.Rows[i].Cells[4].Value;
                }

                exSheet.Columns.AutoFit();
                SaveFileDialog excelFile = new SaveFileDialog();
                excelFile.Title = "Lưu Excel";
                excelFile.Filter = "Sổ làm việc Excel|*.xlsx|Tất cả tệp|*.*";
                excelFile.FileName = "DanhSachHDN";
                excelFile.ShowDialog();
                exBook.SaveAs(excelFile.FileName.ToString());
                MessageBox.Show("Xuất Excel thành công");
            }
            catch
            {

            }
        }
    }
}
