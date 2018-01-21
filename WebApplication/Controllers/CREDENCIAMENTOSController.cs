using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Models;

namespace WebApplication.Content
{
    public class CREDENCIAMENTOSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CREDENCIAMENTOS
        public ActionResult Index()
        {
            var CREDENCIAMENTOS = db.CREDENCIAMENTOS.Include(c => c.COLABORADORES).Include(c => c.CONTRATO).Include(c => c.ESPECIALIDADE_EXAMES);
            return View(CREDENCIAMENTOS.ToList());
        }

        // GET: CREDENCIAMENTOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CREDENCIAMENTOS CREDENCIAMENTOS = db.CREDENCIAMENTOS.Find(id);
            if (CREDENCIAMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(CREDENCIAMENTOS);
        }

        // GET: CREDENCIAMENTOS/Create
        public ActionResult Create()
        {
            
            ViewBag.IDCOLABORADOR = new SelectList(db.COLABORADORES, "ID_COLABORADOR", "NOME_FANTASIA");
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE");
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES, "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE");
            return View();
        }

        // POST: CREDENCIAMENTOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CREDENCIAMENTOS,STATUS,OBSERVACAO,IDCOLABORADOR,IDESP_EXAM,IDCONTRATO,FUNCIONARIO,DATA_CADASTRO,PREVISAO,NOME_ESPECIA,BAIRRO,VLR_PRESTADOR,VLR_100PLANO")] CREDENCIAMENTOS CREDENCIAMENTOS,int id_colaborador)
        {
            if (ModelState.IsValid)
            {
                var colaborador = db.COLABORADORES.Where(s => s.ID_COLABORADOR == id_colaborador).Select(S => S).FirstOrDefault();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = manager.FindById(User.Identity.GetUserId());
                if (colaborador.CEP != null)
                {
                    string primeirasLetrasCep = colaborador.CEP.Substring(0, 5);
                    int cepint = Convert.ToInt32(primeirasLetrasCep);
                    if (cepint >= 01000 && cepint <= 01599)
                    {
                        colaborador.REGIAO = "CENTRO";
                    }
                    else if (cepint >= 02000 && cepint <= 02999)
                    {
                        colaborador.REGIAO = "ZONA NORTE";
                    }
                    else if (cepint >= 03000 && cepint <= 03999)
                    {
                        colaborador.REGIAO = "ZONA LESTE";
                    }
                    else if (cepint >= 08000 && cepint <= 08499)
                    {
                        colaborador.REGIAO = "ZONA LESTE";
                    }
                    else if (cepint >= 04000 && cepint <= 04999)
                    {
                        colaborador.REGIAO = "ZONA SUL";
                    }
                    else if (cepint >= 05000 && cepint <= 05899)
                    {
                        colaborador.REGIAO = "ZONA OESTE";
                    }
                    else if (cepint >= 06000 && cepint <= 09999)
                    {
                        if (cepint >= 08000 && cepint <= 08499)
                        {
                            colaborador.REGIAO = "ZONA LESTE";
                        }
                        else
                        {
                            colaborador.REGIAO = "GRANDE SÃO PAULO";
                        }
                    }
                }
                if (CREDENCIAMENTOS.PREVISAO == DateTime.Parse("01-01-0001"))
                {
                    CREDENCIAMENTOS.PREVISAO = DateTime.Now.Date.AddDays(2);
                }
                CREDENCIAMENTOS.BAIRRO = colaborador.CIDADE + ";" + colaborador.BAIRRO;
                CREDENCIAMENTOS.IDCOLABORADOR = id_colaborador;
                CREDENCIAMENTOS.DATA_CADASTRO = DateTime.Now.Date;
                if (CREDENCIAMENTOS.PREVISAO == null) { CREDENCIAMENTOS.PREVISAO = DateTime.Now.Date.AddDays(2); }
                CREDENCIAMENTOS.FUNCIONARIO = user.NameIdentifier;
                CREDENCIAMENTOS.NOME_ESPECIA = db.ESPECIALIDADE_EXAMES.Where(s => s.ID_ESPECIALIDADE == CREDENCIAMENTOS.IDESP_EXAM).Select(s => s.NOME_ESPECIALIDADE).FirstOrDefault();
                CREDENCIAMENTOS.OBSERVACAO = DateTime.Now.ToString()+" " + CREDENCIAMENTOS.STATUS + " " + CREDENCIAMENTOS.FUNCIONARIO + ":" + " " + CREDENCIAMENTOS.OBSERVACAO;
                if (CREDENCIAMENTOS.STATUS == STATUS_CREDENCIAMENTOS.CONCLUIDO)
                {
                    var met = db.METAS.Where(s => s.IDCONTRATO == CREDENCIAMENTOS.IDCONTRATO).Where(s => s.IDESP_EXAM == CREDENCIAMENTOS.IDESP_EXAM).Select(s => s).FirstOrDefault();
                    met.CONCLUIDO = met.CONCLUIDO + 1;
                }
                db.CREDENCIAMENTOS.Add(CREDENCIAMENTOS);
                db.SaveChanges();
                return RedirectToAction("DETAILS","COLABORADORES", new { id = id_colaborador });
            }
            ViewBag.IDCOLABORADOR = new SelectList(db.COLABORADORES, "ID_COLABORADOR", "NOME_FANTASIA", CREDENCIAMENTOS.IDCOLABORADOR);
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE", CREDENCIAMENTOS.IDCONTRATO);
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES, "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE");

            return View(CREDENCIAMENTOS);
        }

        // GET: CREDENCIAMENTOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CREDENCIAMENTOS CREDENCIAMENTOS = db.CREDENCIAMENTOS.Find(id);
            if (CREDENCIAMENTOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCOLABORADOR = new SelectList(db.COLABORADORES, "ID_COLABORADOR", "NOME_FANTASIA", CREDENCIAMENTOS.IDCOLABORADOR);
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE", CREDENCIAMENTOS.IDCONTRATO);
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES, "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE", CREDENCIAMENTOS.IDESP_EXAM);
            
            return View(CREDENCIAMENTOS);
        }

        // POST: CREDENCIAMENTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CREDENCIAMENTOS,STATUS,OBSERVACAO,IDCOLABORADOR,IDESP_EXAM,IDCONTRATO,NOME_ESPECIA,BAIRRO,VLR_PRESTADOR,VLR_100PLANO")] CREDENCIAMENTOS CREDENCIAMENTOS)
        {

            if (ModelState.IsValid)
            {
                var colaborador = db.COLABORADORES.Where(s => s.ID_COLABORADOR == CREDENCIAMENTOS.IDCOLABORADOR).Select(S => S).FirstOrDefault();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = manager.FindById(User.Identity.GetUserId());
                var obsCrede = db.CREDENCIAMENTOS.Where(s => s.IDCOLABORADOR == CREDENCIAMENTOS.IDCOLABORADOR).Select(s => s.OBSERVACAO).FirstOrDefault();
                CREDENCIAMENTOS.DATA_CADASTRO = db.CREDENCIAMENTOS.Where(s => s.ID_CREDENCIAMENTOS == CREDENCIAMENTOS.ID_CREDENCIAMENTOS).Select(s => s.DATA_CADASTRO).FirstOrDefault();
                CREDENCIAMENTOS.OBSERVACAO=obsCrede + "\n"+ DateTime.Now.ToString() + " " + CREDENCIAMENTOS.STATUS + " " + CREDENCIAMENTOS.FUNCIONARIO + ":" + " " + CREDENCIAMENTOS.OBSERVACAO;
                if(CREDENCIAMENTOS.PREVISAO == DateTime.Parse("01-01-0001"))
                {
                    CREDENCIAMENTOS.PREVISAO = DateTime.Now.Date.AddDays(2);
                }
                if (CREDENCIAMENTOS.STATUS == STATUS_CREDENCIAMENTOS.CONCLUIDO)
                {
                    var met = db.METAS.Where(s => s.IDCONTRATO == CREDENCIAMENTOS.IDCONTRATO).Where(s => s.IDESP_EXAM == CREDENCIAMENTOS.IDESP_EXAM).Select(s => s).FirstOrDefault();
                    met.CONCLUIDO = met.CONCLUIDO + 1;
                }
                if(CREDENCIAMENTOS.PREVISAO == null) { CREDENCIAMENTOS.PREVISAO = DateTime.Now.Date.AddDays(2); }
                CREDENCIAMENTOS.FUNCIONARIO = user.NameIdentifier;
                CREDENCIAMENTOS.NOME_ESPECIA = db.ESPECIALIDADE_EXAMES.Where(s => s.ID_ESPECIALIDADE == CREDENCIAMENTOS.IDESP_EXAM).Select(s => s.NOME_ESPECIALIDADE).FirstOrDefault();
                CREDENCIAMENTOS.BAIRRO = colaborador.CIDADE + ";" + colaborador.BAIRRO;
                CREDENCIAMENTOS.REGIAO = colaborador.REGIAO;
                CREDENCIAMENTOS.NOME_ESPECIA = db.ESPECIALIDADE_EXAMES.Where(s => s.ID_ESPECIALIDADE == CREDENCIAMENTOS.IDESP_EXAM).Select(s => s.NOME_ESPECIALIDADE).FirstOrDefault();
                db.Entry(CREDENCIAMENTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("details", "COLABORADORES", new { id = CREDENCIAMENTOS.IDCOLABORADOR });
            }
            ViewBag.IDCOLABORADOR = new SelectList(db.COLABORADORES, "ID_COLABORADOR", "NOME_FANTASIA", CREDENCIAMENTOS.IDCOLABORADOR);
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE", CREDENCIAMENTOS.IDCONTRATO);
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES.OrderBy(s=>s.NOME_ESPECIALIDADE), "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE", CREDENCIAMENTOS.IDESP_EXAM);
            return RedirectToAction("DETAILS", "COLABORADORES", new { id = CREDENCIAMENTOS.IDCOLABORADOR });
        }

        // GET: CREDENCIAMENTOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CREDENCIAMENTOS CREDENCIAMENTOS = db.CREDENCIAMENTOS.Find(id);
            if (CREDENCIAMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(CREDENCIAMENTOS);
        }

        // POST: CREDENCIAMENTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CREDENCIAMENTOS CREDENCIAMENTOS = db.CREDENCIAMENTOS.Find(id);
            db.CREDENCIAMENTOS.Remove(CREDENCIAMENTOS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CredenciamentosAtrasados()
        {
            DateTime Menos2 = DateTime.Today.AddDays(-2);
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindById(User.Identity.GetUserId());
            var CRED = db.CREDENCIAMENTOS.Where(c => c.PREVISAO >= Menos2).Where(c => c.STATUS == STATUS_CREDENCIAMENTOS.PROPOSTA_ENVIADA).Where(c => c.FUNCIONARIO == user.NameIdentifier);
            return View(CRED.ToList());
        }
        public ActionResult CredenciamentosEncerrados()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindById(User.Identity.GetUserId());
            var CRED = db.CREDENCIAMENTOS.Where(c => DateTime.Today >= c.PREVISAO).Where(c => c.STATUS == STATUS_CREDENCIAMENTOS.PROPOSTA_ENVIADA).Where(c => c.FUNCIONARIO == user.NameIdentifier);
            return View(CRED.ToList());
        }

        public string InfoCredenciamentos(int IDCONTRATO)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindById(User.Identity.GetUserId());
            
            var ocorrenciaStatus = db.CREDENCIAMENTOS
                .Where(s=>s.IDCONTRATO == IDCONTRATO)
                .Where(s => s.FUNCIONARIO == user.NameIdentifier)
                .GroupBy(o => new {
                    o.STATUS
                })
                .Select(s => new
                {
                    status = s.Key.STATUS,
                    total = s.Count()
                })
                .ToList();
            string[] ArrStatus = new string[] {
                        "PROPOSTA ENVIADA",
                        "CONCLUIDO",
                        "CANCELADO",
                        "CONTRATO ELETRONICO RECEBIDO",
                        "CONTRATO_FISICO_RECEBIDO",
                        "CONTRATO ENVIADO",
                        "PROSPECT"
                    };
            var ResumoOcorrenciaStatus =
                JsonConvert.SerializeObject(
                    ocorrenciaStatus.Select(
                        s => new
                        {
                            Status = ArrStatus[Convert.ToInt32(s.status) - 1],
                            total = s.total
                        }
                    )
                );
            return ResumoOcorrenciaStatus;
        }
        public string credenciamentoslancados(int IDCONTRATO,int status)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindById(User.Identity.GetUserId());
            STATUS_CREDENCIAMENTOS statusSelec = new STATUS_CREDENCIAMENTOS();
            statusSelec.Equals(status);
            var credenciamentoslancados = db.CREDENCIAMENTOS.Where(s => s.IDCONTRATO == IDCONTRATO)
                .Where(s => s.STATUS == (STATUS_CREDENCIAMENTOS)status)
                .Where(s => s.FUNCIONARIO == user.NameIdentifier)
                .Select(s => new
                {
                    NOME_FANTASIA = s.COLABORADORES.NOME_FANTASIA,
                    ESPECIALIDADE = s.NOME_ESPECIA,
                    TELEFONE = s.COLABORADORES.TEL1,
                    CONTATO = s.COLABORADORES.CONTATO,
                    IDPRESTADOR = s.IDCOLABORADOR
                }).ToList();
            var CredLancados =
              JsonConvert.SerializeObject(
                  credenciamentoslancados.Select(
                      s => new
                      {
                          NOME_FANTASIA = s.NOME_FANTASIA,
                          ESPECIALIDADE = s.ESPECIALIDADE,
                          TELEFONE = s.TELEFONE,
                          CONTATO = s.CONTATO,
                          IDPRESTADOR = s.IDPRESTADOR
                      }
                  )
              );
            return CredLancados;
        }
        public string preencherPrestAguar(int IDCONTRATO,int ESPECIALIDADE)
        {
            var credenciamentos = db.CREDENCIAMENTOS.Where(s => s.IDCONTRATO == IDCONTRATO)
                .Where(s => s.IDESP_EXAM == ESPECIALIDADE)
                .Where(s=>s.STATUS == STATUS_CREDENCIAMENTOS.CONTRATO_ELETRONICO_RECEBIDO)
                .ToList();
            var credenciamentosJSON = JsonConvert.SerializeObject(
                credenciamentos.Select(s => new
                {
                    IDPRESTADOR = s.IDCOLABORADOR,
                    PRESTADOR = s.COLABORADORES.NOME_FANTASIA,
                    ESPECIALIDADE = s.NOME_ESPECIA,
                    TELEFONE = s.COLABORADORES.TEL1,
                    CONTATO = s.COLABORADORES.CONTATO
                })
                );
            return credenciamentosJSON;
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
