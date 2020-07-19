using System;

namespace app.Operations
{
    public interface IToken
    {
        public IToken Apply(IToken token)
        {
            throw new NotImplementedException();
        }

        public string Mod()
        {
            throw new NotImplementedException();
        }

        public bool SkipLeft => false;
        public bool SkipRight => false;

        public static IToken Dem(string message)
        {
            return Parse(message).token;
        }

        private static (IToken token, string remaining) Parse(string message)
        {
            var tag = message.Substring(0, 2);
            message = message.Substring(2);

            switch (tag)
            {
                case "00":
                    return (NilOperator.Acquire(), message);

                case "11":
                    var r1 = Parse(message);
                    var r2 = Parse(r1.remaining);
                    message = r2.remaining;
                    return (ConsOperator.Acquire(r1.token, r2.token), message);

                case "01":
                case "10": // number
                    var width = message.IndexOf('0');
                    IToken i = null;

                    if (width == 0)
                    {
                        i = ConstantOperator.Acquire(0);
                        message = message.Substring(1);
                    }
                    else
                    {
                        var length = width * 4;

                        var num = Convert.ToInt64(message.Substring(width + 1, length), 2);
                        i = tag == "01" ? ConstantOperator.Acquire(num) : ConstantOperator.Acquire(-num);
                        message = message.Substring(width + length + 1);
                    }

                    return (i, message);
            }

            throw new InvalidOperationException();
        }
    }
}