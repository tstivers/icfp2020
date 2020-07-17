using app.Operations;

namespace app.NewFolder
{
    public class ApOperator : IToken
    {
        public IApplyable Function { get; }
        public IToken Arg { get; }

        public ApOperator(IApplyable func, IToken arg)
        {
            Function = func;
            Arg = arg;
        }

        public void Eval()
        {
            Function.Apply(Arg);
        }
    }
}