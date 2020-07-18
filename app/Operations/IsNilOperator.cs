using System;

namespace app.Operations
{
    public class IsNilOperator : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            if (arg is NilOperator)
                return new KComb();
            else if (arg is ConsOperator)
                return new FComb();
            else
                throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return $"isnill";
        }
    }
}