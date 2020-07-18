using app.Operations;
using System;
using System.Collections;
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

        public Stack<ApOperator> Stack;

        public string Eval()
        {
            string result = "";
            foreach (var line in _lines.Take(_lines.Count - 1))
            {
                ParseLine(line);
            }

            foreach (var key in Variables.Keys.OrderBy(x => x))
            {
                //Console.WriteLine($"reducing {key}");
                //Reduce(Variables[key]);
            }

            var last = ParseTokens(_lines.Last().Skip(2).ToArray());

            var t = Reduce(last, "bleh");

            Console.WriteLine(t);

            return result;
        }

        public void ParseLine(string[] ops)
        {
            if (ops[0].StartsWith(":")) // variable assignment
            {
                Variables[ops[0]] = ParseTokens(ops.Skip(2).ToArray());
            }
        }

        public IToken Reduce(IToken token, string name = null)
        {
            if (name != null)
                Console.WriteLine($"   Reducing {name}");

            Stack<IToken> stack = new Stack<IToken>();

            if (token is LateBoundToken t)
                return Reduce(Variables[t.Id], t.Id);

            if (token is ApOperator ap)
            {
                stack.Push(ap.x);
                stack.Push(ap.f);
            }
            else
            {
                return token;
            }

            while (stack.Count > 1)
            {
                var f = stack.Pop();

                if (f is ApOperator ff)
                {
                    stack.Push(ff.x);
                    stack.Push(ff.f);
                    continue;
                }

                var x = stack.Pop();

                if (f.SkipRight)
                {
                    stack.Push(f.Apply(null));
                    continue;
                }

                if (f.SkipLeft)
                {
                    stack.Push(x.Apply(null));
                    continue;
                }

                if (x is ApOperator xf)
                {
                    x = Reduce(x);
                }

                while (f is LateBoundToken lb)
                    f = Reduce(Variables[lb.Id], lb.Id);

                while (x is LateBoundToken lx)
                    x = Reduce(Variables[lx.Id], lx.Id);

                stack.Push(f.Apply(x));
            }

            var bleh = stack.Pop();
            if (bleh is ApOperator woot)
                return Reduce(woot);

            return bleh;
        }

        public IToken ParseTokens(string[] ops)
        {
            Stack = new Stack<ApOperator>();
            IToken token = null;

            for (int i = 0; i < ops.Length; i++)
            {
                var op = ops[i];

                switch (op)
                {
                    case "ap":
                        token = new ApOperator();
                        break;

                    case "inc":
                        token = new IncOperator();
                        break;

                    case "dec":
                        token = new DecOperator();
                        break;

                    case "neg":
                        token = new NegOperator();
                        break;

                    case "add":
                        token = new AddOperator();
                        break;

                    case "mul":
                        token = new MulOperator();
                        break;

                    case "l":
                        token = new LOperator();
                        break;

                    case "div":
                        token = new DivOperator();
                        break;

                    //case "pwr2":
                    //    return (new Pwr2Operator(), index);

                    case "t":
                        token = new KComb();
                        break;

                    //case "f":
                    //    return (new FComb(), index);

                    case "s":
                        token = new SComb();
                        break;

                    case "c":
                        token = new CComb();
                        break;

                    case "b":
                        token = new BComb();
                        break;

                    case "i":
                        token = new IComb();
                        break;

                    case "cons":
                        token = new ConsOperator();
                        break;

                    case "car":
                        token = new CarOperator();
                        break;

                    case "cdr":
                        token = new CdrOperator();
                        break;

                    case "nil":
                        token = new NilOperator();
                        break;

                    case "isnil":
                        token = new IsNilOperator();
                        break;

                    case "eq":
                        token = new EqOperator();
                        break;

                    case "if0":
                        token = new If0Operator();
                        break;

                    case "lt":
                        token = new LtOperator();
                        break;

                    default:
                        if (decimal.TryParse(op, out var constant)) // int constant
                        {
                            token = new Constant(decimal.Parse(op));
                        }
                        else if (op.StartsWith("x"))
                        {
                            token = new VarOperator(op);
                        }
                        else if (op.StartsWith(":")) // variable reference
                        {
                            token = new LateBoundToken(op, Variables);
                        }
                        else
                            throw new InvalidOperationException();

                        break;
                }

                if (Stack.Count == 0)
                {
                    if (!(token is ApOperator))
                    {
                        if (i != ops.Length - 1)
                            throw new InvalidOperationException();

                        return token;
                    }

                    Stack.Push((ApOperator)token);
                }
                else
                {
                    var top = Stack.Peek();
                    if (top.f == null)
                    {
                        top.f = token;
                        if (token is ApOperator ap)
                            Stack.Push(ap);
                    }
                    else if (top.x == null)
                    {
                        top.x = token;
                        if (token is ApOperator ap)
                            Stack.Push(ap);
                        else
                            while (Stack.Count > 0 && Stack.Peek().x != null)
                            {
                                token = Stack.Pop();
                            }
                    }
                    else
                        throw new InvalidOperationException();
                }
            }

            if (Stack.Count == 1)
                return Stack.Pop();

            return token;
        }
    }
}