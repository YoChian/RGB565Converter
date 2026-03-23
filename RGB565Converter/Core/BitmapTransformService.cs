using System.Drawing;
using System.Drawing.Imaging;

namespace RGB565Converter.Core
{
	public static class BitmapTransformService
	{
		/// <summary>
		/// Resizes each frame in <paramref name="srcFrames"/> to the specified dimensions.
		/// Returns one new <see cref="Bitmap"/> per input frame; callers are responsible for disposing them.
		/// </summary>
		public static Bitmap[] ResizeImage(Bitmap[] srcFrames, int width, int height)
		{
			Bitmap[] result = new Bitmap[srcFrames.Length];
			for (int i = 0; i < srcFrames.Length; i++)
			{
				Bitmap destBmp = new Bitmap(width, height);
				using (Graphics graphics = Graphics.FromImage(destBmp))
				{
					graphics.DrawImage(srcFrames[i],
						new Rectangle(0, 0, width, height),
						new Rectangle(0, 0, srcFrames[i].Width, srcFrames[i].Height),
						GraphicsUnit.Pixel);
				}
				result[i] = destBmp;
			}
			return result;
		}

		/// <summary>
		/// Clips each frame of <paramref name="src"/> by the specified margins.
		/// Returns one new <see cref="Bitmap"/> per frame; callers are responsible for disposing them.
		/// The source image's active frame is reset to frame 0 before returning.
		/// </summary>
		public static Bitmap[] ClipImage(Image src, int left, int top, int right, int bottom, int frames)
		{
			int destWidth = src.Width - left - right;
			int destHeight = src.Height - top - bottom;
			FrameDimension sourceDimension = new FrameDimension(src.FrameDimensionsList[0]);
			Bitmap[] result = new Bitmap[frames];
			for (int i = 0; i < frames; i++)
			{
				src.SelectActiveFrame(sourceDimension, i);
				Bitmap destBmp = new Bitmap(destWidth, destHeight);
				using (Graphics graphics = Graphics.FromImage(destBmp))
				{
					graphics.DrawImage(src,
						new Rectangle(0, 0, destWidth, destHeight),
						new Rectangle(left, top, destWidth, destHeight),
						GraphicsUnit.Pixel);
				}
				result[i] = destBmp;
			}
			src.SelectActiveFrame(sourceDimension, 0);
			return result;
		}
	}
}
