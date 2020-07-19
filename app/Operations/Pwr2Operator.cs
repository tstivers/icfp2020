using app.Extensions;
using System;

namespace app.Operations
{
    public class Pwr2Operator : IToken
    {
        public IToken Apply(IToken arg)
        {
            return ConstantOperator.Acquire((decimal)Math.Pow(2, (double)arg.AsValue()));
        }

        public override string ToString()
        {
            return "pwr2";
        }
    }
}