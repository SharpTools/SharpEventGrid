using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpEventGrid {
    public interface IEventGridClient {
        Task Send(Event eventItem);
        Task Send(IEnumerable<Event> eventItems);
        Task Send(IEnumerable<Event> eventItems, int batchSize);
    }
}