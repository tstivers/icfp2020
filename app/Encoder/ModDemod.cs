using app.Operations;
using System;

namespace app.Encoder
{
    public class ModDemod
    {
        public string Bleh(string message)
        {
            if (message == null)
                return null;

            var decoded = "";

            do
            {
                switch (message.Substring(0, 2))
                {
                    case "01": // positive number
                        var x = NumberDecode(message.Substring(2));
                        decoded += x.number;
                        message = x.remaining;
                        break;

                    case "10": // negative number
                        var y = NumberDecode(message.Substring(2));
                        decoded += -y.number;
                        message = y.remaining;
                        break;

                    case "11": // list
                        decoded += "[ " + Demod(message.Substring(2)) + " ]";
                        message = "";
                        break;

                    case "00": // nil
                        decoded += "nil";
                        message = message.Substring(2);
                        break;
                }

                decoded += " ";
            } while (message.Length > 0);

            return decoded.TrimEnd();
        }

        public (int number, string remaining) NumberDecode(string bits)
        {
            var width = bits.IndexOf('0');
            if (width == 0)
                return (0, bits.Substring(1));

            var length = width * 4;

            var num = Convert.ToInt32(bits.Substring(width + 1, length), 2);

            return (num, bits.Substring(width + length + 1));
        }

        public static string Mod(IToken token)
        {
            return token.Mod();
        }

        public static IToken Demod(string message)
        {
            throw new NotImplementedException();
        }
    }
}