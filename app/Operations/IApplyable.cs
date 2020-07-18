namespace app.Operations
{
    public interface IApplyable : IToken
    {
        public IToken Apply(IToken value);
    }
}