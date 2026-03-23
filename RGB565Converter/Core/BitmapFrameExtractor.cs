using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RGB565Converter.Core
{
	public static class BitmapFrameExtractor
	{
		public static byte[] Extract24BgrFrameData(Bitmap source, int frameCount)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			FrameDimension frameDimension = new FrameDimension(source.FrameDimensionsList[0]);
			int frameSize = source.Width * source.Height * 3;
			byte[] rawData = new byte[frameSize * frameCount];

			for (int frame = 0; frame < frameCount; frame++)
			{
				source.SelectActiveFrame(frameDimension, frame);
				using (Bitmap frameBitmap = new Bitmap(source.Width, source.Height, PixelFormat.Format24bppRgb))
				{
					using (Graphics graphics = Graphics.FromImage(frameBitmap))
					{
						graphics.DrawImage(source, 0, 0, source.Width, source.Height);
					}

					BitmapData bitmapData = frameBitmap.LockBits(
						new Rectangle(0, 0, frameBitmap.Width, frameBitmap.Height),
						ImageLockMode.ReadOnly,
						PixelFormat.Format24bppRgb);

					try
					{
						for (int row = 0; row < frameBitmap.Height; row++)
						{
							IntPtr rowStart = IntPtr.Add(bitmapData.Scan0, row * bitmapData.Stride);
							Marshal.Copy(
								rowStart,
								rawData,
								frame * frameSize + row * frameBitmap.Width * 3,
								frameBitmap.Width * 3);
						}
					}
					finally
					{
						frameBitmap.UnlockBits(bitmapData);
					}
				}
			}

			return rawData;
		}
	}
}
