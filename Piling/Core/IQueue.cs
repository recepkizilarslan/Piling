using System;

namespace Piling.Core
{
    /// <summary>
    /// This interface is for logging.
    /// </summary>
    public interface IQueue<T> : IDisposable
    {
        /// <summary>
        /// Get or Set NewMemberAction action
        /// It fires a event when new member added to queue
        /// </summary>
        public Action NewMemberAction { get; set; }

        /// <summary>
        /// Enqueue a model in queue.
        /// </summary>
        public void Enqueue(T model);

        /// <summary>
        /// Dequeue a model in queue.
        /// </summary>
        /// <returns>T Model</returns>
        public T Dequeue();

        /// <summary>
        /// Get current count of the queue.
        /// </summary>
        /// <returns>Queue count</returns>
        public int Count();
    }
}
