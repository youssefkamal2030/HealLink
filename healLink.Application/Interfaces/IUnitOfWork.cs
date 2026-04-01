using System.Threading;
using System.Threading.Tasks;

namespace healLink.Application.Interfaces
{
    /// <summary>
    /// Wraps a single DbContext transaction. Repositories mutate state;
    /// IUnitOfWork commits it and dispatches domain events atomically.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Persists all pending changes and dispatches any domain events
        /// raised by aggregate roots during the current operation.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
