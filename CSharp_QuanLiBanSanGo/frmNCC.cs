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
    public partial class frmNCC : Form
    {
        DBconfig dtBase = new DBconfig();

        string queryLoad = "SELECT * FROM tNhaCungCap";

        public frmNCC()
        {
            InitializeComponent();
        }

        private bool isCheck()
        {
            if (txtMaNCC.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mã nhà cung cấp");
                txtMaNCC.Focus();

                return false;
            }

            if (txtTenNCC.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên nhà cung cấp");
                txtTenNCC.Focus();

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
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmNCC_Load(object sender, EventArgs e)
        {
            DataTable dtNCC = dtBase.table(queryLoad);
            dgvNCC.DataSource = dtNCC;

            dgvNCC.Columns[0].HeaderText = "Mã nhà cung cấp";
            dgvNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvNCC.Columns[2].HeaderText = "Địa chỉ";
            dgvNCC.Columns[3].HeaderText = "Điện thoại";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            CleanInput();
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNCC.Text = dgvNCC.CurrentRow.Cells[0].Value.ToString();
            txtTenNCC.Text = dgvNCC.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dgvNCC.CurrentRow.Cells[2].Value.ToString();
            txtDienThoai.Text = dgvNCC.CurrentRow.Cells[3].Value.ToString();

            txtMaNCC.Enabled = false;
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
            txtMaNCC.Enabled = true;

            dgvNCC.DataSource = dtBase.table(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                DataTable dtNCC = dtBase.table($"SELECT * FROM tNhaCungCap WHERE MaNCC = '{txtMaNCC.Text}'");
                if (dtNCC.Rows.Count > 0)
                {
                    MessageBox.Show("Mã số ngày đã có, hãy nhập mã số khác");
                    txtMaNCC.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm nhà cung cấp vào danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        dtBase.Excute($"INSERT INTO tNhaCungCap(MaNCC, TenNCC, DiaChi, DienThoai) VALUES(N'{txtMaNCC.Text}', N'{txtTenNCC.Text}', N'{txtDiaChi.Text}', N'{txtDienThoai.Text}')");
                        MessageBox.Show("Bạn đã thêm sản phẩm thành công");

                        dgvNCC.DataSource = dtBase.table(queryLoad);

                        CleanInput();
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                if (MessageBox.Show("Bạn có sửa thông tin nhà cung cấp không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    dtBase.Excute($"UPDATE tNhaCungCap SET TenNCC = N'{txtTenNCC.Text}' WHERE MaNCC = N'{txtMaNCC.Text}'");
                    dtBase.Excute($"UPDATE tNhaCungCap SET DiaChi = N'{txtDiaChi.Text}' WHERE MaNCC = N'{txtMaNCC.Text}'");
                    dtBase.Excute($"UPDATE tNhaCungCap SET DienThoai = N'{txtDienThoai.Text}' WHERE MaNCC = N'{txtMaNCC.Text}'");

                    CleanInput();

                    dgvNCC.DataSource = dtBase.table(queryLoad);
                    MessageBox.Show("Bạn đã sửa thông tin thành công");
                    txtMaNCC.Enabled = true;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa nhà cung cấp có mã là: " + txtMaNCC.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dtBase.Excute($"DELETE tNhaCungCap WHERE MaNCC = N'{txtMaNCC.Text}'");
                dgvNCC.DataSource = dtBase.table(queryLoad);

                CleanInput();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string dk = "";

            if (txtMaNCC.Text.Trim() != "")
            {
                dk += $" AND MaNCC LIKE N'%{txtMaNCC.Text}%'";
            }

            if (txtTenNCC.Text.Trim() != "")
            {
                dk += $" AND TenNCC LIKE N'%{txtTenNCC.Text}%'";
            }

            if (txtDiaChi.Text.Trim() != "")
            {
                dk += $" AND DiaChi LIKE N'%{txtDiaChi.Text}%'";
            }

            if (txtDienThoai.Text.Trim() != "")
            {
                dk += $" AND DienThoai LIKE N'%{txtDienThoai.Text}%'";
            }

            if (dk != "")
            {
                string find = queryLoad + " WHERE MaNCC LIKE N'%NCC%'" + dk;
                dgvNCC.DataSource = dtBase.table(find);
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
                exSheet.get_Range("B2").Value = "Danh sách nhà cung cấp";
                exSheet.get_Range("A3").Value = "Số thứ tự";
                exSheet.get_Range("B3").Value = "Mã nhà cung cấp";
                exSheet.get_Range("C3").Value = "Tên nhà cung cấp";
                exSheet.get_Range("D3").Value = "Địa chỉ";
                exSheet.get_Range("E3").Value = "Điện thoại";

                int n = dgvNCC.Rows.Count;

                for (int i = 0; i < n - 1; i++)
                {
                    exSheet.get_Range("A" + (i + 4).ToString()).Value = (i + 1).ToString();
                    exSheet.get_Range("B" + (i + 4).ToString()).Value = dgvNCC.Rows[i].Cells[0].Value;
                    exSheet.get_Range("C" + (i + 4).ToString()).Value = dgvNCC.Rows[i].Cells[1].Value;
                    exSheet.get_Range("D" + (i + 4).ToString()).Value = dgvNCC.Rows[i].Cells[2].Value;
                    exSheet.get_Range("E" + (i + 4).ToString()).Value = dgvNCC.Rows[i].Cells[3].Value;
                }

                exSheet.Columns.AutoFit();
                exBook.Activate();
                SaveFileDialog excelFile = new SaveFileDialog();
                excelFile.Title = "Lưu Excel";
                excelFile.Filter = "Sổ làm việc Excel|*.xlsx|Tất cả tệp|*.*";
                excelFile.FileName = "DanhSachNCC";
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
