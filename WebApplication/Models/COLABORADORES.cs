using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class COLABORADORES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [GridColumn(Title = "Codigo", SortEnabled = true, FilterEnabled = true)]
        public int ID_COLABORADOR { get; set; }

        [Display(Name = "Nome Fantasia")]
        [StringLength(int.MaxValue)]
        [GridColumn(Title = "Nome Fantasia", SortEnabled = true, FilterEnabled = true)]
        public string NOME_FANTASIA { get; set; }
        [Display(Name = "Razão social")]
        [StringLength(int.MaxValue)]
        [NotMappedColumn]
        public string RAZAO_SOCIAL { get; set; }

        [Display(Name = "CPF/CNPJ")]
        [MinLength(14)]
        [StringLength(150)]
        [NotMappedColumn]
        public string CPF_CNPJ { get; set; }

        [StringLength(3)]
        [GridColumn(Title = "DDD", SortEnabled = true, FilterEnabled = true)]
        [NotMappedColumn]   
        public string DDD { get; set; }
        [Display(Name = "Telefone 1")]
        [StringLength(20)]
        [GridColumn(Title = "Telefone", SortEnabled = true, FilterEnabled = true)]
        public string TEL1 { get; set; }
        [Display(Name = "CELULAR")]
        [StringLength(20)]
        [NotMappedColumn]
        public string TEL2 { get; set; }

        [Display(Name = "UF")]
        [StringLength(100)]
        [NotMappedColumn]
        public string UF { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(100)]
        [GridColumn(Title = "Cidade", SortEnabled = true, FilterEnabled = true)]
        public string CIDADE { get; set; }

        [Display(Name = "CEP")]
        [StringLength(11)]
        [NotMappedColumn]
        public string CEP { get; set; }

        [Display(Name = "Tipo de Logradouro")]
        [StringLength(150)]
        [NotMappedColumn]
        public string TIPO_LOGRADOURO { get; set; }

        [Display(Name = "Logradouro")]
        [StringLength(150)]
        [NotMappedColumn]
        public string LOGRADOURO { get; set; }

        [Display(Name = "Numero")]
        [StringLength(10)]
        [NotMappedColumn]
        public string Numero { get; set; }


        [Display(Name = "Complemento")]
        [StringLength(100)]
        [NotMappedColumn]
        public string COMPLEMENTO { get; set; }


        [Display(Name = "Bairro")]
        [StringLength(100)]
        [NotMappedColumn]
        public string BAIRRO { get; set; }


        [Display(Name = "Especialidade")]
        [StringLength(int.MaxValue)]
        [GridColumn(Title = "Especialidade", SortEnabled = true, FilterEnabled = true)]
        public string ESPECIALIDADE { get; set; }


        [Display(Name = "Cliente")]
        [StringLength(100)]
        [GridColumn(Title = "Cliente", SortEnabled = true, FilterEnabled = true)]
        public string CLIENTE { get; set; }

        [Display(Name = "Tipo")]
        [StringLength(100)]
        [NotMappedColumn]
        public string TIPO { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(100)]
        [EmailAddress]
        [GridColumn(Title = "Email", SortEnabled = true, FilterEnabled = true)]
        public string EMAIL { get; set; }

        [Display(Name = "Contato")]
        [StringLength(100)]
        public string CONTATO  { get; set; }

        [Display(Name = "Regiao")]
        [StringLength(100)]
        public string REGIAO { get; set; }

        //[ForeignKey("CONTRATO")]
        //[Display(Name = "Contrato")]
        //public int IDCONTRATO { get; set; }

        //public CONTRATO CONTRATO { get; set; }

        
    }
}