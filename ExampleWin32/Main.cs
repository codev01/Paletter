using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Paletter;

namespace ExampleWin32
{
	public partial class Main : Form
	{
		private ColorPaletter _paletter = new();

		/// <summary>
		/// общее количество градаций цветов
		/// </summary>
		private int _lengthPalette;
		private static readonly Color[] _defaultColors =
		{
			Color.FromArgb(255, 0, 0), // Красный
			Color.FromArgb(255, 255, 0), // Желтый
			Color.FromArgb(0, 255, 0), // Зеленый
			Color.FromArgb(0, 255, 255), // Голубой
			Color.FromArgb(0, 0, 255) // Синий
		};
		private List<Color> _palette = new List<Color>();
		private List<Color> _previewsColors = _defaultColors.ToList();
		private List<Color> _customColors = _defaultColors.ToList();
		private List<TrackBox> _trackBoxes = new List<TrackBox>();
		private TrackBox _selectedTrackBox = TrackBox.Empty;

		private string _lable_ColorsCount_Text;

		public Main()
			=> InitializeComponent();

		private void Main_Load(object sender, EventArgs e)
		{
			_lable_ColorsCount_Text = lable_colorsCount.Text;

			// чтобы ширина градации не была меньше одного пикселя на экране // иначе всё будет белое
			nud_paletteLength.Maximum = palettePanel.Height;
			nud_paletteLength.Minimum = GetColorsCount();

			nud_paletteLength_ValueChanged(sender, e);
			UpdateTrackList(_customColors.ToList());
		}

		private void SetEnableButtons(bool isEnable)
		{
			TrackBox trackBox = GetSelectedTrackBox();
			if (trackBox.Equals(TrackBox.Empty))
				isEnable = false;

			btn_removeColor.Enabled = isEnable;
			btn_editColor.Enabled = isEnable;

			bool ie_btn = false;
			if (trackBox.Index >= 0 && trackBox.Index < GetColorsCount() && isEnable)
			{
				if (trackBox.Index > 0)
					ie_btn = true;
				btn_setUpPos.Enabled = ie_btn;

				ie_btn = false;
				if (trackBox.Index < GetColorsCount() - 1)
					ie_btn = true;
				btn_setBottomPos.Enabled = ie_btn;
			}
			else
			{
				btn_setUpPos.Enabled = ie_btn;
				btn_setBottomPos.Enabled = ie_btn;
			}
		}

		private TrackBox GetSelectedTrackBox()
			=> _selectedTrackBox;

		private void SelectTrackBox(int index)
			=> _trackBoxes[index].CheckBox.Checked = true;

		private void SetPositionColor(int value)
		{
			TrackBox trackBox = GetSelectedTrackBox();
			RemoveColor(trackBox.Index);
			_customColors.Insert(trackBox.Index + value, trackBox.Color);
			UpdateAll();
			SelectTrackBox(trackBox.Index + value);
		}

		private void UpColorToList()
			=> SetPositionColor(-1);
		private void DownColorToList()
			=> SetPositionColor(1);

		private void AddColor()
		{
			if (GetColorsCount() < _lengthPalette)
			{
				if (ShowColorDialog() is Color color && color != Color.Empty)
				{
					_customColors.Add(color);
					nud_paletteLength.Minimum = GetColorsCount();

					UpdateAll();
					SelectTrackBox(_trackBoxes.Last().Index);
				}
			}
			else
				MessageBox.Show("Цветов не может быть быльше длины палитры");
		}

		private void RemoveColor(int index)
			=> _customColors.RemoveAt(index);

		private void RemoveColor()
		{
			if (GetColorsCount() > 1)
			{
				TrackBox trackBox = GetSelectedTrackBox();
				RemoveColor(trackBox.Index);
				UpdateAll();
				SelectTrackBox(trackBox.Index - 1);
			}
		}

		private void EditColor()
		{
			if (ShowColorDialog() is Color color && color != Color.Empty)
			{
				TrackBox trackBox = GetSelectedTrackBox();
				_customColors[trackBox.Index] = color;
				UpdateAll();
				SelectTrackBox(trackBox.Index);
			}
		}

		private IAsyncResult UpdateAllAsync()
			=> BeginInvoke(new Action(UpdateAll));

		private void UpdateAll()
		{
			PalettePanelRefresh();
			UpdateTrackList(_customColors);
			lable_colorsCount.Text = _lable_ColorsCount_Text + _customColors.Count;
		}

		/// <summary>
		/// возвращает количество активных цветов диапазон градиентов которых можно менять
		/// </summary>
		private int GetColorsCount()
			=> _customColors.Count;

		/// <summary>
		/// получить единицу на которую можно изменить диапазон одного цвета
		/// </summary>
		private int GetTrackMinMax()
			=> _lengthPalette / GetColorsCount();

		private void SetLngthPalette(int length)
		{
			_lengthPalette = length;
			UpdateAllAsync();
		}

		private void PalettePanelRefresh()
			=> palettePanel.Refresh();

