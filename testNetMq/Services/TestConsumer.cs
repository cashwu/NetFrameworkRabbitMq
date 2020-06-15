using System;
using System.Threading;
using Newtonsoft.Json;
using testNetMq.Models;

namespace testNetMq.Services
{
    public class TestConsumer : ITestConsumer
    {
        public void Handler(string key, QueueDto dto)
        {
            Console.WriteLine($"---- {nameof(TestConsumer)} : {key} : {Thread.CurrentThread.ManagedThreadId} ----");
            Console.WriteLine($"---- {JsonConvert.SerializeObject(dto)} ----");
        }
    }
}