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
        DateTime date = new DateTime(1900, 01, 01, 0, 0, 0);

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

            if (dtpNgayBan.Value == date)
            {
                MessageBox.Show("Hãy nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpNgayBan.Focus();

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
            cboNhanVien.SelectedIndex = -1;
            dtpNgayBan.Value = date;
            cboKhachHang.SelectedIndex = -1;
            txtTongTien.Text = "";

            btnSua.Enabled = false;
            btnChiTiet.Enabled = false;
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
            dgvHoaDonBan.Columns[1].HeaderText = "Mã nhân viên";
            dgvHoaDonBan.Columns[2].HeaderText = "Nhân viên";
            dgvHoaDonBan.Columns[3].HeaderText = "Ngày bán";
            dgvHoaDonBan.Columns[4].HeaderText = "Mã khách hàng";
            dgvHoaDonBan.Columns[5].HeaderText = "Khách hàng";
            dgvHoaDonBan.Columns[6].HeaderText = "Tổng tiền (đồng)";

            load_NhanVien();
            load_KhachHang();

            refreshInput();
        }

        private void dgvHoaDonBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSoHDB.Text = dgvHoaDonBan.CurrentRow.Cells[0].Value.ToString();
            cboNhanVien.Text = dgvHoaDonBan.CurrentRow.Cells[2].Value.ToString();
            dtpNgayBan.Text = dgvHoaDonBan.CurrentRow.Cells[3].Value.ToString();
            cboKhachHang.Text = dgvHoaDonBan.CurrentRow.Cells[5].Value.ToString();
            txtTongTien.Text = dgvHoaDonBan.CurrentRow.Cells[6].Value.ToString();

            txtSoHDB.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnChiTiet.Enabled = true;
            btnXoa.Enabled = true;
            btnTimKiem.Enabled = false;
        }

        private void dgvHoaDonBan_DoubleClick(object sender, EventArgs e)
        {
            string soHDB = dgvHoaDonBan.CurrentRow.Cells[0].Value.ToString();
            string query = $"SELECT tChiTietHoaDonBan.SoHDB, tChiTietHoaDonBan.MaHang, TenHangHoa, tChiTietHoaDonBan.SoLuong, GiamGia, ThanhTien FROM tChiTietHoaDonBan JOIN tHoaDonBan ON tChiTietHoaDonBan.SoHDB = tHoaDonBan.SoHDB JOIN tDMHangHoa ON tChiTietHoaDonBan.MaHang = tDMHangHoa.MaHang WHERE tChiTietHoaDonBan.SoHDB = N'{txtSoHDB.Text}'";

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
                            dtBase.getExcute($"INSERT INTO tHoaDonBan(SoHDB, MaNV, NgayBan, MaKhach) VALUES(N'{txtSoHDB.Text}', N'{cboNhanVien.SelectedValue}', N'{dtpNgayBan.Value.ToString("yyyy-MM-dd")} ', N'{cboKhachHang.SelectedValue}')");
                            MessageBox.Show("Bạn đã thêm hoá đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                        dtBase.getExcute($"UPDATE tHoaDonBan SET NgayBan = N'{dtpNgayBan.Value.ToString("yyyy-MM-dd")}' WHERE SoHDB = '{txtSoHDB.Text}'");
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

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            dgvHoaDonBan_DoubleClick(sender, e);
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

            if (cboNhanVien.Text.Trim() != "")
            {
                dk += $" AND TenNV LIKE N'%{cboNhanVien.Text}%'";
            }

            if (dtpNgayBan.Value != date)
            {
                dk += $" AND NgayBan = N'{dtpNgayBan.Value.ToString("yyyy-MM-dd")}'";
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất danh sách sang Excel";
            saveFileDialog.Filter = "Sổ làm việc Excel|*.xlsx|Sổ làm việc Excel 97-2003|*.xls";
            saveFileDialog.FileName = "HoaDonBan";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Excel.Application application = new Excel.Application();
                    application.Application.Workbooks.Add(Type.Missing);
                    Excel.Workbook exBook = application.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                    exSheet.get_Range("A1").Font.Bold = true;
                    exSheet.get_Range("A1").Value = "Hoá đơn bán";
                    application.Cells[2, 1] = "Số thứ tự";

                    for (int i = 0; i < dgvHoaDonBan.Columns.Count; i++)
                    {
                        application.Cells[2, i + 2] = dgvHoaDonBan.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dgvHoaDonBan.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvHoaDonBan.Columns.Count; j++)
                        {
                            application.Cells[i + 3, 1] = (i + 1).ToString();
                            application.Cells[i + 3, j + 2] = dgvHoaDonBan.Rows[i].Cells[j].Value;
                        }
                    }

                    application.Columns.AutoFit();
                    application.ActiveWorkbook.SaveCopyAs(saveFileDialog.FileName);
                    application.ActiveWorkbook.Saved = true;

                    MessageBox.Show("Xuất danh sách sang Excel thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvHoaDonBan_DoubleClick(sender, e);
        }

        private void làmMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
        }

        private void xoáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnXoa_Click(sender, e);
        }
    }
}
