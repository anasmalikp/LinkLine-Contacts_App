using Contacts_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts_App.ContextFolder
{
    public class ContextClass : DbContext
    {
        public ContextClass(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Users> usersTable { get; set; }
        public DbSet<Contacts> contactsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacts>().HasOne(x => x.user).WithMany(x => x.contacts).HasForeignKey(x => x.createdBy);
            base.OnModelCreating(modelBuilder);
        }
    }
}
