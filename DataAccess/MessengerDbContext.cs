using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

namespace DataAccess
{
    public class MessengerDbContext : DbContext
    {
        public DbSet<Credentials> Credentials { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Data Source=MAX-DESKTOP\SQLEXPRESS;
                                          Initial Catalog=MessengerDb;
                                          Integrated Security=True;
                                          Connect Timeout=30;
                                          Encrypt=False;
                                          Trust Server Certificate=False;
                                          Application Intent=ReadWrite;
                                          Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Credentials>().HasKey(c => c.Id);
            modelBuilder.Entity<Credentials>().Property(c => c.Login).IsRequired();
            modelBuilder.Entity<Credentials>().Property(c => c.Password).IsRequired();
            modelBuilder.Entity<Credentials>().Property(c => c.Name).IsRequired();

            modelBuilder.Entity<User>().HasKey(u => u.CredentialsId);
            modelBuilder.Entity<User>().Property(c => c.IPAddress).IsRequired();

            modelBuilder.Entity<Chat>().HasKey(ch => ch.Id);
            modelBuilder.Entity<Message>().HasKey(m => m.Id);

            modelBuilder.Entity<Credentials>().HasOne(c => c.User)
                                              .WithOne(u => u.Credentials)
                                              .HasForeignKey<User>(u => u.CredentialsId);
            modelBuilder.Entity<User>().HasMany(u => u.Chats).WithMany(ch => ch.Members);
            modelBuilder.Entity<Chat>().HasMany(ch => ch.Messages).WithOne(m => m.Chat);

            modelBuilder.Entity<Message>().UseTphMappingStrategy();
        }
    }
}