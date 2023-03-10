using System.Drawing;

using Paletter;

internal class Program
{
	static Color[] colors =
	{
			Color.FromArgb(255, 0, 0), // Красный
			Color.FromArgb(255, 255, 0), // Желтый
			Color.FromArgb(0, 255, 0), // Зеленый
			Color.FromArgb(0, 255, 255), // Голубой
			Color.FromArgb(0, 0, 255) // Синий
	};

	private static void Main(string[] args)
	{ 
		// список цветов в десятеричном представлении
		List<int> colors10 = new List<int>();
		// общее количество градаций цветов
		int length = 50;

		ColorPaletter paletter = new();

		colors10 = paletter.ConvertToDec(paletter.ConvertToHex(paletter.GetColorsPalette(length, setterGradLength, colors)));

		for (int i = 0; i < colors10.Count;)
		{
			Console.Write($"{colors10[i]}, ");
			if (++i % 5 == 0)
				Console.WriteLine();
		}

		Console.WriteLine();
		Console.WriteLine(colors10.Count);
	}

	// корректирует диапазоны переходов цветов
	// вызывается во время каждого перехода 
	// принимает в параметр цвет с которого начинается текущий диапазон
	static double setterGradLength(Color startColor)
	{
		// корректировка
		int r = -1, y = -3, g = 3, lb = 1;

		// уменьшаю диапазон зелёного
		if (colors[0] == startColor)
		{
			return r - 2;   // К -> Ж
		}
		if (colors[1] == startColor)
		{
			return y + 2; // Ж -> З
		}
		if (colors[2] == startColor)
		{
			return g + 2; // З -> Г
		}
		if (colors[3] == startColor)
		{
			return lb - 2; // Г -> С
		}
		return 0;
	}
}