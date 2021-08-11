using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace cChat.Data.Entities
{
    public class ChatMessage :IAuditableEntity, IEntity<long>
    {
        public long Id { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        [Required]
        public int ChatRoomId {get; set;}
        public ChatRoom ChatRoom {get; set;}
        [Required]
        [MaxLength(300)]
        public string Message{get; set;}
        public DateTimeOffset ModifiedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}