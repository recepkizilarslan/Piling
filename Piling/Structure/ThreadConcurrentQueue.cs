using System;
using System.Collections.Concurrent;
using System.Threading;
using Piling.Core;
using static System.GC;

namespace Piling.Structure
{
    /// <summary>
    /// Custom Thread Concurrent Queue
    /// </summary>
    public class ThreadConcurrentQueue : IQueue<Thread>
    {
        /// <summary>
        /// Use ConcurrentQueue for thread safe.
        /// </summary>
        private readonly ConcurrentQueue<Thread> _concurrentQueue = new();

        /// <summary>
        /// Get or Set NewMemberAction action
        /// It fires a event when new member added to Thread queue
        /// </summary>
        public Action NewMemberAction { get; set; }

        /// <summary>
        /// Enqueue Thread member
        /// </summary>
        /// <param name="model">Thread Object</param>
        public void Enqueue(Thread model)
        {
            _concurrentQueue.Enqueue(model);
            NewMemberAction.Invoke();
        }

        /// <summary>
        /// Dequeue Thread member
        /// </summary>
        /// <returns>Thread</returns>
        public Thread Dequeue()
        {
            _concurrentQueue.TryDequeue(out var temp);
            return temp;
        }

        /// <summary>
        /// Get Thread queue count
        /// </summary>
        /// <returns>int count</returns>
        public int Count()
        {
            return _concurrentQueue.Count;
        }

        /// <summary>
        /// Dispose Thread queue
        /// </summary>
        public void Dispose()
        {
            _concurrentQueue.Clear();
            SuppressFinalize(_concurrentQueue);
        }
    }
}
