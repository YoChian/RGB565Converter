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
		private int frames;

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
					image = Image.FromFile(ImagePath);	//只用于输入，转换为Bitmap后，后续操作都使用Bitmap完成
					bmp = (Bitmap)image;
					FrameDimension frameDimension = new FrameDimension(bmp.FrameDimensionsList[0]);
					frames = bmp.GetFrameCount(frameDimension);
					PictureSetup();
				}
				catch (Exception)
				{

					throw;
				}
				picturePathBox.Text = ImagePath;
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
					bmp = (Bitmap)image;
					PictureSetup();
				}
			}
			catch (FileNotFoundException)
			{
				imageLoadLabel.Visible = true;
				pictureBox1.Image = null;

				XpxBox.Enabled = false;
				YpxBox.Enabled = false;
				XPercentageBox.Enabled = false;
				YPercentageBox.Enabled = false;

				LeftClipBox.Enabled = false;
				RightClipBox.Enabled = false;
				TopClipBox.Enabled = false;
				BottomClipBox.Enabled = false;
			}
		}

		/// <summary>
		/// 获得有效图像文件后对预览图像及缩放裁剪相关控件执行的初始化
		/// </summary>
		private void PictureSetup()
		{
			imageLoadLabel.Visible = false;

			XpxBox.Enabled = true;
			YpxBox.Enabled = true;
			XPercentageBox.Enabled = true;
			YPercentageBox.Enabled = true;

			LeftClipBox.Enabled = true;
			RightClipBox.Enabled = true;
			TopClipBox.Enabled = true;
			BottomClipBox.Enabled = true;

			XpxBox.Value = image.Width;
			YpxBox.Value = image.Height;
			XPercentageBox.Value = 100;
			YPercentageBox.Value = 100;

			XPercentageBox.Minimum = 100 / image.Width;
			YPercentageBox.Minimum = 100 / image.Height;

			LeftClipBox.Value = 0;
			RightClipBox.Value = 0;
			TopClipBox.Value = 0;
			BottomClipBox.Value = 0;

			LeftClipBox.Minimum = 0;
			RightClipBox.Minimum = 0;
			TopClipBox.Minimum = 0;
			BottomClipBox.Minimum = 0;

			LeftClipBox.Maximum = image.Width - 1;
			RightClipBox.Maximum = image.Width - 1;
			TopClipBox.Maximum = image.Height - 1;
			BottomClipBox.Maximum = image.Height - 1;

			LeftClipBox.DecimalPlaces = 0;
			RightClipBox.DecimalPlaces = 0;
			TopClipBox.DecimalPlaces = 0;
			BottomClipBox.DecimalPlaces = 0;

			pictureBox1.Image = bmp;
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
			UpdateImage();
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
			UpdateImage();
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
			UpdateImage();
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
			UpdateImage();
		}

		private void linkButton_Click(object sender, EventArgs e)
		{
			scale = !scale;
			linkButton.BackColor = scale ? SystemColors.Highlight : SystemColors.Control;
		}

		/// <summary>
		/// 缩放指定的图像
		/// </summary>
		/// <param name="src">源图像</param>
		/// <param name="width">缩放后的宽度</param>
		/// <param name="height">缩放后的高度</param>
		/// <returns>缩放后的图像</returns>
		private Bitmap ResizeImage(Image src, int width, int height)
		{
			Bitmap destBmp = new Bitmap(width, height);
			ImageFormat srcFormat = src.RawFormat;
			FrameDimension sfd = new FrameDimension(src.FrameDimensionsList[0]);
			FrameDimension dfd = new FrameDimension(destBmp.FrameDimensionsList[0]);
			Graphics g = Graphics.FromImage(destBmp);
			//g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			//g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			//g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			//g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			for (int i = 0; i < frames; i++)
			{
				src.SelectActiveFrame(sfd, i);
				destBmp.SelectActiveFrame(dfd, i);
				g.DrawImage(src,
				   new Rectangle(0, 0, width, height),
				   new Rectangle(0, 0, src.Width, src.Height),
				   GraphicsUnit.Pixel);
			}
			destBmp.SelectActiveFrame(dfd, 0);
			g.Dispose();
			return destBmp;
		}

		/// <summary>
		/// 裁剪指定的图像
		/// </summary>
		/// <param name="src">源图像</param>
		/// <param name="left">裁剪的左边界</param>
		/// <param name="top">裁剪的上边界</param>
		/// <param name="right">裁剪的右边界</param>
		/// <param name="bottom">裁剪的下边界</param>
		/// <returns>裁剪后的图像</returns>
		private Bitmap ClipImage(Image src, int left, int top, int right, int bottom)
		{
			Bitmap destBmp = new Bitmap(src.Width - left - right, src.Height - top - bottom);
			FrameDimension sfd = new FrameDimension(src.FrameDimensionsList[0]);
			FrameDimension dfd = new FrameDimension(destBmp.FrameDimensionsList[0]);
			Graphics g = Graphics.FromImage(destBmp);
			//g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			//g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			//g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			//g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			for (int i = 0; i < frames; i++)
			{
				src.SelectActiveFrame(sfd, i);
				destBmp.SelectActiveFrame(dfd, i);
				g.DrawImage(src,
				   new Rectangle(0, 0, src.Width - left - right, src.Height - top - bottom),
				   new Rectangle(left, top, src.Width - right, src.Height - bottom),
				   GraphicsUnit.Pixel);
			}
			destBmp.SelectActiveFrame(dfd, 0);
			g.Dispose();
			return destBmp;
		}

		/// <summary>
		/// 调整裁剪和缩放后实时对预览图像进行对应操作
		/// </summary>
		private void UpdateImage()
		{
			//TODO:用Graphics裁剪缩放会丢失动画信息，需要改写成把每一帧写入Bitmap数组
			Bitmap tmpBmp = ClipImage(image, (int)LeftClipBox.Value, (int)TopClipBox.Value, (int)RightClipBox.Value, (int)BottomClipBox.Value);
			bmp = ResizeImage(tmpBmp, (int)XpxBox.Value, (int)YpxBox.Value);
			pictureBox1.Image = bmp;
		}

		private void convertButton_Click(object sender, EventArgs e)
		{
			//检查输入文件是否有效
			try
			{
				FileStream fs = File.OpenRead(picturePathBox.Text);
				fs.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "打开输入文件失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			//检查输出路径是否有效
			try
			{
				FileStream fs = File.Create(outputPathBox.Text);
				fs.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "创建输出文件失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			FrameDimension frameDimension = new FrameDimension(bmp.FrameDimensionsList[0]);
			int size = bmp.Width * bmp.Height * 3;
			data = new byte[size * frames];
			for (int i = 0; i < frames; i++)
			{
				bmp.SelectActiveFrame(frameDimension, i);
				BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
											ImageLockMode.ReadOnly,
											PixelFormat.Format24bppRgb);
				IntPtr intPtr = bitmapData.Scan0;
				Marshal.Copy(intPtr, data, size * i, size);
				bmp.UnlockBits(bitmapData);
			}
			progressBar1.SetState(1);
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
				progressBar1.SetState(2);
				return;
			}
			string imageName = Path.GetFileNameWithoutExtension(outputPathBox.Text);
			string arrayFlag = frames > 1 ? "[]" : "";
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
				$"const unsigned short {imageName}{arrayFlag}[{bmp.Width*bmp.Height}] PROGMEM={{",
				e.Result as String,
				"};",
			};
			statusLabel.Text = "写入文件中...";
			File.WriteAllLines(outputPathBox.Text, fileString);
			statusLabel.Text = "转换完成";
		}

		private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			string dataString = "";
			int width=bmp.Width;
			int height=bmp.Height;
			int frameSize = width * height;
			int totalWork = frameSize * frames;
			for (int k = 0; k < frames; k++)
			{
				for (int i = 0; i < height; i++)
				{
					for (int j = 0; j < width; j++)
					{
						int ptr = (j * height + i + frameSize * k) * 3;//从Bitmap转换得到的Byte数组按先列后行顺序排列，单个像素按BGR顺序排列
						Int16 B = (Int16)((data[ptr] >> 3) & 0x001F);
						Int16 G = (Int16)(((data[ptr + 1] >> 2) << 5) & 0x07E0);
						Int16 R = (Int16)((data[ptr + 2] >> 3 << 11) & 0xF800);
						Int16 colorVar = (Int16)(R | G | B);
						dataString += $"0x{colorVar:X4}, ";
						int progress = (i * width + j + 1 + k * frameSize) * 100 / totalWork;
						backgroundWorker1.ReportProgress(progress);
						if (backgroundWorker1.CancellationPending)
						{
							e.Cancel = true;
							return;
						}
					}
				}
			}
			e.Result = dataString;
		}

		private void abortButton_Click(object sender, EventArgs e)
		{
			backgroundWorker1.CancelAsync();
		}

		private void LeftClipBox_ValueChanged(object sender, EventArgs e)
		{
			RightClipBox.Maximum = image.Width - LeftClipBox.Value - 1;
			UpdateImage();
		}

		private void RightClipBox_ValueChanged(object sender, EventArgs e)
		{
			LeftClipBox.Maximum = image.Width - RightClipBox.Value - 1;
			UpdateImage();
		}

		private void TopClipBox_ValueChanged(object sender, EventArgs e)
		{
			BottomClipBox.Maximum = image.Height - TopClipBox.Value - 1;
			UpdateImage();
		}

		private void BottomClipBox_ValueChanged(object sender, EventArgs e)
		{
			TopClipBox.Maximum = image.Height - BottomClipBox.Value - 1;
			UpdateImage();
		}

	}

	public static class ModifyProgressBarColor
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
		public static void SetState(this ProgressBar pBar, int state)
		{
			SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
		}
	}

}
