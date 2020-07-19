using app.Parser;

namespace app.Operations
{
    public class CarOperator : IToken
    {
        private static readonly CarOperator Empty = new CarOperator();

        public static CarOperator Acquire()
        {
            return Empty;
        }

        private CarOperator()
        {
        }

        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);
            return (x0 as ConsOperator).Value1;
        }

        public override string ToString()
        {
            return $"car";
        }
    }
}