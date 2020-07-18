using app.Parser;

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

            var x0 = AlienMessageParser.Reduce(Value1);
            var x1 = AlienMessageParser.Reduce(arg);

            return new Constant((x0 as Constant).Value + (x1 as Constant).Value);
        }

        public override string ToString()
        {
            return $"add [{Value1}]";
        }
    }
}