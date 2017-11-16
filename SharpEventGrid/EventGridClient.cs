using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharpEventGrid {
    public class EventGridClient : IEventGridClient {

        private HttpClient _client;
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };
        private readonly Uri _eventGridUri;

        public EventGridClient(Uri eventGridUri, string sasKey, HttpMessageHandler messageHandler = null) {
            _eventGridUri = eventGridUri;
            _client = new HttpClient(messageHandler ?? new HttpClientHandler());
            _client.DefaultRequestHeaders.Add("aeg-sas-key", sasKey);
        }

        public async Task Send(IEnumerable<Event> eventItems, int batchSize) {
            var sent = 0;
            while (eventItems.Any()) {
                var batch = eventItems.Take(batchSize);
                eventItems = eventItems.Skip(batchSize);
                try {
                    await Send(batch);
                }
                catch(EventGridException ex) {
                    ex.EventsSent = sent;
                    throw ex;
                }
                sent += batchSize;
            }
        }

        public async Task Send(IEnumerable<Event> eventItems) {
            var json = JsonConvert.SerializeObject(eventItems, _jsonSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_eventGridUri, content);
            if (response.IsSuccessStatusCode) {
                return;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            throw new EventGridException((int)response.StatusCode, response.ReasonPhrase, responseContent);
        }

        public async Task Send(Event eventItem) => await Send(new Event[1] { eventItem });
    }
}
