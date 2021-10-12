using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMessage.Models;

namespace TheMessage.Data
{
    public class ChatDbContext:IdentityDbContext<ApplicationUser>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

           
            builder.Entity<Media>()
                .HasKey(m => m.MediaId);

            builder.Entity<Media>()
                .HasOne(m => m.Message)
                .WithMany(m => m.Medias)
                .HasForeignKey(m => m.MessageId);


            builder.Entity<Message>()
               .HasKey(m => m.MessageId);

            builder.Entity<Message>()
               .HasOne(m => m.User)
               .WithMany(m => m.Messages)
               .HasForeignKey(m => m.UserId);

            base.OnModelCreating(builder);
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
