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
    public class CREDENCIAMENTOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NotMappedColumn]
        public int ID_CREDENCIAMENTOS { get; set; }

        [Required]
        [Display(Name ="Status CREDENCIAMENTOS")]
        [GridColumn(Title = "Status", SortEnabled = true, FilterEnabled = true)]
        public STATUS_CREDENCIAMENTOS STATUS { get; set; }


        
        [Display(Name = "Prazo de entrega")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [GridColumn(Title = "Retorno", SortEnabled = true, FilterEnabled = true)]
        public DateTime PREVISAO { get; set; }


        [Display(Name = "Data de cadastro")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [NotMappedColumn]
        public DateTime DATA_CADASTRO { get; set; }

        [Display(Name = "Data de cadastro")]
        [GridColumn(Title = "Funcionario", SortEnabled = true, FilterEnabled = true)]
        public string FUNCIONARIO { get; set; }

        [Required]
        [Display(Name = "observação")]
        [NotMappedColumn]
        public string OBSERVACAO { get; set;}

        [NotMappedColumn]
        public virtual COLABORADORES COLABORADORES { get; set; }
        [ForeignKey("COLABORADORES")]
        [Display(Name = "COLABORADORES")]
        [NotMappedColumn]
        public int IDCOLABORADOR { get; set; }

        [NotMappedColumn]
        public virtual ESPECIALIDADE_EXAMES ESPECIALIDADE_EXAMES { get; set; }

        [ForeignKey("ESPECIALIDADE_EXAMES")]
        [Display(Name = "ESPECIALIDADE OU EXAME")]
        [GridColumn(Title = "Especialidade", SortEnabled = true, FilterEnabled = true)]
        public int IDESP_EXAM { get; set; }

        [NotMappedColumn]
        public virtual CONTRATO CONTRATO { get; set; }

        [ForeignKey("CONTRATO")]
        [Display(Name = "CONTRATO")]
        [NotMappedColumn]
        public int IDCONTRATO { get; set; }

        
        public string NOME_ESPECIA { get; set; }


        public string BAIRRO { get; set; }

        [StringLength(100)]
        public string REGIAO { get; set; }

        [Display (Name ="VALOR DO PRESTADOR")]
        public double VLR_PRESTADOR { get; set; }

        [Display(Name = "VALOR DO PRESTADOR")]
        public double VLR_100PLANO { get; set; }

    }
    public enum STATUS_CREDENCIAMENTOS
    {
        PROPOSTA_ENVIADA=1,
        CONCLUIDO=2,
        CANCELADO=3,
        CONTRATO_ELETRONICO_RECEBIDO = 4,
        CONTRATO_FISICO_RECEBIDO = 5,
        CONTRATO_ENVIADO =6,
        PROSPECT=7,
        DOCUMENTO_RECEBIDO_COM_PENDENCIA=8,
        DOCUMENTO_RECEBIDO=9
    }
}