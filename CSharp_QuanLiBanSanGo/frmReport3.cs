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
    public partial class frmReport3 : Form
    {
        DBconfig dtBase = new DBconfig();

        public frmReport3()
        {
            InitializeComponent();
        }

        private void frmReport3_Load(object sender, EventArgs e)
        {

            this.rvBaoCao.RefreshReport();
        }

        private void CleanInput()
        {
            txtNhapNam.Text = "";
            txtNhapNam.Enabled = true;
            btnBaoCao.Enabled = true;
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            rvBaoCao.LocalReport.ReportEmbeddedResource = "CSharp_QuanLiBanSanGo.Reports.Report3.rdlc";
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "Func_3";
            reportDataSource.Value = dtBase.table($"SELECT * FROM Func_3({txtNhapNam.Text})");
            rvBaoCao.LocalReport.DataSources.Add(reportDataSource);
            this.rvBaoCao.RefreshReport();
            btnBaoCao.Enabled = false;
            txtNhapNam.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            rvBaoCao.Reset();
            CleanInput();
        }
    }
}
