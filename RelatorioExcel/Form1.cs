using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelatorioExcel.Database;
using ClosedXML.Excel;

namespace RelatorioExcel
{
    public partial class Form1 : Form
    {
        MHZPRESTADORESEntities db = new MHZPRESTADORESEntities();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var contratos = db.CONTRATOes.Select(s => new
            {
                id = s.ID_CONTRATO,
                nome = s.NOME_CLIENTE
            }).OrderBy(s => s.nome).ToList();

            foreach( var cont in contratos)
            {
                cbxContrato.Items.Add(cont.nome);
            }
        }

        private void btnAnalitico_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.InitialDirectory = "c:";
                saveFileDialog1.Title = "Relatorio Analitico";
                saveFileDialog1.FileName = "";
                saveFileDialog1.Filter = "Excel (2007)|*.xlsx";
                if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    var idcontrato = db.CONTRATOes.Where(s => s.NOME_CLIENTE == cbxContrato.SelectedItem.ToString()).FirstOrDefault();

                    var selecionarCredenciamentos = db.CREDENCIAMENTOS.Where(s => s.IDCONTRATO == idcontrato.ID_CONTRATO)
                                                                      .Select(s => new
                                                                      {
                                                                          ID = s.ID_CREDENCIAMENTOS,
                                                                          DATA_CADASTRO = s.DATA_CADASTRO,
                                                                          ESPECIALIDADE = s.NOME_ESPECIA,
                                                                          FUNCIONARIO = s.FUNCIONARIO,
                                                                          STATUS = s.STATUS,
                                                                          OBSERVAÇÃO = s.OBSERVACAO
                                                                      }).
                                                                      OrderBy(s => new
                                                                      {
                                                                          s.STATUS,
                                                                          s.FUNCIONARIO,
                                                                          s.DATA_CADASTRO,
                                                                          s.ID
                                                                      }).ToList();


                    int qntconcluida = db.CREDENCIAMENTOS.Where(s => s.IDCONTRATO == idcontrato.ID_CONTRATO).Where(s => s.STATUS == 2).Select(s => s).Count();

                    var qntespecialidade = db.METAS.Where(s => s.IDCONTRATO == idcontrato.ID_CONTRATO).Select(s => s).ToList();


                    Console.WriteLine("Gerando arquivo Excel...");

                    var wb = new XLWorkbook();
                    //Cria a Planilha com o Nome
                    var wsfunc = wb.Worksheets.Add("RELATORIO CREDENCIAMENTOS INTERNO:ANALITICO");

                    wsfunc.Cell("A1").Value = "Relatorio Analitico:";
                    wsfunc.Cell("A3").Value = "Contrato:" + " " + idcontrato.NOME_CLIENTE.ToString();
                    wsfunc.Cell("B3").Value = "META:" + idcontrato.META.ToString();
                    wsfunc.Cell("C3").Value = "Concluido" + qntconcluida.ToString();
                    var range = wsfunc.Range("A1:F2");
                    range.Merge().Style.Font.SetBold().Font.FontSize = 23;
                    // Cabeçalhos do Relatório
                    wsfunc.Cell("A4").Value = "ID DE CREDENCIAMENTO";
                    wsfunc.Cell("B4").Value = "DATA DE CADASTRO";
                    wsfunc.Cell("C4").Value = "FUNCIONARIO";
                    wsfunc.Cell("D4").Value = "ESPECIALIDADE";
                    wsfunc.Cell("E4").Value = "STATUS DE CREDENCIAMENTO";
                    wsfunc.Cell("F4").Value = "OBSERVAÇÃO";

