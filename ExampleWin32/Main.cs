using Paletter;

namespace ExampleWin32
{
	public partial class Main : Form
	{
		private enum Colors
		{
			Red,
			Yellow,
			Green,
			LightBlue,
			Blue
		}

		private int GetColorIndex(Colors color) => (int)color;

		// общее количество градаций цветов
		private int lengthPalette = 50;
		private readonly Color[] colors =
		{
			Color.FromArgb(255, 0, 0), // Красный
			Color.FromArgb(255, 255, 0), // Желтый
			Color.FromArgb(0, 255, 0), // Зеленый
			Color.FromArgb(0, 255, 255), // Голубой
			Color.FromArgb(0, 0, 255) // Синий
		};
		List<Color> palette = null;

		public Main()
		{
			InitializeComponent();			
		}

		private void Main_Load(object sender, EventArgs e)
		{
			trackBar_R.BackColor = colors[GetColorIndex(Colors.Red)];
			trackBar_Y.BackColor = colors[GetColorIndex(Colors.Yellow)];
			trackBar_G.BackColor = colors[GetColorIndex(Colors.Green)];
			trackBar_LB.BackColor = colors[GetColorIndex(Colors.LightBlue)];
			//trackBar_B.BackColor = colors[GetColorIndex(Colors.Blue)];
		}

		private void defaultOffsetPanel_Paint(object sender, PaintEventArgs e)
		{
			int correkt = 7;
			int penWidth = defaultOffsetPanel.Height / lengthPalette;
			int hof = (defaultOffsetPanel.Height / colors.Length) + (penWidth * 2);
			Pen pen = new Pen(Color.Black, penWidth);
			Graphics g = defaultOffsetPanel.CreateGraphics();
			int thishof = correkt;
			foreach (var item in colors)
			{
				g.DrawLine(pen, 0, thishof, defaultOffsetPanel.Width, thishof);
				thishof += hof;
			}
			pen.Dispose();
			g.Dispose();
		}

		private void rootPanel_Paint(object sender, PaintEventArgs e)
		{
			ColorPaletter paletter = new();

			palette = paletter.GetColorsPalette(lengthPalette, setterGradLength, colors);
			int heightRect = palettePanel.Height / lengthPalette;
			int thisHeightRec = 0;

			for (int i = 0; i < palette.Count;)
			{
				DrawRectangle(new Rectangle(0, thisHeightRec, palettePanel.Width, heightRect), palette[i++], palettePanel);
				thisHeightRec += heightRect;
			}

			string colorsOutStr = string.Empty;
			foreach (int item in paletter.ConvertToDec(paletter.ConvertToHex(palette)))
			{
				colorsOutStr += $"{item}, ";
			}

			colorInfo.Text = $"count: {palette.Count}" + Environment.NewLine +
							 $"R: {tsVal_R} | Y: {tsVal_Y} | G: {tsVal_G} | LB: {tsVal_LB}" + Environment.NewLine +
							 colorsOutStr;
		}

		// корректирует диапазоны переходов цветов
		// вызывается во время каждого перехода 
		// принимает в параметр цвет с которого начинается текущий диапазон
		private double setterGradLength(Color startColor)
		{
			// корректировка
			int r = 0, y = 0, g = 0, lb = 0;

			// уменьшаю диапазон зелёного
			if (colors[GetColorIndex(Colors.Red)] == startColor)
			{
				return r - tsVal_R; // К -> Ж
			}
			if (colors[GetColorIndex(Colors.Yellow)] == startColor)
			{
				return y - tsVal_Y; // Ж -> З
			}
			if (colors[GetColorIndex(Colors.Green)] == startColor)
			{
				return g - tsVal_G; // З -> Г
			}
			if (colors[GetColorIndex(Colors.LightBlue)] == startColor)
			{
				return lb - tsVal_LB; // Г -> С
			}
			//if (colors[GetColorIndex(Colors.Blue)] == startColor)
			//{
			//	return tsVal_B; // Г -> С
			//}
			return 0;
		}

		/// <summary>
		/// Рисуем прямоугольники
		/// </summary>
		private void DrawRectangle(Rectangle rectangle, Color color, Control gControl)
		{
			SolidBrush brush = new SolidBrush(color);
			Graphics g = gControl.CreateGraphics();
			g.FillRectangle(brush, rectangle);
			brush.Dispose();
			g.Dispose();
		}

		private int tsVal_R = 0,
					tsVal_Y = 0,
					tsVal_G = 0,
					tsVal_LB = 0;
		//tsVal_B = 0;
		private void TrackBar_ValueChanged(object sender, EventArgs e)
		{
			if (sender is TrackBar trackBar)
			{
				string tag = trackBar.Tag as string;
				int value = trackBar.Value;
				switch (tag)
				{
					case "R": tsVal_R = value; break;
					case "Y": tsVal_Y = value; break;
					case "G": tsVal_G = value; break;
					case "LB": tsVal_LB = value; break;
						//case "B": tsVal_B = value; break;
				}

				if (lengthPalette < (tsVal_R + tsVal_Y + tsVal_G + tsVal_LB) + lengthPalette)
					trackBar.Value -= 1;
				 
			}
			palettePanel.Refresh();
		}
	}
}