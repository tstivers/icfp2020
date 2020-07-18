namespace app.Operations
{
    public class LtOperator : IApplyable
    {
        public IToken Value { get; }

        public LtOperator()
        { }

        private LtOperator(IToken value)
        {
            Value = value;
        }

        public IToken Apply(IToken arg)
        {
            if (Value == null)
                return new LtOperator(arg);

            if ((Value as Constant).Value < (arg as Constant).Value)
                return new KComb();
            else
                return new FComb();
        }
    }
}