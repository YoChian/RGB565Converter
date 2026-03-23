using System.Drawing;
using System.Drawing.Imaging;

namespace RGB565Converter.Core
{
	public static class BitmapTransformService
	{
		public static Bitmap ResizeImage(Image src, int width, int height, int frames)
		{
			Bitmap destBmp = new Bitmap(width, height);
			FrameDimension sourceFrameDimension = new FrameDimension(src.FrameDimensionsList[0]);
			FrameDimension destinationFrameDimension = new FrameDimension(destBmp.FrameDimensionsList[0]);
			Graphics graphics = Graphics.FromImage(destBmp);
			for (int i = 0; i < frames; i++)
			{
				src.SelectActiveFrame(sourceFrameDimension, i);
				destBmp.SelectActiveFrame(destinationFrameDimension, i);
				graphics.DrawImage(src,
				   new Rectangle(0, 0, width, height),
				   new Rectangle(0, 0, src.Width, src.Height),
				   GraphicsUnit.Pixel);
			}
			destBmp.SelectActiveFrame(destinationFrameDimension, 0);
			graphics.Dispose();
			return destBmp;
		}

		public static Bitmap ClipImage(Image src, int left, int top, int right, int bottom, int frames)
		{
			Bitmap destBmp = new Bitmap(src.Width - left - right, src.Height - top - bottom);
			FrameDimension sourceFrameDimension = new FrameDimension(src.FrameDimensionsList[0]);
			FrameDimension destinationFrameDimension = new FrameDimension(destBmp.FrameDimensionsList[0]);
			Graphics graphics = Graphics.FromImage(destBmp);
			for (int i = 0; i < frames; i++)
			{
				src.SelectActiveFrame(sourceFrameDimension, i);
				destBmp.SelectActiveFrame(destinationFrameDimension, i);
				graphics.DrawImage(src,
				   new Rectangle(0, 0, src.Width - left - right, src.Height - top - bottom),
				   new Rectangle(left, top, src.Width - right, src.Height - bottom),
				   GraphicsUnit.Pixel);
			}
			destBmp.SelectActiveFrame(destinationFrameDimension, 0);
			graphics.Dispose();
			return destBmp;
		}
	}
}
