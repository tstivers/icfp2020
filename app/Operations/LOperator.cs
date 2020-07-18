namespace app.Operations
{
    public class LOperator : IToken
    {
        public IToken Value { get; }

        public LOperator()
        { }

        private LOperator(IToken value)
        {
            Value = value;
        }

        public IToken Apply(IToken arg)
        {
            if (Value == null)
                return new LOperator(arg);

            return arg;
        }

        public override string ToString()
        {
            return $"l [{Value}]";
        }
    }
}