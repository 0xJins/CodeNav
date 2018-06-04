﻿using CodeNav.Helpers;
using CodeNav.Models;
using System;
using System.Windows.Forms;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace CodeNav.Windows
{
    public partial class CustomizeBookmarkStylesWindow : Form
    {
        private Label _selectedLabel;

        public CustomizeBookmarkStylesWindow()
        {
            InitializeComponent();
        }

        private void CustomizeBookmarkStylesWindow_Load(object sender, EventArgs e)
        {
            AddBookmarkStyles();
        }

        private void AddBookmarkStyles()
        {
            foreach (var style in BookmarkHelper.GetBookmarkStyles())
            {
                var item = new Label
                {
                    BackColor = ToDrawingColor(style.Background),
                    ForeColor = ToDrawingColor(style.Foreground),
                    Text = "Method",
                    Width = 50,
                    Height = 50,
                    Margin = new Padding(0, 0, 3, 3),
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };
                item.Click += BookmarkStyle_Click;
                bookmarkStylesFlowLayoutPanel.Controls.Add(item);
            }
        }

        private void BookmarkStyle_Click(object sender, EventArgs e)
        {
            foreach (Label control in bookmarkStylesFlowLayoutPanel.Controls)
            {
                control.BorderStyle = BorderStyle.None;
            }

            _selectedLabel = sender as Label;
            _selectedLabel.BorderStyle = BorderStyle.FixedSingle;
        }

        private Color ToDrawingColor(SolidColorBrush solidColorBrush) 
            => Color.FromArgb(solidColorBrush.Color.A, 
                            solidColorBrush.Color.R, 
                            solidColorBrush.Color.G, 
                            solidColorBrush.Color.B);

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _selectedLabel.BackColor = colorDialog1.Color;
            }
        }

        private void foregroundButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _selectedLabel.ForeColor = colorDialog1.Color;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) => Close();

        private void okButton_Click(object sender, EventArgs e)
        {
            BookmarkHelper.SetBookmarkStyles(bookmarkStylesFlowLayoutPanel.Controls);
            Close();
        }
    }
}
