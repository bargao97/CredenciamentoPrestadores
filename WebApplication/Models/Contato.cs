using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CONTATO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCONTATO { get; set; }

        [Required]
        [Display(Name = "Nome do Cliente")]
        public string OBSERVACAO { get; set; }

        [Required]
        [Display(Name = "Meta")]
        public int META { get; set; }

        //public ICollection<COLABORADORES> COLABORADORES { get; set; }

        public ICollection<METAS> METAS { get; set; }

        public ICollection<CREDENCIAMENTOS> CREDENCIAMENTOS { get; set; }
    }
}