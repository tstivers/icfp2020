namespace app.Operations
{
    public class CdrOperator : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            return (arg as ConsOperator).Value2;
        }
    }
}