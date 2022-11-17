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
    public partial class frmReport1 : Form
    {
        DBconfig dtBase = new DBconfig();

        public frmReport1()
        {
            InitializeComponent();
        }

        private void frmReport1_Load(object sender, EventArgs e)
        {
            load_KhachHang();
        }

        private void load_KhachHang()
        {
            cboChonKH.DataSource = dtBase.table("SELECT * FROM tKhachHang");
            cboChonKH.ValueMember = "MaKhach";
            cboChonKH.DisplayMember = "TenKhach";
        }

        private void CleanInput()
        {
            cboChonKH.Text = "";
            cboChonKH.Enabled = true;
            btnBaoCao.Enabled = true;
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            rvBaoCao.LocalReport.ReportEmbeddedResource = "CSharp_QuanLiBanSanGo.Reports.Report1.rdlc";
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "Func_1";
            reportDataSource.Value = dtBase.table($"SELECT * FROM Func_1(N'{cboChonKH.SelectedValue}')");
            rvBaoCao.LocalReport.DataSources.Add(reportDataSource);
            this.rvBaoCao.RefreshReport();
            btnBaoCao.Enabled = false;
            cboChonKH.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            rvBaoCao.Reset();
            CleanInput();
        }
    }
}
