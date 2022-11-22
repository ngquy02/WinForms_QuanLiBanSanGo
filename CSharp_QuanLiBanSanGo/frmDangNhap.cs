using CSharp_QuanLiBanSanGo.Class;
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
    public partial class frmDangNhap : Form
    {
        DBconfig dtBase = new DBconfig();

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private bool checkValidation()
        {
            if(txtTenDangNhap.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên người dùng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTenDangNhap.Focus();

                return false;
            }    

            if(txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMatKhau.Focus();

                return false;
            }    

            return true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtDangNhap = dtBase.getTable($"SELECT * FROM tLogin WHERE Username = N'{txtTenDangNhap.Text}' AND Password = N'{txtMatKhau.Text}'");

                if (dtDangNhap.Rows.Count > 0)
                {
                    try
                    {
                        var th = new Thread(() => Application.Run(new frmQuanLiBanSanGo()));
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Không có tài khoản này hoặc sai mật khẩu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '*')
            {
                txtMatKhau.PasswordChar = '\0';
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
            }
        }

        private void llbQuenMatKhau_Click(object sender, EventArgs e)
        {
            var th = new Thread(() => Application.Run(new frmQuenMatKhau()));
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            this.Close();
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            var th = new Thread(() => Application.Run(new frmDangKi()));
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }    
        }

        private void txtTenDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
            }
        }

        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDangNhap_Click(sender, e);
            }
        }
    }
}
