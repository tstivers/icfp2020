using app.Operations;
using NUnit.Framework;

namespace Tests
{
    public class DemodTests
    {
        [Test]
        public void ConsNilNil()
        {
            Assert.That(IToken.Parse("110000").token.ToString(), Is.EqualTo("cons [nil] [nil]"));
        }

        [Test]
        public void Cons0Nil()
        {
            Assert.That(IToken.Parse("1101000").token.ToString(), Is.EqualTo("cons [0] [nil]"));
        }

        [Test]
        public void ConsBigResponse()
        {
            Assert.That(IToken.Parse("1101100001110111110010011111001010000").token.ToString(), Is.EqualTo("cons [1] [cons [20372] [nil]]"));
        }
    }
}