using System;
using System.Threading.Tasks;

namespace SharpEventGrid.Sample {
    class Program {
        static async Task Main(string[] args) {
            Console.WriteLine("Start");
            var url = "your send grid url";
            var key = "yourkey";

            var data = new String('a', 100);

            var client = new EventGridClient(new Uri(url), key);
            await client.Send(new Event {
                Subject = "/foo",
                EventType = "super-event",
                Data = data
            });
        }
    }
}
