using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RGB565Converter.Core
{
	public static class BitmapFrameExtractor
	{
		/// <summary>
		/// Extracts raw 24-bit BGR pixel data from an array of single-frame bitmaps.
		/// Each frame's pixel data is stored sequentially in the returned byte array.
		/// </summary>
		public static byte[] Extract24BgrFrameData(Bitmap[] frames)
		{
			if (frames == null)
			{
				throw new ArgumentNullException(nameof(frames));
			}

			if (frames.Length == 0)
			{
				throw new ArgumentException("At least one frame is required.", nameof(frames));
			}

			int frameWidth = frames[0].Width;
			int frameHeight = frames[0].Height;
			int frameSize = frameWidth * frameHeight * 3;
			byte[] rawData = new byte[frameSize * frames.Length];

			for (int frame = 0; frame < frames.Length; frame++)
			{
				using (Bitmap frameBitmap = new Bitmap(frameWidth, frameHeight, PixelFormat.Format24bppRgb))
				{
					using (Graphics graphics = Graphics.FromImage(frameBitmap))
					{
						graphics.DrawImage(frames[frame], 0, 0, frameWidth, frameHeight);
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
