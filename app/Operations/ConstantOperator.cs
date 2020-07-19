using System;
using System.Collections.Generic;
using System.Text;

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

        public string Mod()
        {
            var s = new StringBuilder();

            if (Value < 0)
                s.Append("10");
            else
                s.Append("01");

            if (Value == 0)
            {
                return s.Append("0").ToString();
            }

            var number = Convert.ToString((int)Math.Abs(Value), 2);

            throw new NotImplementedException();
        }
    }
}