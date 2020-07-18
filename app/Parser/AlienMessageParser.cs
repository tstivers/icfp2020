using app.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace app.Parser
{
    public class AlienMessageParser
    {
        public Dictionary<string, IToken> Variables = new Dictionary<string, IToken>();

        private List<string[]> _lines;

        public AlienMessageParser(string message)
        {
            _lines = message.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ).Select(x => x.Split(" ")).ToList();
        }

        public string Eval()
        {
            string result = "";
            foreach (var line in _lines)
            {
                result = EvalLine(line);
            }

            Console.WriteLine(result);

            return result;
        }

        public string EvalLine(string[] ops)
        {
            if (ops[0].StartsWith(":")) // variable assignment
            {
                (Variables[ops[0]], _) = EvalOpCode(ops, 2);
                return null;
            }
            else if (ops[0] == "galaxy")
            {
                //Console.WriteLine(Variables[ops[2]]);
                return Variables[ops[2]].Resolve().ToString();
            }
            else
            {
                var (value, _) = EvalOpCode(ops, 0);

                return value.Resolve().ToString();
            }
        }

        public (IToken value, int index) EvalOpCode(string[] ops, int index)
        {
            (IToken value, int index) result;
            var op = ops[index];
            index += 1;

            switch (op)
            {
                case "ap":
                    result = EvalOpCode(ops, index);
                    var apfunc = result.value;
                    result = EvalOpCode(ops, result.index);
                    return (new ApOperator(apfunc, result.value), result.index);

                case "inc":
                    return (new IncOperator(), index);

                case "dec":
                    return (new DecOperator(), index);

                case "neg":
                    return (new NegOperator(), index);

                case "add":
                    return (new AddOperator(), index);

                case "mul":
                    return (new MulOperator(), index);

                case "div":
                    return (new DivOperator(), index);

                case "pwr2":
                    return (new Pwr2Operator(), index);

                case "t":
                    return (new KComb(), index);

                case "f":
                    return (new FComb(), index);

                case "s":
                    return (new SComb(), index);

                case "c":
                    return (new CComb(), index);

                case "b":
                    return (new BComb(), index);

                case "i":
                    return (new IComb(), index);

                case "cons":
                    return (new ConsOperator(), index);

                case "car":
                    return (new CarOperator(), index);

                case "cdr":
                    return (new CdrOperator(), index);

                case "nil":
                    return (new NilOperator(), index);

                case "isnil":
                    return (new IsNilOperator(), index);

                case "eq":
                    return (new EqOperator(), index);

                case "lt":
                    return (new EqOperator(), index);

                default:
                    if (decimal.TryParse(op, out var constant)) // int constant
                    {
                        return (new Constant(decimal.Parse(op)), index);
                    }

                    if (op.StartsWith("x"))
                    {
                        return (new VarOperator(op), index);
                    }

                    if (op.StartsWith(":")) // variable reference
                    {
                        if (Variables.ContainsKey(op))
                            return (Variables[op], index);
                        else
                        {
                            return (new LateBoundToken(op, Variables), index);
                        }
                    }

                    throw new InvalidOperationException();
            }
        }
    }
}