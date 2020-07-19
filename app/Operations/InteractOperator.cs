using app.Extensions;
using app.Parser;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;

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

            var flag = p2.AsCons().Value1.AsValue();

            var newState = p2.AsCons().Value2.AsCons().Value1;
            var data = p2.AsCons().Value2.AsCons().Value2.AsCons();

            var picture = data.AsCons().Value1.AsCons().Value1;
            var pic2 = data.AsCons().Value1.AsCons().Value2.AsCons().Value1;

            var pts = picture.ToCells('#').Concat(pic2.ToCells('+')).ToList();

            var draw = new DrawOperator();
            draw.Draw(pts);

            var x = new List<List<Point>>();

            Debug.Assert(p2.AsCons().Value2.AsCons().Value2.AsCons().Value2 is NilOperator);

            Thread.Sleep(20000);

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