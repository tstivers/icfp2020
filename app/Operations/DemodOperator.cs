using app.Encoder;
using app.Parser;

namespace app.Operations
{
    public class DemodOperator : IToken
    {
        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);

            return ModDemod.Demod(x0);
        }

        public override string ToString()
        {
            return $"dem";
        }
    }
}