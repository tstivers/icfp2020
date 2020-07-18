﻿using app.Parser;

namespace app.Operations
{
    public class IncOperator : IToken
    {
        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);
            return new Constant((x0 as Constant).Value + 1);
        }

        public override string ToString()
        {
            return $"inc";
        }
    }
}