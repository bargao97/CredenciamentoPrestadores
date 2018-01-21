using Newtonsoft.Json;
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
    public class METASController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: METAS
        public ActionResult Index()
        {
            var mETAS = db.METAS.Include(m => m.CONTRATO).Include(m => m.ESPECIALIDADE_EXAMES);
            return View(mETAS.ToList());
        }

        // GET: METAS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            METAS mETAS = db.METAS.Find(id);
            if (mETAS == null)
            {
                return HttpNotFound();
            }
            return View(mETAS);
        }

        // GET: METAS/Create
        public ActionResult Create()
        {
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE");
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES, "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE");
            return View();
        }

        // POST: METAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_META,META,IDESP_EXAM,IDCONTRATO,NOME_ESPECIALIDADE,CONCLUIDO")] METAS mETAS)
        {
            if (ModelState.IsValid)
            {
                string nome_especialidade = db.ESPECIALIDADE_EXAMES.Where(s => s.ID_ESPECIALIDADE == mETAS.IDESP_EXAM).Select(s => s.NOME_ESPECIALIDADE).FirstOrDefault();
                mETAS.CONCLUIDO = 0;
                mETAS.NOME_ESPECIALIDADE = nome_especialidade;
                db.METAS.Add(mETAS);
                db.SaveChanges();
                return RedirectToAction("Details","CONTRATOS", new { id = mETAS.IDCONTRATO});
            }

            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE", mETAS.IDCONTRATO);
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES, "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE", mETAS.IDESP_EXAM);
            return View(mETAS);
        }

        // GET: METAS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            METAS mETAS = db.METAS.Find(id);
            if (mETAS == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE", mETAS.IDCONTRATO);
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES, "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE", mETAS.IDESP_EXAM);
            return View(mETAS);
        }

        // POST: METAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_META,META,IDESP_EXAM,IDCONTRATO,NOME_ESPECIALIDADE,CONCLUIDO")] METAS mETAS)
        {
            if (ModelState.IsValid)
            {
                string nome_especialidade = db.ESPECIALIDADE_EXAMES.Where(s => s.ID_ESPECIALIDADE == mETAS.IDESP_EXAM).Select(s => s.NOME_ESPECIALIDADE).FirstOrDefault();
                mETAS.NOME_ESPECIALIDADE = nome_especialidade;
                db.Entry(mETAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE", mETAS.IDCONTRATO);
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES, "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE", mETAS.IDESP_EXAM);
            return View(mETAS);
        }

        // GET: METAS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            METAS mETAS = db.METAS.Find(id);
            if (mETAS == null)
            {
                return HttpNotFound();
            }
            return View(mETAS);
        }

        // POST: METAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            METAS mETAS = db.METAS.Find(id);
            db.METAS.Remove(mETAS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string ProposCredenciamentosEsp(int IDCONTRATO)
        {
            var metasrestantes = db.METAS.Where(S => S.IDCONTRATO == IDCONTRATO)
                .Where(S => S.CONCLUIDO < S.META)
                .OrderBy(s => s.NOME_ESPECIALIDADE)
                .ToList();
            var metasrestantesJson =
               JsonConvert.SerializeObject(
                   metasrestantes.Select(
                       s => new
                       {
                           NOME_ESPECIALIDADE = s.NOME_ESPECIALIDADE,
                           ID_ESPECIALIDADE = s.IDESP_EXAM
                       }
                   )
               );
            return metasrestantesJson;

        }
        public string MetasPorContrato(int IDCONTRATO,int TIPOMETA)
        {
            string metaJSON ="";
            if (TIPOMETA == 1)
            {
                var meta=db.METAS.Where(s=>s.IDCONTRATO == IDCONTRATO).Where(s => s.CONCLUIDO < s.META).ToList();
                metaJSON = JsonConvert.SerializeObject(
               meta.Select(s => new {
                   IDCONTRATO = s.IDCONTRATO,
                   IDESPECIALIDADE = s.IDESP_EXAM,
                   NOME_ESPECIALIDADE = s.NOME_ESPECIALIDADE,
                   META = s.META,
                   CONCLUIDO = s.CONCLUIDO,
               }));
            }
            else if(TIPOMETA == 2)
            {
              var meta=db.METAS.Where(s => s.IDCONTRATO == IDCONTRATO).Where(s => s.CONCLUIDO >= s.META).ToList();
                metaJSON = JsonConvert.SerializeObject(
               meta.Select(s => new {
                   IDCONTRATO = s.IDCONTRATO,
                   IDESPECIALIDADE = s.IDESP_EXAM,
                   NOME_ESPECIALIDADE = s.NOME_ESPECIALIDADE,
                   META = s.META,
                   CONCLUIDO = s.CONCLUIDO
               }));
            }  
            return metaJSON;
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
