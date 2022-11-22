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
    public partial class frmChiTietHDB : Form
    {
        DBconfig dtBase = new DBconfig();
        string soHDB;
        string queryLoad;

        public frmChiTietHDB()
        {
            InitializeComponent();
        }

        public frmChiTietHDB(string soHDB, string query)
        {
            InitializeComponent();
            this.soHDB = soHDB;
            queryLoad = query;
        }

        private bool checkValidation()
        {
            if (cboMatHang.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboMatHang.Focus();

                return false;
            }

            if (txtSoLuong.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSoLuong.Focus();

                return false;
            }

            if(txtGiamGia.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtGiamGia.Focus();

                return false;
            }

            if (int.Parse(txtGiamGia.Text) > 100 || int.Parse(txtGiamGia.Text) < 0)
            {
                MessageBox.Show("Không hợp lệ, giá trị chỉ nằm từ 0 - 100", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtGiamGia.Focus();

                return false;
            }

            return true;
        }

        private void refreshInput()
        {
            cboMatHang.SelectedIndex = -1;
            txtSoLuong.Text = "";
            txtGiamGia.Text = "";
            txtThanhTien.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void load_MatHang()
        {
            cboMatHang.DataSource = dtBase.getTable("SELECT * FROM tDMHangHoa");
            cboMatHang.ValueMember = "MaHang";
            cboMatHang.DisplayMember = "TenHangHoa";
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmChiTietHDB_Load(object sender, EventArgs e)
        {
            this.Text += soHDB;
            lblSoHDB.Text = soHDB;

            DataTable dtChiTietHDB = dtBase.getTable(queryLoad);
            dgvChiTietHDB.DataSource = dtChiTietHDB;

            dgvChiTietHDB.Columns[0].HeaderText = "Số hoá đơn bán";
            dgvChiTietHDB.Columns[1].HeaderText = "Mã hàng";
            dgvChiTietHDB.Columns[2].HeaderText = "Tên mặt hàng";
            dgvChiTietHDB.Columns[3].HeaderText = "Số lượng";
            dgvChiTietHDB.Columns[4].HeaderText = "Giảm giá (%)";
            dgvChiTietHDB.Columns[5].HeaderText = "Thành tiền (đồng)";

            load_MatHang();

            refreshInput();
        }

        private void dgvChiTietHDB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cboMatHang.Text = dgvChiTietHDB.CurrentRow.Cells[2].Value.ToString();
            txtSoLuong.Text = dgvChiTietHDB.CurrentRow.Cells[3].Value.ToString();
            txtGiamGia.Text = dgvChiTietHDB.CurrentRow.Cells[4].Value.ToString();
            txtThanhTien.Text = dgvChiTietHDB.CurrentRow.Cells[5].Value.ToString();

            string[] arrGiamGia = dgvChiTietHDB.CurrentRow.Cells[4].Value.ToString().Split(',');
            txtGiamGia.Text = arrGiamGia[0];

            cboMatHang.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refreshInput();

            btnThem.Enabled = true;
            cboMatHang.Enabled = true;

            dgvChiTietHDB.DataSource = dtBase.getTable(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtChiTietHDB = dtBase.getTable($"SELECT * FROM tChiTietHoaDonBan WHERE SoHDB = N'{lblSoHDB.Text}' AND MaHang ='{cboMatHang.SelectedValue}'");

                if (dtChiTietHDB.Rows.Count > 0)
                {
                    MessageBox.Show("Mặt hàng này đã có, hãy chọn mặt hàng khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboMatHang.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm mặt hàng này vào danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            dtBase.getExcute($"INSERT INTO tChiTietHoaDonBan(SoHDB, MaHang, SoLuong, GiamGia) VALUES(N'{lblSoHDB.Text}', N'{cboMatHang.SelectedValue}', {txtSoLuong.Text}, {txtGiamGia.Text})");
                            MessageBox.Show("Bạn đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dgvChiTietHDB.DataSource = dtBase.getTable(queryLoad);

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
                if (MessageBox.Show("Bạn có sửa thông tin này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dtBase.getExcute($"UPDATE tChiTietHoaDonBan SET SoLuong = {txtSoLuong.Text} WHERE SoHDB = '{lblSoHDB.Text}' AND MaHang = N'{cboMatHang.SelectedValue}'");
                        dtBase.getExcute($"UPDATE tChiTietHoaDonBan SET GiamGia = {txtGiamGia.Text} WHERE SoHDB = '{lblSoHDB.Text}' AND MaHang = N'{cboMatHang.SelectedValue}'");
                        MessageBox.Show("Bạn đã sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvChiTietHDB.DataSource = dtBase.getTable(queryLoad);
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
            if (MessageBox.Show("Bạn có muốn xóa mặt hàng có mã là: " + cboMatHang.SelectedValue.ToString() + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dtBase.getExcute($"DELETE tChiTietHoaDonBan WHERE SoHDB ='{lblSoHDB.Text}' AND MaHang = N'{cboMatHang.SelectedValue}'");
                    dgvChiTietHDB.DataSource = dtBase.getTable(queryLoad);
                    MessageBox.Show("Bạn đã xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    refreshInput();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất danh sách sang Excel";
            saveFileDialog.Filter = "Sổ làm việc Excel|*.xlsx|Sổ làm việc Excel 97-2003|*.xls";
            saveFileDialog.FileName = "ChiTietHDB";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Excel.Application application = new Excel.Application();
                    application.Application.Workbooks.Add(Type.Missing);
                    Excel.Workbook exBook = application.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                    Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                    exSheet.get_Range("A1").Font.Bold = true;
                    exSheet.get_Range("A1").Value = "Chi tiêt hoá đơn";
                    application.Cells[2, 1] = "Số thứ tự";

                    for (int i = 0; i < dgvChiTietHDB.Columns.Count; i++)
                    {
                        application.Cells[2, i + 2] = dgvChiTietHDB.Columns[i].HeaderText;
                    }

                    for (int i = 0; i < dgvChiTietHDB.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgvChiTietHDB.Columns.Count; j++)
                        {
                            application.Cells[i + 3, 1] = (i + 1).ToString();
                            application.Cells[i + 3, j + 2] = dgvChiTietHDB.Rows[i].Cells[j].Value;
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
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
