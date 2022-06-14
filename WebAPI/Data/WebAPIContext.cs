#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class WebAPIContext: DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=Users;user=root;password=maria";
        public WebAPIContext(DbContextOptions<WebAPIContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
            optionsBuilder.EnableSensitiveDataLogging(true);

        }

        public DbSet<Chat> Chat { get; set; }

        public DbSet<ChatList> ChatList { get; set; }

        public DbSet<Contact> Contact { get; set; }

        public DbSet<ContactList> ContactList { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<MessageList> MessageList { get; set; }

        public DbSet<User> User { get; set; }
    }
}
