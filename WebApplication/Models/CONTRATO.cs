using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class CONTRATO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_CONTRATO { get; set; }

        [Required]
        [Display(Name ="Nome do Cliente")]
        public string NOME_CLIENTE { get; set; }

        [Required]
        [Display(Name ="Meta")]
        public int META { get; set; }

        //public ICollection<COLABORADORES> COLABORADORES { get; set; }

        public ICollection<METAS> METAS { get; set; }

        public ICollection<CREDENCIAMENTOS> CREDENCIAMENTOS { get; set; }
    }
}