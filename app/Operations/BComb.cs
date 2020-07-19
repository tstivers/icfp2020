using System;
using System.Collections.Generic;

namespace app.Operations
{
    public class BComb : IToken
    {
        private static readonly BComb Empty = new BComb();

        public static Dictionary<Tuple<IToken, IToken, IToken>, IToken> Cache = new Dictionary<Tuple<IToken, IToken, IToken>, IToken>();

        private readonly IToken x0;
        private readonly IToken x1;

        public static BComb Acquire()
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
                x = new BComb(arg0, arg1);
            }
            else
            {
                x = new ApOperator(arg0, new ApOperator(arg1, arg2));
            }

            Cache[key] = x;
            return x;
        }

        private BComb()
        {
        }

        private BComb(IToken arg1, IToken arg2)
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
            return $"b [{x0}] [{x1}]";
        }
    }
}