namespace app.Operations
{
    public class VarOperator : IApplyable
    {
        private string _id;

        public VarOperator(string id)
        {
            _id = id;
        }

        public override string ToString()
        {
            return _id;
        }

        public IToken Apply(IToken value)
        {
            return new ApOperator(this, value);
        }
    }
}