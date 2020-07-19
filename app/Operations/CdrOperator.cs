using app.Parser;

namespace app.Operations
{
    public class CdrOperator : IToken
    {
        private static readonly CdrOperator Empty = new CdrOperator();

        public static CdrOperator Acquire()
        {
            return Empty;
        }

        private CdrOperator()
        {
        }

        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);
            return (x0 as ConsOperator).x1;
        }
    }
}