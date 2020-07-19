using app.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace app.Parser
{
    public class AlienMessageParser
    {
        public static Dictionary<string, IToken> Variables = new Dictionary<string, IToken>();
        public static Dictionary<IToken, IToken> ReducedCache = new Dictionary<IToken, IToken>();

        private List<string[]> _lines;

        public AlienMessageParser(string message)
        {
            _lines = message.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ).Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToList();
        }

        public IToken interactToken;
        public static IToken lastInteractResult;

        public IToken Interact(int x, int y)
        {
            System.Diagnostics.Trace.WriteLine($"Interact({x}, {y});");

            var t = new Thread(() =>
            {
                interactToken = Reduce(ApOperator.Acquire(interactToken, ConsOperator.Acquire(ConstantOperator.Acquire(x), ConstantOperator.Acquire(y))));
            }, 1024 * 1024 * 1024);

            t.Start();
            t.Join();
            ClearCaches();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return lastInteractResult;
        }

        public IToken SkipToUniverse()
        {
            Interact(0, 0);
            Interact(0, 0);
            Interact(0, 0);
            Interact(0, 0);
            Interact(0, 0);
            Interact(0, 0);
            Interact(0, 0);
            Interact(0, 0);
            Interact(8, 4);
            Interact(2, -8);
            Interact(3, 6);
            Interact(0, -14);
            Interact(-4, 10);
            Interact(9, -3);
            Interact(-4, 10);
            return Interact(1, 4);
        }

        public static void ClearCaches()
        {
            ReducedCache.Clear();

            BComb.Cache.Clear();
            CComb.Cache.Clear();
            SComb.Cache.Clear();
            KComb.Cache.Clear();

            ApOperator.Cache.Clear();
            ConsOperator.Cache.Clear();

            LateBoundToken.Cache.Clear();

            DivOperator.Cache.Clear();
            MulOperator.Cache.Clear();
            AddOperator.Cache.Clear();
            EqOperator.Cache.Clear();
            LtOperator.Cache.Clear();

            ConstantOperator.Cache.Clear();
            VarOperator.Cache.Clear();
        }

        public void Eval()
        {
            foreach (var line in _lines.Take(_lines.Count - 1))
            {
                ParseLine(line);
            }

            var t = new Thread(() =>
            {
                foreach (var key in Variables.Keys.OrderBy(x => x).ToList())
                {
                    Console.WriteLine($"precompiling {key}");
                    Variables[key] = Reduce(Variables[key]);
                }
            }, 1024 * 1024 * 1024);
            t.Start();
            t.Join();

            var last = ParseTokens(_lines.Last().Skip(2).ToArray());
            interactToken = Reduce(last);
        }

        public void ParseLine(string[] ops)
        {
            if (ops[0].StartsWith(":")) // variable assignment
            {
                Variables[ops[0]] = ParseTokens(ops.Skip(2).ToArray());
            }
        }

        public static IToken Reduce(IToken token)
        {
            if (token == null)
                return null;

            //if (name != null)
            //    Console.WriteLine($"   Reducing {name}");

            if (ReducedCache.ContainsKey(token))
                return ReducedCache[token];

            Stack<IToken> stack = new Stack<IToken>();

            if (token is LateBoundToken t)
                return Reduce(Variables[t.Id]);

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
                    stack.Push(x); // wrong?
                    continue;
                }

                while (f is LateBoundToken lb)
                    f = Reduce(Variables[lb.Id]);

                while (x is LateBoundToken lx)
                    x = Reduce(Variables[lx.Id]);

                stack.Push(f.Apply(x));
            }

            var bleh = stack.Pop();
            if (bleh is ApOperator woot)
            {
                var reduced = Reduce(woot);
                ReducedCache[token] = reduced;
                return reduced;
            }

            ReducedCache[token] = bleh;
            return bleh;
        }

        public IToken ParseTokens(string[] ops)
        {
            var stack = new Stack<ApOperator>();
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
                        token = IncOperator.Acquire();
                        break;

                    case "dec":
                        token = DecOperator.Acquire();
                        break;

                    case "neg":
                        token = NegOperator.Acquire();
                        break;

                    case "add":
                        token = AddOperator.Acquire();
                        break;

                    case "mul":
                        token = MulOperator.Acquire();
                        break;

                    //case "l":
                    //    token = new LOperator();
                    //    break;

                    case "div":
                        token = DivOperator.Acquire();
                        break;

                    //case "pwr2":
                    //    return (new Pwr2Operator(), index);

                    case "t":
                        token = KComb.Acquire();
                        break;

                    //case "f":
                    //    return (new FComb(), index);

                    case "s":
                        token = SComb.Acquire();
                        break;

                    case "c":
                        token = CComb.Acquire();
                        break;

                    case "b":
                        token = BComb.Acquire();
                        break;

                    case "i":
                        token = IComb.Acquire();
                        break;

                    case "cons":
                    case "vec":
                        token = ConsOperator.Acquire();
                        break;

                    case "car":
                        token = CarOperator.Acquire();
                        break;

                    case "cdr":
                        token = CdrOperator.Acquire();
                        break;

                    case "nil":
                        token = NilOperator.Acquire();
                        break;

                    case "isnil":
                        token = IsNilOperator.Acquire();
                        break;

                    case "eq":
                        token = EqOperator.Acquire();
                        break;

                    case "if0":
                        token = new If0Operator();
                        break;

                    case "lt":
                        token = LtOperator.Acquire();
                        break;

                    //case "mod":
                    //    token = new ModOperator();
                    //    break;

                    //case "dem":
                    //    token = new DemodOperator();
                    //    break;

                    case "interact":
                        token = new InteractOperator();
                        break;

                    default:
                        if (decimal.TryParse(op, out var constant)) // int constant
                        {
                            token = ConstantOperator.Acquire(decimal.Parse(op));
                        }
                        else if (op.StartsWith("x"))
                        {
                            token = VarOperator.Acquire(op);
                        }
                        else if (op.StartsWith(":")) // variable reference
                        {
                            token = LateBoundToken.Acquire(op);
                        }
                        else
                            throw new InvalidOperationException();

                        break;
                }

                if (stack.Count == 0)
                {
                    if (!(token is ApOperator))
                    {
                        if (i != ops.Length - 1)
                            throw new InvalidOperationException();

                        return token;
                    }

                    stack.Push((ApOperator)token);
                }
                else
                {
                    var top = stack.Peek();
                    if (top.f == null)
                    {
                        top.f = token;
                        if (token is ApOperator ap)
                            stack.Push(ap);
                    }
                    else if (top.x == null)
                    {
                        top.x = token;
                        if (token is ApOperator ap)
                            stack.Push(ap);
                        else
                            while (stack.Count > 0 && stack.Peek().x != null)
                            {
                                token = stack.Pop();
                            }
                    }
                    else
                        throw new InvalidOperationException();
                }
            }

            if (stack.Count == 1)
                return stack.Pop();

            return token;
        }
    }
}