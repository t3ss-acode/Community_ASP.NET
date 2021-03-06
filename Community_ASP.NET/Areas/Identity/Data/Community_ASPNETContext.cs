﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Community_ASP.NET.Data
{
    public class Community_ASPNETContext : IdentityDbContext<Community_ASPNETUser>
    {
        public DbSet<Community_ASPNETUser> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }

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
            builder.Entity<Community_ASPNETUser>().HasKey(i => i.Id);

            builder.Entity<Message>()
                .HasOne(typeof(Community_ASPNETUser), ("Sender"))
                .WithMany()
                .HasForeignKey("SenderId")
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Message>()
                .Property(m => m.Timestamp)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAddOrUpdate();

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

            builder.Entity<LoginLog>()
                .HasOne(ll => ll.User)
                .WithMany(u => u.LoginLogs)
                .HasForeignKey(ll => ll.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<LoginLog>()
                .Property(ll => ll.Timestamp)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAdd();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Community_ASP.NET;Trusted_Connection=True;");
        }
    }
}
