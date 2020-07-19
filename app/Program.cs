using app.Parser;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace app
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            //var serverUrl = args[0];
            //var playerKey = args[1];
            //Console.WriteLine($"ServerUrl: {serverUrl}; PlayerKey: {playerKey}");

            //if (!Uri.TryCreate(serverUrl, UriKind.Absolute, out var serverUri))
            //{
            //    Console.WriteLine("Failed to parse ServerUrl");
            //    return 1;
            //}

            //using var httpClient = new HttpClient { BaseAddress = serverUri };
            //var requestContent = new StringContent(playerKey, Encoding.UTF8, MediaTypeNames.Text.Plain);
            //using var response = await httpClient.PostAsync("", requestContent);
            //if (!response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine($"Unexpected server response: {response}");
            //    return 2;
            //}

            //var responseString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine($"Server response: {responseString}");

            //return 0;

            var x = new Thread(Parse, 1024 * 1024 * 1024);
            x.Start();
            while (x.IsAlive) ;

            return 0;
        }

        public static void Parse()
        {
            var message = File.ReadAllText(@"..\..\..\..\messages\test.txt");
            var parser = new AlienMessageParser(message);

            parser.Eval();
        }
    }
}