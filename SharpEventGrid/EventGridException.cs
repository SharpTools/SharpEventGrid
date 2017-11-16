using System;

namespace SharpEventGrid {
    public class EventGridException : Exception {
        public int EventsSent { get; set; }
        public int HttpStatusCode { get; set; }
        public string HttpReasonPhrase { get; set; }
        public string ServerMessage { get; set; }

        public EventGridException(int statusCode, string reasonPhrase, string responseContent) {
            HttpStatusCode = statusCode;
            HttpReasonPhrase = reasonPhrase;
            ServerMessage = responseContent;
        }
    }
}
