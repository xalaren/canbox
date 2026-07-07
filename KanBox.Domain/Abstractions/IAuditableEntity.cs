namespace KanBox.Domain.Abstractions
{
    /// <summary>
    /// Represents auditable entities which tracks CreatedOn and ModifiedOn date and times
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// CreatedOn UTC date and time
        /// </summary>
        DateTime CreatedOnUtc { get; set; }
        
        /// <summary>
        /// ModifiedOn UTC date and time
        /// </summary>
        DateTime ModifiedOnUtc { get; set; }
    }
}
