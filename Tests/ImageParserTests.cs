using app.Parser;
using NUnit.Framework;

namespace Tests
{
    public class ImageParserTests
    {
        [Test]
        public void Message17()
        {
            var parser = new AlienImageParser();

            parser.ParseImage(@"c:\users\tstivers\source\repos\icfp2020\images\message17.png");
        }
    }
}