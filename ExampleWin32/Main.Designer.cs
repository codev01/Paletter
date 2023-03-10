namespace ExampleWin32
{
	partial class Main
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.palettePanel = new System.Windows.Forms.Panel();
			this.trackBar_R = new System.Windows.Forms.TrackBar();
			this.trackBar_Y = new System.Windows.Forms.TrackBar();
			this.trackBar_G = new System.Windows.Forms.TrackBar();
			this.trackBar_LB = new System.Windows.Forms.TrackBar();
			this.defaultOffsetPanel = new System.Windows.Forms.Panel();
			this.colorInfo = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_R)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_Y)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_G)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_LB)).BeginInit();
			this.SuspendLayout();
			// 
			// palettePanel
			// 
			this.palettePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.palettePanel.Location = new System.Drawing.Point(41, 0);
			this.palettePanel.Name = "palettePanel";
			this.palettePanel.Size = new System.Drawing.Size(120, 700);
			this.palettePanel.TabIndex = 0;
			this.palettePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.rootPanel_Paint);
			// 
			// trackBar_R
			// 
			this.trackBar_R.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar_R.BackColor = System.Drawing.SystemColors.Control;
			this.trackBar_R.LargeChange = 1;
			this.trackBar_R.Location = new System.Drawing.Point(191, 29);
			this.trackBar_R.Margin = new System.Windows.Forms.Padding(20);
			this.trackBar_R.Minimum = -10;
			this.trackBar_R.Name = "trackBar_R";
			this.trackBar_R.Size = new System.Drawing.Size(259, 45);
			this.trackBar_R.TabIndex = 1;
			this.trackBar_R.Tag = "R";
			this.trackBar_R.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
			// 
			// trackBar_Y
			// 
			this.trackBar_Y.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar_Y.BackColor = System.Drawing.SystemColors.Control;
			this.trackBar_Y.LargeChange = 1;
			this.trackBar_Y.Location = new System.Drawing.Point(191, 114);
			this.trackBar_Y.Margin = new System.Windows.Forms.Padding(20);
			this.trackBar_Y.Minimum = -10;
			this.trackBar_Y.Name = "trackBar_Y";
			this.trackBar_Y.Size = new System.Drawing.Size(259, 45);
			this.trackBar_Y.TabIndex = 2;
			this.trackBar_Y.Tag = "Y";
			this.trackBar_Y.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
			// 
			// trackBar_G
			// 
			this.trackBar_G.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar_G.BackColor = System.Drawing.SystemColors.Control;
			this.trackBar_G.LargeChange = 1;
			this.trackBar_G.Location = new System.Drawing.Point(191, 264);
			this.trackBar_G.Margin = new System.Windows.Forms.Padding(20);
			this.trackBar_G.Minimum = -10;
			this.trackBar_G.Name = "trackBar_G";
			this.trackBar_G.Size = new System.Drawing.Size(259, 45);
			this.trackBar_G.TabIndex = 3;
			this.trackBar_G.Tag = "G";
			this.trackBar_G.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
			// 
			// trackBar_LB
			// 
			this.trackBar_LB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar_LB.BackColor = System.Drawing.SystemColors.Control;
			this.trackBar_LB.LargeChange = 1;
			this.trackBar_LB.Location = new System.Drawing.Point(191, 189);
			this.trackBar_LB.Margin = new System.Windows.Forms.Padding(10);
			this.trackBar_LB.Minimum = -10;
			this.trackBar_LB.Name = "trackBar_LB";
			this.trackBar_LB.Size = new System.Drawing.Size(259, 45);
			this.trackBar_LB.TabIndex = 4;
			this.trackBar_LB.Tag = "LB";
			this.trackBar_LB.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
			// 
			// defaultOffsetPanel
			// 
			this.defaultOffsetPanel.Location = new System.Drawing.Point(0, 0);
			this.defaultOffsetPanel.Name = "defaultOffsetPanel";
			this.defaultOffsetPanel.Size = new System.Drawing.Size(40, 700);
			this.defaultOffsetPanel.TabIndex = 5;
			this.defaultOffsetPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.defaultOffsetPanel_Paint);
			// 
			// colorInfo
			// 
			this.colorInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.colorInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.colorInfo.Location = new System.Drawing.Point(190, 411);
			this.colorInfo.Margin = new System.Windows.Forms.Padding(20);
			this.colorInfo.Multiline = true;
			this.colorInfo.Name = "colorInfo";
			this.colorInfo.Size = new System.Drawing.Size(260, 260);
			this.colorInfo.TabIndex = 6;
			this.colorInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(479, 700);
			this.Controls.Add(this.colorInfo);
			this.Controls.Add(this.defaultOffsetPanel);
			this.Controls.Add(this.trackBar_LB);
			this.Controls.Add(this.trackBar_G);
			this.Controls.Add(this.trackBar_Y);
			this.Controls.Add(this.trackBar_R);
			this.Controls.Add(this.palettePanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Paletter";
			this.Load += new System.EventHandler(this.Main_Load);
			((System.ComponentModel.ISupportInitialize)(this.trackBar_R)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_Y)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_G)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar_LB)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Panel palettePanel;
		private TrackBar trackBar_R;
		private TrackBar trackBar_Y;
		private TrackBar trackBar_G;
		private TrackBar trackBar_LB;
		private Panel defaultOffsetPanel;
		private TextBox colorInfo;
	}
}