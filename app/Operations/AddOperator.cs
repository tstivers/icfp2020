namespace app.Operations
{
    public class AddOperator : IApplyable
    {
        public IToken Value1 { get; }

        public AddOperator()
        { }

        private AddOperator(IToken value)
        {
            Value1 = value;
        }

        public IToken Apply(IToken arg)
        {
            if (Value1 == null)
                return new AddOperator(arg);

            return new Constant((Value1.Resolve() as Constant).Value + (arg.Resolve() as Constant).Value);
        }

        public override string ToString()
        {
            return $"add {Value1}";
        }
    }
}