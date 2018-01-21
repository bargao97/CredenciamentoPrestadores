using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication.Models
{

    public class CustomIdentityUser : IdentityUser
    {
        public string NameIdentifier { get; set; }
        public TIPO_LOGIN TIPO_LOGIN { get; set; }
    }

    public enum TIPO_LOGIN
    {
        ADMINISTRADOR =1,
        USUARIO =2
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

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MHZPRESTADORES", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebApplication.Models.COLABORADORES> COLABORADORES { get; set; }

        public System.Data.Entity.DbSet<WebApplication.Models.CONTRATO> CONTRATOS { get; set; }

        public System.Data.Entity.DbSet<WebApplication.Models.CREDENCIAMENTOS> CREDENCIAMENTOS { get; set; }

        public System.Data.Entity.DbSet<WebApplication.Models.ESPECIALIDADE_EXAMES> ESPECIALIDADE_EXAMES { get; set; }

        public System.Data.Entity.DbSet<WebApplication.Models.METAS> METAS { get; set; }
    }
    public class DatabaseContext : DbContext
    {
        //Define connection-string que será utilizada com o banco de dados
        public DatabaseContext() : base("MHZPRESTADORES") { }

        //Cria referencia das classes utilizadas no banco de dados
        public DbSet<COLABORADORES> colaboradores { get; set; }
        public DbSet<CONTRATO> contrato { get; set; }
        public DbSet<CREDENCIAMENTOS> CREDENCIAMENTOS { get; set; }

        public DbSet<ESPECIALIDADE_EXAMES> especialidade { get; set; }

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