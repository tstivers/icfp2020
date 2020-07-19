using app.Parser;
using System;
using System.Collections.Generic;

namespace app.Operations
{
    public class ConsOperator : IToken
    {
        private static readonly ConsOperator Empty = new ConsOperator();

        public static Dictionary<Tuple<IToken, IToken, IToken>, IToken> Cache = new Dictionary<Tuple<IToken, IToken, IToken>, IToken>();

        public IToken x0;
        public IToken x1;

        public static ConsOperator Acquire()
        {
            return Empty;
        }

        public static IToken Acquire(IToken arg0, IToken arg1, IToken arg2 = null)
        {
            var key = Tuple.Create(arg0, arg1, arg2);
            if (Cache.TryGetValue(key, out var cached))
                return cached;

            IToken x;
            if (arg1 == null)
            {
                x = new ConsOperator(arg0, arg1);
            }
            else if (arg2 == null)
            {
                var a0 = AlienMessageParser.Reduce(arg0);
                var a1 = AlienMessageParser.Reduce(arg1);
                x = new ConsOperator(a0, a1);
            }
            else
            {
                x = ApOperator.Acquire(ApOperator.Acquire(arg2, arg0), arg1);
            }

            Cache[key] = x;
            return x;
        }

        private ConsOperator()
        {
        }

        private ConsOperator(IToken arg1, IToken arg2)
        {
            x0 = arg1;
            x1 = arg2;
        }

        public IToken Apply(IToken arg)
        {
            if (x0 == null)
                return Acquire(arg, null, null);

            if (x1 == null)
                return Acquire(x0, arg, null);

            return Acquire(x0, x1, arg);
        }

        public override string ToString()
        {
            return $"cons [{x0}] [{x1}]";
        }
    }
}