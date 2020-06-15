using System.Text;
using EasyNetQ;
using EasyNetQ.Topology;
using Newtonsoft.Json;

namespace testNetMq.Services
{
    public class QueueSender : IQueueSender
    {
        private readonly IBus _bus;
        private const string Queue = "cashQueue";
        private const string SendExchange = "cashExchange";
        private const string RouteKey = "cashRoute_";

        public QueueSender(IBus bus)
        {
            _bus = bus;
        }

        public void Send<TMessage>(string routingKey, TMessage message)
            where TMessage : new()
        {
            var exchange = _bus.Advanced.ExchangeDeclare(SendExchange, ExchangeType.Direct);
            routingKey = $"{RouteKey}{routingKey}";

            var properties = new MessageProperties();
            var body = Serialize(message);

            _bus.Advanced.Bind(exchange, new Queue(Queue, false), routingKey);
            _bus.Advanced.Publish(exchange, routingKey, false, properties, body);
        }

        private static byte[] Serialize<TMessage>(TMessage message)
            where TMessage : new()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }
    }
}