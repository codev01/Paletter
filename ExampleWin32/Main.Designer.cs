﻿namespace ExampleWin32
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
			this.colorInfo = new System.Windows.Forms.TextBox();
			this.panelTrackers = new System.Windows.Forms.Panel();
			this.nud_paletteLength = new System.Windows.Forms.NumericUpDown();
			this.btn_addColor = new System.Windows.Forms.Button();
			this.btn_removeColor = new System.Windows.Forms.Button();
			this.btn_setUpPos = new System.Windows.Forms.Button();
			this.btn_setBottomPos = new System.Windows.Forms.Button();
			this.label_availColors = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.nud_paletteLength)).BeginInit();
			this.SuspendLayout();
			// 
			// palettePanel
			// 
			this.palettePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.palettePanel.Location = new System.Drawing.Point(0, 0);
			this.palettePanel.Margin = new System.Windows.Forms.Padding(0);
			this.palettePanel.Name = "palettePanel";
			this.palettePanel.Size = new System.Drawing.Size(120, 700);
			this.palettePanel.TabIndex = 0;
			this.palettePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.rootPanel_Paint);
			// 
			// colorInfo
			// 
			this.colorInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.colorInfo.Location = new System.Drawing.Point(140, 428);
			this.colorInfo.Margin = new System.Windows.Forms.Padding(20);
			this.colorInfo.Multiline = true;
			this.colorInfo.Name = "colorInfo";
			this.colorInfo.Size = new System.Drawing.Size(272, 243);
			this.colorInfo.TabIndex = 6;
			this.colorInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// panelTrackers
			// 
			this.panelTrackers.AutoScroll = true;
			this.panelTrackers.Location = new System.Drawing.Point(140, 29);
			this.panelTrackers.Margin = new System.Windows.Forms.Padding(20, 20, 3, 3);
			this.panelTrackers.Name = "panelTrackers";
			this.panelTrackers.Size = new System.Drawing.Size(272, 359);
			this.panelTrackers.TabIndex = 7;
			// 
			// nud_paletteLength
			// 
			this.nud_paletteLength.Location = new System.Drawing.Point(120, 0);
			this.nud_paletteLength.Margin = new System.Windows.Forms.Padding(0);
			this.nud_paletteLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nud_paletteLength.Name = "nud_paletteLength";
			this.nud_paletteLength.Size = new System.Drawing.Size(38, 23);
			this.nud_paletteLength.TabIndex = 8;
			this.nud_paletteLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nud_paletteLength.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
			this.nud_paletteLength.ValueChanged += new System.EventHandler(this.nud_paletteLength_ValueChanged);
			// 
			// btn_addColor
			// 
			this.btn_addColor.Location = new System.Drawing.Point(256, 394);
			this.btn_addColor.Name = "btn_addColor";
			this.btn_addColor.Size = new System.Drawing.Size(75, 23);
			this.btn_addColor.TabIndex = 9;
			this.btn_addColor.Text = "Add";
			this.btn_addColor.UseVisualStyleBackColor = true;
			this.btn_addColor.Click += new System.EventHandler(this.btn_addColor_Click);
			// 
			// btn_removeColor
			// 
			this.btn_removeColor.Location = new System.Drawing.Point(337, 394);
			this.btn_removeColor.Name = "btn_removeColor";
			this.btn_removeColor.Size = new System.Drawing.Size(75, 23);
			this.btn_removeColor.TabIndex = 10;
			this.btn_removeColor.Text = "Remove";
			this.btn_removeColor.UseVisualStyleBackColor = true;
			this.btn_removeColor.Click += new System.EventHandler(this.btn_removeColor_Click);
			// 
			// btn_setUpPos
			// 
			this.btn_setUpPos.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btn_setUpPos.Location = new System.Drawing.Point(418, 154);
			this.btn_setUpPos.Name = "btn_setUpPos";
			this.btn_setUpPos.Size = new System.Drawing.Size(25, 50);
			this.btn_setUpPos.TabIndex = 11;
			this.btn_setUpPos.Text = "↑";
			this.btn_setUpPos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btn_setUpPos.UseVisualStyleBackColor = true;
			this.btn_setUpPos.Click += new System.EventHandler(this.btn_setUpPos_Click);
			// 
			// btn_setBottomPos
			// 
			this.btn_setBottomPos.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btn_setBottomPos.Location = new System.Drawing.Point(418, 210);
			this.btn_setBottomPos.Name = "btn_setBottomPos";
			this.btn_setBottomPos.Size = new System.Drawing.Size(25, 50);
			this.btn_setBottomPos.TabIndex = 12;
			this.btn_setBottomPos.Text = "↓";
			this.btn_setBottomPos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btn_setBottomPos.UseVisualStyleBackColor = true;
			this.btn_setBottomPos.Click += new System.EventHandler(this.btn_setBottomPos_Click);
			// 
			// label_availColors
			// 
			this.label_availColors.AutoSize = true;
			this.label_availColors.Location = new System.Drawing.Point(161, 2);
			this.label_availColors.Name = "label_availColors";
			this.label_availColors.Size = new System.Drawing.Size(127, 15);
			this.label_availColors.TabIndex = 13;
			this.label_availColors.Text = "Availible colors count: ";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(449, 700);
			this.Controls.Add(this.label_availColors);
			this.Controls.Add(this.btn_setBottomPos);
			this.Controls.Add(this.btn_setUpPos);
			this.Controls.Add(this.btn_removeColor);
			this.Controls.Add(this.btn_addColor);
			this.Controls.Add(this.nud_paletteLength);
			this.Controls.Add(this.panelTrackers);
			this.Controls.Add(this.colorInfo);
			this.Controls.Add(this.palettePanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Paletter";
			this.Load += new System.EventHandler(this.Main_Load);
			((System.ComponentModel.ISupportInitialize)(this.nud_paletteLength)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Panel palettePanel;
		private TextBox colorInfo;
		private Panel panelTrackers;
		private NumericUpDown nud_paletteLength;
		private Button btn_addColor;
		private Button btn_removeColor;
		private Button btn_setUpPos;
		private Button btn_setBottomPos;
		private Label label_availColors;
	}
}