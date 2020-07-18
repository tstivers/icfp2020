namespace app.Operations
{
    public class VarOperator : IToken
    {
        public string Id;

        public VarOperator(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id;
        }
    }
}