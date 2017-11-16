using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SharpEventGrid.Tests {
    public class ClientTests {

        private EventGridClient _client;
        private FakeHttpMessageHandler _handler;

        public ClientTests() {
            _handler = new FakeHttpMessageHandler();
            _client = new EventGridClient(new Uri("http://local.com"), "key", _handler);
        }

        [Fact]
        public async Task Should_batch_events() {
            var events = CreateEvents(10);
            await _client.Send(events, 3);
            Assert.Equal(4, _handler.Calls);
        }

        [Fact]
        public async Task Should_send_them_all_at_once() {
            var events = CreateEvents(10);
            await _client.Send(events, 10);
            Assert.Equal(1, _handler.Calls);
        }

        [Fact]
        public async Task Should_send_them_all_at_once_when_batch_is_greater_than_total() {
            var events = CreateEvents(10);
            await _client.Send(events, 100);
            Assert.Equal(1, _handler.Calls);
        }

        [Fact]
        public async Task Should_throw_on_parcial_results() {
            _handler.CallToFail = 2;
            var events = CreateEvents(10);
            try {
                await _client.Send(events, 3);
                Assert.True(false);
            }
            catch(EventGridException ex) {
                Assert.Equal(2, _handler.Calls);
                Assert.Equal(3, ex.EventsSent);
            }
        }

        private List<Event> CreateEvents(int size) {
            var events = new List<Event>();
            for (var i = 0; i < size; i++) {
                events.Add(new Event());
            }
            return events;
        }
    }
}
