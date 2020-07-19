using app.Encoder;
using NUnit.Framework;

namespace Tests
{
    public class DecodeTest
    {
        [Test]
        public void Test0()
        {
            var decode = new ModDemod();
            Assert.That(decode.Bleh("010"), Is.EqualTo("0"));
        }

        [Test]
        public void Test1()
        {
            var decode = new ModDemod();
            Assert.That(decode.Bleh("01100001"), Is.EqualTo("1"));
        }

        [Test]
        public void TestBig()
        {
            var decode = new ModDemod();
            Assert.That(decode.Bleh("110110000111011111100001001111110101000000"), Is.EqualTo("[ 1 [ 81744 nil ] ]"));
            Assert.That(decode.Bleh("110110000111011111100001001111110100110000"), Is.EqualTo("[ 1 [ 81740 nil ] ]"));
        }

        [Test]
        public void TestList()
        {
        }
    }
}