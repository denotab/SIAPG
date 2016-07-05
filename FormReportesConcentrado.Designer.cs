namespace SIAPG
{
    partial class FormReportesConcentrado
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
            this.reportViewerConcentrado = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerConcentrado
            // 
            this.reportViewerConcentrado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerConcentrado.LocalReport.ReportEmbeddedResource = "SIAPG.Reportes.ReportConcentradoPresupuesto.rdlc";
            this.reportViewerConcentrado.Location = new System.Drawing.Point(0, 0);
            this.reportViewerConcentrado.Name = "reportViewerConcentrado";
            this.reportViewerConcentrado.Size = new System.Drawing.Size(910, 564);
            this.reportViewerConcentrado.TabIndex = 0;
            // 
            // FormReportesConcentrado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 564);
            this.Controls.Add(this.reportViewerConcentrado);
            this.Name = "FormReportesConcentrado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormReportesConcentrado";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormReportesConcentrado_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerConcentrado;
    }
}