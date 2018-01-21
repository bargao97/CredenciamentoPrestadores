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

namespace RelatorioExcel
{
    public partial class Unificardados : Form
    {
        public Unificardados()
        {
            InitializeComponent();
        }

        MHZPRESTADORESEntities db = new MHZPRESTADORESEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            var prestcredenciados = db.CREDENCIAMENTOS.Select(s => s.IDCOLABORADOR).Distinct().ToList();


        }
    }
}
