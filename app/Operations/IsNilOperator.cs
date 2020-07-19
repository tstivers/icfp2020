using app.Parser;
using System;

namespace app.Operations
{
    public class IsNilOperator : IToken
    {
        private static readonly IsNilOperator Empty = new IsNilOperator();

        public static IsNilOperator Acquire()
        {
            return Empty;
        }

        private IsNilOperator()
        {
        }

        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);

            if (x0 is NilOperator)
                return new KComb();
            else if (x0 is ConsOperator)
                return new FComb();
            else
                throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return $"isnill";
        }
    }
}