		private void UpdateTrackList(List<Color> colors)
		{
			if (!_previewsColors.SequenceEqual(colors) || _trackBoxes.Count == 0)
			{
				_previewsColors = new List<Color>(colors);
				SetEnableButtons(true);

				_trackBoxes.Clear();
				for (int i = 0; i < colors.Count; i++)
				{
					TrackBar trackBar = new TrackBar();
					trackBar.Orientation = Orientation.Horizontal;
					trackBar.Enabled = true;
					trackBar.SmallChange = 1;
					trackBar.LargeChange = 1;
					trackBar.ValueChanged += TrackBar_ValueChanged;

					CheckBox checkBox = new CheckBox();
					checkBox.AutoSize = false;
					checkBox.Enabled = true;
					checkBox.CheckAlign = ContentAlignment.MiddleCenter;
					checkBox.CheckedChanged += CheckBox_CheckedChanged;

					_trackBoxes.Add(new TrackBox(trackBar, checkBox, colors[i], i));
				}


				int offsetLocationY = 0,
					fullContentHeight = 0,
					rightMargin = 0;

				if (_trackBoxes.Count > 0)
				{
					fullContentHeight = _trackBoxes.Last().TrackBar.Height * colors.Count;
					if (fullContentHeight > panelTrackers.Height)
						rightMargin = SystemInformation.VerticalScrollBarWidth;
				}

				panelTrackers.Controls.Clear();
				foreach (TrackBox trackBox in _trackBoxes)
				{
					int defaultHeight = trackBox.TrackBar.Size.Height;

					trackBox.CheckBox.Size = new Size(defaultHeight, defaultHeight);
					trackBox.TrackBar.Size = new Size(panelTrackers.ClientSize.Width - trackBox.CheckBox.Width - rightMargin, defaultHeight);
					trackBox.CheckBox.Location = new Point(trackBox.TrackBar.Width, offsetLocationY);
					trackBox.TrackBar.Location = new Point(0, offsetLocationY);

					offsetLocationY += defaultHeight;
					panelTrackers.Controls.Add(trackBox.TrackBar);
					panelTrackers.Controls.Add(trackBox.CheckBox);
				}
				_trackBoxes.Last().TrackBar.Enabled = false;
				panelTrackers.Refresh();
			}

			foreach (TrackBox trackBox in _trackBoxes)
			{
				trackBox.TrackBar.Maximum = GetTrackMinMax();
				trackBox.TrackBar.Minimum = -GetTrackMinMax();
			}
		}

		private void RootPanel_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				_palette = _paletter.GetColorsPalette(_lengthPalette, SetterGradLength, _customColors.ToArray());
				int heightRect = palettePanel.Height / _lengthPalette;
				int thisHeightRec = 0;

				for (int i = 0; i < _palette.Count;)
				{
					DrawRectangle(new Rectangle(0, thisHeightRec, palettePanel.Width, heightRect), _palette[i++], palettePanel);
					thisHeightRec += heightRect;
				}

				#region OutInfo

				string colorsOutStr = string.Empty;
				foreach (int item in _paletter.ConvertListColorsToDec(_paletter.ConvertListColorsToHex(_palette)))
				{
					colorsOutStr += $"{item}, ";
				}

				string trackOutStr = "|";
				foreach (TrackBox trackBox in _trackBoxes)
				{
					trackOutStr += $" {trackBox.TrackBar.Tag}: {trackBox.TrackBar.Value} |";
				}

				colorInfo.Text = $"count: {_palette.Count}" + Environment.NewLine +
								 $"colors count: {GetColorsCount()}" + Environment.NewLine +
								 trackOutStr + Environment.NewLine +
								 colorsOutStr;
				#endregion
			}
			catch { }
		}

		// корректирует диапазоны переходов цветов
		// вызывается во время каждого перехода 
		// принимает в параметр цвет с которого начинается текущий диапазон
		private int SetterGradLength(Color startColor, int index)
			=> -_trackBoxes[index].TrackBar.Value;

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
			cd.CustomColors = _paletter.ConvertListColorsToOle(_customColors.Distinct().ToList());
			cd.Color = _customColors.First();
			cd.SolidColorOnly = true;
			cd.FullOpen = true;
			cd.AnyColor = true;
			if (cd.ShowDialog() == DialogResult.OK)
				return cd.Color;

			return Color.Empty;
		}

		private void TrackBar_ValueChanged(object sender, EventArgs e)
		{
			int sumVal = 0;
			_trackBoxes.ForEach(t => sumVal += t.TrackBar.Value);
			if (sender is TrackBar trackBar && trackBar is not null)
			{
				if (_lengthPalette < sumVal + _lengthPalette && trackBar.Minimum <= trackBar.Value && trackBar.Maximum >= trackBar.Value)
					trackBar.Value -= 1;
				else
					PalettePanelRefresh();
			}
		}

		private void CheckBox_CheckedChanged(object? sender, EventArgs e)
		{
			if (sender is CheckBox senderCheckBox)
				if (senderCheckBox.CheckState == CheckState.Checked)
				{
					TrackBox tb = TrackBox.Empty;
					foreach (TrackBox trackBox in _trackBoxes)
						if (senderCheckBox != trackBox.CheckBox)
							trackBox.CheckBox.Checked = false;
						else
							tb = trackBox;

					_selectedTrackBox = tb;
					SetEnableButtons(true);
				}
				else if (senderCheckBox.CheckState == CheckState.Unchecked)
				{
					_selectedTrackBox = TrackBox.Empty;
					SetEnableButtons(false);
				}

		}

		private void nud_paletteLength_ValueChanged(object sender, EventArgs e)
			=> SetLngthPalette((int)nud_paletteLength.Value);

		private void btn_addColor_Click(object sender, EventArgs e)
			=> AddColor();

		private void btn_removeColor_Click(object sender, EventArgs e)
			=> RemoveColor();

		private void btn_editColor_Click(object sender, EventArgs e)
			=> EditColor();

		private void btn_setUpPos_Click(object sender, EventArgs e)
			=> UpColorToList();

		private void btn_setBottomPos_Click(object sender, EventArgs e)
			=> DownColorToList();

		private void btn_reset_Click(object sender, EventArgs e)
		{
			_customColors = _defaultColors.ToList();
			UpdateAllAsync();
		}
	}
}