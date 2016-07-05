namespace SIAPG
{
    partial class FormCatalogoDeFinalidad
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
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonAgregar = new System.Windows.Forms.Button();
            this.buttonEliminar = new System.Windows.Forms.Button();
            this.buttonModificar = new System.Windows.Forms.Button();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.comboBoxFinalidad = new System.Windows.Forms.ComboBox();
            this.groupBoxDatosCaptura = new System.Windows.Forms.GroupBox();
            this.textBoxFinalidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCodigoFinalidad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxDatosCaptura.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancelar.Enabled = false;
            this.buttonCancelar.Location = new System.Drawing.Point(281, 181);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelar.TabIndex = 5;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // buttonAgregar
            // 
            this.buttonAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAgregar.Location = new System.Drawing.Point(157, 181);
            this.buttonAgregar.Name = "buttonAgregar";
            this.buttonAgregar.Size = new System.Drawing.Size(75, 23);
            this.buttonAgregar.TabIndex = 4;
            this.buttonAgregar.Text = "Agregar";
            this.buttonAgregar.UseVisualStyleBackColor = true;
            this.buttonAgregar.Click += new System.EventHandler(this.buttonAgregar_Click);
            // 
            // buttonEliminar
            // 
            this.buttonEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEliminar.Enabled = false;
            this.buttonEliminar.Location = new System.Drawing.Point(33, 181);
            this.buttonEliminar.Name = "buttonEliminar";
            this.buttonEliminar.Size = new System.Drawing.Size(75, 23);
            this.buttonEliminar.TabIndex = 3;
            this.buttonEliminar.Text = "Eliminar...";
            this.buttonEliminar.UseVisualStyleBackColor = true;
            this.buttonEliminar.Click += new System.EventHandler(this.buttonEliminar_Click);
            // 
            // buttonModificar
            // 
            this.buttonModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonModificar.Enabled = false;
            this.buttonModificar.Location = new System.Drawing.Point(405, 181);
            this.buttonModificar.Name = "buttonModificar";
            this.buttonModificar.Size = new System.Drawing.Size(75, 23);
            this.buttonModificar.TabIndex = 6;
            this.buttonModificar.Text = "Modificar";
            this.buttonModificar.UseVisualStyleBackColor = true;
            this.buttonModificar.Click += new System.EventHandler(this.buttonModificar_Click);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGuardar.Enabled = false;
            this.buttonGuardar.Location = new System.Drawing.Point(529, 181);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(75, 23);
            this.buttonGuardar.TabIndex = 7;
            this.buttonGuardar.Text = "Guardar";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // comboBoxFinalidad
            // 
            this.comboBoxFinalidad.FormattingEnabled = true;
            this.comboBoxFinalidad.Location = new System.Drawing.Point(148, 46);
            this.comboBoxFinalidad.Name = "comboBoxFinalidad";
            this.comboBoxFinalidad.Size = new System.Drawing.Size(456, 21);
            this.comboBoxFinalidad.TabIndex = 1;
            this.comboBoxFinalidad.SelectedIndexChanged += new System.EventHandler(this.comboBoxFinalidad_SelectedIndexChanged);
            this.comboBoxFinalidad.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFinalidad_SelectionChangeCommitted);
            this.comboBoxFinalidad.DropDownClosed += new System.EventHandler(this.comboBoxFinalidad_DropDownClosed);
            // 
            // groupBoxDatosCaptura
            // 
            this.groupBoxDatosCaptura.Controls.Add(this.textBoxFinalidad);
            this.groupBoxDatosCaptura.Controls.Add(this.label3);
            this.groupBoxDatosCaptura.Controls.Add(this.textBoxCodigoFinalidad);
            this.groupBoxDatosCaptura.Controls.Add(this.label4);
            this.groupBoxDatosCaptura.Enabled = false;
            this.groupBoxDatosCaptura.Location = new System.Drawing.Point(27, 73);
            this.groupBoxDatosCaptura.Name = "groupBoxDatosCaptura";
            this.groupBoxDatosCaptura.Size = new System.Drawing.Size(574, 93);
            this.groupBoxDatosCaptura.TabIndex = 22;
            this.groupBoxDatosCaptura.TabStop = false;
            // 
            // textBoxFinalidad
            // 
            this.textBoxFinalidad.Enabled = false;
            this.textBoxFinalidad.Location = new System.Drawing.Point(125, 39);
            this.textBoxFinalidad.Multiline = true;
            this.textBoxFinalidad.Name = "textBoxFinalidad";
            this.textBoxFinalidad.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFinalidad.Size = new System.Drawing.Size(439, 40);
            this.textBoxFinalidad.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(50, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Finalidad";
            // 
            // textBoxCodigoFinalidad
            // 
            this.textBoxCodigoFinalidad.Enabled = false;
            this.textBoxCodigoFinalidad.Location = new System.Drawing.Point(125, 13);
            this.textBoxCodigoFinalidad.Name = "textBoxCodigoFinalidad";
            this.textBoxCodigoFinalidad.Size = new System.Drawing.Size(108, 20);
            this.textBoxCodigoFinalidad.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Código finalidad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 25;
            this.label1.Text = "Finalidad";
            // 
            // FormCatalogoDeFinalidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 254);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonAgregar);
            this.Controls.Add(this.buttonEliminar);
            this.Controls.Add(this.buttonModificar);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.comboBoxFinalidad);
            this.Controls.Add(this.groupBoxDatosCaptura);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCatalogoDeFinalidad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo Finalidad";
            this.Load += new System.EventHandler(this.FormCatalogoDeFinalidad_Load);
            this.groupBoxDatosCaptura.ResumeLayout(false);
            this.groupBoxDatosCaptura.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonAgregar;
        private System.Windows.Forms.Button buttonEliminar;
        private System.Windows.Forms.Button buttonModificar;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.ComboBox comboBoxFinalidad;
        private System.Windows.Forms.GroupBox groupBoxDatosCaptura;
        private System.Windows.Forms.TextBox textBoxFinalidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCodigoFinalidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
    }
}