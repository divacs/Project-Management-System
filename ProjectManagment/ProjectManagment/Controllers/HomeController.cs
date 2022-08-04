using Data;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;

namespace ProjectManagment.Controllers
{
    public partial class HomeController : Controller
    {
        // if this user has passed authentication, it calls the (1) method in the controller (2)
        // if it didn't pass, it returns a regular view
        public virtual ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Home");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public virtual ActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                // proverava da li postoji takav juzer sa tim imenom i pasvordom u bazi
                var user = DALUsers.CheckUser(model.UserName, model.Password);
                if (user != null)
                {
                    //  FormsAuthentication.SetAuthCookie
                    //  sets a browser cookie to initiate the user's session
                    //  it's what keeps the user logged in each time a page is posted to the server
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Home", "Home");
                }
            }
            ModelState.AddModelError("Wrong", "Wrong password or username");
            return View();
        }
        public virtual ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
        public virtual ActionResult Home()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Home");
        }

    }
}