﻿namespace app.Operations
{
    public class IncOperator : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            return new Constant((arg as Constant).Value + 1);
        }

        public override string ToString()
        {
            return $"inc";
        }
    }
}