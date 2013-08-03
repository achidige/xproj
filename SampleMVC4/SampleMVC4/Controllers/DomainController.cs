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
    public class DomainController : Controller
    {
        private CDISCModelContext db = new CDISCModelContext();

        //
        // GET: /Domain/

        public ActionResult Index()
        {
            return View(db.Domains.ToList());
        }

        //
        // GET: /Domain/Details/5

        public ActionResult Details(int id = 0)
        {
            Domain domain = db.Domains.Find(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        //
        // GET: /Domain/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Domain/Create

        [HttpPost]
        public ActionResult Create(Domain domain)
        {
            if (ModelState.IsValid)
            {
                db.Domains.Add(domain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(domain);
        }

        //
        // GET: /Domain/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Domain domain = db.Domains.Find(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        //
        // POST: /Domain/Edit/5

        [HttpPost]
        public ActionResult Edit(Domain domain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(domain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(domain);
        }

        //
        // GET: /Domain/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Domain domain = db.Domains.Find(id);
            if (domain == null)
            {
                return HttpNotFound();
            }
            return View(domain);
        }

        //
        // POST: /Domain/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Domain domain = db.Domains.Find(id);
            db.Domains.Remove(domain);
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