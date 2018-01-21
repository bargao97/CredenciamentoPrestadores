using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class GraficoController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Grafico
        public ActionResult Index()
        {
            return View();
        }
        public string credenciamentosPorFuncionario( DateTime dataInicial, DateTime dataFinal,string usuarios)
        {
            var ocorrenciaStatus = db.CREDENCIAMENTOS
            .Where(s=>s.FUNCIONARIO==usuarios)
            .Where(s=>s.DATA_CADASTRO>=dataInicial)
            .Where(s=>s.DATA_CADASTRO<=dataFinal)
            .GroupBy(o => new {
                o.STATUS
            })
            .Select(s => new
            {
                status = s.Key.STATUS,
                total = s.Count()
            })
            .ToList();
                    //Converte resultado da query em JSON
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
           
            //Retorna dados do Grafico no formato JSON (string)           
            return ResumoOcorrenciaStatus;
        }
        public string CredenciamentosPorContrato(DateTime dataInicial, DateTime dataFinal)
        {
            var ocorrenciaStatus = db.CREDENCIAMENTOS
            .Where(s => s.DATA_CADASTRO >= dataInicial)
            .Where(s => s.DATA_CADASTRO <= dataFinal)
            .GroupBy(o => new {
                o.STATUS
            })
            .Select(s => new
            {
                status = s.Key.STATUS,
                total = s.Count()
            })
            .ToList();
            //Converte resultado da query em JSON
            string[] ArrStatus = new string[] {
                        "ANDAMENTO",
                        "CONCLUIDO",
                        "CANCELADO",
                        "CONCLUIDO AGUARDANDO CONTRATO"
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
            //Retorna dados do Grafico no formato JSON (string)           
            return ResumoOcorrenciaStatus;
        }
        public string CredenciamentoPorRegiao(int IDCONTRATO)
        {
            var ocorrenciaStatus = db.CREDENCIAMENTOS
            .Where(s=>s.STATUS == STATUS_CREDENCIAMENTOS.CONCLUIDO)
            .Where(s=>s.IDCONTRATO == IDCONTRATO)
            .GroupBy(o => new {
                o.REGIAO
            })
            .Select(s => new
            {
                regiao = s.Key.REGIAO,
                total = s.Count()
            })
            .ToList();
            //Converte resultado da query em JSON
            var ResumoOcorrenciaStatus =
                JsonConvert.SerializeObject(
                    ocorrenciaStatus.Select(
                        s => new
                        {
                            regiao = s.regiao,
                            total = s.total
                        }
                    )
                );
            //Retorna dados do Grafico no formato JSON (string)           
            return ResumoOcorrenciaStatus;
        }
    }
}