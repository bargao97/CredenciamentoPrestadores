using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class ESPECIALIDADE_EXAMES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_ESPECIALIDADE { get; set; }

        [Required]
        [Display(Name ="Nome da Especialidade")]
        public string NOME_ESPECIALIDADE { get; set; }

        [Display(Name = "TIPO")]
        public  TIPO_EXAM_ESP TIPO_EXAM_ESP { get; set; }

        public ICollection<CREDENCIAMENTOS> CREDENCIAMENTOS { get; set; }
        public ICollection<METAS> METAS { get; set; }
    }
    public enum TIPO_EXAM_ESP
    {
        ESPECIALIDADE =1,
        EXAME =2
    }
}