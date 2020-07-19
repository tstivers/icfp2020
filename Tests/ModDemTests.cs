using app.Encoder;
using app.Operations;
using NUnit.Framework;

namespace Tests
{
    public class ModDemTests
    {
        [Test]
        public void ConsNilNil()
        {
            Assert.That(IToken.Dem("110000").ToString(), Is.EqualTo("cons [nil] [nil]"));
        }

        [Test]
        public void Cons0Nil()
        {
            Assert.That(IToken.Dem("1101000").ToString(), Is.EqualTo("cons [0] [nil]"));
        }

        [Test]
        public void ConsBigResponse()
        {
            Assert.That(IToken.Dem("1101100001110111110010011111001010000").ToString(), Is.EqualTo("cons [1] [cons [20372] [nil]]"));
        }

        [Test]
        public void Mod20372()
        {
            var mod = ModDemod.Bleh(ConstantOperator.Acquire(20372).Mod());
            Assert.That(mod, Is.EqualTo("20372"));
        }

        [Test]
        public void Mod1()
        {
            var mod = ModDemod.Bleh(ConstantOperator.Acquire(1).Mod());
            Assert.That(mod, Is.EqualTo("1"));
        }
    }
}