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
    public partial class frmQuenMatKhau : Form
    {
        DBconfig dtBase = new DBconfig();

        public frmQuenMatKhau()
        {
            InitializeComponent();
        }

        private bool checkValidation()
        {
            if(txtTenDangNhap.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên đăng nhập", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();

                return false;
            }    

            return true;
        }

        private void btnLayLaiMK_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                DataTable dtQuenMK = dtBase.getTable($"SELECT * FROM tLogin WHERE Username = N'{txtTenDangNhap.Text}'");

                if(dtQuenMK.Rows.Count > 0)
                {
                    dgvMatKhau.DataSource = dtBase.getTable($"SELECT Password FROM tLogin WHERE Username = N'{txtTenDangNhap.Text}'");
                }
                else
                {
                    MessageBox.Show("Không có tên đăng nhập này, hãy nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
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
