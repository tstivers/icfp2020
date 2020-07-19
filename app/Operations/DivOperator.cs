using app.Extensions;
using app.Parser;
using System;
using System.Collections.Generic;

namespace app.Operations
{
    public class DivOperator : IToken
    {
        private static DivOperator _empty = new DivOperator();
        public static Dictionary<IToken, DivOperator> Cache = new Dictionary<IToken, DivOperator>();

        public static DivOperator Acquire()
        {
            return _empty;
        }

        private static DivOperator Acquire(IToken arg1)
        {
            if (Cache.TryGetValue(arg1, out var cached))
                return cached;

            var x = new DivOperator(arg1);
            Cache[arg1] = x;

            return x;
        }

        private IToken x0 { get; set; }

        private DivOperator()
        {
        }

        private DivOperator(IToken value)
        {
            x0 = value;
        }

        public IToken Apply(IToken arg)
        {
            if (x0 == null)
                return Acquire(arg);

            x0 = AlienMessageParser.Reduce(x0);
            var x1 = AlienMessageParser.Reduce(arg);

            return ConstantOperator.Acquire(decimal.Round(x0.AsValue() / x1.AsValue(), MidpointRounding.ToZero));
        }

        public override string ToString()
        {
            return $"div [{x0}]";
        }
    }
}