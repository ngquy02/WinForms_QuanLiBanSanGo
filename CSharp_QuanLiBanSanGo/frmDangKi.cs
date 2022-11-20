using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CSharp_QuanLiBanSanGo.Class;
using System.Threading;

namespace CSharp_QuanLiBanSanGo
{
    public partial class frmDangKi : Form
    {
        DBconfig dtBase = new DBconfig();

        public frmDangKi()
        {
            InitializeComponent();
        }

        public bool checkAccount(string account)
        {
            return Regex.IsMatch(account, "^[a-zA-Z0-9]{3,50}$");
        }

        private bool checkValidation()
        {
            if (txtTenDangNhap.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên người dùng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTenDangNhap.Focus();

                return false;
            }

            if(!checkAccount(txtTenDangNhap.Text))
            {
                MessageBox.Show("Tên đăng nhập không hợp lệ, tên đăng nhập chỉ chứa các chữ cái hoặc số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();

                return false;
            }    

            if (txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMatKhau.Focus();

                return false;
            }

            return true;
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            if(checkValidation())
            {
                DataTable dtDangKi = dtBase.getTable($"SELECT * FROM tLogin WHERE Username = N'{txtTenDangNhap.Text}'");

                if(dtDangKi.Rows.Count > 0)
                {
                    MessageBox.Show("Tên người dùng này đã được sử dụng, vui lòng chọn tên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTenDangNhap.Focus();
                }    
                else
                {
                    try
                    {
                        string query = $"INSERT INTO tLogin VALUES(N'{txtTenDangNhap.Text}', N'{txtMatKhau.Text}')";
                        dtBase.getExcute(query);
                        MessageBox.Show("Đăng kí thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }    
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            var th = new Thread(() => Application.Run(new frmDangNhap()));
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
