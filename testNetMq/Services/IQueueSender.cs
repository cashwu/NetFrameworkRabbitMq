namespace testNetMq.Services
{
    public interface IQueueSender
    {
        void Send<TMessage>(string routingKey, TMessage message)
            where TMessage : new();
    }
}