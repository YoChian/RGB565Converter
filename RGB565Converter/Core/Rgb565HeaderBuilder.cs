using System;

namespace RGB565Converter.Core
{
	public static class Rgb565HeaderBuilder
	{
		public static string[] BuildHeaderLines(string imageName, int width, int height, int frames, string pixelData)
		{
			if (string.IsNullOrWhiteSpace(imageName))
			{
				throw new ArgumentException("Image name is required.", nameof(imageName));
			}

			string arrayFlag = frames > 1 ? "[]" : string.Empty;
			return new[]
			{
				"#if defined(__AVR__)",
				"    #include <avr/pgmspace.h>",
				"#elif defined(__PIC32MX__)",
				"    #define PROGMEM",
				"#elif defined(__arm__)",
				"    #define PROGMEM",
				"#endif",
				string.Empty,
				$"const unsigned short {imageName}{arrayFlag}[{width * height}] PROGMEM={{",
				pixelData ?? string.Empty,
				"};",
			};
		}
	}
}
