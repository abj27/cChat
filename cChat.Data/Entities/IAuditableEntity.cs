using System;

namespace cChat.Data.Entities
{
    public interface IAuditableEntity<T>:IEntity<T>
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}