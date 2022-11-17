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

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private bool isCheck()
        {
            if (txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mã nhân viên");
                txtMaNV.Focus();

                return false;
            }

            if (txtTenNV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên nhân viên");
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

            if (dtpNgaySinh.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập ngày sinh");
                dtpNgaySinh.Focus();

                return false;
            }

            if (txtDienThoai.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại");
                txtDienThoai.Focus();

                return false;
            }

            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ");
                txtDiaChi.Focus();

                return false;
            }

            if(cboCongViec.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn công việc");
                cboCongViec.Focus();

                return false;
            }    

            return true;
        }

        private void CleanInput()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            rdoNam.Checked = false;
            rdoNam.Checked = false;
            dtpNgaySinh.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            cboCongViec.Text = "";
        }

        private void load_CongViec()
        {
            cboCongViec.DataSource = dtBase.table("SELECT * FROM tCongViec");
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
            DataTable dtNhanVien = dtBase.table(queryLoad);
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

            CleanInput();
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
            CleanInput();

            btnThem.Enabled = true;
            btnTimKiem.Enabled = true;
            txtMaNV.Enabled = true;

            dgvNhanVien.DataSource = dtBase.table(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                DataTable dtNhanVien = dtBase.table($"SELECT * FROM tNhanVien WHERE MaNV ='{txtMaNV.Text}'");

                if (dtNhanVien.Rows.Count > 0)
                {
                    MessageBox.Show("Mã nhân viên này đã có, hãy nhập mã nhân viên khác");
                    txtMaNV.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm nhân viên này vào danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        dtBase.Excute($"INSERT INTO tNhanVien(MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) VALUES(N'{txtMaNV.Text}', N'{txtTenNV.Text}', N'{gioiTinh}', N'{dtpNgaySinh.Text}', N'{txtDienThoai.Text}', N'{txtDiaChi.Text}', N'{cboCongViec.SelectedValue}')");
                        MessageBox.Show("Bạn đã thêm nhân viên thành công");

                        dgvNhanVien.DataSource = dtBase.table(queryLoad);

                        CleanInput();
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                if (MessageBox.Show("Bạn có sửa thông tin nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    dtBase.Excute($"UPDATE tNhanVien SET TenNV = N'{txtTenNV.Text}' WHERE MaNV = N'{txtMaNV.Text}'");
                    dtBase.Excute($"UPDATE tNhanVien SET GioiTinh = N'{gioiTinh}' WHERE MaNV = N'{txtMaNV.Text}'");
                    dtBase.Excute($"UPDATE tNhanVien SET NgaySinh = N'{dtpNgaySinh.Text}' WHERE MaNV = N'{txtMaNV.Text}'");
                    dtBase.Excute($"UPDATE tNhanVien SET DienThoai = N'{txtDienThoai.Text}' WHERE MaNV = N'{txtMaNV.Text}'");
                    dtBase.Excute($"UPDATE tNhanVien SET DiaChi = N'{txtDiaChi.Text}' WHERE MaNV = N'{txtMaNV.Text}'");
                    dtBase.Excute($"UPDATE tNhanVien SET MaCV = N'{cboCongViec.SelectedValue}' WHERE MaNV = N'" + txtMaNV.Text + "'");

                    CleanInput();

                    dgvNhanVien.DataSource = dtBase.table(queryLoad);
                    MessageBox.Show("Bạn đã sửa thông tin thành công");
                    txtMaNV.Enabled = true;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa nhà nhân viên có mã là: " + txtMaNV.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dtBase.Excute($"DELETE tNhanVien WHERE MaNV ='{txtMaNV.Text}'");
                dgvNhanVien.DataSource = dtBase.table(queryLoad);

                CleanInput();
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

            if (rdoNam.Checked.ToString().Trim() != "")
            {
                dk += $" AND GioiTinh LIKE N'%{rdoNam.Checked}%'";
            }
            else
            {
                dk += $" AND GioiTinh LIKE N'%{rdoNu.Checked}%'";
            }

            if (dtpNgaySinh.Text.Trim() != "")
            {
                dk += $" AND NgaySinh LIKE N'%{dtpNgaySinh.Text}%'";
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
                string find = queryLoad + " WHERE MaNV LIKE N'%NV%'" + dk;
                dgvNhanVien.DataSource = dtBase.table(find);
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
                exSheet.get_Range("B2").Value = "Danh sách nhân viên";
                exSheet.get_Range("A3").Value = "Số thứ tự";
                exSheet.get_Range("B3").Value = "Mã nhân viên";
                exSheet.get_Range("C3").Value = "Tên nhân viên";
                exSheet.get_Range("C3").Value = "Giới tính";
                exSheet.get_Range("C3").Value = "Ngày sinh";
                exSheet.get_Range("D3").Value = "Địa chỉ";
                exSheet.get_Range("E3").Value = "Điện thoại";
                exSheet.get_Range("C3").Value = "Công việc";

                int n = dgvNhanVien.Rows.Count;

                for (int i = 0; i < n - 1; i++)
                {
                    exSheet.get_Range("A" + (i + 4).ToString()).Value = (i + 1).ToString();
                    exSheet.get_Range("B" + (i + 4).ToString()).Value = dgvNhanVien.Rows[i].Cells[0].Value;
                    exSheet.get_Range("C" + (i + 4).ToString()).Value = dgvNhanVien.Rows[i].Cells[1].Value;
                    exSheet.get_Range("D" + (i + 4).ToString()).Value = dgvNhanVien.Rows[i].Cells[2].Value;
                    exSheet.get_Range("E" + (i + 4).ToString()).Value = dgvNhanVien.Rows[i].Cells[3].Value;
                    exSheet.get_Range("F" + (i + 4).ToString()).Value = dgvNhanVien.Rows[i].Cells[4].Value;
                    exSheet.get_Range("G" + (i + 4).ToString()).Value = dgvNhanVien.Rows[i].Cells[5].Value;
                    exSheet.get_Range("H" + (i + 4).ToString()).Value = dgvNhanVien.Rows[i].Cells[6].Value;
                }

                exSheet.Columns.AutoFit();
                exBook.Activate();
                SaveFileDialog excelFile = new SaveFileDialog();
                excelFile.Title = "Lưu Excel";
                excelFile.Filter = "Sổ làm việc Excel|*.xlsx|Tất cả tệp|*.*";
                excelFile.ShowDialog();
                exBook.SaveAs(excelFile.FileName.ToString());
                exApp.Quit();
                MessageBox.Show("Xuất Excel thành công");
            }
            catch
            {
                
            }
        }
    }
}
