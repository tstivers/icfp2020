using app.Parser;

namespace app.Operations
{
    public class InteractOperator : IToken
    {
        public IToken Protocol { get; }
        public IToken State { get; }

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

            return p2;
        }

        private IToken f38(IToken protocol, IToken presult)
        {
            return null;
        }

        public override string ToString()
        {
            return $"s [{Protocol}] [{State}]";
        }
    }
}