using CSharp_QuanLiBanSanGo.Class;
using Microsoft.Reporting.WinForms;
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
    public partial class frmReport4 : Form
    {
        DBconfig dtBase = new DBconfig();

        public frmReport4()
        {
            InitializeComponent();
        }

        private void frmReport4_Load(object sender, EventArgs e)
        {

            this.rvBaoCao.RefreshReport();
        }

        private void CleanInput()
        {
            cboChonThang.Text = "";
            cboChonThang.Enabled = true;
            btnBaoCao.Enabled = true;
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            rvBaoCao.LocalReport.ReportEmbeddedResource = "CSharp_QuanLiBanSanGo.Reports.Report4.rdlc";
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "Func_4";
            reportDataSource.Value = dtBase.table($"SELECT * FROM Func_4('{cboChonThang.Text}')");
            rvBaoCao.LocalReport.DataSources.Add(reportDataSource);
            this.rvBaoCao.RefreshReport();
            btnBaoCao.Enabled = false;
            cboChonThang.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            rvBaoCao.Reset();
            CleanInput();
        }
    }
}
