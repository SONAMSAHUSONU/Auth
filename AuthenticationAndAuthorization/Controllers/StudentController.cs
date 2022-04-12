using AuthenticationAndAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuthenticationAndAuthorization.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student

        public ActionResult Login()
        {
            UserLoginModel userLoginModel = new UserLoginModel();
            return View(userLoginModel);
        }
        [HttpPost]
        public ActionResult Login(UserLoginModel userLoginModel)
        {
            UserModelManager userModelManager = new UserModelManager();
            userLoginModel = userModelManager.UserAuth(userLoginModel);
            if (userLoginModel.IsValid == 1)
            {
                Session["UserEmail"] = userLoginModel.UserEmail;
                FormsAuthentication.SetAuthCookie(userLoginModel.UserName, false);
                var authTicket = new FormsAuthenticationTicket(1, userLoginModel.UserEmail, DateTime.Now, DateTime.Now.AddMinutes(20), false, userLoginModel.Role);
                string encriptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encriptedTicket);
                HttpContext.Response.Cookies.Add(authcookie);
                return RedirectToAction("Index", "Home", userLoginModel);
            }
            else
            {
                userLoginModel.LoginErrorMessage = "Wrong UserName or Password";
                return View("Login",userLoginModel);
            }
        }

    }
}