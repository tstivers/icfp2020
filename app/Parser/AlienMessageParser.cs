using app.NewFolder;
using app.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace app.Parser
{
    public class AlienMessageParser
    {
        public Dictionary<string, IToken> Variables = new Dictionary<string, IToken>();

        public void Eval(string message)
        {
            foreach (var line in message.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ))
            {
                EvalLine(line.Split(" "));
            }
        }

        public string EvalLine(string[] ops)
        {
            if (ops[0].StartsWith(":")) // variable assignment
            {
                //        Variables[ops[0]] = EvalOpCode(ops.Slice(2));
            }
            else
            {
                var (value, _) = EvalOpCode(ops);
                return value.ToString();
            }

            throw new NotImplementedException();
        }

        public (IToken value, string[] remaining) EvalOpCode(string[] ops)
        {
            (IToken value, string[] remaining) result;
            var op = ops[0];
            var remaining = ops.Skip(1).ToArray();

            switch (op)
            {
                case "ap":
                    result = EvalOpCode(remaining);
                    var apfunc = result.value;
                    result = EvalOpCode(result.remaining);
                    return ((apfunc as IApplyable).Apply(result.value), result.remaining);

                case "inc":
                    return (new IncOperator(), remaining);

                case "dec":
                    return (new DecOperator(), remaining);

                case "neg":
                    return (new DecOperator(), remaining);

                case "add":
                    return (new AddOperator(), remaining);

                case "mul":
                    return (new MulOperator(), remaining);

                case "div":
                    return (new DivOperator(), remaining);

                case "t":
                    return (new TrueValue(), remaining);

                case "f":
                    return (new FalseValue(), remaining);

                case "s":
                    return (new SComb(), remaining);

                default:
                    if (int.TryParse(ops[0], out var constant))
                    {
                        return (new Constant(int.Parse(ops[0])), ops.Skip(1).ToArray());
                    }
                    throw new InvalidOperationException();
            }
        }
    }
}