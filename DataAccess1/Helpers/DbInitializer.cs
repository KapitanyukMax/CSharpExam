using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Helpers
{
    public static class DbInitializer
    {
public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User
                {
                    Id = 1,
                    IPAddress = "192.168.1.107",
                    Login = "Max111",
                    Password = "12345678",
                    Name = "Max",
                    PhoneNumber = "+38(068)-762-92-33",
                    MailAddress = "maxik20192006max@gmail.com"
                },
                new User
                {
                    Id = 2,
                    IPAddress = "192.168.1.107",
                    Login = "Max111",
                    Password = "12345678",
                    Name = "Max",
                    PhoneNumber = "+38(068)-762-92-33",
                    MailAddress = "maxik20192006max@gmail.com"
                },
                new User
                {
                    Id = 3,
                    IPAddress = "192.168.1.107",
                    Login = "Max111",
                    Password = "12345678",
                    Name = "Max",
                    PhoneNumber = "+38(068)-762-92-33",
                    MailAddress = "maxik20192006max@gmail.com"
                }
            });
        }

        public static void SeedChats(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>().HasData(new Chat[]
            {
                new Chat
                {
                    Id = 1,
                    Name = "UnitedWork"
                },
                new Chat
                {
                    Id = 2
                },
                new Chat
                {
                    Id = 3
                },
                new Chat
                {
                    Id = 4
                }
            });
        }

        public static void SeedUsersChats(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserChat>().HasData(new UserChat[]
            {
                new UserChat
                {
                    UserId = 1,
                    ChatId = 1
                },
                new UserChat
                {
                    UserId = 1,
                    ChatId = 2
                },
                new UserChat
                {
                    UserId = 1,
                    ChatId = 4
                },
                new UserChat
                {
                    UserId = 2,
                    ChatId = 1
                },
                new UserChat
                {
                    UserId = 2,
                    ChatId = 2
                },
                new UserChat
                {
                    UserId = 2,
                    ChatId = 3
                },
                new UserChat
                {
                    UserId = 3,
                    ChatId = 1
                },
                new UserChat
                {
                    UserId = 3,
                    ChatId = 3
                },
                new UserChat
                {
                    UserId = 3,
                    ChatId = 4
                }
            });
        }

        public static void SeedTextMessages(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TextMessage>().HasData(new TextMessage[]
            {
                new TextMessage
                {
                    Id = 1,
                    SendingTime = new DateTime(2023, 6, 22, 12, 0, 0),
                    ChatId = 1,
                    SenderId = 2,
                    Text = "Hello! How are you?"
                },
                new TextMessage
                {
                    Id = 2,
                    SendingTime = new DateTime(2023, 6, 22, 12, 1, 0),
                    ChatId = 1,
                    SenderId = 1,
                    Text = "I'm fine"
                },
                new TextMessage
                {
                    Id = 3,
                    SendingTime = new DateTime(2023, 6, 22, 12, 5, 0),
                    ChatId = 1,
                    SenderId = 3,
                    Text = "Pretty good) And you?"
                },
                new TextMessage
                {
                    Id = 4,
                    SendingTime = new DateTime(2023, 6, 22, 12, 5, 30),
                    ChatId = 1,
                    SenderId = 2,
                    Text = "Not bad. Let's write some code"
                },
                new TextMessage
                {
                    Id = 5,
                    SendingTime = new DateTime(2023, 6, 20, 18, 0, 0),
                    ChatId = 1,
                    SenderId = 2,
                    Text = "Hi! I found an interesting theme for our course work. It's messenger, what do you think?"
                },
                new TextMessage
                {
                    Id = 7,
                    SendingTime = new DateTime(2023, 6, 20, 18, 2, 0),
                    ChatId = 2,
                    SenderId = 3,
                    Text = "Ok, it's good, let's choose it"
                }
            });
        }

        public static void SeedFileMessages(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileMessage>().HasData(new FileMessage[]
            {
                new FileMessage
                {
                    Id = 6,
                    SendingTime = new DateTime(2023, 6, 20, 18, 0, 30),
                    ChatId = 2,
                    SenderId = 3,
                    Url = @"E:\Max\Coding\CW_dot_net.pdf",
                    Caption = "Here is the file with themes"
                }
            });
        }
    }
}