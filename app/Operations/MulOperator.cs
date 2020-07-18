using app.Parser;

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

            var x0 = AlienMessageParser.Reduce(Value);
            var x1 = AlienMessageParser.Reduce(arg);

            return new Constant((x0 as Constant).Value * (x1 as Constant).Value);
        }

        public override string ToString()
        {
            return $"mul [{Value}]";
        }
    }
}