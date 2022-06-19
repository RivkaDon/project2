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

        public WebAPIContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<Contact>().HasKey(e => new { e.UserId, e.ContactId }).HasName("PK_Contact");
            modelBuilder.Entity<Chat>().HasKey(e => e.Id);
            //modelBuilder.Entity<User>().HasMany<Contact>(e => e.Contacts).WithOne(e => e.User).HasForeignKey(e => e.ContactId);
            //modelBuilder.Entity<Contact>().HasKey(e => e.ContactId);
            //modelBuilder.Entity<Contact>().HasOne(e => e.User).WithMany(e => e.Contacts).HasForeignKey(e => e.ContactId);
        }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<ChatList> ChatList { get; set; }

        public DbSet<Contact> Contact { get; set; }

        //public DbSet<ContactList> ContactList { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<MessageList> MessageList { get; set; }

        public DbSet<User> Users { get; set; }
        //public DbSet<UserList> UserList { get; set; }
    }
}
