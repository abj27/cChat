using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace cChat.Data.Entities
{
    [Index(nameof(Key), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class Bot :IAuditableEntity, IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name {get; set;}
        [MaxLength(20)]
        [Required]
        public string Key {get; set;}
        public DateTimeOffset ModifiedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}