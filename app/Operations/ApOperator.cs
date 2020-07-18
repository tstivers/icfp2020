namespace app.Operations
{
    public class ApOperator : IToken
    {
        private readonly IToken _arg;
        private readonly IToken _func;

        public ApOperator(IToken func, IToken arg)
        {
            _func = func;
            _arg = arg;
        }

        public IToken Resolve()
        {
            return ((IApplyable)_func.Resolve()).Apply(_arg.Resolve());
        }

        public override string ToString()
        {
            return $"ap {_func} {_arg}";
        }
    }
}