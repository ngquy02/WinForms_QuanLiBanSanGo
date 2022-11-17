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

        private bool isCheck()
        {
            if (cboMatHang.Text.Trim() == "")
            {
                MessageBox.Show("Hãy chọn mặt hàng");
                cboMatHang.Focus();

                return false;
            }

            if (txtSoLuong.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số lượng");
                txtSoLuong.Focus();

                return false;
            }

            if(txtGiamGia.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập giảm giá");
                txtGiamGia.Focus();

                return false;
            }    

            return true;
        }

        private void CleanInput()
        {
            cboMatHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "";
            txtThanhTien.Text = "";
        }

        private void load_MatHang()
        {
            cboMatHang.DataSource = dtBase.table("SELECT * FROM tDMHangHoa");
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

            DataTable dtChiTietHDB = dtBase.table(queryLoad);
            dgvChiTietHDB.DataSource = dtChiTietHDB;

            dgvChiTietHDB.Columns[0].HeaderText = "Số hoá đơn bán";
            dgvChiTietHDB.Columns[1].HeaderText = "Tên mặt hàng";
            dgvChiTietHDB.Columns[2].HeaderText = "Số lượng";
            dgvChiTietHDB.Columns[3].HeaderText = "Giảm giá";
            dgvChiTietHDB.Columns[4].HeaderText = "Thành tiền";

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            load_MatHang();

            CleanInput();
        }

        private void dgvChiTietHDB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cboMatHang.Text = dgvChiTietHDB.CurrentRow.Cells[1].Value.ToString();
            txtSoLuong.Text = dgvChiTietHDB.CurrentRow.Cells[2].Value.ToString();
            txtGiamGia.Text = dgvChiTietHDB.CurrentRow.Cells[3].Value.ToString();
            txtThanhTien.Text = dgvChiTietHDB.CurrentRow.Cells[4].Value.ToString();

            string[] arrGiamGia = dgvChiTietHDB.CurrentRow.Cells[3].Value.ToString().Split(',');
            txtGiamGia.Text = arrGiamGia[0];

            cboMatHang.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            CleanInput();

            btnThem.Enabled = true;
            cboMatHang.Enabled = true;

            dgvChiTietHDB.DataSource = dtBase.table(queryLoad);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                DataTable dtChiTietHDB = dtBase.table($"SELECT * FROM tChiTietHoaDonBan WHERE SoHDB = N'{lblSoHDB.Text}' AND MaHang ='{cboMatHang.SelectedValue}'");

                if (dtChiTietHDB.Rows.Count > 0)
                {
                    MessageBox.Show("Mặt hàng này đã có, hãy chọn mặt hàng khác");
                    cboMatHang.Focus();
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn thêm mặt hàng này vào danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        dtBase.Excute($"INSERT INTO tChiTietHoaDonBan(SoHDB, MaHang, SoLuong, GiamGia) VALUES(N'{lblSoHDB.Text}', N'{cboMatHang.SelectedValue}', {txtSoLuong.Text}, {txtGiamGia.Text})");
                        MessageBox.Show("Bạn đã thêm thành công");

                        dgvChiTietHDB.DataSource = dtBase.table(queryLoad);

                        CleanInput();
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (isCheck())
            {
                if (MessageBox.Show("Bạn có sửa thông tin này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    dtBase.Excute($"UPDATE tChiTietHoaDonBan SET SoLuong = {txtSoLuong.Text} WHERE SoHDB = '{lblSoHDB.Text}' AND MaHang = N'{cboMatHang.SelectedValue}'");
                    dtBase.Excute($"UPDATE tChiTietHoaDonBan SET GiamGia = {txtGiamGia} WHERE SoHDB = '{lblSoHDB.Text}' AND MaHang = N'{cboMatHang.SelectedValue}'");

                    CleanInput();

                    dgvChiTietHDB.DataSource = dtBase.table(queryLoad);
                    MessageBox.Show("Bạn đã sửa thông tin thành công");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa mặt hàng có mã là: " + cboMatHang.SelectedValue.ToString() + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                dtBase.Excute($"DELETE tChiTietHoaDonBan WHERE SoHDB ='{lblSoHDB.Text}' AND MaHang = N'{cboMatHang.SelectedValue}'");
                dgvChiTietHDB.DataSource = dtBase.table(queryLoad);

                CleanInput();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
