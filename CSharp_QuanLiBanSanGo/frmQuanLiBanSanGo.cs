using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            btnDMHangHoa.BackColor = Color.FromArgb(206, 223, 209);
            btnHoaDonBan.BackColor = Color.FromArgb(206, 223, 209);
            btnHoaDonNhap.BackColor = Color.FromArgb(206, 223, 209);
            btnNhanVien.BackColor = Color.FromArgb(206, 223, 209);
            btnKhachHang.BackColor = Color.FromArgb(206, 223, 209);
            btnNCC.BackColor = Color.FromArgb(206, 223, 209);
            btnBaoCao.BackColor = Color.FromArgb(206, 223, 209);
        }

        private void quảnLíNgườiDùngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmNguoiDung nguoiDung = new frmNguoiDung();
            nguoiDung.ShowDialog();
        }

        private void devSQLToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmSQL frmSQL = new frmSQL();
            frmSQL.ShowDialog();
        }

        private void quảnLíNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNguoiDung nguoiDung = new frmNguoiDung();
            nguoiDung.ShowDialog();
        }

        private void quảnLíCôngViệcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQLCongViec congViec = new frmQLCongViec();
            congViec.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnLogOut_Click(sender, e);
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnExit_Click(sender, e);
        }

        private void danhMụcHàngHoáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDMHangHoa_Click(sender, e);
        }

        private void hoáĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnHoaDonBan_Click(sender, e);
        }

        private void hoáĐơnNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnHoaDonNhap_Click(sender, e);
        }

        private void danhSáchNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnNhanVien_Click(sender, e);
        }

        private void danhSáchKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnKhachHang_Click(sender, e);
        }

        private void danhSáchNhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnNCC_Click(sender, e);
        }

        private void lậpBáoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBaoCao_Click(sender, e);
        }

        private void vềToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void viếtLệnhTruyVấnSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSQL frmSQL = new frmSQL();
            frmSQL.ShowDialog();
        }

        private void vềỨngDụngNàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnLogOut_Click(sender, e);
        }

        private void thoátToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnExit_Click(sender, e);
        }

        private void ptbHome_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            lblTieuDe.Text = "Trang chủ".ToUpper();
            resetBtn();
        }

        private void btnDMHangHoa_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDMHangHoa());
            lblTieuDe.Text = btnDMHangHoa.Text.ToUpper();
            resetBtn();
            btnDMHangHoa.BackColor = Color.FromArgb(168, 189, 171);
        }

        private void btnHoaDonBan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDonBan());
            lblTieuDe.Text = btnHoaDonBan.Text.ToUpper();
            resetBtn();
            btnHoaDonBan.BackColor = Color.FromArgb(168, 189, 171);
        }

        private void btnHoaDonNhap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDonNhap());
            lblTieuDe.Text = btnHoaDonNhap.Text.ToUpper();
            resetBtn();
            btnHoaDonNhap.BackColor = Color.FromArgb(168, 189, 171);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhanVien());
            lblTieuDe.Text = btnNhanVien.Text.ToUpper();
            resetBtn();
            btnNhanVien.BackColor = Color.FromArgb(168, 189, 171);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKhachHang());
            lblTieuDe.Text = btnKhachHang.Text.ToUpper();
            resetBtn();
            btnKhachHang.BackColor = Color.FromArgb(168, 189, 171);
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNCC());
            lblTieuDe.Text = btnNCC.Text.ToUpper();
            resetBtn();
            btnNCC.BackColor = Color.FromArgb(168, 189, 171);
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmBaoCao());
            lblTieuDe.Text = btnBaoCao.Text.ToUpper();
            resetBtn();
            btnBaoCao.BackColor = Color.FromArgb(168, 189, 171);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var th = new Thread(() => Application.Run(new frmDangNhap()));
                th.SetApartmentState(ApartmentState.STA);
                th.Start();

                this.Close();
            }    
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
