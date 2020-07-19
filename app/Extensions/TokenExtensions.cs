using app.Operations;
using app.Parser;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace app.Extensions
{
    public static class TokenExtensions
    {
        public static ConsOperator AsCons(this IToken token)
        {
            return (ConsOperator)token;
        }

        public static decimal AsValue(this IToken token)
        {
            return ((ConstantOperator)token).Value;
        }

        public static IToken Car(this IToken token)
        {
            return token.AsCons().x0;
        }

        public static IToken Cdr(this IToken token)
        {
            return token.AsCons().x1;
        }

        public static bool IsNil(this IToken token)
        {
            if (token is NilOperator)
                return true;
            if (token is ConsOperator)
                return false;
            throw new ArgumentException();
        }

        public static List<Point> ToPoints(this IToken token)
        {
            var points = new List<Point>();

            while (!token.IsNil())
            {
                var pt = token.Car().AsCons();
                token = token.Cdr();

                points.Add(new Point((int)pt.Car().AsValue(), (int)pt.Cdr().AsValue()));
            }

            return points;
        }

        public static List<Cell> ToCells(this IToken token, char image)
        {
            var cells = new List<Cell>();

            while (!token.IsNil())
            {
                var pt = token.Car().AsCons();
                token = token.Cdr();

                cells.Add(new Cell((int)pt.Car().AsValue(), (int)pt.Cdr().AsValue(), image));
            }

            return cells;
        }

        public static bool IsApplyable(this IToken token)
        {
            return (!(token is ApOperator));
        }

        public static IToken Reduce(this IToken token)
        {
            return AlienMessageParser.Reduce(token);
        }
    }
}