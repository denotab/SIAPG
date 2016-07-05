namespace SIAPG
{
    partial class FormCatalogoDeFuncion
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
            this.textBoxFuncion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCodigoFuncion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxFuncion = new System.Windows.Forms.ComboBox();
            this.groupBoxDatosCaptura.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancelar.Enabled = false;
            this.buttonCancelar.Location = new System.Drawing.Point(318, 217);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelar.TabIndex = 29;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // buttonAgregar
            // 
            this.buttonAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAgregar.Enabled = false;
            this.buttonAgregar.Location = new System.Drawing.Point(194, 217);
            this.buttonAgregar.Name = "buttonAgregar";
            this.buttonAgregar.Size = new System.Drawing.Size(75, 23);
            this.buttonAgregar.TabIndex = 28;
            this.buttonAgregar.Text = "Agregar";
            this.buttonAgregar.UseVisualStyleBackColor = true;
            this.buttonAgregar.Click += new System.EventHandler(this.buttonAgregar_Click);
            // 
            // buttonEliminar
            // 
            this.buttonEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEliminar.Enabled = false;
            this.buttonEliminar.Location = new System.Drawing.Point(70, 217);
            this.buttonEliminar.Name = "buttonEliminar";
            this.buttonEliminar.Size = new System.Drawing.Size(75, 23);
            this.buttonEliminar.TabIndex = 27;
            this.buttonEliminar.Text = "Eliminar...";
            this.buttonEliminar.UseVisualStyleBackColor = true;
            this.buttonEliminar.Click += new System.EventHandler(this.buttonEliminar_Click);
            // 
            // buttonModificar
            // 
            this.buttonModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonModificar.Enabled = false;
            this.buttonModificar.Location = new System.Drawing.Point(442, 217);
            this.buttonModificar.Name = "buttonModificar";
            this.buttonModificar.Size = new System.Drawing.Size(75, 23);
            this.buttonModificar.TabIndex = 30;
            this.buttonModificar.Text = "Modificar";
            this.buttonModificar.UseVisualStyleBackColor = true;
            this.buttonModificar.Click += new System.EventHandler(this.buttonModificar_Click);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGuardar.Enabled = false;
            this.buttonGuardar.Location = new System.Drawing.Point(566, 217);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(75, 23);
            this.buttonGuardar.TabIndex = 31;
            this.buttonGuardar.Text = "Guardar";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // comboBoxFinalidad
            // 
            this.comboBoxFinalidad.FormattingEnabled = true;
            this.comboBoxFinalidad.Location = new System.Drawing.Point(182, 30);
            this.comboBoxFinalidad.Name = "comboBoxFinalidad";
            this.comboBoxFinalidad.Size = new System.Drawing.Size(456, 21);
            this.comboBoxFinalidad.TabIndex = 26;
            this.comboBoxFinalidad.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFinalidad_SelectionChangeCommitted);
            // 
            // groupBoxDatosCaptura
            // 
            this.groupBoxDatosCaptura.Controls.Add(this.textBoxFuncion);
            this.groupBoxDatosCaptura.Controls.Add(this.label3);
            this.groupBoxDatosCaptura.Controls.Add(this.textBoxCodigoFuncion);
            this.groupBoxDatosCaptura.Controls.Add(this.label4);
            this.groupBoxDatosCaptura.Enabled = false;
            this.groupBoxDatosCaptura.Location = new System.Drawing.Point(54, 90);
            this.groupBoxDatosCaptura.Name = "groupBoxDatosCaptura";
            this.groupBoxDatosCaptura.Size = new System.Drawing.Size(574, 93);
            this.groupBoxDatosCaptura.TabIndex = 32;
            this.groupBoxDatosCaptura.TabStop = false;
            // 
            // textBoxFuncion
            // 
            this.textBoxFuncion.Location = new System.Drawing.Point(128, 39);
            this.textBoxFuncion.Multiline = true;
            this.textBoxFuncion.Name = "textBoxFuncion";
            this.textBoxFuncion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFuncion.Size = new System.Drawing.Size(439, 40);
            this.textBoxFuncion.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(71, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Función";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // textBoxCodigoFuncion
            // 
            this.textBoxCodigoFuncion.Location = new System.Drawing.Point(128, 13);
            this.textBoxCodigoFuncion.Name = "textBoxCodigoFuncion";
            this.textBoxCodigoFuncion.Size = new System.Drawing.Size(108, 20);
            this.textBoxCodigoFuncion.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Código función";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(114, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 33;
            this.label1.Text = "Finalidad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 34;
            this.label2.Text = "Función";
            // 
            // comboBoxFuncion
            // 
            this.comboBoxFuncion.FormattingEnabled = true;
            this.comboBoxFuncion.Location = new System.Drawing.Point(182, 63);
            this.comboBoxFuncion.Name = "comboBoxFuncion";
            this.comboBoxFuncion.Size = new System.Drawing.Size(456, 21);
            this.comboBoxFuncion.TabIndex = 35;
            this.comboBoxFuncion.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFuncion_SelectionChangeCommitted);
            // 
            // FormCatalogoDeFuncion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 287);
            this.Controls.Add(this.comboBoxFuncion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonAgregar);
            this.Controls.Add(this.buttonEliminar);
            this.Controls.Add(this.buttonModificar);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.comboBoxFinalidad);
            this.Controls.Add(this.groupBoxDatosCaptura);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCatalogoDeFuncion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo de función";
            this.Load += new System.EventHandler(this.FormCatalogoDeFuncion_Load);
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
        private System.Windows.Forms.TextBox textBoxFuncion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCodigoFuncion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxFuncion;
    }
}