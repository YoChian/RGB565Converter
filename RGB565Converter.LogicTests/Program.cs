using System;
using RGB565Converter.Core;

internal static class Program
{
	private static int Main()
	{
		try
		{
			ShouldEncodeSinglePixels();
			ShouldConvertFramesInRowMajorOrder();
			ShouldBuildHeaderLines();
			Console.WriteLine("All logic tests passed.");
			return 0;
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine(ex);
			return 1;
		}
	}

	private static void ShouldEncodeSinglePixels()
	{
		AssertEqual((ushort)0xF800, Rgb565Encoder.EncodeBgr24(0, 0, 255), "red");
		AssertEqual((ushort)0x07E0, Rgb565Encoder.EncodeBgr24(0, 255, 0), "green");
		AssertEqual((ushort)0x001F, Rgb565Encoder.EncodeBgr24(255, 0, 0), "blue");
	}

	private static void ShouldConvertFramesInRowMajorOrder()
	{
		byte[] data =
		{
			0, 0, 0,
			0, 0, 255,
			0, 255, 0,
			255, 0, 0,
		};

		string actual = Rgb565Encoder.ConvertBgr24FramesToRgb565String(data, 2, 2, 1);
		string expected = "0x0000, 0xF800, 0x07E0, 0x001F, ";
		AssertEqual(expected, actual, "row-major conversion");
	}

	private static void ShouldBuildHeaderLines()
	{
		string[] lines = Rgb565HeaderBuilder.BuildHeaderLines("demo", 2, 2, 1, "0x0000, ");
		AssertEqual("const unsigned short demo[4] PROGMEM={", lines[8], "header declaration");
		AssertEqual("0x0000, ", lines[9], "pixel data line");
	}

	private static void AssertEqual<T>(T expected, T actual, string message)
	{
		if (!Equals(expected, actual))
		{
			throw new InvalidOperationException($"Assertion failed for {message}. Expected: {expected}. Actual: {actual}.");
		}
	}
}
