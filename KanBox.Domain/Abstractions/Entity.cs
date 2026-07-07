namespace KanBox.Domain.Abstractions
{

    /// <summary>
    /// Class that represents base functionality for all entities
    /// </summary>
    public abstract class Entity : IEquatable<Entity>
    {
        private const int HashCodeMultiplier = 31;

        /// <summary>
        /// Primary protected constructor
        /// </summary>
        /// <param name="id">Primary key that identifies entity</param>
        protected Entity(Guid id)
        {
            if (id == Guid.Empty)
            {
                Id = Guid.NewGuid();
                return;
            }

            Id = id;
        }

        /// <summary>
        /// Primary key that identifies entity
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Check equality of two entity examples
        /// </summary>
        /// <param name="left">Left entity example</param>
        /// <param name="right">Right entity example</param>
        /// <returns><br/><b>True</b> - if left entity equals to right entity and both of entities are not null</returns>
        /// <returns><br/><b>False</b> - if left entity not equals to right entity or any of entities are null</returns>
        public static bool operator ==(Entity? left, Entity? right)
        {
            return left is not null && right is not null && left.Equals(right);
        }

        /// <summary>
        /// Reverse of checking equality of two entity examples
        /// </summary>
        /// <param name="left">Left entity example</param>
        /// <param name="right">Right entity example</param>
        /// <returns><br/><b>True</b> - if left entity not equals to right entity</returns>
        /// <returns><br/><b>False</b> - if left entity equals to right entity</returns>
        public static bool operator !=(Entity? left, Entity? right) => !(left == right);

        /// <summary>
        /// Check equality with other entity. Equality will be defined by Id.
        /// </summary>
        /// <param name="other">Other entity to compare with</param>
        /// <returns><br/><b>True</b> - if this entity equals to other entity and other entity is not null and types of entities are the same</returns>
        /// <returns><br/><b>False</b> - if this entity Id not equals to other entity Id or other entity is null or types of entities are not the same</returns>
        public bool Equals(Entity? other)
        {
            return other is not null && other.GetType() == GetType() && other.Id == Id;
        }

        /// <summary>
        /// Check equality with other object
        /// </summary>
        /// <param name="obj">Other object</param>
        /// <returns><br/><b>True</b> - if obj is not null and obj type equal to entity type and obj as Entity is equal to this entity</returns>
        public override bool Equals(object? obj)
        {
            return obj is not null && obj.GetType() == GetType() && obj is Entity entity && entity.Id == Id;
        }


        /// <summary>
        /// Get hash code of this entity
        /// </summary>
        /// <returns>Hash code of this entity</returns>
        public override int GetHashCode()
        {
            return HashCodeMultiplier * Id.GetHashCode();
        }
    }
}
