using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;
using WebApplication1.Model.DTO_s;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Participants)
                .WithOne()
                .HasForeignKey(p => p.ConversationId);

            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Participants)
                .WithOne()                
                .HasForeignKey(m => m.ConversationId);
        }
    }
}
