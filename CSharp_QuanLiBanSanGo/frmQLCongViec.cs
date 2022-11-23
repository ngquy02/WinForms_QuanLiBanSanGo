using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharp_QuanLiBanSanGo.Class;

namespace CSharp_QuanLiBanSanGo
{
    public partial class frmQLCongViec : Form
    {
        DBconfig dtBase = new DBconfig();
        string queryLoad = "SELECT * FROM tCongViec";

        public frmQLCongViec()
        {
            InitializeComponent();
        }

        private bool checkValidation()
        {
            if (txtMaCV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mã công việc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMaCV.Focus();

                return false;
            }

            if (txtTenCV.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên công việc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTenCV.Focus();

                return false;
            }

            if (txtMucLuong.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mức lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMucLuong.Focus();

                return false;
            }

            return true;
        }

        private void refreshInput()
        {
            txtMaCV.Text = "";
            txtTenCV.Text = "";
            txtMucLuong.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void frmQLCongViec_Load(object sender, EventArgs e)
        {
            DataTable dtCongViec = dtBase.getTable(queryLoad);
            dgvCongViec.DataSource = dtCongViec;

            dgvCongViec.Columns[0].HeaderText = "Mã công việc";
            dgvCongViec.Columns[1].HeaderText = "Tên công việc";
            dgvCongViec.Columns[2].HeaderText = "Mức lương";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            refreshInput();
        }

        private void dgvCongViec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaCV.Text = dgvCongViec.CurrentRow.Cells[0].Value.ToString();
            txtTenCV.Text = dgvCongViec.CurrentRow.Cells[1].Value.ToString();
            txtMucLuong.Text = dgvCongViec.CurrentRow.Cells[2].Value.ToString();

            txtMaCV.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refreshInput();

            btnThem.Enabled = true;
            txtMaCV.Enabled = true;

            dgvCongViec.DataSource = dtBase.getTable(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtCongViec = dtBase.getTable($"SELECT * FROM tCongViec WHERE MaCV = N'{txtMaCV.Text}'");

                if (dtCongViec.Rows.Count > 0)
                {
                    MessageBox.Show("Công việc này đã có, vui lòng chọn tên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMaCV.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm công việc vào không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            dtBase.getExcute($"INSERT INTO tCongViec(MaCV, TenCV, MucLuong) VALUES(N'{txtMaCV.Text}', N'{txtTenCV.Text}', N'{txtMucLuong.Text}')");
                            MessageBox.Show("Bạn đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dgvCongViec.DataSource = dtBase.getTable(queryLoad);

                            refreshInput();
                        }
                        catch (Exception ex)
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
                if (MessageBox.Show("Bạn có sửa thông tin không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dtBase.getExcute($"UPDATE tCongViec SET TenCV = {txtTenCV.Text} WHERE MaCV = '{txtMaCV.Text}'");
                        dtBase.getExcute($"UPDATE tCongViec SET MucLuong = {txtMucLuong.Text} WHERE MaCV = '{txtMaCV.Text}'");
                        MessageBox.Show("Bạn đã sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvCongViec.DataSource = dtBase.getTable(queryLoad);
                        refreshInput();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa công việc có mã là: " + txtMaCV.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dtBase.getExcute($"DELETE tCongViec WHERE MaCV ='{txtMaCV.Text}'");
                    dgvCongViec.DataSource = dtBase.getTable(queryLoad);
                    MessageBox.Show("Bạn đã xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    refreshInput();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
