using System;
using System.Collections.Generic;

namespace app.Operations
{
    public class ApOperator : IToken
    {
        private static readonly ApOperator Empty = new ApOperator();

        public static Dictionary<Tuple<IToken, IToken>, IToken> Cache = new Dictionary<Tuple<IToken, IToken>, IToken>();

        public IToken f;
        public IToken x;

        public static ApOperator Acquire()
        {
            return Empty;
        }

        public static IToken Acquire(IToken arg0, IToken arg1)
        {
            var key = Tuple.Create(arg0, arg1);
            if (Cache.TryGetValue(key, out var cached))
                return cached;

            var x = new ApOperator(arg0, arg1);

            Cache[key] = x;
            return x;
        }

        public ApOperator()
        {
        }

        private ApOperator(IToken arg0, IToken arg1)
        {
            f = arg0;
            x = arg1;
        }

        public override string ToString()
        {
            return $"ap [{f}] [{x}]";
        }
    }
}