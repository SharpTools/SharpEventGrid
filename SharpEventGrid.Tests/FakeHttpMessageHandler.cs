using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharpEventGrid.Tests {
    public class FakeHttpMessageHandler : HttpMessageHandler {
        public int Calls { get; set; }
        public int CallToFail { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            Calls++;
            var response = new HttpResponseMessage();
            if (CallToFail > 0 && CallToFail == Calls) {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ReasonPhrase = "reason";
                response.Content = new StringContent("foo");
            }
            return Task.FromResult(response);
        }
    }
}