                    //Variavel que ira delimitar de quando ira começar o relatorio
                    var linha = 5;
                    DateTime? dataperiodo = DateTime.Now;
                    //Corppo do relatorio
                    foreach (var c in selecionarCredenciamentos)
                    {
                        
                            wsfunc.Cell("A" + linha.ToString()).Value = c.ID.ToString();
                            wsfunc.Cell("B" + linha.ToString()).Value = c.DATA_CADASTRO.ToString();
                            wsfunc.Cell("C" + linha.ToString()).Value = c.FUNCIONARIO.ToString();
                            wsfunc.Cell("D" + linha.ToString()).Value = c.ESPECIALIDADE.ToString();
                            if (c.STATUS == 1) { wsfunc.Cell("E" + linha.ToString()).Value = "ANDAMENTO"; }else
                            if (c.STATUS == 2) { wsfunc.Cell("E" + linha.ToString()).Value = "CONCLUIDO"; }else
                            if (c.STATUS == 3) { wsfunc.Cell("E" + linha.ToString()).Value = "CANCELADO"; }
                            wsfunc.Cell("F" + linha.ToString()).Value = c.OBSERVAÇÃO.ToString();
                        linha++;
                    }
                    // Crio uma Tabela para ativar os Filtros
                    range = wsfunc.Range("A4:F" + linha.ToString());
                    range.CreateTable();

                    // Ajusto o tamanho da coluna com o conteúdo da coluna
                    wsfunc.Columns("1-8").AdjustToContents();
                    linha = linha + 2;
                    wsfunc.Cell("A" + linha.ToString()).Value = "Meta Concluida por especialdiades";
                    linha++;
                    wsfunc.Cell("A" + linha.ToString()).Value = "Especialidade";
                    wsfunc.Cell("B" + linha.ToString()).Value = "Quantidade";
                    linha++;
                  
                    // Salvar o arquivo em Disco
                    wb.SaveAs(saveFileDialog1.FileName.ToString());

                    // Liberar objetos
                    wsfunc.Dispose();
                    wb.Dispose();
                    MessageBox.Show("Relatorio gerado com sucesso");

                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.InitialDirectory = "c:";
                saveFileDialog1.Title = "Relatorio Cliente";
                saveFileDialog1.FileName = "";
                saveFileDialog1.Filter = "Excel (2007)|*.xlsx";
                if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    var idcontrato = db.CONTRATOes.Where(s => s.NOME_CLIENTE == cbxContrato.SelectedItem.ToString()).FirstOrDefault();

                    var prestcredenciados = db.CREDENCIAMENTOS.Where(s => s.IDCONTRATO == idcontrato.ID_CONTRATO).Select(s => s.IDCOLABORADOR).Distinct().ToList();

                    int qntconcluida = db.CREDENCIAMENTOS.Where(s => s.IDCONTRATO == idcontrato.ID_CONTRATO).Where(s => s.STATUS == 2).Select(s => s).Count();

                    var qntespecialidade = db.METAS.Where(s => s.IDCONTRATO == idcontrato.ID_CONTRATO).Select(s => s).ToList();

                   


                    Console.WriteLine("Gerando arquivo Excel...");

                    var wb = new XLWorkbook();
                    //Cria a Planilha com o Nome
                    var ws = wb.Worksheets.Add("Relatorio");

                    ws.Cell("A1").Value = "Relatorio cliente:"+idcontrato.NOME_CLIENTE;
                    ws.Cell("B3").Value = "META:" + idcontrato.META.ToString();
                    ws.Cell("C3").Value = "Concluido" + qntconcluida.ToString();
                    var range = ws.Range("A1:s2");
                    range.Merge().Style.Font.SetBold().Font.FontSize = 33;
                    // Cabeçalhos do Relatório
                    ws.Cell("A4").Value = "ID";
                    ws.Cell("B4").Value = "PRESTADOR";
                    ws.Cell("C4").Value = "CPF/CNPJ";
                    ws.Cell("D4").Value = "TIPO";
                    ws.Cell("E4").Value = "DDD";
                    ws.Cell("F4").Value = "TEL1";
                    ws.Cell("G4").Value = "TEL2";
                    ws.Cell("H4").Value = "UF";
                    ws.Cell("I4").Value = "CIDADE";
                    ws.Cell("J4").Value = "CEP";
                    ws.Cell("K4").Value = "LOGRADOURO";
                    ws.Cell("L4").Value = "Nº";
                    ws.Cell("M4").Value = "COMPLEMENTO";
                    ws.Cell("N4").Value = "BAIRRO";
                    ws.Cell("O4").Value = "EMAIL";
                    ws.Cell("P4").Value = "CONTATO";
                    ws.Cell("Q4").Value = "ESPECIALDADES DO PRESTADOR";
                    ws.Cell("R4").Value = "ESPECIALIDADES EM ANDAMENTO DE CREDENCIAMENTO";
                    ws.Cell("S4").Value = "ESPECIALIDADES COM CREDENCIAMENTO CANCELADO";
                    ws.Cell("T4").Value = "ESPECIALIDADES CREDENCIADAS";
                    

