using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Piling.Core;
using Piling.Model;

namespace Piling.Implementation
{
    /// <summary>
    /// This class manage add log file process.
    /// </summary>
    public class CsvWriter : IWriter
    {
        /// <summary>
        /// Csv output path
        /// </summary>
        private string _outputPath;

        /// <summary>
        /// Static mutex object
        /// </summary>
        private static readonly Mutex Mutex = new();

        /// <summary>
        /// Create a file
        /// </summary>
        /// <param name="fileName"></param>
        public void Create(string fileName)
        {
            _outputPath = fileName;
        }

        /// <summary>
        /// Write Status
        /// </summary>
        /// <param name="request"></param>
        public async Task Write(Request request)
        {
            Mutex.WaitOne();

            await using (StreamWriter sw = new(_outputPath, append: true))
            {
                await sw.WriteLineAsync($"{request.Ip},{request.Port},{request.Status},{request.Time},{request.ElapsedTime}");
                sw.Close();
            }

            Mutex.ReleaseMutex();
        }



        /// <summary>
        /// This function check file exist and create file
        /// </summary>
        /// <param name="path">File path</param>
        private void FileControl(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }
    }
}
