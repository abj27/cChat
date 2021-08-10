using System;

namespace cChat.Data.Entities
{
    public interface IAuditableEntity
    {
        public DateTimeOffset ModifiedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}