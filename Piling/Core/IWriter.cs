using System.Threading.Tasks;
using Piling.Model;

namespace Piling.Core
{
    /// <summary>
    /// This interface is result output rules
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Create folder
        /// </summary>
        /// <param name="fileName">The file name that will write to results</param>
        public void Create(string fileName);

        /// <summary>
        /// Write request to file
        /// </summary>
        /// <param name="request"></param>
        public Task Write(Request request);
    }
}
