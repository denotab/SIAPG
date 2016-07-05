namespace SIAPG
{
    partial class FormAbrirHojasDeTrabajoIngreso
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridviewHojas = new System.Windows.Forms.DataGridView();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.NombreHoja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UltimaModificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdHojaIngreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridviewHojas)).BeginInit();
            this.SuspendLayout();
            // 
            // GridviewHojas
            // 
            this.GridviewHojas.AllowUserToAddRows = false;
            this.GridviewHojas.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridviewHojas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridviewHojas.ColumnHeadersHeight = 28;
            this.GridviewHojas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NombreHoja,
            this.FechaCreacion,
            this.UltimaModificacion,
            this.Observaciones,
            this.IdHojaIngreso});
            this.GridviewHojas.Dock = System.Windows.Forms.DockStyle.Top;
            this.GridviewHojas.EnableHeadersVisualStyles = false;
            this.GridviewHojas.Location = new System.Drawing.Point(0, 0);
            this.GridviewHojas.Name = "GridviewHojas";
            this.GridviewHojas.RowHeadersWidth = 26;
            this.GridviewHojas.Size = new System.Drawing.Size(658, 259);
            this.GridviewHojas.TabIndex = 9;
            this.GridviewHojas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridviewHojas_CellDoubleClick);
            this.GridviewHojas.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridviewHojas_RowEnter);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Location = new System.Drawing.Point(484, 285);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(75, 23);
            this.buttonGuardar.TabIndex = 11;
            this.buttonGuardar.Text = "Abrir";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(161, 285);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NombreHoja
            // 
            this.NombreHoja.HeaderText = "Nombre de la hoja";
            this.NombreHoja.Name = "NombreHoja";
            this.NombreHoja.Width = 200;
            // 
            // FechaCreacion
            // 
            this.FechaCreacion.HeaderText = "Fecha creación";
            this.FechaCreacion.Name = "FechaCreacion";
            this.FechaCreacion.Width = 130;
            // 
            // UltimaModificacion
            // 
            this.UltimaModificacion.HeaderText = "Última modificación";
            this.UltimaModificacion.Name = "UltimaModificacion";
            this.UltimaModificacion.Width = 130;
            // 
            // Observaciones
            // 
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.Width = 200;
            // 
            // IdHojaIngreso
            // 
            this.IdHojaIngreso.HeaderText = "IdHojaPresupuesto";
            this.IdHojaIngreso.Name = "IdHojaIngreso";
            this.IdHojaIngreso.ReadOnly = true;
            this.IdHojaIngreso.Visible = false;
            // 
            // FormAbrirHojasDeTrabajoIngreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 316);
            this.Controls.Add(this.GridviewHojas);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormAbrirHojasDeTrabajoIngreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abrir hoja de trabajo Ingreso";
            this.Load += new System.EventHandler(this.FormAbrirHojasDeTrabajoIngreso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridviewHojas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridviewHojas;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreHoja;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaCreacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn UltimaModificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdHojaIngreso;
    }
}