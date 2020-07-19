using app.Parser;
using System;
using System.Collections.Generic;

namespace app.Operations
{
    public class CComb : IToken
    {
        private static readonly CComb Empty = new CComb();

        public static Dictionary<Tuple<IToken, IToken, IToken>, IToken> Cache = new Dictionary<Tuple<IToken, IToken, IToken>, IToken>();

        private IToken x0;
        private IToken x1;

        public static CComb Acquire()
        {
            return Empty;
        }

        private static IToken Acquire(IToken arg0, IToken arg1, IToken arg2)
        {
            var key = Tuple.Create(arg0, arg1, arg2);
            if (Cache.TryGetValue(key, out var cached))
                return cached;

            IToken x;
            if (arg2 == null)
            {
                x = new CComb(arg0, arg1);
            }
            else
            {
                x = AlienMessageParser.Reduce(ApOperator.Acquire(ApOperator.Acquire(arg0, arg2), arg1));
            }

            Cache[key] = x;
            return x;
        }

        private CComb()
        {
        }

        private CComb(IToken arg1, IToken arg2)
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
            return $"c [{x0}] [{x1}]";
        }
    }
}