using System;
using Newtonsoft.Json;
using testNetMq.Controllers;
using testNetMq.Models;

namespace testNetMq.Services
{
    public class TestConsumer : ITestConsumer
    {
        public void Handler(QueueDto dto)
        {
            Console.WriteLine($"---- {nameof(TestConsumer)} ----");
            Console.WriteLine($"---- {JsonConvert.SerializeObject(dto)} ----");
        }
    }
}