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
    public partial class frmSQL : Form
    {
        DBconfig dtBase = new DBconfig();

        public frmSQL()
        {
            InitializeComponent();
        }

        private void btnThucThi_Click(object sender, EventArgs e)
        {
            string query = rtbSQL.Text;

            if(rtbSQL.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập lệnh truy vấn");
            }
            else
            {
                try
                {
                    dgvSQL.DataSource = dtBase.getTable(query);
                    MessageBox.Show("Thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }    
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }    
        }

        private void rtbSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                btnThucThi_Click(sender, e);
            }
        }
    }
}
