namespace app.Operations
{
    public class FComb : IToken
    {
        private static FComb _empty = new FComb();
        private static FComb _applied = new FComb(ConstantOperator.Acquire(0));

        public static FComb Acquire()
        {
            return _empty;
        }

        private IToken x0 { get; set; }

        private FComb()
        {
        }

        private FComb(IToken value)
        {
            x0 = value;
        }

        public bool SkipLeft => x0 != null;

        public IToken Apply(IToken arg)
        {
            if (x0 == null)
                return _applied;

            return arg;
        }

        public override string ToString()
        {
            return $"f [{x0}]";
        }
    }
}