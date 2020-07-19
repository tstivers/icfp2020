using app.Parser;

namespace app.Operations
{
    public class DecOperator : IToken
    {
        private static readonly DecOperator Empty = new DecOperator();

        public static DecOperator Acquire()
        {
            return Empty;
        }

        private DecOperator()
        {
        }

        public IToken Apply(IToken arg)
        {
            var x0 = AlienMessageParser.Reduce(arg);
            return new Constant((x0 as Constant).Value - 1);
        }

        public override string ToString()
        {
            return $"dec";
        }
    }
}