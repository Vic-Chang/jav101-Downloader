namespace Jav101Downloader
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_download = new System.Windows.Forms.Button();
            this.tb_url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_save_path = new System.Windows.Forms.Button();
            this.tb_path = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lb_version = new System.Windows.Forms.Label();
            this.lb_progress = new System.Windows.Forms.Label();
            this.lb_bitrate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_download
            // 
            this.btn_download.BackColor = System.Drawing.Color.Coral;
            this.btn_download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_download.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_download.ForeColor = System.Drawing.Color.SlateBlue;
            this.btn_download.Location = new System.Drawing.Point(334, 17);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(75, 23);
            this.btn_download.TabIndex = 0;
            this.btn_download.Text = "開始下載";
            this.btn_download.UseVisualStyleBackColor = false;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // tb_url
            // 
            this.tb_url.Location = new System.Drawing.Point(51, 17);
            this.tb_url.Name = "tb_url";
            this.tb_url.Size = new System.Drawing.Size(277, 22);
            this.tb_url.TabIndex = 1;
            this.tb_url.Text = "https://v.jav101.com/play/avid595f4ee3b2bc1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "網址:";
            // 
            // btn_save_path
            // 
            this.btn_save_path.Location = new System.Drawing.Point(10, 52);
            this.btn_save_path.Name = "btn_save_path";
            this.btn_save_path.Size = new System.Drawing.Size(73, 23);
            this.btn_save_path.TabIndex = 3;
            this.btn_save_path.Text = "儲存位置";
            this.btn_save_path.UseVisualStyleBackColor = true;
            this.btn_save_path.Click += new System.EventHandler(this.btn_save_path_Click);
            // 
            // tb_path
            // 
            this.tb_path.ForeColor = System.Drawing.Color.Tomato;
            this.tb_path.Location = new System.Drawing.Point(81, 53);
            this.tb_path.Name = "tb_path";
            this.tb_path.ReadOnly = true;
            this.tb_path.Size = new System.Drawing.Size(328, 22);
            this.tb_path.TabIndex = 4;
            this.tb_path.Text = "預設下載路徑為桌面";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 89);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(365, 22);
            this.progressBar1.TabIndex = 5;
            // 
            // lb_version
            // 
            this.lb_version.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lb_version.Cursor = System.Windows.Forms.Cursors.Help;
            this.lb_version.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_version.Location = new System.Drawing.Point(339, 116);
            this.lb_version.Name = "lb_version";
            this.lb_version.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lb_version.Size = new System.Drawing.Size(70, 12);
            this.lb_version.TabIndex = 6;
            this.lb_version.Text = "v 1.0";
            this.lb_version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lb_version.Click += new System.EventHandler(this.lb_version_Click);
            // 
            // lb_progress
            // 
            this.lb_progress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lb_progress.Location = new System.Drawing.Point(383, 95);
            this.lb_progress.Name = "lb_progress";
            this.lb_progress.Size = new System.Drawing.Size(35, 12);
            this.lb_progress.TabIndex = 8;
            this.lb_progress.Text = "0 %";
            this.lb_progress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_bitrate
            // 
            this.lb_bitrate.AutoSize = true;
            this.lb_bitrate.Location = new System.Drawing.Point(13, 116);
            this.lb_bitrate.Name = "lb_bitrate";
            this.lb_bitrate.Size = new System.Drawing.Size(8, 12);
            this.lb_bitrate.TabIndex = 9;
            this.lb_bitrate.Text = " ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 137);
            this.Controls.Add(this.lb_bitrate);
            this.Controls.Add(this.lb_progress);
            this.Controls.Add(this.lb_version);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tb_path);
            this.Controls.Add(this.btn_save_path);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_url);
            this.Controls.Add(this.btn_download);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "啪啪啪研習所下載器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.TextBox tb_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_version;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox tb_path;
        private System.Windows.Forms.Button btn_save_path;
        private System.Windows.Forms.Label lb_progress;
        private System.Windows.Forms.Label lb_bitrate;
    }
}

