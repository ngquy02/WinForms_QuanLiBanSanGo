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
    public partial class frmBaoCao : Form
    {
        DBconfig dtBase = new DBconfig();

        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmReport1_Load(object sender, EventArgs e)
        {
            load_KhachHang();
            refreshInput();
        }

        private void load_KhachHang()
        {
            cboChonKH.DataSource = dtBase.getTable("SELECT * FROM tKhachHang");
            cboChonKH.ValueMember = "MaKhach";
            cboChonKH.DisplayMember = "TenKhach";
        }

        private void refreshInput()
        {
            cboChonBaoCao.SelectedIndex = -1;
            cboChonKH.SelectedIndex = -1;
            cboChonThang.SelectedIndex = -1;
            txtNhapNam.Text = "";
            cboChonBaoCao.Enabled = true;
            cboChonKH.Enabled = false;
            cboChonThang.Enabled = false;
            txtNhapNam.Enabled = false;
            btnBaoCao.Enabled = false;
        }

        private void cboChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnBaoCao.Enabled = true;

            if (cboChonBaoCao.SelectedIndex == 0)
            {
                cboChonKH.Enabled = true;
                cboChonThang.Enabled = false;
                txtNhapNam.Enabled = false;
            }

            if (cboChonBaoCao.SelectedIndex == 1)
            {
                cboChonThang.Enabled = true;
                cboChonKH.Enabled = false;
                txtNhapNam.Enabled = false;
            }

            if (cboChonBaoCao.SelectedIndex == 2)
            {
                txtNhapNam.Enabled = true;
                cboChonKH.Enabled = false;
                cboChonThang.Enabled = false;
            }

            if (cboChonBaoCao.SelectedIndex == 3)
            {
                cboChonThang.Enabled = true;
                txtNhapNam.Enabled = false;
                cboChonKH.Enabled = false;
            }
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            cboChonBaoCao.Enabled = false;

            if(cboChonBaoCao.SelectedIndex == 0)
            {
                if(cboChonKH.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy chọn khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboChonKH.Focus();
                }
                else
                {
                    rvBaoCao.LocalReport.ReportEmbeddedResource = "CSharp_QuanLiBanSanGo.Reports.Report1.rdlc";
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "Report1";
                    reportDataSource.Value = dtBase.getTable($"SELECT * FROM Report1(N'{cboChonKH.SelectedValue}')");
                    rvBaoCao.LocalReport.DataSources.Add(reportDataSource);
                    this.rvBaoCao.RefreshReport();
                    btnBaoCao.Enabled = false;
                    cboChonKH.Enabled = false;
                }    
            }
            
            if(cboChonBaoCao.SelectedIndex == 1)
            {
                if (cboChonThang.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy chọn tháng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboChonThang.Focus();
                }
                else
                {
                    rvBaoCao.LocalReport.ReportEmbeddedResource = "CSharp_QuanLiBanSanGo.Reports.Report2.rdlc";
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "Report2";
                    reportDataSource.Value = dtBase.getTable($"SELECT * FROM Report2({cboChonThang.Text})");
                    rvBaoCao.LocalReport.DataSources.Add(reportDataSource);
                    this.rvBaoCao.RefreshReport();
                    btnBaoCao.Enabled = false;
                    cboChonThang.Enabled = false;
                }
            } 
            
            if(cboChonBaoCao.SelectedIndex == 2)
            {
                if (txtNhapNam.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy nhập năm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNhapNam.Focus();
                }
                else
                {
                    rvBaoCao.LocalReport.ReportEmbeddedResource = "CSharp_QuanLiBanSanGo.Reports.Report3.rdlc";
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "Report3";
                    reportDataSource.Value = dtBase.getTable($"SELECT * FROM Report3({txtNhapNam.Text})");
                    rvBaoCao.LocalReport.DataSources.Add(reportDataSource);
                    this.rvBaoCao.RefreshReport();
                    btnBaoCao.Enabled = false;
                    txtNhapNam.Enabled = false;
                }
            }  
            
            if(cboChonBaoCao.SelectedIndex == 3)
            {
                if (cboChonThang.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy chọn tháng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboChonThang.Focus();
                }
                else
                {
                    rvBaoCao.LocalReport.ReportEmbeddedResource = "CSharp_QuanLiBanSanGo.Reports.Report4.rdlc";
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "Report4";
                    reportDataSource.Value = dtBase.getTable($"SELECT * FROM Report4('{cboChonThang.Text}')");
                    rvBaoCao.LocalReport.DataSources.Add(reportDataSource);
                    this.rvBaoCao.RefreshReport();
                    btnBaoCao.Enabled = false;
                    cboChonThang.Enabled = false;
                }    
            }    
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            rvBaoCao.Reset();
            refreshInput();
        }
    }
}
