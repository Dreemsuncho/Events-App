using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Events.Data.Entities;

namespace Events.Data
{
    public class EventsDbContext : IdentityDbContext<Account>
    {
        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base(options) { }

        public DbSet<Account> AccountSet { get; }
        public DbSet<Event> EventSet { get; }
        public DbSet<Comment> CommentSet { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>().ToTable("AccountSet");
            builder.Entity<Event>().ToTable("EventSet");
            builder.Entity<Comment>().ToTable("CommentSet");
        }
    }
}
