namespace app.Operations
{
    public class CComb : IApplyable
    {
        public IToken x0 { get; }
        public IToken x1 { get; }

        public CComb()
        { }

        private CComb(IToken arg)
        {
            x0 = arg;
        }

        private CComb(IToken arg1, IToken arg2)
        {
            x0 = arg1;
            x1 = arg2;
        }

        public IToken Apply(IToken x2)
        {
            if (x0 == null)
                return new CComb(x2);

            if (x1 == null)
                return new CComb(x0, x2);

            return new ApOperator(new ApOperator(x0, x2), x1);
        }

        public override string ToString()
        {
            return $"c [{x0}] [{x1}]";
        }
    }
}