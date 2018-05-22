using Contact_List.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact_List.Repository
{
    public class UnitOfWork : IDisposable
    {
        public ContactsContext db = new ContactsContext();
        private UserRepository userRepository;
        private ContactRepository contactRepository;

        public UnitOfWork(ContactsContext context)
        {
            db = context;
        }

        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public ContactRepository Contacts
        {
            get
            {
                if (contactRepository == null)
                    contactRepository = new ContactRepository(db);
                return contactRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
