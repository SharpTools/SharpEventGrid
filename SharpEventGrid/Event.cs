using System;

namespace SharpEventGrid {
    public class Event {
            public string Id { get; set; } = Guid.NewGuid().ToString();
            public string EventType { get; set; }
            public DateTime EventTime { get; set; } = DateTime.Now;
            public string Topic { get; set; }
            public string Subject { get; set; }
            public object Data { get; set; }
        }
}
