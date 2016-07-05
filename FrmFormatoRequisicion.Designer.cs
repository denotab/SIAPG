namespace SIAPG
{
    partial class FrmFormatoRequisicion
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
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.SuspendLayout();
            // 
            // printDocument1
            // 
            this.printDocument1.DocumentName = global::SIAPG.Properties.Settings.Default.prueba;
            this.printDocument1.OriginAtMargins = true;
            // 
            // printPreviewControl1
            // 
            this.printPreviewControl1.AutoZoom = false;
            this.printPreviewControl1.Document = this.printDocument1;
            this.printPreviewControl1.Location = new System.Drawing.Point(-5, 31);
            this.printPreviewControl1.Name = "printPreviewControl1";
            this.printPreviewControl1.Size = new System.Drawing.Size(548, 408);
            this.printPreviewControl1.TabIndex = 0;
            this.printPreviewControl1.Zoom = 0.36818181818181817D;
            // 
            // FrmFormatoRequisicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 438);
            this.Controls.Add(this.printPreviewControl1);
            this.Name = "FrmFormatoRequisicion";
            this.Text = "Impresion de Formato";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewControl printPreviewControl1;
    }
}