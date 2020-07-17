using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace app.Parser
{
    public class AlienImageParser
    {
        public string ParseImage(string path)
        {
            string results = "";

            // load image
            var image = Image.Load<Rgba32>(path);
            var ul = image[0, 0];

            // parse image
            // ...
            // profit

            return results;
        }
    }
}