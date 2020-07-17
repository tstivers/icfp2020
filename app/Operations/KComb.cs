namespace app.Operations
{
    public class KComb : IApplyable
    {
        public IToken Value { get; }

        public KComb()
        { }

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
            return "t";
        }
    }
}