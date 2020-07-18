using app.Parser;
using System;

namespace app.Operations
{
    public class IsNilOperator : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);

            if (x0 is NilOperator)
                return new KComb();
            else if (x0 is ConsOperator)
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