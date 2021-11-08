using System;
using Piling.Model;

namespace Piling.Core
{
    /// <summary>
    /// This interface is includes rule of request
    /// </summary>
    public interface IRequest : IDisposable
    {
        /// <summary>
        /// Send request
        /// </summary>
        public TcpRequest Send();
    }
}
