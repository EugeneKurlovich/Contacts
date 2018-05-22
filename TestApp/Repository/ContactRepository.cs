using Contact_List.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact_List.Repository
{
    public class ContactRepository : IRepository<Contact>
    {
        private ContactsContext db;

        public ContactRepository(ContactsContext context)
        {
            this.db = context;
        }

        public IEnumerable<Contact> GetAll()
        {
            return db.Contacts.Include(i => i.User.login);
        }

        public Contact Get(int id)
        {
            return db.Contacts.Find(id);
        }

        public void Create(Contact contact)
        {
            db.Contacts.Add(contact);
        }

        public IEnumerable<Contact> getContactsByUserId(int id)
        {
            var contacts = ( from c in db.Contacts where c.UserId.Equals(id) select c).ToList();
            return contacts;
        }

        public void Update(Contact contact)
        {
            db.Entry(contact).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact != null)
                db.Contacts.Remove(contact);
        }
    }
}
