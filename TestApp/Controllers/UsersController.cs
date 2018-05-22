using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact_List.Models;
using Contact_List.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Contact_List.Controllers
{
    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {
        UnitOfWork uow;
        public UsersController(ContactsContext db)
        {
            uow = new UnitOfWork(db);
        }

        [HttpDelete]
        public IActionResult deleteAccount([FromBody]User user)
        {
            uow.Users.Delete(user.Id);
            uow.Save();         
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return uow.Users.GetAll().ToList();
        }

        [HttpPost]
        public IActionResult SignUp([FromForm]User user)
        {
            uow.Users.Create(user);
            uow.Save();
            return RedirectToAction("Index", "Home");
        }

        [HttpPut]
        public IActionResult EditUser([FromBody] User user)
        {
            User updatedUser = uow.Users.getUserById(user.Id);
            updatedUser.firstName = user.firstName;
            updatedUser.lastName = user.lastName;
            updatedUser.password = user.password;
            uow.Users.Update(updatedUser);
            uow.Save();
            return RedirectToAction("Profile", "Home");
        }   
    }
}