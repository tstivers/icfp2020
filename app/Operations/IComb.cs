namespace app.Operations
{
    public class IComb : IToken
    {
        private static readonly IComb Empty = new IComb();

        public static IComb Acquire()
        {
            return Empty;
        }

        private IComb()
        {
        }

        public IToken Apply(IToken arg)
        {
            return arg;
        }

        public override string ToString()
        {
            return $"i";
        }
    }
}