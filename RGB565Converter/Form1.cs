using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGB565Converter
{
	public partial class Form1 : Form
	{
		private Image image;
		private bool scale = true;
		private Bitmap bmp;
		private byte[] data;

		public Form1()
		{
			InitializeComponent();
			backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
			backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
			backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
		}

		private void pictureSelectButton_Click(object sender, EventArgs e)
		{
			DialogResult result = openFileDialog1.ShowDialog();
			if(result == DialogResult.OK)
			{
				string ImagePath = openFileDialog1.FileName;
				try
				{
					image = Image.FromFile(ImagePath);
				}
				catch (Exception)
				{

					throw;
				}
				picturePathBox.Text = ImagePath;
				pictureSetup();
			}
		}

		private void picturePathBox_TextChanged(object sender, EventArgs e)
		{
			string ImagePath = picturePathBox.Text;
			try
			{
				if (!string.IsNullOrEmpty(ImagePath)&&!string.IsNullOrWhiteSpace(ImagePath)) 
				{
					image = Image.FromFile(ImagePath);
					pictureSetup();
				}
			}
			catch (System.IO.FileNotFoundException)
			{
				imageLoadLabel.Visible = true;
				pictureBox1.Image = null;
				XpxBox.Enabled = false;
				YpxBox.Enabled = false;
				XPercentageBox.Enabled = false;
				YPercentageBox.Enabled = false;
			}
		}

		private void pictureSetup()
		{
			imageLoadLabel.Visible = false;
			XpxBox.Enabled = true;
			YpxBox.Enabled = true;
			XPercentageBox.Enabled = true;
			YPercentageBox.Enabled = true;
			XpxBox.Value = image.Width;
			YpxBox.Value = image.Height;
			XPercentageBox.Value = 100;
			YPercentageBox.Value = 100;
			pictureBox1.Image = image;
		}

		private void outputPathButton_Click(object sender, EventArgs e)
		{
			DialogResult result = saveFileDialog1.ShowDialog();
			if(result == DialogResult.OK)
			{
				string OutputPath = saveFileDialog1.FileName;
				outputPathBox.Text = OutputPath;
			}
		}

		private void XpxBox_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				XPercentageBox.Value = XpxBox.Value / image.Width * 100;
				if (scale)
				{
					YPercentageBox.Value = XPercentageBox.Value;
					YpxBox.Value = YPercentageBox.Value / 100 * image.Height;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				if (scale)
				{
					YPercentageBox.Value = YpxBox.Value / image.Height * 100;
					XPercentageBox.Value = YPercentageBox.Value;
				}
				XpxBox.Value = XPercentageBox.Value / 100 * image.Width;
			}
			
		}

		private void YpxBox_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				YPercentageBox.Value = YpxBox.Value / image.Height * 100;
				if (scale)
				{
					XPercentageBox.Value = YPercentageBox.Value;
					XpxBox.Value = XPercentageBox.Value / 100 * image.Width;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				if (scale)
				{
					XPercentageBox.Value = XpxBox.Value / image.Width * 100;
					YPercentageBox.Value = XPercentageBox.Value;
				}
				YpxBox.Value = YPercentageBox.Value / 100 * image.Height;
			}
			
		}

		private void XPercentageBox_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				XpxBox.Value = XPercentageBox.Value / 100 * image.Width;
				if (scale)
				{
					YPercentageBox.Value = XPercentageBox.Value;
					YpxBox.Value = YPercentageBox.Value / 100 * image.Height;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				if (scale)
				{
					YPercentageBox.Value = YpxBox.Value / image.Height * 100;
					XpxBox.Value = YPercentageBox.Value / 100 * image.Width;
				}
				XPercentageBox.Value = XpxBox.Value / image.Width * 100;
			}
		}

		private void YPercentageBox_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				YpxBox.Value = YPercentageBox.Value / 100 * image.Height;
				if (scale)
				{
					XPercentageBox.Value = YPercentageBox.Value;
					XpxBox.Value = XPercentageBox.Value / 100 * image.Width;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				if(scale)
				{
					XPercentageBox.Value = XpxBox.Value / image.Width * 100;
					YpxBox.Value = XPercentageBox.Value / 100 * image.Height;
				}
				YPercentageBox.Value = YpxBox.Value / image.Height * 100;
			}
		}

		private void linkButton_Click(object sender, EventArgs e)
		{
			scale = !scale;
			linkButton.BackColor=scale ? SystemColors.Highlight : SystemColors.Control;
		}

		private void convertButton_Click(object sender, EventArgs e)
		{
			//检查输出路径是否有效
			try
			{
				FileStream fs = File.Create(outputPathBox.Text);
				fs.Close();
			}
			catch (Exception)
			{
				statusLabel.Text = "输出路径不正确";
			}
			
			bmp = (Bitmap)image;
			BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
										ImageLockMode.ReadWrite,
										PixelFormat.Format24bppRgb);
			int size = bmp.Width * bmp.Height * 3;
			data = new byte[size];
			IntPtr intPtr = bitmapData.Scan0;
			Marshal.Copy(intPtr, data, 0, size);
			bmp.UnlockBits(bitmapData);
			backgroundWorker1.RunWorkerAsync();
		}

		private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBar1.Value = e.ProgressPercentage;
			statusLabel.Text = $"转换中...{e.ProgressPercentage}%";
		}

		private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				statusLabel.Text = "转换已中止";
				
				return;
			}
			string imageName = Path.GetFileNameWithoutExtension(outputPathBox.Text);
			string[] fileString =
			{
				"#if defined(__AVR__)",
				"    #include <avr/pgmspace.h>",
				"#elif defined(__PIC32MX__)",
				"    #define PROGMEM",
				"#elif defined(__arm__)",
				"    #define PROGMEM",
				"#endif",
				"",
				$"const unsigned short {imageName}[{(bmp.Width*bmp.Height).ToString()}] PROGMEM={{",
				e.Result as String,
				"};",
			};
			File.WriteAllLines(outputPathBox.Text, fileString);
			statusLabel.Text = "转换完成";
		}

		private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			string dataString = "";
			for (int i = 0; i < bmp.Height; i++)
			{
				for (int j = 0; j < bmp.Width; j++)
				{
					int ptr = (j * bmp.Height + i) * 3;//从Bitmap转换得到的Byte数组按先列后行顺序排列，单个像素按BGR顺序排列
					Int16 B = (Int16)((data[ptr] >> 3)&0x001F);
					Int16 G = (Int16)(((data[ptr + 1] >> 2) << 5)&0x07E0);
					Int16 R = (Int16)((data[ptr + 2] >> 3 <<11)&0xF800);
					Int16 colorVar = (Int16)(R | G | B);
					dataString += "0x" + colorVar.ToString("X4") + ", ";
					backgroundWorker1.ReportProgress((i * bmp.Height + j + 1) * 100 / (bmp.Width * bmp.Height));
					if(backgroundWorker1.CancellationPending)
					{
						e.Cancel = true;
						return;
					}
				}
			}
			e.Result = dataString;
		}

		private void abortButton_Click(object sender, EventArgs e)
		{
			backgroundWorker1.CancelAsync();
		}
	}
}
