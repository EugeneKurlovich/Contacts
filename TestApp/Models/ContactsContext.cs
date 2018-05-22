using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact_List.Models
{
    public class ContactsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public ContactsContext(DbContextOptions<ContactsContext> options)
            : base(options)
        {
        }
        public ContactsContext()
        {

        }
    }
}
