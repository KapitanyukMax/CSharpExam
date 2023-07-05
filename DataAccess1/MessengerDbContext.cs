using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;
using DataAccess.Helpers;

namespace DataAccess
{
    public class MessengerDbContext : DbContext
    {
        public MessengerDbContext()
        {
            //this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserChat> UsersChats { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<TextMessage> TextMessages { get; set; }

        public DbSet<FileMessage> FileMessages { get; set; }

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
                                          MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(c => c.Id);
            modelBuilder.Entity<User>().HasAlternateKey(c => c.Login);
            modelBuilder.Entity<User>().Property(c => c.Id)
                                       .ValueGeneratedOnAdd()
                                       .UseIdentityColumn();
            modelBuilder.Entity<User>().Property(c => c.Login).IsRequired();
            modelBuilder.Entity<User>().Property(c => c.Password).IsRequired();
            modelBuilder.Entity<User>().Property(c => c.Name).IsRequired();

            modelBuilder.Entity<UserChat>().HasKey(uch => new { uch.UserId, uch.ChatId });

            modelBuilder.Entity<Chat>().HasKey(ch => ch.Id);
            modelBuilder.Entity<Chat>().Property(ch => ch.Id)
                                       .ValueGeneratedOnAdd()
                                       .UseIdentityColumn();
            modelBuilder.Entity<Chat>().Property(ch => ch.Name).IsRequired();

            modelBuilder.Entity<Message>().HasKey(m => m.Id);
            modelBuilder.Entity<Message>().Property(m => m.Id)
                                          .ValueGeneratedOnAdd()
                                          .UseIdentityColumn();
            modelBuilder.Entity<Message>().Property(m => m.SendingTime).IsRequired();
            modelBuilder.Entity<Message>().Property(m => m.Command).IsRequired();

            modelBuilder.Entity<User>().HasMany(u => u.UsersChats)
                                       .WithOne(uch => uch.User)
                                       .HasForeignKey(uch => uch.UserId);
            modelBuilder.Entity<UserChat>().HasOne(uch => uch.Chat)
                                           .WithMany(ch => ch.UsersChats)
                                           .HasForeignKey(uch => uch.ChatId);
            modelBuilder.Entity<Chat>().HasMany(ch => ch.Messages)
                                       .WithOne(m => m.Chat)
                                       .HasForeignKey(m => m.ChatId);
            modelBuilder.Entity<Message>().HasOne(m => m.Sender)
                                          .WithMany(s => s.Messages)
                                          .HasForeignKey(m => m.SenderId);

            modelBuilder.Entity<Message>().UseTphMappingStrategy();

            modelBuilder.SeedUsers();
            modelBuilder.SeedChats();
            modelBuilder.SeedUsersChats();
            modelBuilder.SeedTextMessages();
            modelBuilder.SeedFileMessages();
        }
    }
}