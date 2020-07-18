namespace app.Operations
{
    public class FComb : IApplyable
    {
        public IToken Value { get; }

        public FComb()
        { }

        private FComb(IToken value)
        {
            Value = value;
        }

        public bool SkipLeft => Value != null;

        public IToken Apply(IToken arg)
        {
            if (Value == null)
                return new FComb(arg);

            return arg;
        }

        public override string ToString()
        {
            return "f";
        }
    }
}