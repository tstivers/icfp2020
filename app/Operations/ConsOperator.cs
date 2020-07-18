﻿namespace app.Operations
{
    public class ConsOperator : IApplyable
    {
        public IToken Value1 { get; }
        public IToken Value2 { get; }

        public ConsOperator()
        { }

        private ConsOperator(IToken value1)
        {
            Value1 = value1;
        }

        private ConsOperator(IToken value1, IToken value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public IToken Apply(IToken arg)
        {
            if (Value1 == null)
                return new ConsOperator(arg);

            if (Value2 == null)
                return new ConsOperator(Value1, arg);

            var arg1 = arg.Apply(Value1) as IApplyable;

            return arg1.Apply(Value2);
        }

        public override string ToString()
        {
            return $"cons [{Value1}] [{Value2}]";
        }
    }
}