using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simple.NETCore.QueueManager
{
    /// <summary>
    /// Queue Service Manager
    /// </summary>
    /// <typeparam name="T">T: handle type</typeparam>
    public class QueueService<T>
    {
        /// <summary>
        /// Queue Configurations
        /// </summary>
        public readonly QueueConfigurations QueueConfigurations;
        /// <summary>
        /// Producer-Consumer QueyeException collection
        /// </summary>
        public readonly ICollection<QueueException> Exceptions;
        /// <summary>
        ///T: handle Blocking Collection
        /// </summary>
        private readonly BlockingCollection<T> BlockingCollection;
        /// <summary>
        /// Queue constructor
        /// </summary>
        public QueueService()
        {
            QueueConfigurations = new QueueConfigurations();
            Exceptions = new List<QueueException>();
            BlockingCollection = new BlockingCollection<T>();
        }
        /// <summary>
        /// Queue constructor
        /// </summary>
        /// <param name="queueConfigurations">Queue configurations</param>
        public QueueService(QueueConfigurations queueConfigurations)
        {
            QueueConfigurations = queueConfigurations;
            Exceptions = new List<QueueException>();
            BlockingCollection = new BlockingCollection<T>();
        }
        /// <summary>
        /// Producer Method
        /// </summary>
        /// <param name="queueCount">Queue Count</param>
        /// <param name="func">Func<int,T> Custom Consumer Method, int:queue index, T: handle type</param>
        private void Producer(int queueCount, Func<int,T> func)
        {
            Parallel.For(0, queueCount,
                new ParallelOptions { MaxDegreeOfParallelism = QueueConfigurations.ProducerMaxDegreeOfParallelism },
                index =>
            {
                try
                {
                    var queue = func(index);
                    BlockingCollection.Add(queue);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(new QueueException(ExceptionMethod.Producer, $"index:{index}|{ex.Message}", ex));
                }
            });

            BlockingCollection.CompleteAdding();
        }
        /// <summary>
        /// Consumer Method
        /// </summary>
        /// <param name="action">Action<T> Custom Consumer Method, T: handle type</param>
        private void Consumer(Action<T> action)
        {
            Parallel.ForEach(BlockingCollection.GetConsumingEnumerable(),
                new ParallelOptions { MaxDegreeOfParallelism = QueueConfigurations.ConsumerMaxDegreeOfParallelism },
                (item, state, index) =>
            {
                try
                {
                    action(item);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(new QueueException(ExceptionMethod.Consumer, ex.Message, ex));
                }
            });
        }
        /// <summary>
        /// Start process Producer-Consumer
        /// </summary>
        /// <param name="queueCount">Queue Count</param>
        /// <param name="funcProducer">Func<int,T> Custom Consumer Method, int:queue index, T: handle type</param>
        /// <param name="actionConsumer">Action<T> Custom Consumer Method, T: handle type</param>
        public void StartProcess(int queueCount, Func<int, T> funcProducer, Action<T> actionConsumer)
        {
            var taskArray = new Task[]
            {
                    Task.Factory.StartNew(() => Producer(queueCount, funcProducer)),
                    Task.Factory.StartNew(() => Consumer(actionConsumer))
            };
            Task.WaitAll(taskArray);
        }
    }
}
