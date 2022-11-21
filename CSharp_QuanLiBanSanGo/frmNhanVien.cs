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
    public partial class frmNhanVien : Form
    {
        DBconfig dtBase = new DBconfig();
        string queryLoad = "SELECT * FROM View_NhanVien";
        string gioiTinh;
        DateTime date = new DateTime(1900, 01, 01, 0, 0, 0);

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private bool checkValidation()
        {
            if (txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMaNV.Focus();

                return false;
            }

            if (txtTenNV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTenNV.Focus();

                return false;
            }

            if(rdoNam.Checked == true)
            {
                gioiTinh = rdoNam.Text;
            }
            else
            {
                gioiTinh = rdoNu.Text;
            }

            if (dtpNgaySinh.Value == date)
            {
                MessageBox.Show("Hãy nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpNgaySinh.Focus();

                return false;
            }

            if (txtDienThoai.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDienThoai.Focus();

                return false;
            }

            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDiaChi.Focus();

                return false;
            }

            if(cboCongViec.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn công việc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboCongViec.Focus();

                return false;
            }    

            return true;
        }

        private void refreshInput()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            dtpNgaySinh.Value = date;
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            cboCongViec.SelectedIndex = -1;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void load_CongViec()
        {
            cboCongViec.DataSource = dtBase.getTable("SELECT * FROM tCongViec");
            cboCongViec.ValueMember = "MaCV";
            cboCongViec.DisplayMember = "TenCV";
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            DataTable dtNhanVien = dtBase.getTable(queryLoad);
            dgvNhanVien.DataSource = dtNhanVien;

            dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns[2].HeaderText = "Giới tính";
            dgvNhanVien.Columns[3].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns[4].HeaderText = "Điện thoại";
            dgvNhanVien.Columns[5].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[6].HeaderText = "Công việc";

            load_CongViec();

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            refreshInput();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.Text = dgvNhanVien.CurrentRow.Cells[0].Value.ToString();
            txtTenNV.Text = dgvNhanVien.CurrentRow.Cells[1].Value.ToString();

            if (dgvNhanVien.CurrentRow.Cells[2].Value.ToString() == rdoNam.Text)
            {
                rdoNam.Checked = true;
            }
            else
            {
                rdoNu.Checked = true;
            }    

            dtpNgaySinh.Text = dgvNhanVien.CurrentRow.Cells[3].Value.ToString();
            txtDienThoai.Text = dgvNhanVien.CurrentRow.Cells[4].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells[5].Value.ToString();
            cboCongViec.Text = dgvNhanVien.CurrentRow.Cells[6].Value.ToString();

            txtMaNV.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnTimKiem.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refreshInput();

            btnThem.Enabled = true;
            btnTimKiem.Enabled = true;
            txtMaNV.Enabled = true;

            dgvNhanVien.DataSource = dtBase.getTable(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtNhanVien = dtBase.getTable($"SELECT * FROM tNhanVien WHERE MaNV ='{txtMaNV.Text}'");

                if (dtNhanVien.Rows.Count > 0)
                {
                    MessageBox.Show("Mã nhân viên này đã có, hãy nhập mã nhân viên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMaNV.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm nhân viên này vào danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            dtBase.getExcute($"INSERT INTO tNhanVien(MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) VALUES(N'{txtMaNV.Text}', N'{txtTenNV.Text}', N'{gioiTinh}', N'{dtpNgaySinh.Value.ToString("yyyy-MM-dd")}', N'{txtDienThoai.Text}', N'{txtDiaChi.Text}', N'{cboCongViec.SelectedValue}')");

                            MessageBox.Show("Bạn đã thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvNhanVien.DataSource = dtBase.getTable(queryLoad);
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
                if (MessageBox.Show("Bạn có sửa thông tin nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dtBase.getExcute($"UPDATE tNhanVien SET TenNV = N'{txtTenNV.Text}' WHERE MaNV = N'{txtMaNV.Text}'");
                        dtBase.getExcute($"UPDATE tNhanVien SET GioiTinh = N'{gioiTinh}' WHERE MaNV = N'{txtMaNV.Text}'");
                        dtBase.getExcute($"UPDATE tNhanVien SET NgaySinh = N'{dtpNgaySinh.Value.ToString("yyyy-MM-dd")}' WHERE MaNV = N'{txtMaNV.Text}'");
                        dtBase.getExcute($"UPDATE tNhanVien SET DienThoai = N'{txtDienThoai.Text}' WHERE MaNV = N'{txtMaNV.Text}'");
                        dtBase.getExcute($"UPDATE tNhanVien SET DiaChi = N'{txtDiaChi.Text}' WHERE MaNV = N'{txtMaNV.Text}'");
                        dtBase.getExcute($"UPDATE tNhanVien SET MaCV = N'{cboCongViec.SelectedValue}' WHERE MaNV = N'" + txtMaNV.Text + "'");

                        dgvNhanVien.DataSource = dtBase.getTable(queryLoad);
                        MessageBox.Show("Bạn đã sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMaNV.Enabled = true;
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
            if (MessageBox.Show("Bạn có muốn xóa nhà nhân viên có mã là: " + txtMaNV.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    dtBase.getExcute($"DELETE tNhanVien WHERE MaNV ='{txtMaNV.Text}'");

                    dgvNhanVien.DataSource = dtBase.getTable(queryLoad);
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

            if (txtMaNV.Text.Trim() != "")
            {
                dk += $" AND MaNV LIKE N'%{txtMaNV.Text}%'";
            }

            if (txtTenNV.Text.Trim() != "")
            {
                dk += $" AND TenNV LIKE N'%{txtTenNV.Text}%'";
            }

            if (rdoNam.Checked == true)
            {
                dk += $" AND GioiTinh LIKE N'%{rdoNam.Text}%'";
            }
            
            if(rdoNu.Checked == true)
            {
                dk += $" AND GioiTinh LIKE N'%{rdoNu.Text}%'";
            }

            if (dtpNgaySinh.Value.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                dk += $" AND NgaySinh LIKE N'%{dtpNgaySinh.Value.ToString("yyyy-MM-dd")}%'";
            }

            if (txtDienThoai.Text.Trim() != "")
            {
                dk += $" AND DienThoai LIKE N'%{txtDienThoai.Text}%'";
            }

            if (txtDiaChi.Text.Trim() != "")
            {
                dk += $" AND DiaChi LIKE N'%{txtDiaChi.Text}'";
            }

            if (cboCongViec.Text.Trim() != "")
            {
                dk += $" AND CongViec LIKE N'%{cboCongViec.Text}%'";
            }

            if (dk != "")
            {
                string find = queryLoad + " WHERE MaNV LIKE N'%%'" + dk;
                dgvNhanVien.DataSource = dtBase.getTable(find);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất danh sách sang Excel";
            saveFileDialog.Filter = "Sổ làm việc Excel|*.xlsx|Sổ làm việc Excel 97-2003|*.xls";
            saveFileDialog.FileName = "NhanVien";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Excel.Application application = new Excel.Application();
                    application.Application.Workbooks.Add(Type.Missing);
                    Excel.Workbook exBook = application.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                    exSheet.get_Range("A1").Font.Bold = true;
                    exSheet.get_Range("A1").Value = "Nhân viên";
                    application.Cells[2, 1] = "Số thứ tự";

                    for (int i = 0; i < dgvNhanVien.Columns.Count; i++)
                    {
                        application.Cells[2, i + 2] = dgvNhanVien.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dgvNhanVien.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvNhanVien.Columns.Count; j++)
                        {
                            application.Cells[i + 3, 1] = (i + 1).ToString();
                            application.Cells[i + 3, j + 2] = dgvNhanVien.Rows[i].Cells[j].Value;
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
