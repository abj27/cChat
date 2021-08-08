using System.Security.Cryptography.X509Certificates;
using cChat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace cChat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ChatRoom> ChatRooms {get; set;}
        public DbSet<ChatMessage> ChatMessages {get; set;}
        public DbSet<Bot> Bots {get; set;}
    }
}
