using app.Parser;

namespace app.Operations
{
    public class DemodOperator : IToken
    {
        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);

            return arg;
        }

        public override string ToString()
        {
            return $"dem";
        }
    }
}