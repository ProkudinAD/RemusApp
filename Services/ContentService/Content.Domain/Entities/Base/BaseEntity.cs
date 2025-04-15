using Content.Domain.Interfaces;

namespace Content.Domain.Entities.Base
{
    public abstract class BaseEntity : BaseEntity<Guid>
    {
        protected BaseEntity() { }
    }

    /// <summary>
    /// Оставляем возможность использовать другие типы идентификатора
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseEntity<TKey> : IEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public TKey Id { get; set; }

        protected BaseEntity() { }
    }
}