using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVCDemo.Models;
using System.Web.Security;
using MVCDemo.DAL;
using System.Web.SessionState;

namespace MVCDemo.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db = new UserContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(MVCDemo.Models.User model)
        {
            User user = DataAccess.IsValid(model.UserName, model.Password);
            if (user.UserName != null)
            {
                //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                FormsAuthentication.SetAuthCookie(model.UserName, false);

                Session["UserName"] = user.UserName;
                Session["UserEmail"] = user.Email;
                return RedirectToAction("Home", "Home");
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserName"] = null;
            Session["UserEmail"] = null;
            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(MVCDemo.Models.User model)
        {
            if (ModelState.IsValid)
            {
                if (DataAccess.CreateUser(model) == 1)
                {
                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    return View("Error");
                }

            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return View(model);
            }
        }

    }

}

