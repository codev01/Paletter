using System.Windows.Forms;

using Paletter;

namespace ExampleWin32
{
	public partial class Main : Form
	{
		private ColorPaletter paletter = new();

		/// <summary>
		/// общее количество градаций цветов
		/// </summary>
		private int lengthPalette;
		private string label_availColorsText;
		private static readonly Color[] defaultColors =
		{
			Color.FromArgb(255, 0, 0), // Красный
			Color.FromArgb(255, 255, 0), // Желтый
			Color.FromArgb(0, 255, 0), // Зеленый
			Color.FromArgb(0, 255, 255), // Голубой
			Color.FromArgb(0, 0, 255) // Синий
		};
		private List<Color> palette = new List<Color>();
		private List<Color> customColors = defaultColors.ToList();
		private List<TrackBar> trackBars = new List<TrackBar>();

		public Main()
		{
			InitializeComponent();
			label_availColorsText = label_availColors.Text;
		}

		private void Main_Load(object sender, EventArgs e)
		{
			// чтобы ширина градации не была меньше одного пикселя на экране // иначе всё будет белое
			nud_paletteLength.Maximum = palettePanel.Height;
			
			nud_paletteLength_ValueChanged(sender, e);
			UpdateTrackList(customColors.ToList());
		}

		private void UpColorToList(Color color)
		{
			UpdateAll();
		}

		private void DownColorToList(Color color)
		{
			UpdateAll();
		}

		private void AddColor(Color color)
		{
			if (ValidAvailColors())
			{
				customColors.Add(color);
				label_availColors.Text = label_availColorsText + GetAvailColorsCount();

				UpdateAll();
			}
			else
				MessageBox.Show("Превышено количество цветов на текущую длину палитры");
		}

		private void RemoveColor(Color color)
		{
			customColors.Remove(color);
			UpdateAll();
		}

		private void UpdateAll()
		{
			PalettePanelRefresh();
			UpdateTrackList(customColors);
		}

		/// <summary>
		/// возвращает количество активных цветов диапазон градиентов которых можно менять
		/// </summary>
		private int GetColorsCount() 
			=> customColors.Count;

		/// <summary>
		/// возвращает количество цветов, которые ещё можно добавить
		/// </summary>
		private int GetAvailColorsCount() 
			=> (lengthPalette / GetTrackMinMax()) - GetColorsCount();

		/// <summary>
		/// тру если можно добавить ещё цвет
		/// </summary>
		private bool ValidAvailColors() 
			=> GetAvailColorsCount() > 0;

		/// <summary>
		/// получить единицу на которую можно изменить диапазон одного цвета
		/// </summary>
		private int GetTrackMinMax() 
			=> lengthPalette / GetColorsCount();

		private void PalettePanelRefresh()
			=> palettePanel.Refresh();

		private void UpdateTrackList(List<Color> colors)
		{
			bool isTrackEnable = true;

			if (true)
			{

			}
			trackBars.Clear();
			for (int i = 0; i < colors.Count;)
			{
				TrackBar track = new TrackBar();
				track.BackColor = colors[i];
				track.Orientation = Orientation.Horizontal;
				track.Enabled = isTrackEnable;
				track.Tag = i++;
				track.Minimum = -GetTrackMinMax();
				track.Maximum = GetTrackMinMax();
				track.SmallChange = 1;
				track.LargeChange = 1;
				track.ValueChanged += TrackBar_ValueChanged;
				trackBars.Add(track);
			}

			panelTrackers.Controls.Clear();
			int offfsetTracker = 0;
			foreach (TrackBar track in trackBars)
			{
				track.Size = new Size(panelTrackers.Width - 18, track.Size.Height);
				track.Location = new Point(0, offfsetTracker);
				offfsetTracker += track.Size.Height;
				panelTrackers.Controls.Add(track);
			}
			trackBars.Last().Enabled = false;
			panelTrackers.Refresh();
		}

		private void rootPanel_Paint(object sender, PaintEventArgs e)
		{
			palette = paletter.GetColorsPalette(lengthPalette, setterGradLength, customColors.ToArray());
			int heightRect = palettePanel.Height / lengthPalette;
			int thisHeightRec = 0;

			for (int i = 0; i < palette.Count;)
			{
				DrawRectangle(new Rectangle(0, thisHeightRec, palettePanel.Width, heightRect), palette[i++], palettePanel);
				thisHeightRec += heightRect;
			}

			#region OutInfo

			string colorsOutStr = string.Empty;
			foreach (int item in paletter.ConvertToDec(paletter.ConvertToHex(palette)))
			{
				colorsOutStr += $"{item}, ";
			}

			string trackOutStr = "|";
			foreach (TrackBar track in trackBars)
			{
				trackOutStr += $" {track.Tag}: {track.Value} |";
			}

			colorInfo.Text = $"count: {palette.Count}" + Environment.NewLine +
							 trackOutStr + Environment.NewLine +
							 colorsOutStr;
			#endregion
		}

		// корректирует диапазоны переходов цветов
		// вызывается во время каждого перехода 
		// принимает в параметр цвет с которого начинается текущий диапазон
		private int setterGradLength(Color startColor)
		{
			for (int i = 0; i < GetColorsCount() - 1;)
			{
				if (customColors[i] == startColor)
				{
					if (i == 2)
					{

					}
					if (i == 3)
					{

					}
					var qwe = trackBars[i].Value;
					return -qwe;
					//return -trackBars[--i].Value;
				}
				i++;
			}

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

		private Color ShowColorDialog()
		{
			ColorDialog cd = new ColorDialog();
			cd.CustomColors = paletter.ConvertToDec(paletter.ConvertToHex(customColors)).ToArray();
			cd.SolidColorOnly = true;
			cd.FullOpen = true;
			if (cd.ShowDialog() == DialogResult.OK)
				return cd.Color;

			return Color.Empty;
		}

		private void nud_paletteLength_ValueChanged(object sender, EventArgs e)
		{
			lengthPalette = (int)nud_paletteLength.Value;
			UpdateAll();
		}

		private void TrackBar_ValueChanged(object sender, EventArgs e)
		{
			int sumVal = 0;
			trackBars.ForEach(t => sumVal += t.Value);
			if (sender is TrackBar trackBar && trackBar is not null)
			{
				if ((int)trackBar.Tag == 3)
				{

				}

				var qwe = trackBar.Value;

				if (lengthPalette < sumVal + lengthPalette && trackBar.Minimum < trackBar.Value && trackBar.Maximum > trackBar.Value)
					trackBar.Value -= 1;
				else
					PalettePanelRefresh();
			}
		}

		private void btn_addColor_Click(object sender, EventArgs e)
		{
			if (ShowColorDialog() is Color color && color != Color.Empty)
			{
				AddColor(color);
			}
		}

		private void btn_removeColor_Click(object sender, EventArgs e)
		{
			RemoveColor(Color.Empty);
		}

		private void btn_setUpPos_Click(object sender, EventArgs e)
		{
			UpColorToList(Color.Empty);
		}

		private void btn_setBottomPos_Click(object sender, EventArgs e)
		{
			DownColorToList(Color.Empty);
		}
	}
}