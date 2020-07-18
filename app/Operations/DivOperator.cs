using app.Parser;
using System;

namespace app.Operations
{
    public class DivOperator : IToken
    {
        public IToken Value { get; }

        public DivOperator()
        { }

        private DivOperator(IToken value)
        {
            Value = value;
        }

        public IToken Apply(IToken arg)
        {
            if (Value == null)
                return new DivOperator(arg);

            var x0 = AlienMessageParser.Reduce(Value);
            var x1 = AlienMessageParser.Reduce(arg);

            return new Constant(decimal.Round((x0 as Constant).Value / (x1 as Constant).Value, MidpointRounding.ToZero));
        }
    }
}