namespace app.Operations
{
    public class BComb : IApplyable
    {
        public IToken x0 { get; }
        public IToken x1 { get; }

        public BComb()
        { }

        private BComb(IToken value1)
        {
            x0 = value1;
        }

        private BComb(IToken value1, IToken value2)
        {
            x0 = value1;
            x1 = value2;
        }

        public IToken Apply(IToken x2)
        {
            if (x0 == null)
                return new BComb(x2);

            if (x1 == null)
                return new BComb(x0, x2);

            return new ApOperator(x0, new ApOperator(x1, x2));
        }

        public override string ToString()
        {
            return $"b [{x0}] [{x1}]";
        }
    }
}