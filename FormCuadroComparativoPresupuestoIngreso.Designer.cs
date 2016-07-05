namespace SIAPG
{
    partial class FormCuadroComparativoPresupuestoIngreso
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCuadroComparativoPresupuestoIngreso));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelopciones = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.imageListOpciones = new System.Windows.Forms.ImageList(this.components);
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.GridviewCargaPresupuestal = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonAjustes = new System.Windows.Forms.Button();
            this.panelopciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridviewCargaPresupuestal)).BeginInit();
            this.SuspendLayout();
            // 
            // panelopciones
            // 
            this.panelopciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(224)))), ((int)(((byte)(244)))));
            this.panelopciones.Controls.Add(this.button1);
            this.panelopciones.Controls.Add(this.button5);
            this.panelopciones.Controls.Add(this.button4);
            this.panelopciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelopciones.Location = new System.Drawing.Point(0, 0);
            this.panelopciones.Name = "panelopciones";
            this.panelopciones.Size = new System.Drawing.Size(803, 47);
            this.panelopciones.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageIndex = 2;
            this.button1.ImageList = this.imageListOpciones;
            this.button1.Location = new System.Drawing.Point(693, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 28);
            this.button1.TabIndex = 16;
            this.button1.Text = "       Exportar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // imageListOpciones
            // 
            this.imageListOpciones.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListOpciones.ImageStream")));
            this.imageListOpciones.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListOpciones.Images.SetKeyName(0, "Imprimir.png");
            this.imageListOpciones.Images.SetKeyName(1, "Exportar.png");
            this.imageListOpciones.Images.SetKeyName(2, "ExportarPDF.png");
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Enabled = false;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.ImageIndex = 1;
            this.button5.ImageList = this.imageListOpciones;
            this.button5.Location = new System.Drawing.Point(596, 9);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 28);
            this.button5.TabIndex = 15;
            this.button5.Text = "       Exportar";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Enabled = false;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.ImageIndex = 0;
            this.button4.ImageList = this.imageListOpciones;
            this.button4.Location = new System.Drawing.Point(502, 9);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 28);
            this.button4.TabIndex = 14;
            this.button4.Text = "       Imprimir";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // GridviewCargaPresupuestal
            // 
            this.GridviewCargaPresupuestal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridviewCargaPresupuestal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridviewCargaPresupuestal.ColumnHeadersHeight = 28;
            this.GridviewCargaPresupuestal.EnableHeadersVisualStyles = false;
            this.GridviewCargaPresupuestal.Location = new System.Drawing.Point(0, 52);
            this.GridviewCargaPresupuestal.Name = "GridviewCargaPresupuestal";
            this.GridviewCargaPresupuestal.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.GridviewCargaPresupuestal.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GridviewCargaPresupuestal.RowHeadersWidth = 60;
            this.GridviewCargaPresupuestal.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GridviewCargaPresupuestal.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.GridviewCargaPresupuestal.Size = new System.Drawing.Size(803, 249);
            this.GridviewCargaPresupuestal.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(165, 308);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Aceptar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonAjustes
            // 
            this.buttonAjustes.Location = new System.Drawing.Point(566, 308);
            this.buttonAjustes.Name = "buttonAjustes";
            this.buttonAjustes.Size = new System.Drawing.Size(115, 23);
            this.buttonAjustes.TabIndex = 15;
            this.buttonAjustes.Text = "Ajuste aumático >>";
            this.buttonAjustes.UseVisualStyleBackColor = true;
            this.buttonAjustes.Click += new System.EventHandler(this.button3_Click);
            // 
            // FormCuadroComparativoPresupuestoIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 343);
            this.Controls.Add(this.buttonAjustes);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panelopciones);
            this.Controls.Add(this.GridviewCargaPresupuestal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormCuadroComparativoPresupuestoIngreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuadro comparativo Presupuesto-Ingreso";
            this.Load += new System.EventHandler(this.FormCuadroComparativoPresupuestoIngreso_Load);
            this.panelopciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridviewCargaPresupuestal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelopciones;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageListOpciones;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView GridviewCargaPresupuestal;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonAjustes;
    }
}