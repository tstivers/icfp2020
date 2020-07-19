using app.Extensions;
using app.Parser;
using System.Collections.Generic;

namespace app.Operations
{
    public class AddOperator : IToken
    {
        private static AddOperator _empty = new AddOperator();
        public static Dictionary<IToken, AddOperator> Cache = new Dictionary<IToken, AddOperator>();

        public static AddOperator Acquire()
        {
            return _empty;
        }

        private static AddOperator Acquire(IToken arg1)
        {
            if (Cache.TryGetValue(arg1, out var cached))
                return cached;

            var x = new AddOperator(arg1);
            Cache[arg1] = x;

            return x;
        }

        private IToken x0 { get; set; }

        private AddOperator()
        {
        }

        private AddOperator(IToken value)
        {
            x0 = value;
        }

        public IToken Apply(IToken arg)
        {
            if (x0 == null)
                return Acquire(arg);

            x0 = AlienMessageParser.Reduce(x0);
            var x1 = AlienMessageParser.Reduce(arg);

            return new Constant(x0.AsValue() + x1.AsValue());
        }

        public override string ToString()
        {
            return $"add [{x0}]";
        }
    }
}