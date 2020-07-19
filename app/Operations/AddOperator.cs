using app.Extensions;
using app.Parser;
using System;
using System.Collections.Generic;

namespace app.Operations
{
    public class AddOperator : IToken
    {
        private static readonly AddOperator Empty = new AddOperator();

        public static Dictionary<Tuple<IToken, IToken>, IToken> Cache = new Dictionary<Tuple<IToken, IToken>, IToken>();

        private IToken x0;

        public static AddOperator Acquire()
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
                x = new AddOperator(arg0);
            }
            else
            {
                var x0 = AlienMessageParser.Reduce(arg0);
                var x1 = AlienMessageParser.Reduce(arg1);
                x = ConstantOperator.Acquire(x0.AsValue() + x1.AsValue());
            }

            Cache[key] = x;
            return x;
        }

        private AddOperator()
        {
        }

        private AddOperator(IToken arg1)
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
            return $"add [{x0}]";
        }
    }
}