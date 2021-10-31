using System;
using System.Threading;
using Piling.Model;

namespace Piling
{
    /// <summary>
    /// Control application state
    /// </summary>
    public class StateManager
    {
        /// <summary>
        /// Parsed ip addresses count;
        /// </summary>
        public volatile int ParsedIpAddress = 0;

        /// <summary>
        /// Created requests count
        /// </summary>
        public volatile int CreatedRequest = 0;

        /// <summary>
        /// Sent tcp requests count
        /// </summary>
        public volatile int SentRequests = 0;

        /// <summary>
        /// Success requests count
        /// </summary>
        public volatile int SuccessResults = 0;

        /// <summary>
        /// Use semaphore slim for thread restriction.
        /// </summary>
        private SemaphoreSlim _semaphore;

        /// <summary>
        /// State changer action 
        /// </summary>
        public Action<State> MainStateChangeAction;

        /// <summary>
        /// Thread synchronization object
        /// </summary>
        private EventWaitHandle _event;

        /// <summary>
        /// Start thread state.
        /// </summary>
        /// <param name="threadCount">Thread count</param>
        public void Start(int threadCount)
        {
           _semaphore = new SemaphoreSlim(threadCount);
            _event = new ManualResetEvent(false);
            _event.Set();
            MainStateChangeAction += ChangeState;
        }

        /// <summary>
        /// Change state
        /// </summary>
        /// <param name="state">State</param>
        private void ChangeState(State state)
        {
            switch (state)
            {
                case State.Stop:
                    Stop();
                    break;
                case State.Running:
                    break;
                case State.Resume:
                    Resume();
                    break;
                case State.Pause:
                    Pause();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        /// <summary>
        /// Pause app
        /// </summary>
        private void Pause() => _event.Reset();

        /// <summary>
        /// Resume app.
        /// </summary>
        public void Resume() => _event.Set();

        /// <summary>
        /// Stop app.
        /// </summary>
        private void Stop()
        {
            _event.Close();
            _event.Dispose();
        }

        /// <summary>
        /// State listener
        /// </summary>
        public void ListenState()
        {
            _event.WaitOne();
        }
    }
}
