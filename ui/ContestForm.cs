using app.Extensions;
using app.Operations;
using app.Parser;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ui
{
    public partial class ContestForm : Form
    {
        private AlienMessageParser Parser { get; }
        public List<Bitmap> PlayField { get; set; }
        public Rectangle Dimensions { get; set; }

        public List<Color> Colors = new List<Color> { Color.Black, Color.Blue, Color.Aqua, Color.Red, Color.Green };

        public ContestForm()
        {
            InitializeComponent();
            var message = File.ReadAllText(@"c:\users\tstivers\source\repos\icfp2020\messages\test.txt");
            Parser = new AlienMessageParser(message);
            Parser.Eval();
            PlayField = new List<Bitmap> { new Bitmap(1, 1) };
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
            var pics = result.Car();

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

            using (var c = Colors.AsEnumerable().GetEnumerator())
            {
                foreach (var pic in pictures)
                {
                    c.MoveNext();
                    var bm = new Bitmap(Dimensions.Right - Dimensions.Left + 1, Dimensions.Bottom - Dimensions.Top + 1);

                    foreach (var point in pic)
                    {
                        bm.SetPixel(point.X + -minX, point.Y + -minY,
                            Color.FromArgb(128, c.Current.R, c.Current.G, c.Current.B));
                    }

                    PlayField.Add(bm);
                }
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
            if (PlayField != null)
            {
                foreach (var bm in PlayField)
                {
                    e.Graphics.DrawImage(bm, 0, 0, pictureBox.Width, pictureBox.Height);
                }
            }
        }

        private Point GetScaledPoint(Point p)
        {
            var px = (double)p.X / pictureBox.Width;
            var py = (double)p.Y / pictureBox.Height;

            return new Point((int)((Dimensions.Width + 0.5) * px) + Dimensions.Left, (int)((Dimensions.Height + 0.5) * py) + Dimensions.Top);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var p = GetScaledPoint(e.Location);
            this.Text = $"({p.X}, {p.Y})";
        }
    }
}