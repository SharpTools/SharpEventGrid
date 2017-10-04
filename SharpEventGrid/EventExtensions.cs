using Newtonsoft.Json;

namespace SharpEventGrid {
    public static class EventExtensions {
        public static T DeserializeEvent<T>(this Event item) {
            return JsonConvert.DeserializeObject<T>(item.Data.ToString());
        }
    }
}
