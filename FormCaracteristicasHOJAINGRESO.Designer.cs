namespace SIAPG
{
    partial class FormCaracteristicasHOJAINGRESO
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
            this.textBoxObservaciones = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFechaCreacion = new System.Windows.Forms.TextBox();
            this.textBoxFechaModificacion = new System.Windows.Forms.TextBox();
            this.textBoxNombreHoja = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timerFecha = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBoxHojas = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBoxObservaciones
            // 
            this.textBoxObservaciones.BackColor = System.Drawing.Color.White;
            this.textBoxObservaciones.Location = new System.Drawing.Point(190, 119);
            this.textBoxObservaciones.Multiline = true;
            this.textBoxObservaciones.Name = "textBoxObservaciones";
            this.textBoxObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxObservaciones.Size = new System.Drawing.Size(264, 48);
            this.textBoxObservaciones.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Observaciones:";
            // 
            // textBoxFechaCreacion
            // 
            this.textBoxFechaCreacion.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxFechaCreacion.Enabled = false;
            this.textBoxFechaCreacion.Location = new System.Drawing.Point(190, 20);
            this.textBoxFechaCreacion.Name = "textBoxFechaCreacion";
            this.textBoxFechaCreacion.Size = new System.Drawing.Size(151, 20);
            this.textBoxFechaCreacion.TabIndex = 20;
            // 
            // textBoxFechaModificacion
            // 
            this.textBoxFechaModificacion.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxFechaModificacion.Enabled = false;
            this.textBoxFechaModificacion.Location = new System.Drawing.Point(190, 53);
            this.textBoxFechaModificacion.Name = "textBoxFechaModificacion";
            this.textBoxFechaModificacion.Size = new System.Drawing.Size(151, 20);
            this.textBoxFechaModificacion.TabIndex = 19;
            // 
            // textBoxNombreHoja
            // 
            this.textBoxNombreHoja.BackColor = System.Drawing.Color.White;
            this.textBoxNombreHoja.Location = new System.Drawing.Point(190, 86);
            this.textBoxNombreHoja.Name = "textBoxNombreHoja";
            this.textBoxNombreHoja.Size = new System.Drawing.Size(264, 20);
            this.textBoxNombreHoja.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Fecha de creación:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Fecha última modificación:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 15;
            this.label1.Tag = "";
            this.label1.Text = "Nombre de la hoja de trabajo:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(365, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Aceptar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timerFecha
            // 
            this.timerFecha.Interval = 600;
            this.timerFecha.Tick += new System.EventHandler(this.timerFecha_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 24;
            this.label5.Tag = "v";
            this.label5.Text = "Agregar a  una hoja existente";
            // 
            // button3
            // 
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.button3.Image = global::SIAPG.Properties.Resources.MenConRellenar;
            this.button3.Location = new System.Drawing.Point(432, 176);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(21, 23);
            this.button3.TabIndex = 23;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBoxHojas
            // 
            this.comboBoxHojas.FormattingEnabled = true;
            this.comboBoxHojas.Location = new System.Drawing.Point(190, 178);
            this.comboBoxHojas.Name = "comboBoxHojas";
            this.comboBoxHojas.Size = new System.Drawing.Size(263, 21);
            this.comboBoxHojas.TabIndex = 25;
            this.comboBoxHojas.Visible = false;
            this.comboBoxHojas.SelectionChangeCommitted += new System.EventHandler(this.comboBoxHojas_SelectionChangeCommitted);
            // 
            // FormCaracteristicasHOJAINGRESO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 268);
            this.Controls.Add(this.textBoxObservaciones);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxFechaCreacion);
            this.Controls.Add(this.textBoxFechaModificacion);
            this.Controls.Add(this.textBoxNombreHoja);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.comboBoxHojas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormCaracteristicasHOJAINGRESO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Propiedades Hoja de Ingreso";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCaracteristicasHOJAINGRESO_FormClosing);
            this.Load += new System.EventHandler(this.FormCaracteristicasHOJAINGRESO_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxObservaciones;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFechaCreacion;
        private System.Windows.Forms.TextBox textBoxFechaModificacion;
        private System.Windows.Forms.TextBox textBoxNombreHoja;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timerFecha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBoxHojas;
    }
}