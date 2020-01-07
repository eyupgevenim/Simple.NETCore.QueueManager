namespace Simple.NETCore.QueueManager
{
    /// <summary>
    /// Queue Configurations
    /// </summary>
    public class QueueConfigurations
    {
        /// <summary>
        /// Producer MaxDegreeOfParallelism
        /// </summary>
        public int ProducerMaxDegreeOfParallelism { get; }
        /// <summary>
        /// Consumer MaxDegreeOfParallelism
        /// </summary>
        public int ConsumerMaxDegreeOfParallelism { get; }
        public QueueConfigurations() 
        {
            this.ProducerMaxDegreeOfParallelism = 1;
            this.ConsumerMaxDegreeOfParallelism = 1;
        }
        /// <summary>
        /// Queue Configurations constructor
        /// </summary>
        /// <param name="producerMaxDegreeOfParallelism">Producer MaxDegreeOfParallelism</param>
        /// <param name="consumerMaxDegreeOfParallelism">Consumer MaxDegreeOfParallelism</param>
        public QueueConfigurations(int producerMaxDegreeOfParallelism, int consumerMaxDegreeOfParallelism)
        {
            this.ProducerMaxDegreeOfParallelism = producerMaxDegreeOfParallelism;
            this.ConsumerMaxDegreeOfParallelism = consumerMaxDegreeOfParallelism;
        }
    }
}
