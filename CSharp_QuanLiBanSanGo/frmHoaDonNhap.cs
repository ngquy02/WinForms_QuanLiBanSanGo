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
        DateTime date = new DateTime(1900, 01, 01, 0, 0, 0);

        public frmHoaDonNhap()
        {
            InitializeComponent();
        }

        private bool checkValidation()
        {
            if (txtSoHDN.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số hoá đơn nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSoHDN.Focus();

                return false;
            }

            if (cboNhanVien.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboNhanVien.Focus();

                return false;
            }

            if (dtpNgayNhap.Value == date)
            {
                MessageBox.Show("Hãy nhập ngày nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpNgayNhap.Focus();

                return false;
            }

            if (cboNCC.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboNCC.Focus();

                return false;
            }

            return true;
        }

        private void refreshInput()
        {
            txtSoHDN.Text = "";
            cboNhanVien.SelectedIndex = -1;
            dtpNgayNhap.Value = date;
            cboNCC.SelectedIndex = -1;
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

        private void load_NCC()
        {
            cboNCC.DataSource = dtBase.getTable("SELECT * FROM tNhaCungCap");
            cboNCC.ValueMember = "MaNCC";
            cboNCC.DisplayMember = "TenNCC";
        }

        private void frmHoaDonNhap_Load(object sender, EventArgs e)
        {
            DataTable dtHDB = dtBase.getTable(queryLoad);
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

            refreshInput();
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
            refreshInput();

            btnThem.Enabled = true;
            btnTimKiem.Enabled = true;
            txtSoHDN.Enabled = true;

            dgvHoaDonNhap.DataSource = dtBase.getTable(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtHDN = dtBase.getTable($"SELECT * FROM tHoaDonNhap WHERE SoHDN ='{txtSoHDN.Text}'");

                if (dtHDN.Rows.Count > 0)
                {
                    MessageBox.Show("Số hoá đơn này đã có, hãy nhập số hoá đơn khác");
                    txtSoHDN.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm hoá đơn này vào không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            dtBase.getExcute($"INSERT INTO tHoaDonNhap(SoHDN, MaNV, NgayNhap, MaNCC) VALUES(N'{txtSoHDN.Text}', N'{cboNhanVien.SelectedValue.ToString()}', N'{dtpNgayNhap.Value.ToString("yyyy-MM-dd")}', N'{cboNCC.SelectedValue}')");
                            MessageBox.Show("Bạn đã thêm hoá đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dgvHoaDonNhap.DataSource = dtBase.getTable(queryLoad);

                            refreshInput();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
                        dtBase.getExcute($"UPDATE tHoaDonNhap SET MaNV = N'{cboNhanVien.SelectedValue}' WHERE SoHDN = '{txtSoHDN.Text}'");
                        dtBase.getExcute($"UPDATE tHoaDonNhap SET NgayNhap = N'{dtpNgayNhap.Value.ToString("yyyy-MM-dd")}' WHERE SoHDN = '{txtSoHDN.Text}'");
                        dtBase.getExcute($"UPDATE tHoaDonNhap SET MaNCC = N'{cboNCC.SelectedValue}' WHERE SoHDN = '{txtSoHDN.Text}'");

                        refreshInput();

                        dgvHoaDonNhap.DataSource = dtBase.getTable(queryLoad);
                        MessageBox.Show("Bạn đã sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSoHDN.Enabled = true;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa hoá đơn có mã là: " + txtSoHDN.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    dtBase.getExcute($"DELETE tHoaDonNhap WHERE SoHDN ='{txtSoHDN.Text}'");
                    dgvHoaDonNhap.DataSource = dtBase.getTable(queryLoad);
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

            if (txtSoHDN.Text.Trim() != "")
            {
                dk += $" AND SoHDB LIKE N'{txtSoHDN.Text}%'";
            }

            if (cboNhanVien.Text.Trim() != "")
            {
                dk += $" AND TenNV LIKE N'{cboNhanVien.Text}%'";
            }

            if (dtpNgayNhap.Value != date)
            {
                dk += $" AND NgayBan LIKE N'%{dtpNgayNhap.Value.ToString("yyyy-MM-dd")}%'";
            }

            if (cboNCC.Text.Trim() != "")
            {
                dk += $" AND SoHDB LIKE N'{cboNCC.Text}%'";
            }

            if (dk != "")
            {
                string find = queryLoad + " WHERE SoHDN LIKE N'%HDN%'" + dk;
                dgvHoaDonNhap.DataSource = dtBase.getTable(find);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất danh sách sang Excel";
            saveFileDialog.Filter = "Sổ làm việc Excel|*.xlsx|Sổ làm việc Excel 97-2003|*.xls";
            saveFileDialog.FileName = "HoaDonNhap";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Excel.Application application = new Excel.Application();
                    application.Application.Workbooks.Add(Type.Missing);
                    Excel.Workbook exBook = application.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                    exSheet.get_Range("A1").Font.Bold = true;
                    exSheet.get_Range("A1").Value = "Hoá đơn nhập";
                    application.Cells[2, 1] = "Số thứ tự";

                    for (int i = 0; i < dgvHoaDonNhap.Columns.Count; i++)
                    {
                        application.Cells[2, i + 2] = dgvHoaDonNhap.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dgvHoaDonNhap.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvHoaDonNhap.Columns.Count; j++)
                        {
                            application.Cells[i + 3, 1] = (i + 1).ToString();
                            application.Cells[i + 3, j + 2] = dgvHoaDonNhap.Rows[i].Cells[j].Value;
                        }
                    }

                    application.Columns.AutoFit();
                    application.ActiveWorkbook.SaveCopyAs(saveFileDialog.FileName);
                    application.ActiveWorkbook.Saved = true;

                    MessageBox.Show("Xuất danh sách sang Excel thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimNC_Click(object sender, EventArgs e)
        {
            frmTKHDN timHDN = new frmTKHDN();
            timHDN.ShowDialog();
        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvHoaDonNhap_DoubleClick(sender, e);
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
