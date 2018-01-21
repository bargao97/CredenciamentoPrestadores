using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ESPECIALIDADE_EXAMESController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ESPECIALIDADE_EXAMES
        public ActionResult Index()
        {
            return View(db.ESPECIALIDADE_EXAMES.ToList());
        }

        // GET: ESPECIALIDADE_EXAMES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESPECIALIDADE_EXAMES eSPECIALIDADE_EXAMES = db.ESPECIALIDADE_EXAMES.Find(id);
            if (eSPECIALIDADE_EXAMES == null)
            {
                return HttpNotFound();
            }
            return View(eSPECIALIDADE_EXAMES);
        }

        // GET: ESPECIALIDADE_EXAMES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ESPECIALIDADE_EXAMES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ESPECIALIDADE,NOME_ESPECIALIDADE,TIPO_EXAM_ESP")] ESPECIALIDADE_EXAMES eSPECIALIDADE_EXAMES)
        {
            if (ModelState.IsValid)
            {
                db.ESPECIALIDADE_EXAMES.Add(eSPECIALIDADE_EXAMES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eSPECIALIDADE_EXAMES);
        }

        // GET: ESPECIALIDADE_EXAMES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESPECIALIDADE_EXAMES eSPECIALIDADE_EXAMES = db.ESPECIALIDADE_EXAMES.Find(id);
            if (eSPECIALIDADE_EXAMES == null)
            {
                return HttpNotFound();
            }
            return View(eSPECIALIDADE_EXAMES);
        }

        // POST: ESPECIALIDADE_EXAMES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ESPECIALIDADE,NOME_ESPECIALIDADE,TIPO_EXAM_ESP")] ESPECIALIDADE_EXAMES eSPECIALIDADE_EXAMES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eSPECIALIDADE_EXAMES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eSPECIALIDADE_EXAMES);
        }

        // GET: ESPECIALIDADE_EXAMES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESPECIALIDADE_EXAMES eSPECIALIDADE_EXAMES = db.ESPECIALIDADE_EXAMES.Find(id);
            if (eSPECIALIDADE_EXAMES == null)
            {
                return HttpNotFound();
            }
            return View(eSPECIALIDADE_EXAMES);
        }

        // POST: ESPECIALIDADE_EXAMES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ESPECIALIDADE_EXAMES eSPECIALIDADE_EXAMES = db.ESPECIALIDADE_EXAMES.Find(id);
            db.ESPECIALIDADE_EXAMES.Remove(eSPECIALIDADE_EXAMES);
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
