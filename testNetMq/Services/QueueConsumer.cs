using System;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Newtonsoft.Json;
using testNetMq.Models;

namespace testNetMq.Services
{
    public class QueueConsumer : IQueueConsumer
    {
        private readonly IBus _bus;
        private readonly ITestConsumer _testConsumer;

        private const string Queue = "cashQueue";

        public QueueConsumer(IBus bus, ITestConsumer testConsumer)
        {
            _bus = bus;
            _testConsumer = testConsumer;
        }
        
        public void Register()
        {
            var queue = _bus.Advanced.QueueDeclare(Queue);
            
            _bus.Advanced.Consume(queue, (body, properties, info) => Task.Run(() =>
            {
                var message = Encoding.UTF8.GetString(body);
                
                try
                {
                    var dto = JsonConvert.DeserializeObject<QueueDto>(message);

                    if (dto == null)
                    {
                        return;
                    }

                    _testConsumer.Handler(dto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }));

        }

        public void Deregister()
        {
            
        }
    }
}