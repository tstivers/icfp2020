using app.Extensions;
using app.Parser;

namespace app.Operations
{
    public class NegOperator : IToken
    {
        private static readonly NegOperator Empty = new NegOperator();

        public static NegOperator Acquire()
        {
            return Empty;
        }

        private NegOperator()
        {
        }

        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);
            return new Constant(-x0.AsValue());
        }

        public override string ToString()
        {
            return "neg";
        }
    }
}