using app.NewFolder;

namespace app.Operations
{
    public class DivOperator : IApplyable
    {
        public IToken Value { get; }

        public DivOperator()
        { }

        private DivOperator(IToken value)
        {
            Value = value;
        }

        public IToken Apply(IToken arg)
        {
            if (Value == null)
                return new DivOperator(arg);

            return new Constant((Value as Constant).Value / (arg as Constant).Value);
        }
    }
}