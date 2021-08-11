using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using cChat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace cChat.Data
{
    public interface IApplicationDbContext
    {
        DbSet<ChatRoom> ChatRooms { get; set; }
        DbSet<ChatMessage> ChatMessages { get; set; }
        DbSet<IdentityUser> Users{ get; set; }
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
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser{
                Id = new Guid().ToString(),
                UserName= "System",
                NormalizedUserName= "System",
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
