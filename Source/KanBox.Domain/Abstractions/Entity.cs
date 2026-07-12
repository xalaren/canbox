namespace KanBox.Domain.Abstractions
{

    /// <summary>
    /// Class that represents base functionality for all entities
    /// </summary>
    public abstract class Entity<T> : IAuditable where T : struct
    {
        /// <summary>
        /// Primary protected constructor
        /// </summary>
        protected Entity() { }

        public abstract T Id { get; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime ModifiedOnUtc { get; private set; }
    }
}
