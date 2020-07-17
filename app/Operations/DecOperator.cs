namespace app.Operations
{
    public class DecOperator : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            return new Constant((arg as Constant).Value - 1);
        }
    }
}