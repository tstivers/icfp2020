using app.Parser;

namespace app.Operations
{
    public class LtOperator : IToken
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

            var x0 = AlienMessageParser.Reduce(Value);
            var x1 = AlienMessageParser.Reduce(arg);

            if ((x0 as ConstantOperator).Value < (x1 as ConstantOperator).Value)
                return new KComb();
            else
                return new FComb();
        }

        public override string ToString()
        {
            return $"lt [{Value}]";
        }
    }
}