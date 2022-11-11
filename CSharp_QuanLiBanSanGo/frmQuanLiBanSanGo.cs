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
    public partial class frmQuanLiBanSanGo : Form
    {
        private Form currentFormChild;

        public frmQuanLiBanSanGo()
        {
            InitializeComponent();
        }

        private void frmQuanLiBanSanGo_Load(object sender, EventArgs e)
        {

        }

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlBody.Controls.Add(childForm);
            pnlBody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        
        private void resetBtn()
        {
            btnDMHangHoa.BackColor = Color.FromArgb(64, 0, 0);
            btnHoaDonBan.BackColor = Color.FromArgb(64, 0, 0);
            btnHoaDonNhap.BackColor = Color.FromArgb(64, 0, 0);
            btnNhanVien.BackColor = Color.FromArgb(64, 0, 0);
            btnKhachHang.BackColor = Color.FromArgb(64, 0, 0);
            btnNCC.BackColor = Color.FromArgb(64, 0, 0);
        }

        private void ptbHome_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            lblTieuDe.Text = "Quản lí bán sàn gỗ";
            resetBtn();
        }

        private void btnDMHangHoa_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDMHangHoa());
            lblTieuDe.Text = btnDMHangHoa.Text;
            resetBtn();
            btnDMHangHoa.BackColor = Color.Black;
        }

        private void btnHoaDonBan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDonBan());
            lblTieuDe.Text = btnHoaDonBan.Text;
            resetBtn();
            btnHoaDonBan.BackColor = Color.Black;
        }

        private void btnHoaDonNhap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDonNhap());
            lblTieuDe.Text = btnHoaDonNhap.Text;
            resetBtn();
            btnHoaDonNhap.BackColor = Color.Black;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhanVien());
            lblTieuDe.Text = btnNhanVien.Text;
            resetBtn();
            btnNhanVien.BackColor = Color.Black;
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKhachHang());
            lblTieuDe.Text = btnKhachHang.Text;
            resetBtn();
            btnKhachHang.BackColor = Color.Black;
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNCC());
            lblTieuDe.Text = btnNCC.Text;
            resetBtn();
            btnNCC.BackColor = Color.Black;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
