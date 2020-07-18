namespace app.Operations
{
    public class If0Operator : IApplyable
    {
        public IToken Value1 { get; }
        public IToken Value2 { get; }

        public If0Operator()
        { }

        private If0Operator(IToken value1)
        {
            Value1 = value1;
        }

        private If0Operator(IToken value1, IToken value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public IToken Apply(IToken arg)
        {
            if (Value1 == null)
                return new If0Operator(arg);

            if (Value2 == null)
                return new If0Operator(Value1, arg);

            if (((Constant)Value1).Value == 0)
            {
                return Value2;
            }
            else
            {
                return arg;
            }
        }
    }
}