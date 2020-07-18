namespace app.Operations
{
    public interface IToken
    {
        public IToken Resolve()
        {
            return this;
        }
    }
}