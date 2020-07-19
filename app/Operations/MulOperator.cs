using app.Extensions;
using app.Parser;
using System.Collections.Generic;

namespace app.Operations
{
    public class MulOperator : IToken
    {
        private static MulOperator _empty = new MulOperator();
        public static Dictionary<IToken, MulOperator> Cache = new Dictionary<IToken, MulOperator>();

        public static MulOperator Acquire()
        {
            return _empty;
        }

        private static MulOperator Acquire(IToken arg1)
        {
            if (Cache.TryGetValue(arg1, out var cached))
                return cached;

            var x = new MulOperator(arg1);
            Cache[arg1] = x;

            return x;
        }

        private IToken x0 { get; set; }

        private MulOperator()
        {
        }

        private MulOperator(IToken value)
        {
            x0 = value;
        }

        public IToken Apply(IToken arg)
        {
            if (x0 == null)
                return Acquire(arg);

            x0 = AlienMessageParser.Reduce(x0);
            var x1 = AlienMessageParser.Reduce(arg);

            return new Constant(x0.AsValue() * x1.AsValue());
        }

        public override string ToString()
        {
            return $"mul [{x0}]";
        }
    }
}