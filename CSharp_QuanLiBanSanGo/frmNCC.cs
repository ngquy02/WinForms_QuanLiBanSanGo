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

        private bool checkValidation()
        {
            if (txtMaNCC.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMaNCC.Focus();

                return false;
            }

            if (txtTenNCC.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTenNCC.Focus();

                return false;
            }

            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDiaChi.Focus();

                return false;
            }

            if (txtDienThoai.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDienThoai.Focus();

                return false;
            }

            return true;
        }

        private void refreshInput()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
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
            DataTable dtNCC = dtBase.getTable(queryLoad);
            dgvNCC.DataSource = dtNCC;

            dgvNCC.Columns[0].HeaderText = "Mã nhà cung cấp";
            dgvNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvNCC.Columns[2].HeaderText = "Địa chỉ";
            dgvNCC.Columns[3].HeaderText = "Điện thoại";

            refreshInput();
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
            refreshInput();

            btnThem.Enabled = true;
            btnTimKiem.Enabled = true;
            txtMaNCC.Enabled = true;

            dgvNCC.DataSource = dtBase.getTable(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtNCC = dtBase.getTable($"SELECT * FROM tNhaCungCap WHERE MaNCC = '{txtMaNCC.Text}'");
                if (dtNCC.Rows.Count > 0)
                {
                    MessageBox.Show("Mã số ngày đã có, hãy nhập mã số khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMaNCC.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm nhà cung cấp vào danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        try
                        {
                            dtBase.getExcute($"INSERT INTO tNhaCungCap(MaNCC, TenNCC, DiaChi, DienThoai) VALUES(N'{txtMaNCC.Text}', N'{txtTenNCC.Text}', N'{txtDiaChi.Text}', N'{txtDienThoai.Text}')");

                            MessageBox.Show("Bạn đã thêm sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvNCC.DataSource = dtBase.getTable(queryLoad);
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
                if (MessageBox.Show("Bạn có sửa thông tin nhà cung cấp không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        dtBase.getExcute($"UPDATE tNhaCungCap SET TenNCC = N'{txtTenNCC.Text}' WHERE MaNCC = N'{txtMaNCC.Text}'");
                        dtBase.getExcute($"UPDATE tNhaCungCap SET DiaChi = N'{txtDiaChi.Text}' WHERE MaNCC = N'{txtMaNCC.Text}'");
                        dtBase.getExcute($"UPDATE tNhaCungCap SET DienThoai = N'{txtDienThoai.Text}' WHERE MaNCC = N'{txtMaNCC.Text}'");

                        dgvNCC.DataSource = dtBase.getTable(queryLoad);
                        MessageBox.Show("Bạn đã sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMaNCC.Enabled = true;
                        refreshInput();
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
            if (MessageBox.Show("Bạn có muốn xóa nhà cung cấp có mã là: " + txtMaNCC.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dtBase.getExcute($"DELETE tNhaCungCap WHERE MaNCC = N'{txtMaNCC.Text}'");

                    dgvNCC.DataSource = dtBase.getTable(queryLoad);
                    MessageBox.Show("Bạn đã xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                dgvNCC.DataSource = dtBase.getTable(find);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất danh sách sang Excel";
            saveFileDialog.Filter = "Sổ làm việc Excel|*.xlsx|Sổ làm việc Excel 97-2003|*.xls";
            saveFileDialog.FileName = "NhaCungCap";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Excel.Application application = new Excel.Application();
                    application.Application.Workbooks.Add(Type.Missing);
                    Excel.Workbook exBook = application.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                    exSheet.get_Range("A1").Font.Bold = true;
                    exSheet.get_Range("A1").Value = "Nhà cung cấp";
                    application.Cells[2, 1] = "Số thứ tự";

                    for (int i = 0; i < dgvNCC.Columns.Count; i++)
                    {
                        application.Cells[2, i + 2] = dgvNCC.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dgvNCC.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvNCC.Columns.Count; j++)
                        {
                            application.Cells[i + 3, 1] = (i + 1).ToString();
                            application.Cells[i + 3, j + 2] = dgvNCC.Rows[i].Cells[j].Value;
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
