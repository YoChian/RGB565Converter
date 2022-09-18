namespace RGB565Converter
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.linkButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.YPercentageBox = new System.Windows.Forms.NumericUpDown();
			this.XPercentageBox = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.YpxBox = new System.Windows.Forms.NumericUpDown();
			this.XpxBox = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.picturePathBox = new System.Windows.Forms.TextBox();
			this.pictureSelectButton = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.outputPathButton = new System.Windows.Forms.Button();
			this.outputPathBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.convertButton = new System.Windows.Forms.Button();
			this.abortButton = new System.Windows.Forms.Button();
			this.statusLabel = new System.Windows.Forms.Label();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.imageLoadLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.YPercentageBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.XPercentageBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.YpxBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.XpxBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(13, 13);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(256, 256);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.linkButton);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.YPercentageBox);
			this.groupBox1.Controls.Add(this.XPercentageBox);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.YpxBox);
			this.groupBox1.Controls.Add(this.XpxBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(276, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(325, 84);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "缩放";
			// 
			// linkButton
			// 
			this.linkButton.BackColor = System.Drawing.SystemColors.Highlight;
			this.linkButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.linkButton.Image = global::RGB565Converter.Properties.Resources.ic_fluent_link_24_regular;
			this.linkButton.Location = new System.Drawing.Point(277, 33);
			this.linkButton.Name = "linkButton";
			this.linkButton.Size = new System.Drawing.Size(38, 24);
			this.linkButton.TabIndex = 10;
			this.linkButton.UseVisualStyleBackColor = false;
			this.linkButton.Click += new System.EventHandler(this.linkButton_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(259, 50);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(11, 12);
			this.label5.TabIndex = 9;
			this.label5.Text = "%";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(259, 23);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(11, 12);
			this.label6.TabIndex = 8;
			this.label6.Text = "%";
			// 
			// YPercentageBox
			// 
			this.YPercentageBox.DecimalPlaces = 2;
			this.YPercentageBox.Enabled = false;
			this.YPercentageBox.Location = new System.Drawing.Point(153, 46);
			this.YPercentageBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.YPercentageBox.Name = "YPercentageBox";
			this.YPercentageBox.Size = new System.Drawing.Size(100, 21);
			this.YPercentageBox.TabIndex = 7;
			this.YPercentageBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.YPercentageBox.ValueChanged += new System.EventHandler(this.YPercentageBox_ValueChanged);
			// 
			// XPercentageBox
			// 
			this.XPercentageBox.DecimalPlaces = 2;
			this.XPercentageBox.Enabled = false;
			this.XPercentageBox.Location = new System.Drawing.Point(153, 19);
			this.XPercentageBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.XPercentageBox.Name = "XPercentageBox";
			this.XPercentageBox.Size = new System.Drawing.Size(100, 21);
			this.XPercentageBox.TabIndex = 6;
			this.XPercentageBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.XPercentageBox.ValueChanged += new System.EventHandler(this.XPercentageBox_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(130, 50);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 12);
			this.label4.TabIndex = 5;
			this.label4.Text = "px";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(130, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "px";
			// 
			// YpxBox
			// 
			this.YpxBox.Enabled = false;
			this.YpxBox.Location = new System.Drawing.Point(24, 46);
			this.YpxBox.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
			this.YpxBox.Name = "YpxBox";
			this.YpxBox.Size = new System.Drawing.Size(100, 21);
			this.YpxBox.TabIndex = 3;
			this.YpxBox.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.YpxBox.ValueChanged += new System.EventHandler(this.YpxBox_ValueChanged);
			// 
			// XpxBox
			// 
			this.XpxBox.Enabled = false;
			this.XpxBox.Location = new System.Drawing.Point(24, 19);
			this.XpxBox.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
			this.XpxBox.Name = "XpxBox";
			this.XpxBox.Size = new System.Drawing.Size(100, 21);
			this.XpxBox.TabIndex = 2;
			this.XpxBox.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.XpxBox.ValueChanged += new System.EventHandler(this.XpxBox_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 50);
			this.label2.Margin = new System.Windows.Forms.Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(11, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Y";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 23);
			this.label1.Margin = new System.Windows.Forms.Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(11, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "X";
			// 
			// picturePathBox
			// 
			this.picturePathBox.Location = new System.Drawing.Point(43, 18);
			this.picturePathBox.Name = "picturePathBox";
			this.picturePathBox.Size = new System.Drawing.Size(174, 21);
			this.picturePathBox.TabIndex = 2;
			this.picturePathBox.TextChanged += new System.EventHandler(this.picturePathBox_TextChanged);
			// 
			// pictureSelectButton
			// 
			this.pictureSelectButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.pictureSelectButton.Location = new System.Drawing.Point(223, 18);
			this.pictureSelectButton.Name = "pictureSelectButton";
			this.pictureSelectButton.Size = new System.Drawing.Size(75, 23);
			this.pictureSelectButton.TabIndex = 3;
			this.pictureSelectButton.Text = "选择图像";
			this.pictureSelectButton.UseVisualStyleBackColor = true;
			this.pictureSelectButton.Click += new System.EventHandler(this.pictureSelectButton_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "JPG文件|*.jpg;*.jpe;*.jpeg|PNG文件|*.png|BMP文件|*.bmp|GIF文件|*.gif";
			this.openFileDialog1.Title = "选择转换的图像";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.outputPathButton);
			this.groupBox2.Controls.Add(this.outputPathBox);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.pictureSelectButton);
			this.groupBox2.Controls.Add(this.picturePathBox);
			this.groupBox2.Location = new System.Drawing.Point(276, 104);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(325, 81);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "文件";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(8, 50);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(29, 12);
			this.label8.TabIndex = 4;
			this.label8.Text = "输出";
			// 
			// outputPathButton
			// 
			this.outputPathButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.outputPathButton.Location = new System.Drawing.Point(223, 45);
			this.outputPathButton.Name = "outputPathButton";
			this.outputPathButton.Size = new System.Drawing.Size(75, 23);
			this.outputPathButton.TabIndex = 6;
			this.outputPathButton.Text = "选择路径";
			this.outputPathButton.UseVisualStyleBackColor = true;
			this.outputPathButton.Click += new System.EventHandler(this.outputPathButton_Click);
			// 
			// outputPathBox
			// 
			this.outputPathBox.Location = new System.Drawing.Point(43, 45);
			this.outputPathBox.Name = "outputPathBox";
			this.outputPathBox.Size = new System.Drawing.Size(174, 21);
			this.outputPathBox.TabIndex = 5;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 23);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(29, 12);
			this.label7.TabIndex = 0;
			this.label7.Text = "输入";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(275, 246);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(325, 23);
			this.progressBar1.TabIndex = 5;
			// 
			// convertButton
			// 
			this.convertButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.convertButton.Location = new System.Drawing.Point(275, 191);
			this.convertButton.Name = "convertButton";
			this.convertButton.Size = new System.Drawing.Size(75, 23);
			this.convertButton.TabIndex = 6;
			this.convertButton.Text = "开始";
			this.convertButton.UseVisualStyleBackColor = true;
			this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
			// 
			// abortButton
			// 
			this.abortButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.abortButton.Location = new System.Drawing.Point(356, 191);
			this.abortButton.Name = "abortButton";
			this.abortButton.Size = new System.Drawing.Size(75, 23);
			this.abortButton.TabIndex = 7;
			this.abortButton.Text = "中止";
			this.abortButton.UseVisualStyleBackColor = true;
			// 
			// statusLabel
			// 
			this.statusLabel.AutoSize = true;
			this.statusLabel.Location = new System.Drawing.Point(276, 228);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(95, 12);
			this.statusLabel.TabIndex = 8;
			this.statusLabel.Text = "等待处理开始...";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "h";
			this.saveFileDialog1.FileName = "output.h";
			this.saveFileDialog1.Filter = "C/C++ Header|*.h";
			this.saveFileDialog1.Title = "选择输出的文件";
			// 
			// imageLoadLabel
			// 
			this.imageLoadLabel.AutoSize = true;
			this.imageLoadLabel.BackColor = System.Drawing.SystemColors.Window;
			this.imageLoadLabel.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.imageLoadLabel.Location = new System.Drawing.Point(45, 120);
			this.imageLoadLabel.Name = "imageLoadLabel";
			this.imageLoadLabel.Size = new System.Drawing.Size(195, 46);
			this.imageLoadLabel.TabIndex = 9;
			this.imageLoadLabel.Text = "无预览图像";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(614, 285);
			this.Controls.Add(this.imageLoadLabel);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.abortButton);
			this.Controls.Add(this.convertButton);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "RGB565Converter";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.YPercentageBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.XPercentageBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.YpxBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.XpxBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown YpxBox;
		private System.Windows.Forms.NumericUpDown XpxBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button linkButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown YPercentageBox;
		private System.Windows.Forms.NumericUpDown XPercentageBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox picturePathBox;
		private System.Windows.Forms.Button pictureSelectButton;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button outputPathButton;
		private System.Windows.Forms.TextBox outputPathBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button convertButton;
		private System.Windows.Forms.Button abortButton;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Label imageLoadLabel;
	}
}

