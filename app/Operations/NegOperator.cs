using app.Parser;

namespace app.Operations
{
    public class NegOperator : IToken
    {
        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);
            return new Constant(-(x0 as Constant).Value);
        }

        public override string ToString()
        {
            return "neg";
        }
    }
}