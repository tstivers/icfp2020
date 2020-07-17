using System;

namespace app.Operations
{
    public class Pwr2Operator : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            return new Constant((decimal)Math.Pow(2, (double)(arg as Constant).Value));
        }
    }
}