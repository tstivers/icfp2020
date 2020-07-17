namespace app.Operations
{
    public class AddOperator : IApplyable
    {
        public IToken Value { get; }

        public AddOperator()
        { }

        private AddOperator(IToken value)
        {
            Value = value;
        }

        public IToken Apply(IToken arg)
        {
            if (Value == null)
                return new AddOperator(arg);

            return new Constant((Value as Constant).Value + (arg as Constant).Value);
        }
    }
}