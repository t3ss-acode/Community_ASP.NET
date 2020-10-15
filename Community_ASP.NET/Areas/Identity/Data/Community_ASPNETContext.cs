using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Areas.Identity.Data;
using Community_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Community_ASP.NET.Data
{
    public class Community_ASPNETContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        public Community_ASPNETContext(DbContextOptions<Community_ASPNETContext> options)
            : base(options)
        {
        }

        public Community_ASPNETContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Group>().HasKey(i => i.Id);
            builder.Entity<Message>().HasKey(i => i.Id);
            builder.Entity<User>().HasKey(i => i.Id);

            builder.Entity<Message>()
                .HasOne(typeof(User), ("Sender"))
                .WithMany()
                .HasForeignKey("SenderId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });
            builder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);
            builder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);
        }
    }
}
