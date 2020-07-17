using app.NewFolder;

namespace app.Operations
{
    public class MulOperator : IApplyable
    {
        public IToken Value { get; }

        public MulOperator()
        { }

        private MulOperator(IToken value)
        {
            Value = value;
        }

        public IToken Apply(IToken arg)
        {
            if (Value == null)
                return new MulOperator(arg);

            return new Constant((Value as Constant).Value * (arg as Constant).Value);
        }
    }
}