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
    public class COLABORADORESController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: COLABORADORES
        public ActionResult Index()
        {
            ///Carrega as DropDowns de busca
            ViewBag.especialidade = db.ESPECIALIDADE_EXAMES.OrderBy(S => S.NOME_ESPECIALIDADE).ToList();
            var regiao = db.COLABORADORES.Where(s => s.REGIAO != null).OrderBy(S => S.REGIAO).Distinct()
                 .ToList();
            ViewBag.regiao = regiao.OrderBy(s => s.REGIAO).Select(s => s.REGIAO).Distinct().ToList();
            var cidade = db.COLABORADORES.Where(s => s.CIDADE != null).OrderBy(s => s.CIDADE).Distinct()
                .ToList();
            ViewBag.cidade = cidade.OrderBy(s => s.CIDADE).Select(s => s.CIDADE).Distinct().ToList();
            return View(db.COLABORADORES.ToList());
        }
        public ActionResult BuscarColaborador(string query,string especialidade,string cidade,string regiao)
        {
            ///Carrega as DropDowns de busca
            ViewBag.especialidade = db.ESPECIALIDADE_EXAMES.OrderBy(S => S.NOME_ESPECIALIDADE).ToList();
            var regiaobus = db.COLABORADORES.Where(s => s.REGIAO != null).OrderBy(S => S.REGIAO).Distinct()
                            .ToList();
            ViewBag.regiao = regiaobus.OrderBy(s => s.REGIAO).Select(s => s.REGIAO).Distinct().ToList();
            var cidadebus = db.COLABORADORES.Where(s => s.CIDADE != null).OrderBy(s => s.CIDADE).Distinct()
                .ToList();
            ViewBag.cidade = cidadebus.OrderBy(s => s.CIDADE).Select(s => s.CIDADE).Distinct().ToList();
            var busca = db.COLABORADORES.ToList();

            if (cidade != null && cidade != "")
            {
                busca = busca.Where(s => s.CIDADE != null && s.CIDADE.ToString().Contains(cidade)).ToList();
            }
            if (regiao != null && regiao != "")
            {
                busca = busca.Where(s => s.REGIAO != null && s.REGIAO.ToString().Contains(regiao)).ToList();
            }
            if (especialidade!= null && especialidade!="")
            {
                busca = busca.Where(s =>s.ESPECIALIDADE !=null && s.ESPECIALIDADE.ToString().Contains(especialidade)).ToList();
            }
            if (query != "")
            {
                busca = db.COLABORADORES.Where(s =>
                  s.ID_COLABORADOR.ToString().Contains(query) ||
                  s.NOME_FANTASIA.ToString().Contains(query) ||
                  s.CPF_CNPJ.Contains(query) ||
                  s.CIDADE.Contains(query) ||
                  s.LOGRADOURO.Contains(query) ||
                  s.RAZAO_SOCIAL.Contains(query) ||
                  s.TEL1.Contains(query) ||
                  s.TEL2.Contains(query) ||
                  s.TIPO_LOGRADOURO.Contains(query) ||
                  s.Numero.Contains(query) ||
                  s.ESPECIALIDADE.Contains(query)).ToList();
            }
            return View(busca);
        }


        // GET: COLABORADORES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLABORADORES cOLABORADORES = db.COLABORADORES.Find(id);
            if (cOLABORADORES == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE");
            ViewBag.IDESP_EXAM = new SelectList(db.ESPECIALIDADE_EXAMES.OrderBy(s =>s.NOME_ESPECIALIDADE), "ID_ESPECIALIDADE", "NOME_ESPECIALIDADE");
            ViewBag.CREDENCIAMENTOS = db.CREDENCIAMENTOS.Where(s => s.IDCOLABORADOR == id).OrderByDescending(s => s.PREVISAO);
            return View(cOLABORADORES);
        }
        // GET: COLABORADORES/Create
        public ActionResult Create()
        {
            ViewBag.esp = db.ESPECIALIDADE_EXAMES.OrderBy(S => S.NOME_ESPECIALIDADE).ToList();
            return View();
        }
        // POST: COLABORADORES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_COLABORADOR,NOME_FANTASIA,RAZAO_SOCIAL,CPF_CNPJ,DDD,TEL1,TEL2,UF,CIDADE,CEP,TIPO_LOGRADOURO,LOGRADOURO,Numero,COMPLEMENTO,BAIRRO,ESPECIALIDADE,CLIENTE,TIPO,EMAIL,CONTATO")] COLABORADORES cOLABORADORES,object IDESP_EXAM)
        {
            if (ModelState.IsValid)
            {
                if(cOLABORADORES.CEP != null)
                {
                    string primeirasLetrasCep = cOLABORADORES.CEP.Substring(0, 5);
                    int cepint = Convert.ToInt32(primeirasLetrasCep);
                    if (cepint >= 01000 && cepint <= 01599)
                    {
                        cOLABORADORES.REGIAO = "CENTRO";
                    }
                    else if (cepint >= 02000 && cepint <= 02999)
                    {
                        cOLABORADORES.REGIAO = "ZONA NORTE";
                    }
                    else if (cepint >= 03000 && cepint <= 03999)
                    {
                        cOLABORADORES.REGIAO = "ZONA LESTE";
                    }
                    else if (cepint >= 08000 && cepint <= 08499)
                    {
                        cOLABORADORES.REGIAO = "ZONA LESTE";
                    }
                    else if (cepint >= 04000 && cepint <= 04999)
                    {
                        cOLABORADORES.REGIAO = "ZONA SUL";
                    }
                    else if (cepint >= 05000 && cepint <= 05899)
                    {
                        cOLABORADORES.REGIAO = "ZONA OESTE";
                    }
                    else if (cepint >= 06000 && cepint <= 09999)
                    {
                        if (cepint >= 08000 && cepint <= 08499)
                        {
                            cOLABORADORES.REGIAO = "ZONA LESTE";
                        }
                        else
                        {
                            cOLABORADORES.REGIAO = "GRANDE SÃO PAULO";
                        }
                    }
                }
                IDESP_EXAM.ToString();
                 cOLABORADORES.ESPECIALIDADE = IDESP_EXAM.ToString();;
                db.COLABORADORES.Add(cOLABORADORES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE");
            ViewBag.esp = db.ESPECIALIDADE_EXAMES.OrderBy(S => S.NOME_ESPECIALIDADE).ToList();
            return View(cOLABORADORES);
        }
        // GET: COLABORADORES/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.esp = db.ESPECIALIDADE_EXAMES.OrderBy(S => S.NOME_ESPECIALIDADE).ToList();
            var teste = ViewBag.ESPECIALIDADE;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLABORADORES cOLABORADORES = db.COLABORADORES.Find(id);
            if (cOLABORADORES == null)
            {
                return HttpNotFound();
            }
            return View(cOLABORADORES);
        }
        // POST: COLABORADORES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_COLABORADOR,NOME_FANTASIA,RAZAO_SOCIAL,CPF_CNPJ,DDD,TEL1,TEL2,UF,CIDADE,CEP,TIPO_LOGRADOURO,LOGRADOURO,Numero,COMPLEMENTO,BAIRRO,ESPECIALIDADE,CLIENTE,TIPO,EMAIL,CONTATO")] COLABORADORES cOLABORADORES)
        {
            if (ModelState.IsValid)
            {  
                db.Entry(cOLABORADORES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOLABORADORES);
        }
        // GET: COLABORADORES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COLABORADORES cOLABORADORES = db.COLABORADORES.Find(id);
            if (cOLABORADORES == null)
            {
                return HttpNotFound();
            }
            return View(cOLABORADORES);
        }
        // POST: COLABORADORES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COLABORADORES cOLABORADORES = db.COLABORADORES.Find(id);
            db.COLABORADORES.Remove(cOLABORADORES);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string VerificaCPF(string CPF)
        {
            int cep = db.COLABORADORES.Where(S => S.CPF_CNPJ == CPF).Select(S => S.ID_COLABORADOR).FirstOrDefault();
            string cepconvert = Convert.ToString(cep);
            return cepconvert;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public string ProposCredenciamentosRegiao(string ESPECIALIDADE)
        {
            var credProposReg = db.COLABORADORES.Where(S => S.ESPECIALIDADE.Contains(ESPECIALIDADE))
                .GroupBy(S => new { REGIAO = S.REGIAO})
                .Select(S=>new {REGIAO= S.Key.REGIAO,TOTAL = S.Count() })
                .ToList();
            var credProposRegJSON =
                JsonConvert.SerializeObject(credProposReg.Select(s => new {REGIAO = s.REGIAO }));
            return credProposRegJSON;
        }
        public string ProporCredTabela(string ESPECIALIDADE, string REGIAO)
        {
            string CredLancados = " ";
            if (REGIAO == "TODOS")
            {
                var credenciamentoslancados = db.COLABORADORES.Where(s => s.ESPECIALIDADE.Contains(ESPECIALIDADE))
                .Select(s => new
                {
                    NOME_FANTASIA = s.NOME_FANTASIA,
                    TELEFONE = s.TEL1,
                    CONTATO = s.CONTATO,
                    IDPRESTADOR = s.ID_COLABORADOR
                }).ToList();
                 CredLancados =
                  JsonConvert.SerializeObject(
                      credenciamentoslancados.Select(
                          s => new
                          {
                              NOME_FANTASIA = s.NOME_FANTASIA,
                              TELEFONE = s.TELEFONE,
                              CONTATO = s.CONTATO,
                              IDPRESTADOR = s.IDPRESTADOR
                          }
                      )
                  );
            }
            else
            {
                var credenciamentoslancados = db.COLABORADORES.Where(s => s.ESPECIALIDADE.Contains(ESPECIALIDADE))
                .Where(s => s.REGIAO.Contains(REGIAO))
                .Select(s => new
                {
                    NOME_FANTASIA = s.NOME_FANTASIA,
                    TELEFONE = s.TEL1,
                    CONTATO = s.CONTATO,
                    IDPRESTADOR = s.ID_COLABORADOR
                }).ToList();
                CredLancados =
                    JsonConvert.SerializeObject(
                        credenciamentoslancados.Select(
                            s => new
                            {
                                NOME_FANTASIA = s.NOME_FANTASIA,
                                TELEFONE = s.TELEFONE,
                                CONTATO = s.CONTATO,
                                IDPRESTADOR = s.IDPRESTADOR
                            }
                        )
                    );
            }
            
            return CredLancados;
        }
    }
}
