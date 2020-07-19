using app.Parser;

namespace app.Operations
{
    public class ModOperator : IToken
    {
        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);

            return arg;
        }

        public override string ToString()
        {
            return $"mod";
        }
    }
}