                    //Variavel que ira delimitar de quando ira começar o relatorio
                    var linha = 5;
                    DateTime? dataperiodo = DateTime.Now;
                    //Corppo do relatorio
                    foreach (var c in prestcredenciados)
                    {
                        var colaborador = db.COLABORADORES.Where(s => s.ID_COLABORADOR == c).FirstOrDefault();

                        ws.Cell("A" + linha.ToString()).Value = colaborador.ID_COLABORADOR.ToString();
                        if (colaborador != null) { ws.Cell("B" + linha.ToString()).Value = colaborador.NOME_FANTASIA.ToString(); }
                        if (colaborador.CPF_CNPJ != null) { ws.Cell("C" + linha.ToString()).Value = colaborador.CPF_CNPJ.ToString(); }
                        if (colaborador.TIPO != null) { ws.Cell("D" + linha.ToString()).Value = colaborador.TIPO.ToString(); }
                        if (colaborador.DDD != null) { ws.Cell("E" + linha.ToString()).Value = colaborador.DDD.ToString(); }
                        if (colaborador.TEL1 != null) { ws.Cell("F" + linha.ToString()).Value = colaborador.TEL1.ToString(); }
                        if (colaborador.TEL2 != null) { ws.Cell("G" + linha.ToString()).Value = colaborador.TEL2.ToString(); }
                        if (colaborador.UF != null) { ws.Cell("H" + linha.ToString()).Value = colaborador.UF.ToString(); }
                        if (colaborador.CIDADE != null) { ws.Cell("I" + linha.ToString()).Value = colaborador.CIDADE.ToString(); }
                        if (colaborador.CEP != null) { ws.Cell("J" + linha.ToString()).Value = colaborador.CEP.ToString(); }
                        if (colaborador.LOGRADOURO != null) { ws.Cell("K" + linha.ToString()).Value = colaborador.LOGRADOURO.ToString(); }
                        if (colaborador.Numero != null) { ws.Cell("L" + linha.ToString()).Value = colaborador.Numero.ToString(); }
                        if (colaborador.COMPLEMENTO != null) { ws.Cell("M" + linha.ToString()).Value = colaborador.COMPLEMENTO.ToString(); }
                        if (colaborador.BAIRRO != null) { ws.Cell("N" + linha.ToString()).Value = colaborador.BAIRRO.ToString(); }
                        if (colaborador.EMAIL != null) { ws.Cell("O" + linha.ToString()).Value = colaborador.EMAIL.ToString(); }
                        if (colaborador.CONTATO != null) { ws.Cell("P" + linha.ToString()).Value = colaborador.CONTATO.ToString(); }
                        if (colaborador.ESPECIALIDADE != null) { ws.Cell("Q" + linha.ToString()).Value = colaborador.ESPECIALIDADE.ToString(); }

                        var credenciamentos = db.CREDENCIAMENTOS.Where(s => s.IDCOLABORADOR == c).ToList();
                        string textocredAndamento = "";
                        string textocredConcluido = "";
                        string textocredCancelado = "";

                        foreach (var item in credenciamentos)
                        {
                            if(item.STATUS == 1)
                            {
                                    textocredAndamento= textocredAndamento + item.NOME_ESPECIA + ";";
                            }
                            if (item.STATUS == 2)
                            {

                                textocredConcluido = textocredConcluido + item.NOME_ESPECIA + ";";
                            }
                            if (item.STATUS == 3)
                            {
                                textocredCancelado = textocredCancelado + item.NOME_ESPECIA + ";";
                            }
                        }
                        ws.Cell("Q" + linha.ToString()).Value = textocredAndamento;
                        ws.Cell("R" + linha.ToString()).Value = textocredCancelado;
                        ws.Cell("S" + linha.ToString()).Value = textocredConcluido;
                        linha++;
                    }
                    // Crio uma Tabela para ativar os Filtros
                    range = ws.Range("A4:S" + linha.ToString());
                    range.CreateTable();

