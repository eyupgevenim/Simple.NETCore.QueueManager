using System;

namespace Simple.NETCore.QueueManager
{
    /// <summary>
    /// Queue Exception
    /// </summary>
    public class QueueException : Exception
    {
        public ExceptionMethod Method { get; }
        internal QueueException(ExceptionMethod method) :base()
        {
            Method = method;
        }
        internal QueueException(ExceptionMethod method, string? message) : base(message)
        {
            Method = method;
        }
        internal QueueException(ExceptionMethod method, string? message, Exception? innerException) : base(message, innerException)
        {
            Method = method;
        }
    }
    /// <summary>
    /// Exception Method
    /// </summary>
    public enum ExceptionMethod
    {
        Producer = 1,
        Consumer = 2
    }
}
