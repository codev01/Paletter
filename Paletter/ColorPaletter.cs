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
		public List<Color> GetLinearGradientColors(double length, Color colorStart, Color colorEnd)
		{
			List<Color> colorsList = new List<Color>((int)length);

			colorsList.Add(colorStart);

			if (length > 0)
			{
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

					Color color = Color.FromArgb((int)r, (int)g, (int)b);
					colorsList.Add(color);
				}
			}
			else
				throw new ArgumentException("'length' должно быть больше '0'");


			return colorsList;
		}

		/// <summary>
		/// Получить список цветов палитры
		/// </summary>
		/// <param name="length"> 
		/// Длина палитры
		/// </param>
		/// <param name="setterGradLength"> Делегат, позволяющий менять длину отдельных градаций </param>
		/// <param name="colors"> Цвета, входящие в состав палитры </param>
		public List<Color> GetColorsPalette(int length, Func<Color, int>? setterGradLength = null, params Color[] colors)
		{

			//foreach (int value in GetValidValues(50, 49 /*length, colors.Length*/))
			//{
			//	length = value;
			//	break;
			//}


			//if (length % colors.Length - 1 != 0) // ((double)54 / (double)4 - (double)0.5) * 4 = 52
			//	length = (int)((double)length / (double)(colors.Length - 1) - (double)(length % colors.Length - 1)) * colors.Length;
			List<Color> outPalette = new List<Color>(length);
			Color colorStart = colors.First();
			Color colorEnd = colors.Last();
			int c1_c2_gradientLength = length / colors.Length;

			//outPalette.Add(colorStart);



			//int[] valuesOffset = new int[colors.Length - 1];
			//if (!ValidateValue(length, colors.Length))
			//{
			//	List<int> offsets = new List<int>();
			//	int notAvailColorsCount = length - (c1_c2_gradientLength * colors.Length)/* + c1_c2_gradientLength - 1*/;


			//	int j = 0;
			//	List<int> naccOffsets = new List<int>();
			//	for (int i = 0; i < length; i++)
			//	{
			//		if (j == colors.Length)
			//			j = 1;

			//		naccOffsets.Add(j++);
			//	}

			//	notAvailColorsCount = naccOffsets[length - colors.Length];

			//	notAvailColorsCount += (length / colors.Length - 1);
			//	int nacc = notAvailColorsCount;
			//	//if (notAvailColorsCount <= colors.Length - 1)
			//	//	notAvailColorsCount++;
			//	int plus = 1;
			//	for (int i = 0; i < colors.Length - 1; i++)
			//	{
			//		if (notAvailColorsCount != 0)
			//		{
			//			offsets.Add(1);
			//			notAvailColorsCount--;
			//		}
			//		else
			//			offsets.Add(0);
			//	}
			//	valuesOffset = offsets.Reverse<int>().ToArray();

			//	if (nacc > colors.Length - 1)
			//	{
			//		valuesOffset[valuesOffset.Length - 1]++;
			//	}
			//	//if (notAvailColorsCount <= colors.Length - 1)
			//	//	valuesOffset[valuesOffset.Length - 1]++;
			//}
			//else if (length != colors.Length)
			//	valuesOffset[valuesOffset.Length - 1]++;

			for (int i = 0; i < length; i++)
			{

			}

			int transCount = colors.Length - 1;
			int plus = 0;

			List<int> test = new List<int>();

			int f = 0;
			for (int i = 0; i < length; i++)
			{
				if (f == colors.Length)
				{
					f = 1;
					plus++;
				}
				test.Add(f++);
			}
			
			int[] valuesOffset = new int[colors.Length - 1];
			List<int> offsets = new List<int>();

			int r = test.Last();
			for (int i = 0; i < colors.Length - 1; i++)
			{
				if (r > i && r != 0)
				{
					//if (length - (c1_c2_gradientLength * colors.Length) != transCount)
						offsets.Add(1);
					//else
					//	offsets.Add(length / colors.Length);
					//r--;
				}
				else
					offsets.Add(0);
			}
			valuesOffset = offsets.Reverse<int>().ToArray();

			for (int i = 0; i < plus - 1; i++)
			{
				for (int k = 0; k < colors.Length - 1; k++)
				{
					valuesOffset[k] += 1;
				}
			}

			for (int i = 0; i < colors.Length - 1;)
			{
				Color c1 = colors[i];
				Color c2 = colors[++i];

				//if (!(i < colors.Length - 1))
				//{
				//	c1_c2_gradientLength++;
				//}

				//if (c1_c2_gradientLength % 2 != 0)
				//	c1_c2_gradientLength -= 1;

				double setter = 1/*c1_c2_gradientLength*/;

				//if (setterGradLength != null)
				//	setter = c1_c2_gradientLength - setterGradLength(colors[i - 1]);

				setter += valuesOffset[i - 1];

				List<Color> c1_c2_gradient = GetLinearGradientColors(setter, c1, c2);

				//c1_c2_gradient.Remove(c1_c2_gradient.First());
				c1_c2_gradient.Remove(c1_c2_gradient.Last());

				foreach (Color color in c1_c2_gradient)
					outPalette.Add(color);
			}

			if (outPalette.Last() != colorEnd)
				outPalette.Add(colorEnd);

			//if (ValidateValue(length, colors.Length))
			//	outPalette.Add(Color.Empty);

			//if (outPalette.Count < length)
			//	outPalette.Add(Color.Empty);

			return outPalette;
		}


		/// <summary>
		/// Получить массив делимых чисел
		/// </summary>
		/// <param name="isFirsBreak"> Получить только первое делимое значение </param>
		/// <returns></returns>
		public int[] GetValidValues(int length, int colorsCount, bool isFirsBreak = false)
		{
			List<int> values = new List<int>();
			for (int i = length; length >= i && i > 0;)
			{
				if (ValidateValue(length, colorsCount))
					values.Add(i--);
				i -= i % colorsCount;

				if(i == colorsCount)
					return values.ToArray();

				if (isFirsBreak)
					break;
			}
			return values.ToArray();
		}

		public bool ValidateValue(int length, int colorsCount)
		{
			if (colorsCount > length)
				throw new ArgumentException("Цветов больше чем длина палитры, сделай с собой что-нибудь");
			return length % colorsCount == 0;
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