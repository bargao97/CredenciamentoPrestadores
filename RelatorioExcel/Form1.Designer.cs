namespace RelatorioExcel
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAnalitico = new System.Windows.Forms.Button();
            this.btnCliente = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cbxContrato = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnAnalitico
            // 
            this.btnAnalitico.Location = new System.Drawing.Point(63, 100);
            this.btnAnalitico.Name = "btnAnalitico";
            this.btnAnalitico.Size = new System.Drawing.Size(133, 41);
            this.btnAnalitico.TabIndex = 4;
            this.btnAnalitico.Text = "Relatorio Interno";
            this.btnAnalitico.UseVisualStyleBackColor = true;
            this.btnAnalitico.Click += new System.EventHandler(this.btnAnalitico_Click);
            // 
            // btnCliente
            // 
            this.btnCliente.Location = new System.Drawing.Point(293, 100);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(143, 41);
            this.btnCliente.TabIndex = 5;
            this.btnCliente.Text = "Relatorio cliente";
            this.btnCliente.UseVisualStyleBackColor = true;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // cbxContrato
            // 
            this.cbxContrato.FormattingEnabled = true;
            this.cbxContrato.Location = new System.Drawing.Point(63, 46);
            this.cbxContrato.Name = "cbxContrato";
            this.cbxContrato.Size = new System.Drawing.Size(373, 21);
            this.cbxContrato.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 160);
            this.Controls.Add(this.cbxContrato);
            this.Controls.Add(this.btnCliente);
            this.Controls.Add(this.btnAnalitico);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAnalitico;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox cbxContrato;
    }
}

