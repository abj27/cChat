using System;
using System.Linq;
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
        public override int SaveChanges()
        {
            SetCreatedDate(); 
            SetModifiedDate(); 
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //We are generating the single chatroom as seed data
            modelBuilder.Entity<ChatRoom>().HasData(new ChatRoom{
                Id = 1,
                Name = "Default Chat Room",
                ModifiedDate = DateTimeOffset.Now,
                CreatedDate = DateTimeOffset.Now,
            });
            //We are generating the default bot and the quotes bot as seed data 
            modelBuilder.Entity<Bot>().HasData(
            new Bot{
                Id = 1,
                Name = "DefaultBot",
                ModifiedDate = DateTimeOffset.Now,
                CreatedDate = DateTimeOffset.Now,
                Key = ""
            },
            new Bot
            {
                Id = 2,
                Name = "StockQuotesBot",
                ModifiedDate = DateTimeOffset.Now,
                CreatedDate = DateTimeOffset.Now,
                Key = "stock"
            });
        }
        private void SetModifiedDate()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().Where(x => x.State == EntityState.Modified))
            {
                entry.Entity.ModifiedDate= DateTimeOffset.Now;
            }
        }

        public void SetCreatedDate()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.CreatedDate = DateTimeOffset.Now;
            }
        }
    }
}
