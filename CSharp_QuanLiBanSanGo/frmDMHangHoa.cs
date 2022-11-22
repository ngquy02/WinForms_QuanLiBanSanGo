using CSharp_QuanLiBanSanGo.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CSharp_QuanLiBanSanGo
{
    public partial class frmDMHangHoa : Form
    {
        DBconfig dtBase = new DBconfig();
        string queryLoad = "SELECT * FROM View_DMHangHoa";

        public frmDMHangHoa()
        {
            InitializeComponent();
        }

        private bool checkValidation()
        {
            if (txtMaHang.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMaHang.Focus();

                return false;
            }

            if (txtTenHang.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTenHang.Focus();

                return false;
            }

            if (txtDonGiaNhap.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập đơn giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDonGiaNhap.Focus();

                return false;
            }

            if (txtThoiGianBaoHanh.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập thời gian bảo hành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtThoiGianBaoHanh.Focus();

                return false;
            }

            if (cboKichThuoc.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn kích thước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboKichThuoc.Focus();

                return false;
            }

            if (cboLoaiGo.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn loại gỗ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboLoaiGo.Focus();

                return false;
            }

            if (cboDacDiem.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn đặc điểm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboDacDiem.Focus();

                return false;
            }

            if (cboXuatXu.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn xuất xứ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboXuatXu.Focus();

                return false;
            }

            if (cboMauSac.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn màu sắc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboMauSac.Focus();

                return false;
            }

            if (cboCongDung.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn công dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboCongDung.Focus();

                return false;
            }

            if(ptbAnh.Image == null)
            {
                MessageBox.Show("Hãy chọn ảnh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ptbAnh.Focus();

                return false;
            }    

            return true;
        }

        private void refreshInput()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            txtDonGiaNhap.Text = "";
            txtDonGiaBan.Text = "";
            txtSoLuong.Text = "";
            txtThoiGianBaoHanh.Text = "";
            cboKichThuoc.SelectedIndex = -1;
            cboLoaiGo.SelectedIndex = -1;
            cboDacDiem.SelectedIndex = -1;
            cboXuatXu.SelectedIndex = -1;
            cboMauSac.SelectedIndex = -1;
            cboCongDung.SelectedIndex = -1;
            ptbAnh.Image = null;
            rtbGhiChu.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void load_LoaiGo()
        {
            cboLoaiGo.DataSource = dtBase.getTable("SELECT * FROM tLoaiGo");
            cboLoaiGo.ValueMember = "MaLoaiGo";
            cboLoaiGo.DisplayMember = "TenLoaiGo";
        }

        private void load_KichThuoc()
        {
            cboKichThuoc.DataSource = dtBase.getTable("SELECT * FROM tKichThuoc");
            cboKichThuoc.ValueMember = "MaKichThuoc";
            cboKichThuoc.DisplayMember = "TenKichThuoc";
        }

        private void load_DacDiem()
        {
            cboDacDiem.DataSource = dtBase.getTable("SELECT * FROM tDacDiem");
            cboDacDiem.ValueMember = "MaDacDiem";
            cboDacDiem.DisplayMember = "TenDacDiem";
        }

        private void load_CongDung()
        {
            cboCongDung.DataSource = dtBase.getTable("SELECT * FROM tCongDung");
            cboCongDung.ValueMember = "MaCongDung";
            cboCongDung.DisplayMember = "TenCongDung";
        }

        private void load_MauSac()
        {
            cboMauSac.DataSource = dtBase.getTable("SELECT * FROM tMauSac");
            cboMauSac.ValueMember = "MaMau";
            cboMauSac.DisplayMember = "TenMau";
        }

        private void load_NuocSanXuat()
        {
            cboXuatXu.DataSource = dtBase.getTable("SELECT * FROM tNuocSanXuat");
            cboXuatXu.ValueMember = "MaNuocSX";
            cboXuatXu.DisplayMember = "TenNuocSX";
        }

        private void txtDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtThoiGianBaoHanh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmDMHangHoa_Load(object sender, EventArgs e)
        {
            DataTable dtHangHoa = dtBase.getTable(queryLoad);
            dgvDMHangHoa.DataSource = dtHangHoa;

            dgvDMHangHoa.Columns[0].HeaderText = "Mã hàng";
            dgvDMHangHoa.Columns[1].HeaderText = "Tên hàng hoá";
            dgvDMHangHoa.Columns[2].HeaderText = "Loại gỗ";
            dgvDMHangHoa.Columns[3].HeaderText = "Kích thước";
            dgvDMHangHoa.Columns[4].HeaderText = "Đặc điểm";
            dgvDMHangHoa.Columns[5].HeaderText = "Công dụng";
            dgvDMHangHoa.Columns[6].HeaderText = "Màu sắc";
            dgvDMHangHoa.Columns[7].HeaderText = "Xuất xứ";
            dgvDMHangHoa.Columns[8].HeaderText = "Số lượng";
            dgvDMHangHoa.Columns[9].HeaderText = "Đơn giá nhập (đồng)";
            dgvDMHangHoa.Columns[10].HeaderText = "Đơn giá bán (đồng)";
            dgvDMHangHoa.Columns[11].HeaderText = "Bảo hành (năm)";
            dgvDMHangHoa.Columns[12].HeaderText = "Ảnh";
            dgvDMHangHoa.Columns[13].HeaderText = "Ghi chú";

            load_LoaiGo();
            load_KichThuoc();
            load_DacDiem();
            load_CongDung();
            load_MauSac();
            load_NuocSanXuat();

            refreshInput();
        }

        private void dgvDMHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHang.Text = dgvDMHangHoa.CurrentRow.Cells[0].Value.ToString();
            txtTenHang.Text = dgvDMHangHoa.CurrentRow.Cells[1].Value.ToString();
            cboLoaiGo.Text = dgvDMHangHoa.CurrentRow.Cells[2].Value.ToString();
            cboKichThuoc.Text = dgvDMHangHoa.CurrentRow.Cells[3].Value.ToString();
            cboDacDiem.Text = dgvDMHangHoa.CurrentRow.Cells[4].Value.ToString();
            cboCongDung.Text = dgvDMHangHoa.CurrentRow.Cells[5].Value.ToString();
            cboMauSac.Text = dgvDMHangHoa.CurrentRow.Cells[6].Value.ToString();
            cboXuatXu.Text = dgvDMHangHoa.CurrentRow.Cells[7].Value.ToString();
            txtSoLuong.Text = dgvDMHangHoa.CurrentRow.Cells[8].Value.ToString();

            string[] arrDonGiaNhap = dgvDMHangHoa.CurrentRow.Cells[9].Value.ToString().Split(',');
            string[] arrDonGiaBan = dgvDMHangHoa.CurrentRow.Cells[10].Value.ToString().Split(',');

            txtDonGiaNhap.Text = arrDonGiaNhap[0];
            txtDonGiaBan.Text = arrDonGiaBan[0];
            txtThoiGianBaoHanh.Text = dgvDMHangHoa.CurrentRow.Cells[11].Value.ToString();

            string path = dgvDMHangHoa.CurrentRow.Cells[12].Value.ToString();
            if (!File.Exists(path))
            {
                ptbAnh.Image = null;
            }
            else
            {
                ptbAnh.ImageLocation = path;
            }

            rtbGhiChu.Text = dgvDMHangHoa.CurrentRow.Cells[13].Value.ToString();

            txtMaHang.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnTimKiem.Enabled = false;
        }


        private void btnNhapAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh";
            openFileDialog.Filter = "Tệp hình ảnh (*.jpg;*.jpeg)|*.jpg;*.jpeg|Tất cả tệp|*.*";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ptbAnh.ImageLocation = openFileDialog.FileName;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvDMHangHoa.DataSource = dtBase.getTable(queryLoad);

            btnThem.Enabled = true;
            btnLamMoi.Enabled = true;
            btnTimKiem.Enabled = true;
            txtMaHang.Enabled = true;

            refreshInput();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string queryAdd = $"INSERT INTO tDMHangHoa (MaHang, TenHangHoa, MaLoaiGo, MaKichThuoc, MaDacDiem, MaCongDung, MaMau, MaNuocSX, SoLuong, DonGiaNhap, ThoiGianBaoHanh, Anh, GhiChu) VALUES(N'{txtMaHang.Text}', N'{txtTenHang.Text}', N'{cboLoaiGo.SelectedValue}', N'{cboKichThuoc.SelectedValue}', N'{cboDacDiem.SelectedValue}', N'{cboCongDung.SelectedValue}', N'{cboMauSac.SelectedValue}', N'{cboXuatXu.SelectedValue}', {txtSoLuong.Text}, {txtDonGiaNhap.Text}, {txtThoiGianBaoHanh.Text}, '{ptbAnh.ImageLocation}', N'{rtbGhiChu.Text}')";

            if (checkValidation())
            {
                DataTable dtHangHoa = dtBase.getTable("SELECT * FROM tDMHangHoa WHERE" + " MaHang ='" + (txtMaHang.Text).Trim() + "'");
                if (dtHangHoa.Rows.Count > 0)
                {
                    MessageBox.Show("Mặt hàng này đã có, hãy nhập mã hàng khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMaHang.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm sản phẩm vào danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            dtBase.getExcute(queryAdd);
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvDMHangHoa.DataSource = dtBase.getTable(queryLoad);
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
            string queryUpdate = $"UPDATE tDMHangHoa SET TenHangHoa = N'{txtTenHang.Text}', MaLoaiGo = N'{cboLoaiGo.SelectedValue}', MaKichThuoc = N'{cboKichThuoc.SelectedValue}', MaDacDiem = N'{cboDacDiem.SelectedValue}', MaCongDung = N'{cboCongDung.SelectedValue}', MaMau = N'{cboMauSac.SelectedValue}', MaNuocSX = N'{cboXuatXu.SelectedValue}', SoLuong = {txtSoLuong.Text}, DonGiaNhap = {txtDonGiaNhap.Text}, ThoiGianBaoHanh = {txtThoiGianBaoHanh.Text}, Anh = '{ptbAnh.ImageLocation}', GhiChu = N'{rtbGhiChu.Text}' WHERE MaHang = N'{txtMaHang.Text}'";

            if (checkValidation())
            {
                if (MessageBox.Show("Bạn có sửa thông tin sản phẩm không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dtBase.getExcute(queryUpdate);
                        MessageBox.Show("Bạn đã sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvDMHangHoa.DataSource = dtBase.getTable(queryLoad);
                        txtMaHang.Enabled = true;
                        refreshInput();
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
            if (MessageBox.Show("Bạn có muốn xóa mặt hàng có mã là: " + txtMaHang.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    dtBase.getExcute($"DELETE tDMHangHoa WHERE MaHang = N'{txtMaHang.Text}'");
                    dgvDMHangHoa.DataSource = dtBase.getTable(queryLoad);
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

            if (txtMaHang.Text.Trim() != "")
            {
                dk += $" AND MaHang LIKE '%{txtMaHang.Text}%'";
            }

            if (txtTenHang.Text.Trim() != "")
            {
                dk += $" AND TenHangHoa LIKE N'%{txtTenHang.Text}%'";
            }

            if (cboLoaiGo.Text.Trim() != "")
            {
                dk += $" AND TenLoaiGo LIKE N'%{cboLoaiGo.Text}%'";
            }

            if (cboKichThuoc.Text.Trim() != "")
            {
                dk += $" AND TenKichThuoc LIKE N'%{cboKichThuoc.Text}%'";
            }

            if (cboDacDiem.Text.Trim() != "")
            {
                dk += $" AND TenDacDiem LIKE N'%{cboDacDiem.Text}%'";
            }

            if (cboCongDung.Text.Trim() != "")
            {
                dk += $" AND TenCongDung LIKE N'%{cboCongDung.Text}%'";
            }

            if (cboMauSac.Text.Trim() != "")
            {
                dk += $" AND TenMau LIKE N'%{cboMauSac.Text}%'";
            }

            if (cboXuatXu.Text.Trim() != "")
            {
                dk += $" AND TenNuocSX LIKE N'%{cboXuatXu.Text}%'";
            }

            if (dk != "")
            {
                string find = queryLoad + " WHERE MaHang LIKE N'%%'" + dk;
                dgvDMHangHoa.DataSource = dtBase.getTable(find);
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
                exSheet.get_Range("B2").Value = "DANH MỤC HÀNG HOÁ";
                exSheet.get_Range("A3").Value = "Số thứ tự";
                exSheet.get_Range("B3").Value = "Mã hàng";
                exSheet.get_Range("C3").Value = "Tên hàng hoá";
                exSheet.get_Range("D3").Value = "Loại gỗ";
                exSheet.get_Range("E3").Value = "Kích thước";
                exSheet.get_Range("F3").Value = "Đặc điểm";
                exSheet.get_Range("G3").Value = "Công dụng";
                exSheet.get_Range("H3").Value = "Màu sắc";
                exSheet.get_Range("I3").Value = "Xuất xứ";
                exSheet.get_Range("J3").Value = "Số lượng";
                exSheet.get_Range("K3").Value = "Đơn giá nhập";
                exSheet.get_Range("M3").Value = "Thời gian bảo hành";
                exSheet.get_Range("N3").Value = "Ghi chú";

                for (int i = 0; i < dgvDMHangHoa.Rows.Count; i++)
                {
                    exSheet.get_Range("A" + (i + 4).ToString()).Value = (i + 1).ToString();
                    exSheet.get_Range("B" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[0].Value;
                    exSheet.get_Range("C" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[1].Value;
                    exSheet.get_Range("D" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[2].Value;
                    exSheet.get_Range("E" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[3].Value;
                    exSheet.get_Range("F" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[4].Value;
                    exSheet.get_Range("G" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[5].Value;
                    exSheet.get_Range("H" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[6].Value;
                    exSheet.get_Range("I" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[7].Value;
                    exSheet.get_Range("J" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[8].Value;
                    exSheet.get_Range("K" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[9].Value;
                    exSheet.get_Range("L" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[10].Value;
                    exSheet.get_Range("M" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[11].Value;
                    exSheet.get_Range("N" + (i + 4).ToString()).Value = dgvDMHangHoa.Rows[i].Cells[13].Value;
                }

                exSheet.Columns.AutoFit();
                exBook.Activate();
                SaveFileDialog excelFile = new SaveFileDialog();
                excelFile.Title = "Lưu Excel";
                excelFile.Filter = "Sổ làm việc Excel|*.xlsx|Sổ làm việc Excel 97-2003|*.xls";
                excelFile.FileName = "DanhMucHangHoa";
                excelFile.ShowDialog();
                exBook.SaveAs(excelFile.FileName.ToString());
                exApp.Quit();
                MessageBox.Show("Đã xuất Excel thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xoáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnXoa_Click(sender, e);
        }

        private void làmMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
        }
    }
}
