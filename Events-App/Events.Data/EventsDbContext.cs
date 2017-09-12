using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Events.Data.Entities;
using Events.Data.Configuration;
using System.Linq;
using System;

namespace Events.Data
{
    public class EventsDbContext : IdentityDbContext<Account, ApplicationRole, Guid>
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base(options) { }

        public DbSet<Account> AccountSet { get; set; }
        public DbSet<Event> EventSet { get; set; }
        public DbSet<Comment> CommentSet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>().ToTable("AccountSet");
            builder.Entity<Event>().ToTable("EventSet");
            builder.Entity<Comment>().ToTable("CommentSet");

            builder.ApplyConfiguration(new AccountConfig());
            builder.ApplyConfiguration(new CommentConfig());
            builder.ApplyConfiguration(new EventConfig());
        }
    }
}
