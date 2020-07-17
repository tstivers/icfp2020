using app.Parser;
using NUnit.Framework;
using System.IO;

namespace Tests
{
    public class MessageParserTest
    {
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

            Assert.That(parser.EvalLine("ap ap ap c add 1 2".Split(" ")), Is.EqualTo("3"));
        }

        [Test]
        public void BComb()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine("ap ap ap b inc dec 1".Split(" ")), Is.EqualTo("1"));
        }

        [Test]
        public void Pow2Test()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine("ap ap ap s ap ap c ap eq 0 1 ap ap b ap mul 2 ap ap b pwr2 ap add -1 0".Split(" ")), Is.EqualTo("1"));
            Assert.That(parser.EvalLine("ap ap ap s ap ap c ap eq 0 1 ap ap b ap mul 2 ap ap b pwr2 ap add -1 1".Split(" ")), Is.EqualTo("2"));
            Assert.That(parser.EvalLine("ap ap ap s ap ap c ap eq 0 1 ap ap b ap mul 2 ap ap b pwr2 ap add -1 3".Split(" ")), Is.EqualTo("8"));
            Assert.That(parser.EvalLine("ap ap ap s ap ap c ap eq 0 1 ap ap b ap mul 2 ap ap b pwr2 ap add -1 8".Split(" ")), Is.EqualTo("256"));
        }

        [Test]
        public void IsNilOperator()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine("ap isnil nil".Split(" ")), Is.EqualTo("t"));
            Assert.That(parser.EvalLine("ap isnil ap ap cons 1 2".Split(" ")), Is.EqualTo("f"));
        }

        [Test]
        public void ConsReturn()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine("ap ap cons 7 ap ap cons 123229502148636 nil".Split(" ")), Is.EqualTo("t"));
        }

        [Test]
        public void VariableTest()
        {
            var parser = new AlienMessageParser();

            Assert.That(parser.EvalLine(":1029 = ap ap cons 7 ap ap cons 123229502148636 nil".Split(" ")), Is.EqualTo("t"));
        }

        [Test]
        public void FileTest()
        {
            var message = File.ReadAllText(@"c:\users\tstivers\source\repos\icfp2020\messages\galaxy.txt");
            var parser = new AlienMessageParser();

            Assert.That(parser.Eval(message), Is.EqualTo("t"));
        }
    }
}