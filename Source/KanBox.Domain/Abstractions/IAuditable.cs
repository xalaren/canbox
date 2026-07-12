namespace KanBox.Domain.Abstractions
{
    /// <summary>
    /// Represents auditable entities which tracks CreatedOn and ModifiedOn date and times
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// CreatedOn UTC date and time
        /// </summary>
        DateTime CreatedOnUtc { get; }
        
        /// <summary>
        /// ModifiedOn UTC date and time
        /// </summary>
        DateTime ModifiedOnUtc { get; }
    }
}
