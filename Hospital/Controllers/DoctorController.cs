using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private db_hospitalEntities db = new db_hospitalEntities();
        // GET: Doctor
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(doctor doctor)
        {
            bool result = db.doctors.Any(d => d.email.Equals(doctor.email) && d.password.Equals(doctor.password));

            if (result)
            {
                return RedirectToAction("Index", "Patients");
            }
            else
            {
                ViewBag.Message = "Invalid login!!";
            }

            return View();
        }

    }

}