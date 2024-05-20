using KursovaBack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace KursovaBack.AppDbContext
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }
        public DbSet<ChatHub> ChatHubs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserConnectionId> UsersConnectionIds { get; set; }
        public DbSet<VideoChatHub> VideoChatHubs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VideoChatConnection> VideoChatConnections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

          

           

            modelBuilder.Entity<Message>()
                .HasOne(m => m.ChatHub)
                .WithMany(ch => ch.Messages)
                .HasForeignKey(m => m.ChatHubId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Cascade);


          
        }
    }
}   
