using AuthenticationAndAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    public class RegistrationsController : Controller
    {
        // GET: Registrations
        public ActionResult UserRegistration()
        {
            UserModelManager userModelManager = new UserModelManager();
            List<UserLoginModel> ListData = userModelManager.GetUserRegistration();

            return View(ListData);
        }
        public ActionResult Registration()
        {
            UserLoginModel userLoginModel = new UserLoginModel();
            //if (x == "Insert")
            //{

            //}
            return View("UserRegistrationDetails", userLoginModel);
        }
        [HttpPost]
        public ActionResult Registration(UserLoginModel userLoginModel)
        {
            UserModelManager userModelManager = new UserModelManager();
            userModelManager.InsertUserDetails(userLoginModel);
            RedirectToRouteResult rr = RedirectToAction("UserRegistration", "Registrations");
            ActionResult ar = rr;
            return ar;
        }

    }
}