using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;
namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult DashboardV1()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindById(User.Identity.GetUserId());
            //Carregar as DropDowns dos widgets
            ViewBag.usuarios = new SelectList(db.Users, "NameIdentifier", "NameIdentifier");
            ViewBag.IDCONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE");
            ViewBag.IDCONTRATOINFO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE");
            ViewBag.IDCONTRATOLANC = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE");
            ViewBag.IDCONTRATOPROPOR= new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE");
            ViewBag.IDCONTRATOMETAS = new SelectList(db.CONTRATOS, "ID_CONTRATO", "NOME_CLIENTE");
            //calcula a quantidade de atrasos de retorno dos prestadores por parte dos credenciadores
            DateTime Menos2 = DateTime.Today.AddDays(-2);
            ViewBag.CredenciamentosAtrasados = db.CREDENCIAMENTOS
                   .Where(c => c.PREVISAO >= Menos2 ).Where(c=>c.STATUS == STATUS_CREDENCIAMENTOS.PROPOSTA_ENVIADA).Where(c => c.FUNCIONARIO == user.NameIdentifier).Count();

            ViewBag.CredenciamentosEncerrados = db.CREDENCIAMENTOS.Where(c => DateTime.Today >= c.PREVISAO).Where(c => c.STATUS == STATUS_CREDENCIAMENTOS.PROPOSTA_ENVIADA).Where(c => c.FUNCIONARIO == user.NameIdentifier).Count();

            //Carrega os ultimos credenciamentos do funcionario logado
            ViewBag.carregaUltimosContatos = db.CREDENCIAMENTOS.Where(c => c.STATUS == STATUS_CREDENCIAMENTOS.PROPOSTA_ENVIADA).Where(c => c.FUNCIONARIO == user.NameIdentifier).OrderByDescending(s => s.DATA_CADASTRO).Take(10);

            ViewBag.CredenciamentosTotal = db.CREDENCIAMENTOS.Where(c => c.FUNCIONARIO == user.NameIdentifier).Count();
            ViewBag.CredenciamentosConcluidos = db.CREDENCIAMENTOS.Where(c => c.FUNCIONARIO == user.NameIdentifier).Where(s => s.STATUS == STATUS_CREDENCIAMENTOS.CONCLUIDO||s.STATUS == STATUS_CREDENCIAMENTOS.CONTRATO_ELETRONICO_RECEBIDO).Count();
            ViewBag.CredenciamentosAndamento = db.CREDENCIAMENTOS.Where(c => c.FUNCIONARIO == user.NameIdentifier).Where(s => s.STATUS == STATUS_CREDENCIAMENTOS.PROPOSTA_ENVIADA).Count();
            ViewBag.CredenciamentosCancelados = db.CREDENCIAMENTOS.Where(c => c.FUNCIONARIO == user.NameIdentifier).Where(s => s.STATUS == STATUS_CREDENCIAMENTOS.CANCELADO).Count();

            ///Carrega as DropDowns de busca
            ViewBag.especialidade = db.ESPECIALIDADE_EXAMES.OrderBy(S => S.NOME_ESPECIALIDADE).ToList();
            var regiao = db.COLABORADORES.Where(s => s.REGIAO != null).OrderBy(S => S.REGIAO).Distinct()
                .ToList();
            ViewBag.regiao = regiao.OrderBy(s=>s.REGIAO).Select(s => s.REGIAO).Distinct().ToList(); 
            var cidade =db.COLABORADORES.Where(s => s.CIDADE != null).OrderBy(s => s.CIDADE).Distinct()
                .ToList();
            ViewBag.cidade = cidade.OrderBy(s => s.CIDADE).Select(s => s.CIDADE).Distinct().ToList();

            return View();
        }
        public ActionResult DashboardV2()
        {
            return View();
        }
    }
}