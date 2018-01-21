using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GridMvc.DataAnnotations;

namespace WebApplication.Models
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class METAS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [GridColumn(Title = "ID", SortEnabled = true, FilterEnabled = true)]
        public int ID_META { get; set; }

        [Required]
        [Display(Name = "META")]
        [GridColumn(Title = "Meta", SortEnabled = true, FilterEnabled = true)]
        public int META { get; set; }

        [ForeignKey("ESPECIALIDADE_EXAMES")]
        [Display(Name ="Especialidade")]
        [NotMappedColumn]
        public int IDESP_EXAM { get; set; }


        public virtual ESPECIALIDADE_EXAMES ESPECIALIDADE_EXAMES { get; set; }

        [Display(Name = "Nome Especialidade")]
        [GridColumn(Title = "Nome da especialidade", SortEnabled = true, FilterEnabled = true)]

        public string NOME_ESPECIALIDADE { get; set; }


        public virtual CONTRATO CONTRATO { get; set; }

        [ForeignKey("CONTRATO")]
        [Display(Name = "Contrato")]
        [NotMappedColumn]
        public int IDCONTRATO { get; set; }


        [NotMappedColumn]
        public int CONCLUIDO { get; set; }



    }
}