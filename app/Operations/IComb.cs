namespace app.Operations
{
    public class IComb : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            return arg;
        }
    }
}