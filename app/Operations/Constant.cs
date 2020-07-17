using app.NewFolder;

namespace app.Operations
{
    public class Constant : IToken
    {
        public Constant(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}