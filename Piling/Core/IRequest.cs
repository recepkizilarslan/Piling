using System;

namespace Piling.Core
{
    /// <summary>
    /// This interface is includes rule of request
    /// </summary>
    public interface IRequest<T> : IDisposable
    {
        /// <summary>
        /// Send request
        /// </summary>
        public T Send();
    }
}
