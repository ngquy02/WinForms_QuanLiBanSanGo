namespace CSharp_QuanLiBanSanGo
{
    partial class frmDangNhap
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDangKi = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.llbQuenMatKhau = new System.Windows.Forms.LinkLabel();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(419, 52);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(133, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐĂNG NHẬP";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDangKi);
            this.panel2.Controls.Add(this.btnThoat);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 282);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 55);
            this.panel2.TabIndex = 2;
            // 
            // btnDangKi
            // 
            this.btnDangKi.Image = global::CSharp_QuanLiBanSanGo.Properties.Resources.add_user;
            this.btnDangKi.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDangKi.Location = new System.Drawing.Point(239, 7);
            this.btnDangKi.Name = "btnDangKi";
            this.btnDangKi.Size = new System.Drawing.Size(110, 40);
            this.btnDangKi.TabIndex = 1;
            this.btnDangKi.Text = "Đăng &kí";
            this.btnDangKi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDangKi.UseVisualStyleBackColor = true;
            this.btnDangKi.Click += new System.EventHandler(this.btnDangKi_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Image = global::CSharp_QuanLiBanSanGo.Properties.Resources.close;
            this.btnThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnThoat.Location = new System.Drawing.Point(70, 7);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(110, 40);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "&Thoát";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitter2);
            this.panel3.Controls.Add(this.splitter1);
            this.panel3.Controls.Add(this.btnDangNhap);
            this.panel3.Controls.Add(this.llbQuenMatKhau);
            this.panel3.Controls.Add(this.txtMatKhau);
            this.panel3.Controls.Add(this.txtTenDangNhap);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(419, 230);
            this.panel3.TabIndex = 1;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(419, 1);
            this.splitter2.TabIndex = 0;
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 229);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(419, 1);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Image = global::CSharp_QuanLiBanSanGo.Properties.Resources.enter;
            this.btnDangNhap.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDangNhap.Location = new System.Drawing.Point(240, 140);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(110, 40);
            this.btnDangNhap.TabIndex = 3;
            this.btnDangNhap.Text = "Đăng &nhập";
            this.btnDangNhap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // llbQuenMatKhau
            // 
            this.llbQuenMatKhau.AutoSize = true;
            this.llbQuenMatKhau.Location = new System.Drawing.Point(68, 154);
            this.llbQuenMatKhau.Name = "llbQuenMatKhau";
            this.llbQuenMatKhau.Size = new System.Drawing.Size(80, 13);
            this.llbQuenMatKhau.TabIndex = 4;
            this.llbQuenMatKhau.TabStop = true;
            this.llbQuenMatKhau.Text = "&Quên mật khẩu";
            this.llbQuenMatKhau.Click += new System.EventHandler(this.llbQuenMatKhau_Click);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(175, 103);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(175, 20);
            this.txtMatKhau.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtMatKhau, "Điền mật khẩu");
            this.txtMatKhau.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMatKhau_KeyPress);
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Location = new System.Drawing.Point(175, 66);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(175, 20);
            this.txtTenDangNhap.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtTenDangNhap, "Điền tên đăng nhập");
            this.txtTenDangNhap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTenDangNhap_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mật khẩu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên đăng nhập";
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 337);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnDangKi;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llbQuenMatKhau;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}