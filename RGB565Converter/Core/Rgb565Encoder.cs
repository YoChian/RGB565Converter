using System;
using System.Text;

namespace RGB565Converter.Core
{
	public static class Rgb565Encoder
	{
		public static ushort EncodeBgr24(byte blue, byte green, byte red)
		{
			return (ushort)((((red >> 3) << 11) & 0xF800)
				| (((green >> 2) << 5) & 0x07E0)
				| ((blue >> 3) & 0x001F));
		}

		public static string ConvertBgr24FramesToRgb565String(byte[] data, int width, int height, int frames, IProgress<int> progress = null, Func<bool> cancellationRequested = null)
		{
			if (data == null)
			{
				throw new ArgumentNullException(nameof(data));
			}

			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(width));
			}

			if (height <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(height));
			}

			if (frames <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(frames));
			}

			int frameSize = width * height;
			int expectedLength = frameSize * frames * 3;
			if (data.Length != expectedLength)
			{
				throw new ArgumentException("Input data length does not match width, height, and frame count.", nameof(data));
			}

			StringBuilder dataBuilder = new StringBuilder(frameSize * frames * 8);
			int totalWork = frameSize * frames;
			for (int frame = 0; frame < frames; frame++)
			{
				for (int row = 0; row < height; row++)
				{
					for (int column = 0; column < width; column++)
					{
						int pixelIndex = (frame * frameSize) + (row * width) + column;
						int ptr = pixelIndex * 3;
						ushort color = EncodeBgr24(data[ptr], data[ptr + 1], data[ptr + 2]);
						dataBuilder.AppendFormat("0x{0:X4}, ", color);

						progress?.Report((pixelIndex + 1) * 100 / totalWork);
						if (cancellationRequested != null && cancellationRequested())
						{
							return null;
						}
					}
				}
			}

			return dataBuilder.ToString();
		}
	}
}
