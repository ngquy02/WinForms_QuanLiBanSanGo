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
        }

        private void rtbSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                btnThucThi_Click(sender, e);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            rtbSQL.Text = "";
        }

        private void frmSQL_Load(object sender, EventArgs e)
        {
            
        }
    }
}
