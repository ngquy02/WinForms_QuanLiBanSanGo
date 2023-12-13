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
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            lblVersion.Text = "Phiên bản 1.1";
            lblCopyright.Text = "2022. Đại học Giao thông Vận tải";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
