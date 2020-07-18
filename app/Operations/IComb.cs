namespace app.Operations
{
    public class IComb : IApplyable
    {
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