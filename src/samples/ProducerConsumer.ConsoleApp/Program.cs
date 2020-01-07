using Simple.NETCore.QueueManager;
using System;

namespace ProducerConsumer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("started...");


            Func<int, string> producer = (index) =>
            {
                System.Threading.Thread.Sleep(1500);

                var todoSome = $"index:{index}";
                Console.WriteLine($" + producer => {todoSome}");
                return todoSome;
            };

            Action<string> consumer = (result) =>
            {
                System.Threading.Thread.Sleep(1000);

                Console.WriteLine($" - consumered => {result}");
            };

            var queueConfigurations = new QueueConfigurations(3, 1);
            var queueService = new QueueService<string>(queueConfigurations);
            queueService.StartProcess(100, producer, consumer);

            foreach (var ex in queueService.Exceptions)
                Console.WriteLine($"Exception-> Method:{ex.Method}|Message:{ex.Message}");


            Console.WriteLine("end...");

            Console.ReadLine();
        }
    }
}
