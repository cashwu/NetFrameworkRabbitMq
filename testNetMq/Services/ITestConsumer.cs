using testNetMq.Controllers;
using testNetMq.Models;

namespace testNetMq.Services
{
    public interface ITestConsumer
    {
        void Handler(string key, QueueDto dto);
    }
}