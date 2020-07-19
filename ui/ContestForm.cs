using app.Extensions;
using app.Operations;
using app.Parser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ui
{
    public partial class ContestForm : Form
    {
        private int EditingColorIdx { get; set; }
        private AlienMessageParser Parser { get; }
        public List<Bitmap> PlayField { get; set; }
        public Rectangle Dimensions { get; set; }

        public List<Color> Colors = new List<Color>();

        private bool ShowGrid = false;

        public ContestForm()
        {
            InitializeComponent();
            var message = File.ReadAllText(@"..\..\..\..\messages\test.txt");
            Parser = new AlienMessageParser(message);
            Parser.Eval();
            PlayField = new List<Bitmap> { new Bitmap(1, 1) };
            LoadColors();
            DisplayColorList();
            RedrawPlayfield(Parser.SkipToUniverse());
        }

        public void LoadColors()
        {
            if (File.Exists("./Colors.bin"))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("./Colors.bin", FileMode.Open, FileAccess.Read);
                Colors = (List<Color>)formatter.Deserialize(stream);
                stream.Close();
            }
            else Colors = new List<Color> { Color.Black, Color.SlateGray, Color.White, Color.Red, Color.Green, Color.Yellow, Color.Blue, Color.Brown, Color.Purple, Color.Teal };
        }

        public void SaveColors()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("./Colors.bin", FileMode.OpenOrCreate, FileAccess.Write);
            formatter.Serialize(stream, Colors);
            stream.Close();
        }

        public void DisplayColorList()
        {
            pnlColors.Controls.Clear();
            for (int idx = 0; idx < Colors.Count; idx++)
            {
                string labelText;
                switch (idx)
                {
                    case 0:
                        labelText = "Background";
                        break;

                    case 1:
                        labelText = "Grid";
                        break;

                    default:
                        labelText = "Layer " + (idx - 1);
                        break;
                }
                var btnColorButton = new ColorButton(labelText, Colors[idx], idx);
                btnColorButton.Width = 100;
                btnColorButton.Click += new System.EventHandler(colorBtnClick);
                pnlColors.Controls.Add(btnColorButton);
            }
        }

        private void colorBtnClick(object sender, EventArgs args)
        {
            ColorButton btn = (sender as ColorButton);
            ColorPicker.Color = btn.LayerColor;
            EditingColorIdx = btn.ColorIdx;
            if (ColorPicker.ShowDialog() != DialogResult.Cancel)
            {
                Colors[btn.ColorIdx] = ColorPicker.Color;
                btn.SetColor(ColorPicker.Color);
                pictureBox.Invalidate();
                SaveColors();
            }
        }

        private void pictureBox_Click(object sender, System.EventArgs e)
        {
            MouseEventArgs e2 = (MouseEventArgs)e;
            var p = GetScaledPoint(e2.Location);
            Text = "Working...";
            var result = Parser.Interact(p.X, p.Y);
            RedrawPlayfield(result);
        }

        private void RedrawPlayfield(IToken result)
        {
            var pics = result;

            List<List<Point>> pictures = new List<List<Point>>();

            while (!pics.IsNil())
            {
                var picture = pics.Car().ToPoints();
                pictures.Add(picture);
                pics = pics.Cdr();
            }

            while (pictures.Count > Colors.Count)
            {
                Colors.Add(Color.Yellow);
            }

            if (pictures.Count == 0)
            {
                PlayField = new List<Bitmap> { new Bitmap(1, 1) };
                return;
            }

            var minX = pictures.Select(x => x.Select(x => x.X).MinOrDefault()).MinOrDefault();
            var maxX = pictures.Select(x => x.Select(x => x.X).MaxOrDefault()).MaxOrDefault();

            var minY = pictures.Select(x => x.Select(x => x.Y).MinOrDefault()).MinOrDefault();
            var maxY = pictures.Select(x => x.Select(x => x.Y).MaxOrDefault()).MaxOrDefault();

            Dimensions = new Rectangle(minX, minY, maxX + -minX, maxY + -minY);

            PlayField.Clear();

            var ci = 2;
            foreach (var pic in pictures)
            {
                var color = Colors[ci++];

                var bm = new Bitmap(Dimensions.Right - Dimensions.Left + 1, Dimensions.Bottom - Dimensions.Top + 1);

                foreach (var point in pic)
                {
                    bm.SetPixel(point.X + -minX, point.Y + -minY,
                        Color.FromArgb(128, color.R, color.G, color.B));
                }

                PlayField.Add(bm);
            }

            pictureBox.Invalidate();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            //var bm = new Bitmap(PlayField.Width, PlayField.Height);
            //using (var g = Graphics.FromImage(bm))
            //{
            //    g.InterpolationMode = InterpolationMode.NearestNeighbor;
            //    g.PixelOffsetMode = PixelOffsetMode.Half;
            //    g.DrawImage(PlayField, 0, 0, bm.Width, bm.Height);
            //}

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

            // draw background
            Bitmap background = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (var g = Graphics.FromImage(background))
            {
                g.Clear(Colors[0]);
            }

            e.Graphics.DrawImage(background, 0, 0, pictureBox.Width, pictureBox.Height);

            // draw layers
            if (PlayField != null)
            {
                foreach (var bm in PlayField)
                {
                    e.Graphics.DrawImage(bm, 0, 0, pictureBox.Width, pictureBox.Height);
                }
            }

            // draw grid
            if (ShowGrid)
            {
                Bitmap grid = new Bitmap(pictureBox.Width, pictureBox.Height);
                using (var g = Graphics.FromImage(grid))
                {
                    g.Clear(Color.FromArgb(0, 0, 0, 0));
                    var last = 0;
                    for (int x = 0; x < pictureBox.Width; x++) // vertical lines
                    {
                        var pt = GetScaledPoint(new Point(x, 0));

                        if (pt.X != last)
                        {
                            g.DrawLine(new Pen(Colors[1]), new Point(x, 0), new Point(x, pictureBox.Height));
                            last = pt.X;
                        }
                    }

                    last = 0;
                    for (int y = 0; y < pictureBox.Height; y++) // horizontal lines
                    {
                        var pt = GetScaledPoint(new Point(0, y));

                        if (pt.Y != last)
                        {
                            g.DrawLine(new Pen(Colors[1]), new Point(0, y), new Point(pictureBox.Width, y));
                            last = pt.Y;
                        }
                    }
                }

                e.Graphics.DrawImage(grid, 0, 0, pictureBox.Width, pictureBox.Height);
            }
        }

        private Point GetScaledPoint(Point p)
        {
            var px = (double)p.X / pictureBox.Width;
            var py = (double)p.Y / pictureBox.Height;

            return new Point((int)((Dimensions.Width + 1) * px) + Dimensions.Left, (int)((Dimensions.Height + 1) * py) + Dimensions.Top);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var p = GetScaledPoint(e.Location);
            this.Text = $"({p.X}, {p.Y})";
        }

        private void chkGrid_CheckedChanged(object sender, System.EventArgs e)
        {
            ShowGrid = chkGrid.Checked;
            pictureBox.Invalidate();
        }

        private void btnCacheKill_Click(object sender, System.EventArgs e)
        {
            AlienMessageParser.ClearCaches();
        }
    }

    internal class ColorButton : Button
    {
        public int ColorIdx { get; set; }
        public Color LayerColor { get; set; }

        public ColorButton(string labelText, Color boundColor, int colorIndex)
        {
            var pnlColorDisplay = new Panel();
            pnlColorDisplay.BackColor = boundColor;
            pnlColorDisplay.Width = 10;
            pnlColorDisplay.Height = 10;
            Controls.Add(pnlColorDisplay);
            Text = labelText;
            ColorIdx = colorIndex;
            LayerColor = boundColor;
        }

        public void SetColor(Color newColor)
        {
            var colorDisplay = (Controls[0] as Panel);
            colorDisplay.BackColor = newColor;
        }
    }
}