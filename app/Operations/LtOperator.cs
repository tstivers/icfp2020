using app.Extensions;
using app.Parser;
using System;
using System.Collections.Generic;

namespace app.Operations
{
    public class LtOperator : IToken
    {
        private static readonly LtOperator Empty = new LtOperator();

        public static Dictionary<Tuple<IToken, IToken>, IToken> Cache = new Dictionary<Tuple<IToken, IToken>, IToken>();

        private IToken x0;

        public static LtOperator Acquire()
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
                x = new LtOperator(arg0);
            }
            else
            {
                var x0 = AlienMessageParser.Reduce(arg0);
                var x1 = AlienMessageParser.Reduce(arg1);
                x = x0.AsValue() < x1.AsValue() ? (IToken)KComb.Acquire() : FComb.Acquire();
            }

            Cache[key] = x;
            return x;
        }

        private LtOperator()
        {
        }

        private LtOperator(IToken arg1)
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
            return $"lt [{x0}]";
        }
    }
}