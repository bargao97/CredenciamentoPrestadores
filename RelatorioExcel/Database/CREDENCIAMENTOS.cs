//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RelatorioExcel.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class CREDENCIAMENTOS
    {
        public int ID_CREDENCIAMENTOS { get; set; }
        public int STATUS { get; set; }
        public System.DateTime PREVISAO { get; set; }
        public System.DateTime DATA_CADASTRO { get; set; }
        public string FUNCIONARIO { get; set; }
        public string OBSERVACAO { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int IDESP_EXAM { get; set; }
        public int IDCONTRATO { get; set; }
        public string NOME_ESPECIA { get; set; }
    
        public virtual COLABORADORES COLABORADORES { get; set; }
        public virtual CONTRATOes CONTRATOes { get; set; }
        public virtual ESPECIALIDADE_EXAMES ESPECIALIDADE_EXAMES { get; set; }
    }
}
