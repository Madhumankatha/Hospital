using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital;

namespace Hospital.Controllers
{
    public class PrescriptionsController : Controller
    {
        private db_hospitalEntities db = new db_hospitalEntities();

        // GET: Prescriptions
        public ActionResult Index()
        {
            var prescriptions = db.prescriptions.Include(p => p.doctor).Include(p => p.patient);
            return View(prescriptions.ToList());
        }

        // GET: Prescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prescription prescription = db.prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // GET: Prescriptions/Create
        public ActionResult Create()
        {
            ViewBag.doc_id = new SelectList(db.doctors, "id", "dname");
            ViewBag.pat_id = new SelectList(db.patients, "id", "pname");
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,doc_id,pat_id,prescription1")] prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.prescriptions.Add(prescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.doc_id = new SelectList(db.doctors, "id", "dname", prescription.doc_id);
            ViewBag.pat_id = new SelectList(db.patients, "id", "pname", prescription.pat_id);
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prescription prescription = db.prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.doc_id = new SelectList(db.doctors, "id", "dname", prescription.doc_id);
            ViewBag.pat_id = new SelectList(db.patients, "id", "pname", prescription.pat_id);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,doc_id,pat_id,prescription1")] prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.doc_id = new SelectList(db.doctors, "id", "dname", prescription.doc_id);
            ViewBag.pat_id = new SelectList(db.patients, "id", "pname", prescription.pat_id);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prescription prescription = db.prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            prescription prescription = db.prescriptions.Find(id);
            db.prescriptions.Remove(prescription);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
