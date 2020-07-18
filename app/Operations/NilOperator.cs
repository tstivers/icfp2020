namespace app.Operations
{
    public class NilOperator : IApplyable
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