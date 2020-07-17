namespace app.Operations
{
    public class EqOperator : IApplyable
    {
        public IToken Value { get; }

        public EqOperator()
        { }

        private EqOperator(IToken value)
        {
            Value = value;
        }

        public IToken Apply(IToken arg)
        {
            if (Value == null)
                return new EqOperator(arg);

            if ((Value as Constant).Value == (arg as Constant).Value)
                return new KComb();
            else
                return new FComb();
        }
    }
}