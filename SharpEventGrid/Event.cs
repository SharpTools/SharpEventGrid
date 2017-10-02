using System;

namespace SharpEventGrid {
    public class Event {
        /// <summary>
        /// Unique identifier for the event.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// One of the registered event types for this event source.
        /// </summary>
        public string EventType { get; set; }
        /// <summary>
        /// The time the event is generated based on the provider's UTC time.
        /// </summary>
        public DateTime EventTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Full resource path to the event source. This field is not writeable.
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// Publisher-defined path to the event subject.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Event data specific to the resource provider.
        /// </summary>
        public object Data { get; set; }
    }
}
