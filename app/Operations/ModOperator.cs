using app.Encoder;
using app.Parser;

namespace app.Operations
{
    public class ModOperator : IToken
    {
        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);

            return ModDemod.Mod(x0);
        }

        public override string ToString()
        {
            return $"mod";
        }
    }
}