using app.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace app.Operations
{
    public class DrawOperator : IToken
    {
        public DrawOperator()
        {
        }

        public IToken Apply(IToken token)
        {
            Draw(token.ToCells('*'));

            return null;
        }

        public void Draw(List<Cell> cells)
        {
            int xorigin = cells.Select(x => x.X).Min();
            int yorigin = cells.Select(x => x.Y).Min();

            int width = cells.Select(x => x.X).Max() + 1;
            int height = cells.Select(x => x.Y).Max() + 1;

            Console.WriteLine($"origin: {xorigin}, {yorigin}  size: {width}, {height}");

            for (int y = yorigin; y < height; y++)
            {
                for (int x = xorigin; x < width; x++)
                {
                    var cell = cells.FirstOrDefault(z => z.X == x && z.Y == y);

                    if (cell != null)
                    {
                        Console.Write(cell.Image);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}