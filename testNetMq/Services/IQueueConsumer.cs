namespace testNetMq.Services
{
    public interface IQueueConsumer
    {
        void Register();

        void Deregister();
    }
}