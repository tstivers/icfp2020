using System.Collections.Generic;

namespace app.Operations
{
    public class ConstantOperator : IToken
    {
        public static Dictionary<decimal, ConstantOperator> Cache = new Dictionary<decimal, ConstantOperator>();

        public readonly decimal Value;

        public static ConstantOperator Acquire(decimal value)
        {
            if (Cache.TryGetValue(value, out var cached))
                return cached;

            var x = new ConstantOperator(value);
            Cache[value] = x;

            return x;
        }

        private ConstantOperator(decimal value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}