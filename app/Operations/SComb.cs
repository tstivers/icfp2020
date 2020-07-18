namespace app.Operations
{
    public class SComb : IApplyable
    {
        public IApplyable x0 { get; }
        public IApplyable x1 { get; }

        public SComb()
        { }

        private SComb(IToken value1)
        {
            x0 = (IApplyable)value1;
        }

        private SComb(IToken value1, IToken value2)
        {
            x0 = (IApplyable)value1;
            x1 = (IApplyable)value2;
        }

        public IToken Apply(IToken x2)
        {
            if (x0 == null)
                return new SComb(x2);

            if (x1 == null)
                return new SComb(x0, x2);

            return new ApOperator(new ApOperator(x0, x2), new ApOperator(x1, x2));
        }

        public override string ToString()
        {
            return $"s [{x0}] [{x1}]";
        }
    }
}