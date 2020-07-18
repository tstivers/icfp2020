namespace app.Operations
{
    public class CarOperator : IApplyable
    {
        public IToken Apply(IToken arg)
        {
            return (arg as ConsOperator).Value1;
        }

        public override string ToString()
        {
            return $"car";
        }
    }
}