using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hospital.Controllers
{
    public class LoginController : Controller
    {
        private db_hospitalEntities db = new db_hospitalEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(patient patient)
        {
            bool result =  db.patients.Any(p => p.email.Equals(patient.email) && p.password.Equals(patient.password));

            if (result)
            {
                patient patient1 = db.patients.Single(p => p.email.Equals(patient.email) && p.password.Equals(patient.password));
                Session["email"] = patient1.email;
                Session["pat_id"] = patient1.id;

                FormsAuthentication.SetAuthCookie(patient1.email, false);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Message = "Invalid username and password";
            }

            return View();
        }



        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(patient patient)
        {
            patient.role = "Patient";
            db.patients.Add(patient);
            db.SaveChanges();
            ViewBag.Message = "Account Created successfully!!";
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["email"] = null;
            Session["pat_id"] = null;
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.    
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.    
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Index", "Home");
        }

    }
}