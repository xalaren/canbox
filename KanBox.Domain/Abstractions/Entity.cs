namespace KanBox.Domain.Abstractions
{

    /// <summary>
    /// Class that represents base functionality for all entities
    /// </summary>
    public abstract class Entity : IAuditable
    {
        /// <summary>
        /// Primary protected constructor
        /// </summary>
        protected Entity() { }

        public DateTime CreatedOnUtc { get; protected set; }
        public DateTime ModifiedOnUtc { get; protected set; }
    }
}
