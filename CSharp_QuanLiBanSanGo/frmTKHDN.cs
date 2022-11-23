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
    public partial class frmTKHDN : Form
    {
        DBconfig dtBase = new DBconfig();
        string queryLoad = "SELECT tHoaDonNhap.SoHDN, MaNV, MaHang, NgayNhap, SoLuong, DonGia, MaNCC, GiamGia, ThanhTien FROM tHoaDonNhap JOIN tChiTietHoaDonNhap ON tHoaDonNhap.SoHDN = tChiTietHoaDonNhap.SoHDN";
        DateTime date = new DateTime(1900, 01, 01, 0, 0, 0);

        public frmTKHDN()
        {
            InitializeComponent();
        }

        private void refreshInput()
        {
            txtSoHDN.Text = "";
            txtNhanVien.Text = "";
            txtMaHang.Text = "";
            dtpNgayNhap.Value = date;
            txtSoLuong.Text = "";
            txtDonGia.Text = "";
            txtNCC.Text = "";
            txtGiamGia.Text = "";
            txtThanhTien.Text = "";
        }

        private void frmTKHDN_Load(object sender, EventArgs e)
        {
            DataTable dtHDB = dtBase.getTable(queryLoad);
            dgvChiTietHDN.DataSource = dtHDB;

            dgvChiTietHDN.Columns[0].HeaderText = "Số hoá đơn nhập";
            dgvChiTietHDN.Columns[1].HeaderText = "Nhân viên";
            dgvChiTietHDN.Columns[2].HeaderText = "Mặt hàng";
            dgvChiTietHDN.Columns[3].HeaderText = "Ngày nhập";
            dgvChiTietHDN.Columns[4].HeaderText = "Số lượng";
            dgvChiTietHDN.Columns[5].HeaderText = "Đơn giá";
            dgvChiTietHDN.Columns[6].HeaderText = "Nhà cung cấp";
            dgvChiTietHDN.Columns[7].HeaderText = "Giảm giá";
            dgvChiTietHDN.Columns[8].HeaderText = "Thành tiền";

            refreshInput();
        }

        private void dgvChiTietHDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSoHDN.Text = dgvChiTietHDN.CurrentRow.Cells[0].Value.ToString();
            txtNhanVien.Text = dgvChiTietHDN.CurrentRow.Cells[1].Value.ToString();
            txtMaHang.Text = dgvChiTietHDN.CurrentRow.Cells[2].Value.ToString();
            dtpNgayNhap.Text = dgvChiTietHDN.CurrentRow.Cells[3].Value.ToString();
            txtSoLuong.Text = dgvChiTietHDN.CurrentRow.Cells[4].Value.ToString();
            txtDonGia.Text = dgvChiTietHDN.CurrentRow.Cells[5].Value.ToString();
            txtNCC.Text = dgvChiTietHDN.CurrentRow.Cells[6].Value.ToString();
            txtGiamGia.Text = dgvChiTietHDN.CurrentRow.Cells[7].Value.ToString();
            txtThanhTien.Text = dgvChiTietHDN.CurrentRow.Cells[8].Value.ToString();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            refreshInput();

            dgvChiTietHDN.DataSource = dtBase.getTable(queryLoad);
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string dk = "";

            if (txtMaHang.Text.Trim() != "")
            {
                dk += $" AND MaHang LIKE '%{txtMaHang.Text}%'";
            }

            if (dtpNgayNhap.Value != date)
            {
                dk += $" AND NgayNhap = '{dtpNgayNhap.Value.ToString("yyyy-MM-dd")}'";
            }

            if (txtNCC.Text.Trim() != "")
            {
                dk += $" AND MaNCC LIKE N'%{txtNCC.Text}%'";
            }

            if (dk != "")
            {
                string find = queryLoad + " WHERE tHoaDonNhap.SoHDN LIKE N'%%'" + dk;
                dgvChiTietHDN.DataSource = dtBase.getTable(find);
            }
        }
    }
}
