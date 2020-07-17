using app.NewFolder;

namespace app.Operations
{
    public class NegOperator : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            return new Constant(-(arg as Constant).Value);
        }
    }
}