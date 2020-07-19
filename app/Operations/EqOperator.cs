using app.Extensions;
using app.Parser;
using System;
using System.Collections.Generic;

namespace app.Operations
{
    public class EqOperator : IToken
    {
        private static readonly EqOperator Empty = new EqOperator();

        public static Dictionary<Tuple<IToken, IToken>, IToken> Cache = new Dictionary<Tuple<IToken, IToken>, IToken>();

        private IToken x0;

        public static EqOperator Acquire()
        {
            return Empty;
        }

        private static IToken Acquire(IToken arg0, IToken arg1)
        {
            var key = Tuple.Create(arg0, arg1);
            if (Cache.TryGetValue(key, out var cached))
                return cached;

            IToken x;
            if (arg1 == null)
            {
                x = new EqOperator(arg0);
            }
            else
            {
                var x0 = AlienMessageParser.Reduce(arg0);
                var x1 = AlienMessageParser.Reduce(arg1);
                x = x0.AsValue() == x1.AsValue() ? (IToken)KComb.Acquire() : FComb.Acquire();
            }

            Cache[key] = x;
            return x;
        }

        private EqOperator()
        {
        }

        private EqOperator(IToken arg1)
        {
            x0 = arg1;
        }

        public IToken Apply(IToken arg)
        {
            if (x0 == null)
                return Acquire(arg, null);

            return Acquire(x0, arg);
        }

        public override string ToString()
        {
            return $"eq [{x0}]";
        }
    }
}