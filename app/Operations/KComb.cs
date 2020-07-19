namespace app.Operations
{
    public class KComb : IToken
    {
        public IToken Value { get; }

        public KComb()
        {
        }

        public bool SkipRight => Value != null;

        private KComb(IToken value)
        {
            Value = value;
        }

        public IToken Apply(IToken arg)
        {
            if (Value == null)
                return new KComb(arg);

            return Value;
        }

        public override string ToString()
        {
            return $"t [{Value}]";
        }
    }
}