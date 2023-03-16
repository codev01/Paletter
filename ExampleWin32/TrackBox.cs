using System.Drawing;
using System.Windows.Forms;

namespace ExampleWin32
{
	internal class TrackBox
	{
		public static readonly TrackBox Empty = new TrackBox();

		public TrackBar TrackBar { get; set; }
		public CheckBox CheckBox { get; set; }
		public int Index { get; set; } = -1;
		public Color Color
		{
			get => _color;
			set
			{
				_color = value;
				TrackBar.BackColor = _color;
				CheckBox.BackColor = _color;
			}
		}

		public TrackBox() { }
		public TrackBox(TrackBar trackBar, CheckBox checkBox, Color color, int index)
		{
			TrackBar = trackBar;
			CheckBox = checkBox;
			Color = color;
			Index = index;
		}

		private Color _color = Color.Transparent;
	}
}
