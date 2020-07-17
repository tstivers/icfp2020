namespace app.Operations
{
    public class Constant : IToken
    {
        public Constant(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}