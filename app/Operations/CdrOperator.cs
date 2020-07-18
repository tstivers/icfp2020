using app.Parser;

namespace app.Operations
{
    public class CdrOperator : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);
            return (x0 as ConsOperator).Value2;
        }
    }
}