using System;
using System.Text;

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

			string sanitizedImageName = SanitizeIdentifier(imageName);
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
				$"const unsigned short {sanitizedImageName}{arrayFlag}[{width * height}] PROGMEM={{",
				pixelData ?? string.Empty,
				"};",
			};
		}

		public static string SanitizeIdentifier(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException("Identifier value is required.", nameof(value));
			}

			StringBuilder builder = new StringBuilder(value.Length + 1);
			foreach (char character in value)
			{
				if (char.IsLetterOrDigit(character) || character == '_')
				{
					builder.Append(character);
				}
				else
				{
					builder.Append('_');
				}
			}

			if (builder.Length == 0 || char.IsDigit(builder[0]))
			{
				builder.Insert(0, '_');
			}

			return builder.ToString();
		}
	}
}
