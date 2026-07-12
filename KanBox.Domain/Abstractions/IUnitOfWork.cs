namespace KanBox.Domain.Abstractions
{
    /// <summary>
    /// Interface for unit of work pattern
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves changes
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A task that represents async save operation is ended</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
