namespace app.Operations
{
    public class NilOperator : IToken
    {
        private static readonly NilOperator Empty = new NilOperator();

        public static NilOperator Acquire()
        {
            return Empty;
        }

        private NilOperator()
        {
        }

        public IToken Apply(IToken arg)
        {
            return new KComb();
        }

        public override string ToString()
        {
            return "nil";
        }
    }
}