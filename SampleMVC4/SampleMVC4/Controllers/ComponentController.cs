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
    public class ComponentController : Controller
    {
        private CDISCModelContext db = new CDISCModelContext();

        //
        // GET: /Component/

        public ActionResult Index()
        {
            return View(db.Components.ToList());
        }

        //
        // GET: /Component/Details/5

        public ActionResult Details(int id = 0)
        {
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        //
        // GET: /Component/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Component/Create

        [HttpPost]
        public ActionResult Create(Component component)
        {
            if (ModelState.IsValid)
            {
                db.Components.Add(component);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(component);
        }

        //
        // GET: /Component/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        //
        // POST: /Component/Edit/5

        [HttpPost]
        public ActionResult Edit(Component component)
        {
            if (ModelState.IsValid)
            {
                db.Entry(component).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(component);
        }

        //
        // GET: /Component/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        //
        // POST: /Component/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Component component = db.Components.Find(id);
            db.Components.Remove(component);
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