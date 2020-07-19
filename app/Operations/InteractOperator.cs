using app.Extensions;
using app.Parser;
using System.Diagnostics;

namespace app.Operations
{
    public class InteractOperator : IToken
    {
        public IToken Protocol { get; }
        public IToken State { get; private set; }

        public InteractOperator()
        { }

        private InteractOperator(IToken value1)
        {
            Protocol = value1;
        }

        private InteractOperator(IToken value1, IToken value2)
        {
            Protocol = value1;
            State = value2;
        }

        public IToken Apply(IToken arg)
        {
            if (Protocol == null)
                return new InteractOperator(arg);

            if (State == null)
                return new InteractOperator(Protocol, arg);

            var Vector = AlienMessageParser.Reduce(arg);

            var p1 = AlienMessageParser.Reduce(Protocol.Apply(State));
            var p2 = AlienMessageParser.Reduce(p1.Apply(Vector));

            var flag = p2.Car().AsValue();
            var newState = IToken.Dem(p2.Cdr().Car().Mod());
            var data = p2.Cdr().Cdr().Car();

            if (flag != 0)
            {
                var s = new SendOperator();
                var v = s.Apply(data);
                return new InteractOperator(Protocol, newState).Apply(v);
            }

            AlienMessageParser.lastInteractResult = data;

            Debug.Assert(p2.Cdr().Cdr().Cdr() is NilOperator);

            return new InteractOperator(Protocol, newState);
        }

        public override string ToString()
        {
            return $"interact [{Protocol}] [{State}]";
        }
    }
}