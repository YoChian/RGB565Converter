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

		public Form1()
		{
			InitializeComponent();
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

		private async void convertButton_Click(object sender, EventArgs e)
		{
			//检查输出路径是否有效
			string imageName;
			try
			{
				File.Create(outputPathBox.Text);
			}
			catch (Exception)
			{
				statusLabel.Text = "输出路径不正确";
			}
			imageName = Path.GetFileNameWithoutExtension(outputPathBox.Text);

			Bitmap bmp = (Bitmap)image;
			BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
										ImageLockMode.ReadWrite,
										PixelFormat.Format24bppRgb);
			int size = bmp.Width * bmp.Height * 3;
			byte[] data = new byte[size];
			IntPtr intPtr = bitmapData.Scan0;
			Marshal.Copy(intPtr, data, 0, size);
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
				"const unsigned short " + imageName + "[841] PROGMEM={",
				"",
				"};",
			};
			
			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					int ptr = (j * bmp.Width + i) * 3;
					Int16 colorVar = (Int16)((data[ptr] >> 3)<<11 + (data[ptr + 1] >> 2)<<5 + data[ptr+2]>>3);
					fileString[9] += "0x" + colorVar.ToString("X4") + ", ";
					progressBar1.Value = (i * bmp.Width + j + 1) / (bmp.Width * bmp.Height) * 100;
				}
			}
			File.WriteAllLines(outputPathBox.Text, fileString);
			statusLabel.Text = "转换完成";
		}
	}
}
