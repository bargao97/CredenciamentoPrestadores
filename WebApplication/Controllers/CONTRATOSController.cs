using Newtonsoft.Json;
using System;
using System.Collections;
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
    public class CONTRATOSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CONTRATOS
        public ActionResult Index()
        {
            return View(db.CONTRATOS.ToList());
        }

        // GET: CONTRATOS/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.metas = db.METAS.Where(s => s.IDCONTRATO == id).OrderByDescending(s=>s.CONCLUIDO).ToList();
            var metasProspeccao = db.METAS.Where(s => s.IDCONTRATO == id).OrderBy(s => s.IDESP_EXAM).ToList();
            
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES, "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATO cONTRATO = db.CONTRATOS.Find(id);
            if (cONTRATO == null)
            {
                return HttpNotFound();
            }
            return View(cONTRATO);
        }

        // GET: CONTRATOS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CONTRATOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CONTRATO,NOME_CLIENTE,META")] CONTRATO cONTRATO)
        {
            if (ModelState.IsValid)
            {
                db.CONTRATOS.Add(cONTRATO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cONTRATO);
        }

        // GET: CONTRATOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATO cONTRATO = db.CONTRATOS.Find(id);
            if (cONTRATO == null)
            {
                return HttpNotFound();
            }
            return View(cONTRATO);
        }

        // POST: CONTRATOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CONTRATO,NOME_CLIENTE,META")] CONTRATO cONTRATO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTRATO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cONTRATO);
        }

        // GET: CONTRATOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATO cONTRATO = db.CONTRATOS.Find(id);
            if (cONTRATO == null)
            {
                return HttpNotFound();
            }
            return View(cONTRATO);
        }

        // POST: CONTRATOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONTRATO cONTRATO = db.CONTRATOS.Find(id);
            db.CONTRATOS.Remove(cONTRATO);
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
        public string AguradandoContratoPrestadores(int idContrato, int idespecialidade)
        {
            var colaboradores = db.CREDENCIAMENTOS.Where(s => s.IDCONTRATO == idContrato)
                .Where(s => s.IDESP_EXAM == idespecialidade)
                .Where(s => s.STATUS == STATUS_CREDENCIAMENTOS.CONTRATO_ELETRONICO_RECEBIDO)
                .Select(s => new
                {
                    s.COLABORADORES.NOME_FANTASIA,
                    s.NOME_ESPECIA,
                    s.IDCOLABORADOR,
                    s.OBSERVACAO
                })
                .ToList();
            var PrestadoresSelecionados =
            JsonConvert.SerializeObject(
                colaboradores.Select(
                    s => new
                    {
                        Prestador = s.NOME_FANTASIA,
                        Especialidade = s.NOME_ESPECIA,
                        Observacao = s.OBSERVACAO,
                        IDPrestador = s.IDCOLABORADOR
                    }
                )
            );
            return PrestadoresSelecionados;
        }
    }
}
