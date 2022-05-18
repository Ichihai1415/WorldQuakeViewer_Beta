namespace WorldQuakeViewer
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.JsonTimer = new System.Windows.Forms.Timer(this.components);
            this.USGS0 = new System.Windows.Forms.Label();
            this.USGS1 = new System.Windows.Forms.Label();
            this.ShingenImg = new System.Windows.Forms.PictureBox();
            this.MainImg = new System.Windows.Forms.PictureBox();
            this.ShingenLabel1 = new System.Windows.Forms.Label();
            this.ShingenLabel2 = new System.Windows.Forms.Label();
            this.NetworkError = new System.Windows.Forms.Label();
            this.USGS2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ShingenImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainImg)).BeginInit();
            this.SuspendLayout();
            // 
            // JsonTimer
            // 
            this.JsonTimer.Enabled = true;
            this.JsonTimer.Interval = 300;
            this.JsonTimer.Tick += new System.EventHandler(this.JsonTimer_Tick);
            // 
            // USGS0
            // 
            this.USGS0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(30)))));
            this.USGS0.Font = new System.Drawing.Font("Koruri Regular", 10F);
            this.USGS0.Location = new System.Drawing.Point(0, 0);
            this.USGS0.Margin = new System.Windows.Forms.Padding(0);
            this.USGS0.Name = "USGS0";
            this.USGS0.Size = new System.Drawing.Size(400, 100);
            this.USGS0.TabIndex = 1;
            this.USGS0.Text = "USGS地震情報";
            // 
            // USGS1
            // 
            this.USGS1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(60)))));
            this.USGS1.Font = new System.Drawing.Font("Koruri Regular", 11F);
            this.USGS1.Location = new System.Drawing.Point(3, 17);
            this.USGS1.Margin = new System.Windows.Forms.Padding(0);
            this.USGS1.Name = "USGS1";
            this.USGS1.Size = new System.Drawing.Size(396, 81);
            this.USGS1.TabIndex = 2;
            this.USGS1.Text = "情報取得中...";
            // 
            // ShingenImg
            // 
            this.ShingenImg.BackColor = System.Drawing.Color.White;
            this.ShingenImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ShingenImg.ImageLocation = "";
            this.ShingenImg.Location = new System.Drawing.Point(181, 281);
            this.ShingenImg.Margin = new System.Windows.Forms.Padding(0);
            this.ShingenImg.Name = "ShingenImg";
            this.ShingenImg.Size = new System.Drawing.Size(0, 0);
            this.ShingenImg.TabIndex = 5;
            this.ShingenImg.TabStop = false;
            // 
            // MainImg
            // 
            this.MainImg.BackColor = System.Drawing.Color.Black;
            this.MainImg.Location = new System.Drawing.Point(400, 500);
            this.MainImg.Margin = new System.Windows.Forms.Padding(0);
            this.MainImg.Name = "MainImg";
            this.MainImg.Size = new System.Drawing.Size(1800, 900);
            this.MainImg.TabIndex = 0;
            this.MainImg.TabStop = false;
            // 
            // ShingenLabel1
            // 
            this.ShingenLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ShingenLabel1.Font = new System.Drawing.Font("MS UI Gothic", 50F);
            this.ShingenLabel1.ForeColor = System.Drawing.Color.Red;
            this.ShingenLabel1.Location = new System.Drawing.Point(0, 100);
            this.ShingenLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.ShingenLabel1.Name = "ShingenLabel1";
            this.ShingenLabel1.Size = new System.Drawing.Size(0, 0);
            this.ShingenLabel1.TabIndex = 6;
            this.ShingenLabel1.Text = "×";
            this.ShingenLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShingenLabel2
            // 
            this.ShingenLabel2.BackColor = System.Drawing.Color.Black;
            this.ShingenLabel2.Font = new System.Drawing.Font("MS UI Gothic", 60F);
            this.ShingenLabel2.ForeColor = System.Drawing.Color.Yellow;
            this.ShingenLabel2.Location = new System.Drawing.Point(0, 100);
            this.ShingenLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.ShingenLabel2.Name = "ShingenLabel2";
            this.ShingenLabel2.Size = new System.Drawing.Size(0, 0);
            this.ShingenLabel2.TabIndex = 7;
            this.ShingenLabel2.Text = "×";
            this.ShingenLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NetworkError
            // 
            this.NetworkError.AutoSize = true;
            this.NetworkError.Font = new System.Drawing.Font("Koruri Regular", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NetworkError.Location = new System.Drawing.Point(284, 485);
            this.NetworkError.Name = "NetworkError";
            this.NetworkError.Size = new System.Drawing.Size(0, 17);
            this.NetworkError.TabIndex = 8;
            // 
            // USGS2
            // 
            this.USGS2.AutoSize = true;
            this.USGS2.Font = new System.Drawing.Font("Roboto", 20F);
            this.USGS2.Location = new System.Drawing.Point(264, 63);
            this.USGS2.Name = "USGS2";
            this.USGS2.Size = new System.Drawing.Size(0, 33);
            this.USGS2.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(400, 500);
            this.Controls.Add(this.USGS2);
            this.Controls.Add(this.NetworkError);
            this.Controls.Add(this.ShingenLabel1);
            this.Controls.Add(this.ShingenLabel2);
            this.Controls.Add(this.ShingenImg);
            this.Controls.Add(this.USGS1);
            this.Controls.Add(this.USGS0);
            this.Controls.Add(this.MainImg);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "WorldQuakeViewer";
            ((System.ComponentModel.ISupportInitialize)(this.ShingenImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer JsonTimer;
        private System.Windows.Forms.PictureBox MainImg;
        private System.Windows.Forms.Label USGS0;
        private System.Windows.Forms.Label USGS1;
        private System.Windows.Forms.PictureBox ShingenImg;
        private System.Windows.Forms.Label ShingenLabel1;
        private System.Windows.Forms.Label ShingenLabel2;
        private System.Windows.Forms.Label NetworkError;
        private System.Windows.Forms.Label USGS2;
    }
}

