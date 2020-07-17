using app.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace app.Parser
{
    public class AlienMessageParser
    {
        public Dictionary<string, IToken> Variables = new Dictionary<string, IToken>();

        public string Eval(string message)
        {
            string result = "";
            foreach (var line in message.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ))
            {
                result = EvalLine(line.Split(" "));
            }

            return result;
        }

        public string EvalLine(string[] ops)
        {
            if (ops[0].StartsWith(":")) // variable assignment
            {
                (Variables[ops[0]], _) = EvalOpCode(ops.Skip(2).ToArray());
                return Variables[ops[0]].ToString();
            }
            else
            {
                var (value, _) = EvalOpCode(ops);
                return value.ToString();
            }
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
                    return (new NegOperator(), remaining);

                case "add":
                    return (new AddOperator(), remaining);

                case "mul":
                    return (new MulOperator(), remaining);

                case "div":
                    return (new DivOperator(), remaining);

                case "pwr2":
                    return (new Pwr2Operator(), remaining);

                case "t":
                    return (new KComb(), remaining);

                case "f":
                    return (new FComb(), remaining);

                case "s":
                    return (new SComb(), remaining);

                case "c":
                    return (new CComb(), remaining);

                case "b":
                    return (new BComb(), remaining);

                case "i":
                    return (new IComb(), remaining);

                case "cons":
                    return (new ConsOperator(), remaining);

                case "car":
                    return (new CarOperator(), remaining);

                case "cdr":
                    return (new CdrOperator(), remaining);

                case "nil":
                    return (new NilOperator(), remaining);

                case "isnil":
                    return (new IsNilOperator(), remaining);

                case "eq":
                    return (new EqOperator(), remaining);

                default:
                    if (decimal.TryParse(ops[0], out var constant)) // int constant
                    {
                        return (new Constant(decimal.Parse(op)), remaining);
                    }

                    if (op.StartsWith(":")) // variable reference
                    {
                        if (Variables.ContainsKey(op))
                            return (Variables[op], remaining);
                        else
                        {
                            return (new LateBoundToken(op), remaining);
                        }
                    }

                    throw new InvalidOperationException();
            }
        }
    }
}