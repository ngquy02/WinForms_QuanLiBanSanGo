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
    public partial class frmKhachHang : Form
    {
        DBconfig dtBase = new DBconfig();
        string queryLoad = "SELECT * FROM tKhachHang";

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private bool checkValidation()
        {
            if (txtMaKhach.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMaKhach.Focus();

                return false;
            }

            if (txtTenKhach.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTenKhach.Focus();

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
            txtMaKhach.Text = "";
            txtTenKhach.Text = "";
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

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            DataTable dtKhachHang = dtBase.getTable(queryLoad);
            dgvKhachHang.DataSource = dtKhachHang;

            dgvKhachHang.Columns[0].HeaderText = "Mã khách hàng";
            dgvKhachHang.Columns[1].HeaderText = "Tên khách hàng";
            dgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns[3].HeaderText = "Điện thoại";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            refreshInput();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKhach.Text = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
            txtTenKhach.Text = dgvKhachHang.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells[2].Value.ToString();
            txtDienThoai.Text = dgvKhachHang.CurrentRow.Cells[3].Value.ToString();

            txtMaKhach.Enabled = false;
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
            txtMaKhach.Enabled = true;

            dgvKhachHang.DataSource = dtBase.getTable(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtKhachHang = dtBase.getTable($"SELECT * FROM tKhachHang WHERE MaKhach ='{txtMaKhach.Text}'");
                if (dtKhachHang.Rows.Count > 0)
                {
                    MessageBox.Show("Mã khách này đã có, hãy nhập mã khách hàng khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMaKhach.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm khách hàng này vào danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            dtBase.getExcute($"INSERT INTO tKhachHang(MaKhach, TenKhach, DiaChi, DienThoai) VALUES(N'{txtMaKhach.Text}', N'{txtTenKhach.Text}', N'{txtDiaChi.Text}', N'{txtDienThoai.Text}')");

                            MessageBox.Show("Bạn đã thêm khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvKhachHang.DataSource = dtBase.getTable(queryLoad);
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
                if (MessageBox.Show("Bạn có sửa thông tin nhà khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dtBase.getExcute($"UPDATE tKhachHang SET TenKhach = N'{txtTenKhach.Text}' WHERE MaNCC = N'{txtMaKhach.Text}'");
                        dtBase.getExcute($"UPDATE tKhachHang SET DiaChi = N'{txtDiaChi.Text}' WHERE MaNCC = N'{txtMaKhach.Text}'");
                        dtBase.getExcute($"UPDATE tKhachHang SET DienThoai = N'{txtDienThoai.Text}' WHERE MaNCC = N'{txtMaKhach.Text}'");

                        dgvKhachHang.DataSource = dtBase.getTable(queryLoad);
                        MessageBox.Show("Bạn đã sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMaKhach.Enabled = true;
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
            if (MessageBox.Show("Bạn có muốn xóa nhà khách hàng có mã là: " + txtMaKhach.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dtBase.getExcute($"DELETE tKhachHang WHERE MaKhach ='" + txtMaKhach.Text + "'");

                    dgvKhachHang.DataSource = dtBase.getTable(queryLoad);
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

            if (txtMaKhach.Text.Trim() != "")
            {
                dk += $" AND MaKhach LIKE N'%{txtMaKhach.Text}%'";
            }

            if (txtTenKhach.Text.Trim() != "")
            {
                dk += $" AND TenKhach LIKE N'%{txtTenKhach.Text}%'";
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
                string find = queryLoad + " WHERE MaKhach LIKE N'%KH%'" + dk;
                dgvKhachHang.DataSource = dtBase.getTable(find);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất danh sách sang Excel";
            saveFileDialog.Filter = "Sổ làm việc Excel|*.xlsx|Sổ làm việc Excel 97-2003|*.xls";
            saveFileDialog.FileName = "KhachHang";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Excel.Application application = new Excel.Application();
                    application.Application.Workbooks.Add(Type.Missing);
                    Excel.Workbook exBook = application.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                    exSheet.get_Range("A1").Font.Bold = true;
                    exSheet.get_Range("A1").Value = "Khách hàng";
                    application.Cells[2, 1] = "Số thứ tự";

                    for (int i = 0; i < dgvKhachHang.Columns.Count; i++)
                    {
                        application.Cells[2, i + 2] = dgvKhachHang.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dgvKhachHang.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvKhachHang.Columns.Count; j++)
                        {
                            application.Cells[i + 3, 1] = (i + 1).ToString();
                            application.Cells[i + 3, j + 2] = dgvKhachHang.Rows[i].Cells[j].Value;
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
