﻿using app.Parser;

namespace app.Operations
{
    public class SComb : IApplyable
    {
        public IToken x0 { get; }
        public IToken x1 { get; }

        public SComb()
        { }

        private SComb(IToken value1)
        {
            x0 = value1;
        }

        private SComb(IToken value1, IToken value2)
        {
            x0 = value1;
            x1 = value2;
        }

        public IToken Apply(IToken x2)
        {
            if (x0 == null)
                return new SComb(x2);

            if (x1 == null)
                return new SComb(x0, x2);

            var a0 = AlienMessageParser.Reduce(x0);
            var a1 = AlienMessageParser.Reduce(x1);
            var a2 = AlienMessageParser.Reduce(x2);

            return new ApOperator(new ApOperator(a0, a2), new ApOperator(a1, a2));
        }

        public override string ToString()
        {
            return $"s [{x0}] [{x1}]";
        }
    }
}