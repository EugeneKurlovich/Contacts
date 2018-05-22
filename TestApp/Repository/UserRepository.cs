using Contact_List.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact_List.Repository
{
    public class UserRepository :IRepository<User>
    {
        private ContactsContext db;

        public UserRepository(ContactsContext context)
        {
            this.db = context;
        }

        public bool checkExistUser(string log, string pass)
        {
            foreach(User user in db.Users)
            {
                if (user.login.Equals(log) && user.password.Equals(pass))
                    return true;
            }
            return false;
        }

        public User getUserById(int id)
        {
            User user = db.Users.FirstOrDefault(u => u.Id.Equals(id));
            return user;
        }

        public User getUserIdByLogin(string log)
        {
            User user = db.Users.FirstOrDefault(u => u.login.Equals(log));
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.Include(i => i.Contacts);
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }
    }
}
