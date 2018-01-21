using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebApplication.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;


namespace WebApplication.Database
{
    public class CustomIdentityUser : IdentityUser
    {
        public string NameIdentifier { get; set; }
    }
    public class ApplicationUser : CustomIdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim("WebApplication.Models.RegisterViewModel.NameIdentifier", NameIdentifier));
            userIdentity.AddClaim(new Claim("WebApplication.Models.RegisterViewModel.Email", Email));

            return userIdentity;
        }
    }

    public class DatabaseContext : DbContext
    {
        //Define connection-string que será utilizada com o banco de dados
        public DatabaseContext() : base("MHZPRESTADORES") { }

        //Cria referencia das classes utilizadas no banco de dados
        public DbSet<COLABORADORES> colaboradores { get; set; }
        public DbSet<CONTRATO> contrato { get; set; }
        public DbSet<CREDENCIAMENTO> credenciamento { get; set; }

        public DbSet<ESPECIALIDADE> especialidade { get; set; }

        public DbSet<EXAMES> exames { get; set; }

        public DbSet<METAS> metas { get; set; }

        //On Model Creating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Desativa nomes de tabelas no plural
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Desativa Cascade Delete
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

    }
}