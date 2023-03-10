using System.Drawing;

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
		/// <param name="k"> -1 < k < 1 </param>
		public List<Color> GetLinearGradientColors(double length, Color colorStart, Color colorEnd, double k = 0)
		{
			if (!(-1 <= k && k <= 1))
				throw new InvalidOperationException("'k' меньше -1 или больше 1");

			List<Color> colorsList = new List<Color>();

			colorsList.Add(colorStart);

			if (length > 2)
			{
				double step = MAX_RGB / (double)length + k;

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

					Color color = Color.FromArgb((int)r, (int)g, (int)b);

					if (colorsList.Count != 0)
						if (colorsList.Last() == color)
							// если так то надо править код, у меня нет времени на это
							throw new Exception("GetLinearGradientColors() -> Такой цвет уже есть в списке! Что-то пошло не так!");

					colorsList.Add(color);
				}
			}

			if (colorsList.Last() != colorEnd)
				colorsList.Add(colorEnd);

			return colorsList;
		}

		/// <summary>
		/// Получить список цветов палитры
		/// </summary>
		/// <param name="length"> Длина палитры </param>
		/// <param name="setterGradLength"> Делегат, позволяющий менять длину отдельных переходов </param>
		/// <param name="colors"> Цвета, входящие в состав палитры </param>
		public List<Color> GetColorsPalette(int length, Func<Color, double>? setterGradLength = null, params Color[] colors)
		{
			List<Color> outGradient = new List<Color>(length);
			Color colorStart = colors.First();
			Color colorEnd = colors.Last();

			outGradient.Add(colorStart);
			for (int i = 0; i < colors.Length - 1;)
			{
				double thisColorGradLength = length / (colors.Length - 1);
				Color c1 = colors[i];
				Color c2 = colors[++i];
				if (!(i < colors.Length - 1))
				{
					thisColorGradLength++;
				}

				double setter = thisColorGradLength;

				if (setterGradLength != null)
					setter = thisColorGradLength - setterGradLength(colors[i - 1]);

				List<Color> twoColorsGrad = GetLinearGradientColors(setter, c1, c2, 0);

				twoColorsGrad.Remove(twoColorsGrad.First());

				foreach (Color color in twoColorsGrad)
					outGradient.Add(color);
			}

			if (outGradient.Last() != colorEnd)
				outGradient.Add(colorEnd);

			return outGradient;
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

		public List<string> ConvertToHex(List<Color> ColorsList)
		{
			List<string> colorsList = new List<string>(ColorsList.Count);
			foreach (Color color in ColorsList)
			{
				colorsList.Add(ConvertColorToHex(color));
			}
			return colorsList;
		}

		public string ConvertColorToHex(byte r, byte g, byte b) =>
			r.ToString("X2") + b.ToString("X2") + b.ToString("X2");

		public string ConvertColorToHex(int r, int g, int b)
		{
			_fixMaxMin(ref r);
			_fixMaxMin(ref g);
			_fixMaxMin(ref b);
			return r.ToString("X2") + b.ToString("X2") + b.ToString("X2");
		}

		string ConvertColorToHex(Color c) =>
			c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");

		public List<int> ConvertToDec(List<string> hexColorsList)
		{
			List<int> decColorsList = new List<int>(hexColorsList.Count);
			foreach (string hex in hexColorsList)
			{
				decColorsList.Add(int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
			}
			return decColorsList;
		}

		int ConvertColorToDec(int r, int g, int b) =>
			int.Parse(ConvertColorToHex(Color.FromArgb(_fixMaxMin(r), _fixMaxMin(g), _fixMaxMin(b))), System.Globalization.NumberStyles.HexNumber);

		#endregion
	}
}