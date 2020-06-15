using System.Text;
using EasyNetQ;
using EasyNetQ.Topology;
using Newtonsoft.Json;

namespace testNetMq.Services
{
    public class QueueSender2 : IQueueSender
    {
        private readonly IBus _bus;
        private const string SendExchange = "cashExchangeFanout";
        private const string RouteKey = "cashRouteFanout_";

        public QueueSender2(IBus bus)
        {
            _bus = bus;
        }

        public void Send<TMessage>(string routingKey, TMessage message)
            where TMessage : new()
        {
            var exchange = _bus.Advanced.ExchangeDeclare(SendExchange, ExchangeType.Fanout);
            // routingKey = $"{RouteKey}{routingKey}_12323";

            var properties = new MessageProperties();
            var body = Serialize(message);

            // _bus.Advanced.Bind(exchange, new Queue(Queue, false), routingKey);
            _bus.Advanced.Publish(exchange, string.Empty, false, properties, body);
        }

        private static byte[] Serialize<TMessage>(TMessage message)
            where TMessage : new()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }
    }
}