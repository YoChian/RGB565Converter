using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RGB565Converter.Core;

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
		/// 调整裁剪和缩放后实时对预览图像进行对应操作
		/// </summary>
		private void UpdateImage()
		{
			//TODO:用Graphics裁剪缩放会丢失动画信息，需要改写成把每一帧写入Bitmap数组
			Bitmap tmpBmp = BitmapTransformService.ClipImage(image, (int)LeftClipBox.Value, (int)TopClipBox.Value, (int)RightClipBox.Value, (int)BottomClipBox.Value, frames);
			bmp = BitmapTransformService.ResizeImage(tmpBmp, (int)XpxBox.Value, (int)YpxBox.Value, frames);
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

			data = BitmapFrameExtractor.Extract24BgrFrameData(bmp, frames);
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
			string[] fileString = Rgb565HeaderBuilder.BuildHeaderLines(imageName, bmp.Width, bmp.Height, frames, e.Result as string);
			statusLabel.Text = "写入文件中...";
			File.WriteAllLines(outputPathBox.Text, fileString);
			statusLabel.Text = "转换完成";
		}

		private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Result = Rgb565Encoder.ConvertBgr24FramesToRgb565String(
				data,
				bmp.Width,
				bmp.Height,
				frames,
				new Progress<int>(progress => backgroundWorker1.ReportProgress(progress)),
				() => backgroundWorker1.CancellationPending);

			if (backgroundWorker1.CancellationPending && e.Result == null)
			{
				e.Cancel = true;
			}
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
