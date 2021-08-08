using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace cChat.Data.Entities
{
    public class Bot :IAuditableEntity<int>
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name {get; set;}
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}