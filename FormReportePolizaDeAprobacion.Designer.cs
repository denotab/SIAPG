namespace SIAPG
{
    partial class FormReportePolizaDeAprobacion
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
            this.reportPolizaAprobacion = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportPolizaAprobacion
            // 
            this.reportPolizaAprobacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportPolizaAprobacion.LocalReport.ReportEmbeddedResource = "SIAPG.Reportes.ReportPolizaDeAprobacionPresupuestal.rdlc";
            this.reportPolizaAprobacion.Location = new System.Drawing.Point(2, 12);
            this.reportPolizaAprobacion.Name = "reportPolizaAprobacion";
            this.reportPolizaAprobacion.Size = new System.Drawing.Size(563, 410);
            this.reportPolizaAprobacion.TabIndex = 0;
            // 
            // FormReportePolizaDeAprobacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 434);
            this.Controls.Add(this.reportPolizaAprobacion);
            this.Name = "FormReportePolizaDeAprobacion";
            this.Text = "Póliza de aprobación";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormReportePolizaDeAprobacion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportPolizaAprobacion;
    }
}