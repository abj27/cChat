using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cChat.Data.Entities
{
    
    public class ChatRoom: IAuditableEntity<int>
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name {get; set;}
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public List<ChatMessage> ChatMessages {get; set;} = new List<ChatMessage>();
    }
}
