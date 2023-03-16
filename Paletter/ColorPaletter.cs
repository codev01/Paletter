using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paletter
{
	/// <summary>
	/// Предоставляет методы для создания палитры и 
	/// </summary>
	public class ColorPaletter
	{
		/// <summary>
		/// максимальное или минимальное значение цвета
		/// </summary>
		private const byte MIN_RGB = 0,
						   MAX_RGB = 255;

		/// <summary>
		/// Получить список цветов линейного градиента
		/// </summary>
		/// <param name="length"> Количество градаций </param>
		/// <param name="colorStart"> Начальный цвет градиента </param>
		/// <param name="colorEnd"> Конечный цвет градиента </param>
		public List<Color> GetLinearGradientColors(double length, Color colorStart, Color colorEnd)
		{
			if (length < 0)
				length = 2;

			List<Color> colorsList = new List<Color>((int)length);
			colorsList.Add(colorStart);

			double step = MAX_RGB / length;

			double r = colorStart.R,
				   g = colorStart.G,
				   b = colorStart.B;

			for (double i = 0; i < length; i++)
			{
				if (r != colorEnd.R)
					if (r < colorEnd.R)
						r += step;
					else
						r -= step;

				if (g != colorEnd.G)
					if (g < colorEnd.G)
						g += step;
					else
						g -= step;

				if (b != colorEnd.B)
					if (b < colorEnd.B)
						b += step;
					else
						b -= step;

				Color color = Color.FromArgb(_fixMaxMin((int)r),
											 _fixMaxMin((int)g),
											 _fixMaxMin((int)b));

				colorsList.Add(color);
			}

			return colorsList;
		}

		/// <summary>
		/// Получить список цветов палитры
		/// </summary>
		/// <param name="length"> 
		/// Длина палитры
		/// </param>
		/// <param name="setterGradientLength"> Делегат, позволяющий менять длину отдельных градаций </param>
		/// <param name="colors"> Цвета, входящие в состав палитры </param>
		public List<Color> GetColorsPalette(int length, Func<Color, int, int>? setterGradientLength = null, params Color[] colors)
		{
			if (length < colors.Length || length < 0)
				length = colors.Length;

			List<Color> outPalette = new List<Color>(length);
			Color colorEnd = colors.Last();
			int transCount = colors.Length - 1,
					  plus = 0,
					  step = 0;

			List<int> roadMap = new List<int>();
			for (int i = 0; i < length; i++)
			{
				if (step == colors.Length)
				{
					step = 1;
					plus++;
				}
				roadMap.Add(step++);
			}

			int[] valuesOffset = new int[transCount];
			if (length != colors.Length)
			{
				List<int> offsets = new List<int>();

				int r = roadMap.Last();
				for (int i = 0; i < transCount; i++)
				{
					if (r > i && r != 0)
						offsets.Add(1);
					else
						offsets.Add(0);
				}
				valuesOffset = offsets.Reverse<int>().ToArray();

				for (int i = 0; i < plus - 1; i++)
				{
					for (int ii = 0; ii < transCount; ii++)
					{
						valuesOffset[ii] += 1;
					}
				}
			}

			for (int i = 0; i < transCount;)
			{
				Color c1 = colors[i];
				Color c2 = colors[++i];

				int setter = 1;

				setter += valuesOffset[i - 1];
				if (setterGradientLength is not null)
					setter -= setterGradientLength(c1, i - 1);

				List<Color> c1_c2_gradient = GetLinearGradientColors(setter, c1, c2);

				c1_c2_gradient.Remove(c1_c2_gradient.Last());

				foreach (Color color in c1_c2_gradient)
					outPalette.Add(color);
			}

			if (outPalette.Count > 0)
				if (outPalette.Last() != colorEnd)
					outPalette.Add(colorEnd);

			return outPalette;
		}

		// return 0, если rgbNum < 0; 255, если rgbNum > 255; в остальных случаях rgbNum
		private int _fixMaxMin(int rgbNum)
		{
			_fixMaxMin(ref rgbNum);
			return rgbNum;
		}

		private void _fixMaxMin(ref int rgbNum) =>
			rgbNum = rgbNum <= MIN_RGB ?
				MIN_RGB : rgbNum >= MAX_RGB ? MAX_RGB : rgbNum;

		#region Converters

		public List<string> ConvertListColorsToHex(List<Color> ColorsList)
		{
			List<string> colorsList = new List<string>(ColorsList.Count);
			foreach (Color color in ColorsList)
			{
				colorsList.Add(ConvertColorToHex(color));
			}
			return colorsList;
		}

		public List<int> ConvertListColorsToDec(List<string> hexColors)
		{
			List<int> decColorsList = new List<int>(hexColors.Count);
			foreach (string hex in hexColors)
				decColorsList.Add(ConvertColorToDec(hex));
			return decColorsList;
		}

		public List<int> ConvertListColorsToDec(List<Color> colors)
		{
			List<int> decColorsList = new List<int>(colors.Count);
			foreach (Color color in colors)
				decColorsList.Add(ConvertColorToDec(color));
			return decColorsList;
		}

		public int[] ConvertListColorsToOle(List<Color> colors)
		{
			List<int> decColorsList = new List<int>(colors.Count);
			foreach (Color color in colors)
				decColorsList.Add(ColorTranslator.ToOle(color));
			return decColorsList.ToArray();
		}

		string ConvertColorToHex(Color color) =>
			ColorTranslator.ToHtml(color).Substring(1);

		public string ConvertColorToHex(byte r, byte g, byte b) =>
			ColorTranslator.ToHtml(Color.FromArgb(r, g, b)).Substring(1);

		public string ConvertColorToHex(int r, int g, int b)
		{
			_fixMaxMin(ref r);
			_fixMaxMin(ref g);
			_fixMaxMin(ref b);
			return ConvertColorToHex(r, g, b);
		}

		public int ConvertColorToDec(Color color) 
			=> ConvertColorToDec(ConvertColorToHex(color));

		public int ConvertColorToDec(string hex) =>
			int.Parse(hex, System.Globalization.NumberStyles.HexNumber);

		public int ConvertColorToDec(int r, int g, int b) =>
			ConvertColorToDec(ConvertColorToHex(_fixMaxMin(r), _fixMaxMin(g), _fixMaxMin(b)));

		#endregion
	}
}