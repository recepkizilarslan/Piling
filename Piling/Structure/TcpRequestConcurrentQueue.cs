using System;
using System.Collections.Concurrent;
using Piling.Core;
using Piling.Model;
using static System.GC;

namespace Piling.Structure
{
    /// <summary>
    /// Custom TcpRequest Concurrent Queue
    /// </summary>
    public class TcpRequestConcurrentQueue : IQueue<TcpRequest>
    {
        /// <summary>
        /// Use ConcurrentQueue for thread safe.
        /// </summary>
        private readonly ConcurrentQueue<TcpRequest> _concurrentQueue = new();

        /// <summary>
        /// Get or Set NewMemberAction action
        /// It fires a event when new member added to TcpRequest queue
        /// </summary>
        public Action NewMemberAction { get; set; }

        /// <summary>
        /// Enqueue TcpRequest member
        /// </summary>
        /// <param name="model">TcpRequest Object</param>
        public void Enqueue(TcpRequest model)
        {
            _concurrentQueue.Enqueue(model);
            NewMemberAction.Invoke();
        }

        /// <summary>
        /// Dequeue TcpRequest member
        /// </summary>
        /// <returns>TcpRequest</returns>
        public TcpRequest Dequeue()
        {
            _concurrentQueue.TryDequeue(out var temp);
            return temp;
        }

        /// <summary>
        /// Get TcpRequest queue count
        /// </summary>
        /// <returns>int count</returns>
        public int Count()
        {
            return _concurrentQueue.Count;
        }

        /// <summary>
        /// Dispose TcpRequest queue
        /// </summary>
        public void Dispose()
        {
            _concurrentQueue.Clear();
            SuppressFinalize(_concurrentQueue);
        }
    }
}
