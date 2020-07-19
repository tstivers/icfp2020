using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;

namespace app.Operations
{
    public class SendOperator : IToken
    {
        public IToken Apply(IToken token)
        {
            if (!Uri.TryCreate("https://icfpc2020-api.testkontur.ru/aliens/send", UriKind.Absolute, out var serverUri))
            {
                throw new InvalidOperationException();
            }

            using var httpClient = new HttpClient { BaseAddress = serverUri };
            var message = token.Mod();
            var requestContent = new StringContent(message, Encoding.UTF8, MediaTypeNames.Text.Plain);
            using var response = httpClient.PostAsync("?apiKey=f01dd9315e064ba388e0402d128a23f2", requestContent).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException();
            }

            var responseString = response.Content.ReadAsStringAsync().Result;

            return IToken.Dem(responseString);
        }
    }
}