Simple Dot Net Core Queue Manager
=========================
Simple.NETCore.QueueManager is a basic producer-consumer model that uses dataflow. for .Net Core

.net core producer-consumer sample:
The Produce method calls the Post method in a loop to synchronously write data to the target block. After the Produce method writes all data to the target block, it calls the Complete method to indicate that the block will never have additional data available. The Consume method uses the async and await operators to asynchronously compute the T data type that are received from the BlockingCollection<T> object.

Resources
---------
[How to: Implement a Producer-Consumer Dataflow Pattern](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-implement-a-producer-consumer-dataflow-pattern)
