using System.Security.Cryptography.X509Certificates;
using cChat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace cChat.Data
{
    public interface IApplicationDbContext
    {
        DbSet<ChatRoom> ChatRooms { get; set; }
        DbSet<ChatMessage> ChatMessages { get; set; }
        DbSet<Bot> Bots { get; set; }
        DbContext Instance { get; }
    }

    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ChatRoom> ChatRooms {get; set;}
        public DbSet<ChatMessage> ChatMessages {get; set;}
        public DbSet<Bot> Bots {get; set;}
        public DbContext Instance => this ;
    }
}
