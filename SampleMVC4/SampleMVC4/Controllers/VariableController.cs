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
    public class VariableController : Controller
    {
        private CDISCModelContext db = new CDISCModelContext();

        //
        // GET: /Variable/

        public ActionResult Index()
        {
            return View(db.Variables.ToList());
        }

        //
        // GET: /Variable/Details/5

        public ActionResult Details(int id = 0)
        {
            Variable variable = db.Variables.Find(id);
            if (variable == null)
            {
                return HttpNotFound();
            }
            return View(variable);
        }

        //
        // GET: /Variable/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Variable/Create

        [HttpPost]
        public ActionResult Create(Variable variable)
        {
            if (ModelState.IsValid)
            {
                db.Variables.Add(variable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(variable);
        }

        //
        // GET: /Variable/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Variable variable = db.Variables.Find(id);
            if (variable == null)
            {
                return HttpNotFound();
            }
            return View(variable);
        }

        //
        // POST: /Variable/Edit/5

        [HttpPost]
        public ActionResult Edit(Variable variable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(variable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(variable);
        }

        //
        // GET: /Variable/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Variable variable = db.Variables.Find(id);
            if (variable == null)
            {
                return HttpNotFound();
            }
            return View(variable);
        }

        //
        // POST: /Variable/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Variable variable = db.Variables.Find(id);
            db.Variables.Remove(variable);
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