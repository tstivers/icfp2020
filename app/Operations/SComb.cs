using System;
using System.Collections.Generic;

namespace app.Operations
{
    public class SComb : IToken
    {
        private static readonly SComb Empty = new SComb();

        private static Dictionary<Tuple<IToken, IToken, IToken>, IToken> _cache = new Dictionary<Tuple<IToken, IToken, IToken>, IToken>();

        private IToken x0;
        private IToken x1;

        public static SComb Acquire()
        {
            return Empty;
        }

        private static IToken Acquire(IToken arg0, IToken arg1, IToken arg2)
        {
            var key = Tuple.Create(arg0, arg1, arg2);
            if (_cache.TryGetValue(key, out var cached))
                return cached;

            IToken x;
            if (arg2 == null)
            {
                x = new SComb(arg0, arg1);
            }
            else
            {
                x = new ApOperator(new ApOperator(arg0, arg2), new ApOperator(arg1, arg2));
            }

            _cache[key] = x;
            return x;
        }

        private SComb()
        {
        }

        private SComb(IToken arg1, IToken arg2)
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
            return $"s [{x0}] [{x1}]";
        }
    }
}