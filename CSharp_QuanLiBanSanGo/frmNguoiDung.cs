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

namespace CSharp_QuanLiBanSanGo
{
    public partial class frmNguoiDung : Form
    {
        DBconfig dtBase = new DBconfig();
        string queryLoad = "SELECT * FROM tLogin";

        public frmNguoiDung()
        {
            InitializeComponent();
        }

        private bool checkValidation()
        {
            if (txtTenNguoiDung.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTenNguoiDung.Focus();

                return false;
            }

            if (txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMatKhau.Focus();

                return false;
            }

            return true;
        }

        private void refreshInput()
        {
            txtTenNguoiDung.Text = "";
            txtMatKhau.Text = "";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void frmNguoiDung_Load(object sender, EventArgs e)
        {
            DataTable dtNguoiDung = dtBase.getTable(queryLoad);
            dgvNguoiDung.DataSource = dtNguoiDung;

            dgvNguoiDung.Columns[0].HeaderText = "Tên người dùng";
            dgvNguoiDung.Columns[1].HeaderText = "Mật khẩu";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            refreshInput();
        }

        private void dgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTenNguoiDung.Text = dgvNguoiDung.CurrentRow.Cells[0].Value.ToString();
            txtMatKhau.Text = dgvNguoiDung.CurrentRow.Cells[1].Value.ToString();

            txtTenNguoiDung.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refreshInput();

            btnThem.Enabled = true;
            txtTenNguoiDung.Enabled = true;

            dgvNguoiDung.DataSource = dtBase.getTable(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtNguoiDung = dtBase.getTable($"SELECT * FROM tLogin WHERE Username = N'{txtTenNguoiDung.Text}'");

                if (dtNguoiDung.Rows.Count > 0)
                {
                    MessageBox.Show("Người dùng này đã có, vui lòng chọn tên khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTenNguoiDung.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm người dùng này vào không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            dtBase.getExcute($"INSERT INTO tLogin(Username, Password) VALUES(N'{txtTenNguoiDung.Text}', N'{txtMatKhau.Text}')");
                            MessageBox.Show("Bạn đã thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dgvNguoiDung.DataSource = dtBase.getTable(queryLoad);

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
                if (MessageBox.Show("Bạn có đổi mật khẩu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        dtBase.getExcute($"UPDATE tLogin SET Password = '{txtMatKhau.Text}' WHERE Username = N'{txtTenNguoiDung.Text}'");
                        MessageBox.Show("Bạn đã sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvNguoiDung.DataSource = dtBase.getTable(queryLoad);
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
            if (MessageBox.Show("Bạn có muốn xóa người dùng có tên là: " + txtTenNguoiDung.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dtBase.getExcute($"DELETE tLogin WHERE Username ='{txtTenNguoiDung.Text}'");
                    dgvNguoiDung.DataSource = dtBase.getTable(queryLoad);
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
