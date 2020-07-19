namespace app
{
    public class Cell
    {
        public Cell(int x, int y, char image)
        {
            X = x;
            Y = y;
            Image = image;
        }

        public char Image;
        public int X;
        public int Y;
    }
}