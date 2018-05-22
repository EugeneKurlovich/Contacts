using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact_List.Models;
using Contact_List.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Contact_List.Controllers
{
    [Route("api/v1/[controller]")]
    public class ContactsController : Controller
    {
        UnitOfWork uow;
        public ContactsController(ContactsContext db)
        {
            uow = new UnitOfWork(db);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserContacts()
        {
            return View("UserContacts",uow.Contacts.getContactsByUserId(uow.Users.getUserIdByLogin(User.Identity.Name).Id));
        }

        [HttpPost]
        public IActionResult createContact([FromForm] Contact contact)
        {
            contact.UserId = uow.Users.getUserIdByLogin(User.Identity.Name).Id;
            uow.Contacts.Create(contact);
            uow.Users.getUserIdByLogin(User.Identity.Name).ammountContacts++;
            uow.Save();
            return View("UserContacts", uow.Contacts.getContactsByUserId(uow.Users.getUserIdByLogin(User.Identity.Name).Id));
        }

        [HttpPut]
        public void editContact([FromBody] Contact contact)
        {
            Contact updatedContact = uow.Contacts.Get(contact.Id);
            updatedContact.setInformation(contact);
            uow.Contacts.Update(updatedContact);
            uow.Save();
        }

        [HttpDelete]
        public IActionResult deleteContact([FromBody] Contact c)
        {
            uow.Contacts.Delete(c.Id);
            uow.Users.getUserIdByLogin(User.Identity.Name).ammountContacts--;
            uow.Save();
            return PartialView("UserContactsList", uow.Contacts.getContactsByUserId(uow.Users.getUserIdByLogin(User.Identity.Name).Id));
        }
    }
}