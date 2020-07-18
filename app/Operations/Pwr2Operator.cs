using System;

namespace app.Operations
{
    public class Pwr2Operator : IToken
    {
        public IToken Apply(IToken arg)
        {
            return new Constant((decimal)Math.Pow(2, (double)(arg as Constant).Value));
        }

        public override string ToString()
        {
            return "neg";
        }
    }
}