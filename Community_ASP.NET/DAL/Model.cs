using Community_ASP.NET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community_ASP.NET.DAL
{
    public class CommunityContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Message> Messages { get; set; }
        public DbSet<Entities.MessageStatus> MessageStatuses { get; set; }
        public DbSet<Entities.Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=community;Trusted_Connection=True;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Community");

            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<MessageStatus>().ToTable("MessageStatus");
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Group>().HasKey(i => i.Id);
            modelBuilder.Entity<Message>().HasKey(i => i.Id);
            modelBuilder.Entity<MessageStatus>().HasKey(i => new {i.MessageId, i.UserId });
            modelBuilder.Entity<User>().HasKey(i => i.Id);

            modelBuilder.Entity<Message>()
                .HasOne(typeof (User),("Sender"))
                .WithMany()
                .HasForeignKey("SenderId")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MessageStatus>()
                .HasOne(typeof(User), ("User"))
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
