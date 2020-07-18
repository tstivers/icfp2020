namespace app.Operations
{
    public class NilOperator : IToken
    {
        public IToken Apply(IToken arg)
        {
            return new KComb();
        }

        public override string ToString()
        {
            return "nil";
        }
    }
}