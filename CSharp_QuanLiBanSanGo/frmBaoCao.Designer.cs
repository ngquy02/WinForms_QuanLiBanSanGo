namespace CSharp_QuanLiBanSanGo
{
    partial class frmBaoCao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaoCao));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNhapNam = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboChonKH = new System.Windows.Forms.ComboBox();
            this.cboChonThang = new System.Windows.Forms.ComboBox();
            this.cboChonBaoCao = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rvBaoCao = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 175);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnBaoCao);
            this.groupBox2.Controls.Add(this.btnLamMoi);
            this.groupBox2.Location = new System.Drawing.Point(805, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 157);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thao tác";
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBaoCao.Image = global::CSharp_QuanLiBanSanGo.Properties.Resources.printer;
            this.btnBaoCao.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBaoCao.Location = new System.Drawing.Point(18, 30);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(130, 40);
            this.btnBaoCao.TabIndex = 5;
            this.btnBaoCao.Text = "&Xuất báo cáo";
            this.btnBaoCao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBaoCao.UseVisualStyleBackColor = true;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.Image = global::CSharp_QuanLiBanSanGo.Properties.Resources.refresh;
            this.btnLamMoi.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLamMoi.Location = new System.Drawing.Point(18, 95);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(130, 40);
            this.btnLamMoi.TabIndex = 6;
            this.btnLamMoi.Text = "&Làm mới";
            this.btnLamMoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNhapNam);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboChonKH);
            this.groupBox1.Controls.Add(this.cboChonThang);
            this.groupBox1.Controls.Add(this.cboChonBaoCao);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 157);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chọn báo cáo và đầu vào";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Chọn loại báo cáo";
            // 
            // txtNhapNam
            // 
            this.txtNhapNam.Location = new System.Drawing.Point(169, 119);
            this.txtNhapNam.Name = "txtNhapNam";
            this.txtNhapNam.Size = new System.Drawing.Size(176, 20);
            this.txtNhapNam.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Chọn khách hàng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Nhập năm";
            // 
            // cboChonKH
            // 
            this.cboChonKH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChonKH.FormattingEnabled = true;
            this.cboChonKH.Location = new System.Drawing.Point(169, 53);
            this.cboChonKH.Name = "cboChonKH";
            this.cboChonKH.Size = new System.Drawing.Size(176, 21);
            this.cboChonKH.TabIndex = 2;
            // 
            // cboChonThang
            // 
            this.cboChonThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChonThang.FormattingEnabled = true;
            this.cboChonThang.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cboChonThang.Location = new System.Drawing.Point(169, 86);
            this.cboChonThang.Name = "cboChonThang";
            this.cboChonThang.Size = new System.Drawing.Size(176, 21);
            this.cboChonThang.TabIndex = 3;
            // 
            // cboChonBaoCao
            // 
            this.cboChonBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboChonBaoCao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChonBaoCao.FormattingEnabled = true;
            this.cboChonBaoCao.Items.AddRange(new object[] {
            "Báo cáo danh sách 2 hàng hoá được mua nhiều nhất từ một khách hàng chọn trước",
            "Báo cáo danh sách hoá đơn và tổng tiền nhập hàng theo tháng chọn trước",
            "Báo cáo danh sách 5 hoá đơn có tổng tiền bán hàng nhiều nhất theo năm chọn trước",
            "Báo cáo danh sách họ tên, tổng tiền của 2 nhân viên bán được nhiều tiền nhất theo" +
                " một tháng chọn trước"});
            this.cboChonBaoCao.Location = new System.Drawing.Point(169, 20);
            this.cboChonBaoCao.Name = "cboChonBaoCao";
            this.cboChonBaoCao.Size = new System.Drawing.Size(612, 21);
            this.cboChonBaoCao.TabIndex = 1;
            this.cboChonBaoCao.SelectedIndexChanged += new System.EventHandler(this.cboChonBaoCao_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Chọn tháng";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rvBaoCao);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 175);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 386);
            this.panel2.TabIndex = 1;
            // 
            // rvBaoCao
            // 
            this.rvBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvBaoCao.Location = new System.Drawing.Point(0, 0);
            this.rvBaoCao.Name = "rvBaoCao";
            this.rvBaoCao.ServerReport.BearerToken = null;
            this.rvBaoCao.Size = new System.Drawing.Size(984, 386);
            this.rvBaoCao.TabIndex = 0;
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo cáo";
            this.Load += new System.EventHandler(this.frmReport1_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.ComboBox cboChonKH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer rvBaoCao;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.ComboBox cboChonBaoCao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboChonThang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNhapNam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}