using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleMVC4.Models;

namespace SampleMVC4.Controllers
{
    public class StudyController : Controller
    {
        private CDISCModelContext db = new CDISCModelContext();

        //
        // GET: /Study/

        public ActionResult Index()
        {
            return View(db.Studies.ToList());
        }

        //
        // GET: /Study/Details/5

        public ActionResult Details(int id = 0)
        {
            Study study = db.Studies.Find(id);
            if (study == null)
            {
                return HttpNotFound();
            }
            return View(study);
        }

        //
        // GET: /Study/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Study/Create

        [HttpPost]
        public ActionResult Create(Study study)
        {
            if (ModelState.IsValid)
            {
                db.Studies.Add(study);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(study);
        }

        //
        // GET: /Study/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Study study = db.Studies.Find(id);
            if (study == null)
            {
                return HttpNotFound();
            }
            return View(study);
        }

        //
        // POST: /Study/Edit/5

        [HttpPost]
        public ActionResult Edit(Study study)
        {
            if (ModelState.IsValid)
            {
                db.Entry(study).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(study);
        }

        //
        // GET: /Study/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Study study = db.Studies.Find(id);
            if (study == null)
            {
                return HttpNotFound();
            }
            return View(study);
        }

        //
        // POST: /Study/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Study study = db.Studies.Find(id);
            db.Studies.Remove(study);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}