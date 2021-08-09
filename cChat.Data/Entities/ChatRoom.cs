using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace cChat.Data.Entities
{
    
    [Index(nameof(Name), IsUnique = true)]
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
