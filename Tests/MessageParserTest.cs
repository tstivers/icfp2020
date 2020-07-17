using app.Parser;
using NUnit.Framework;

namespace Tests
{
    public class MessageParserTest
    {
        [Test]
        public void Power2()
        {
            var parser = new AlienMessageParser();

            parser.Eval("ap ap s ap ap c ap eq 0 1 ap ap b ap mul 2 ap ap b pwr2 ap add -1");
        }

        [Test]
        public void Inc()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine("ap inc 1".Split(" ")), Is.EqualTo("2"));
        }

        [Test]
        public void Add()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine("ap ap add 1 2".Split(" ")), Is.EqualTo("3"));
        }

        [Test]
        public void SComb()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine("ap ap ap s add inc 1".Split(" ")), Is.EqualTo("3"));
        }

        [Test]
        public void SComb2()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine("ap ap ap s mul ap add 1 6".Split(" ")), Is.EqualTo("42"));
        }

        [Test]
        public void CComb()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine("ap ap ap s mul ap add 1 6".Split(" ")), Is.EqualTo("42"));
        }
    }
}