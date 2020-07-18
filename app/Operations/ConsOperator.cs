namespace app.Operations
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

            //var x0 = AlienMessageParser.Reduce(Value1);
            //var x1 = AlienMessageParser.Reduce(Value2);
            //var x2 = AlienMessageParser.Reduce(arg);

            return new ApOperator(new ApOperator(arg, Value1), Value2);
        }

        public override string ToString()
        {
            return $"cons [{Value1}] [{Value2}]";
        }
    }
}