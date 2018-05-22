using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Contact_List.Models;
using Contact_List.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Contact_List.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork uow;
        public HomeController(ContactsContext db)
        {
            uow = new UnitOfWork(db);
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult nameAsc()
        {
            return View("UserContacts", uow.Contacts.getContactsByUserId(uow.Users.getUserIdByLogin(User.Identity.Name).Id).OrderBy(c => c.contactName));
        }
        public IActionResult nameDsc()
        {
            return View("UserContacts", uow.Contacts.getContactsByUserId(uow.Users.getUserIdByLogin(User.Identity.Name).Id).OrderByDescending(c => c.contactName));
        }

        public IActionResult surnameAsc()
        {
            return View("UserContacts", uow.Contacts.getContactsByUserId(uow.Users.getUserIdByLogin(User.Identity.Name).Id).OrderBy(c => c.contactSurname));
        }
        public IActionResult surnameDsc()
        {
            return View("UserContacts", uow.Contacts.getContactsByUserId(uow.Users.getUserIdByLogin(User.Identity.Name).Id).OrderByDescending(c => c.contactSurname));
        }

        public IActionResult emailAsc()
        {
            return View("UserContacts", uow.Contacts.getContactsByUserId(uow.Users.getUserIdByLogin(User.Identity.Name).Id).OrderBy(c => c.contactEmail));
        }
        public IActionResult emailDsc()
        {
            return View("UserContacts", uow.Contacts.getContactsByUserId(uow.Users.getUserIdByLogin(User.Identity.Name).Id).OrderByDescending(c => c.contactEmail));
        }

        private async Task Authenticate(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult getEditPage(int id)
        {
            return View("EditContact",uow.Contacts.Get(id));
        }

        public IActionResult getCreatePage()
        {
            return View("CreateContact");
        }

        public IActionResult Profile()
        {
            User user = uow.Users.getUserById(uow.Users.getUserIdByLogin(User.Identity.Name).Id);
            return View("Profile", user);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string password, string login)
        {
            if (uow.Users.checkExistUser(login, password))
            {
               await Authenticate(login);
                return RedirectToAction("UserContacts", "Contacts");
            }
               

            return View("Index");
        }

        public IActionResult Registration()
        {
            return View("Registration");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