                    // Ajusto o tamanho da coluna com o conteúdo da coluna
                    ws.Columns("1-8").AdjustToContents();

                   // Relatorio por especialidade
                     var wsesp = wb.Worksheets.Add("POR ESPECIALIDADE");

                    wsesp.Cell("A1").Value = "Relatorio por especialidade";
                    wsesp.Cell("A3").Value = "Contrato:" + " " + idcontrato.NOME_CLIENTE.ToString();
                    wsesp.Cell("B3").Value = "META:" + idcontrato.META.ToString();
                    wsesp.Cell("C3").Value = "Concluido" + qntconcluida.ToString();
                     range = wsesp.Range("A1:F2");
                    range.Merge().Style.Font.SetBold().Font.FontSize = 23;
                    // Cabeçalhos do Relatório
                    wsesp.Cell("A4").Value = "Especialidade";
                    wsesp.Cell("B4").Value = "Meta";
                    wsesp.Cell("C4").Value = "Quantidade";

                    linha = 5;

                    foreach (var i in qntespecialidade)
                    {
                        wsesp.Cell("A" + linha.ToString()).Value = i.NOME_ESPECIALIDADE;
                        wsesp.Cell("B" + linha.ToString()).Value = i.META;
                        wsesp.Cell("C" + linha.ToString()).Value = i.CONCLUIDO;
                        linha++;
                    }
                    range = wsesp.Range("A4:C" + linha.ToString());
                    range.CreateTable();

                    //Por localidade
                    //var wsloc = wb.Worksheets.Add("POR LOCALIDADE SIMPLIFICADO");

                    //var credlocalidade = db.CREDENCIAMENTOS.Where(s => s.IDCONTRATO == idcontrato.ID_CONTRATO)
                    //                                                  .Select(s => new
                    //                                                  {
                    //                                                      BAIRRO = s.COLABORADORES.BAIRRO,
                    //                                                      TOTAL = s.COLABORADORES.BAIRRO.Count()
                    //                                                  }).
                    //                                                  OrderBy(s => new
                    //                                                  {
                    //                                                      s.BAIRRO
                    //                                                  }).ToList();



                    //wsloc.Cell("A1").Value = "Relatorio por localidade";
                    //wsloc.Cell("A3").Value = "Contrato:" + " " + idcontrato.NOME_CLIENTE.ToString();
                    //wsloc.Cell("B3").Value = "META:" + idcontrato.META.ToString();
                    //wsloc.Cell("C3").Value = "Concluido" + qntconcluida.ToString();
                    //range = wsloc.Range("A1:F2");
                    //range.Merge().Style.Font.SetBold().Font.FontSize = 23;
                    //// Cabeçalhos do Relatório
                    //linha = 5;
                    //wsloc.Cell("A4").Value = "BAIRRO";
                    //wsloc.Cell("B4").Value = "TOTAL";

                    //foreach (var i in credlocalidade)
                    //{
                    //    wsesp.Cell("A" + linha.ToString()).Value = i.BAIRRO;
                    //    wsesp.Cell("B" + linha.ToString()).Value = i.TOTAL;
                    //    linha++;
                    //}

                    // Salvar o arquivo em Disco
                    wb.SaveAs(saveFileDialog1.FileName.ToString());

                    // Liberar objetos
                    ws.Dispose();
                    wsesp.Dispose();
                    //wsloc.Dispose();
                    wb.Dispose();
                    MessageBox.Show("Relatorio gerado com sucesso");

                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
