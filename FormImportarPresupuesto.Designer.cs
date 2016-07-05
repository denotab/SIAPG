namespace SIAPG
{
    partial class FormImportarPresupuesto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportarPresupuesto));
            this.buttonImportar = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.openFileDialogRuta = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxOpciones = new System.Windows.Forms.GroupBox();
            this.groupBoxPropiedades = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxColumna = new System.Windows.Forms.TextBox();
            this.textBoxRenglon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxHoja = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxRuta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerAlerta = new System.Windows.Forms.Timer(this.components);
            this.groupBoxOpciones.SuspendLayout();
            this.groupBoxPropiedades.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonImportar
            // 
            this.buttonImportar.Enabled = false;
            this.buttonImportar.Location = new System.Drawing.Point(469, 262);
            this.buttonImportar.Name = "buttonImportar";
            this.buttonImportar.Size = new System.Drawing.Size(111, 23);
            this.buttonImportar.TabIndex = 10;
            this.buttonImportar.Text = "Importar";
            this.buttonImportar.UseVisualStyleBackColor = true;
            this.buttonImportar.Click += new System.EventHandler(this.buttonImportar_Click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Location = new System.Drawing.Point(95, 262);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(131, 23);
            this.buttonCancelar.TabIndex = 9;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // openFileDialogRuta
            // 
            this.openFileDialogRuta.FileName = "openFileDialog1";
            // 
            // groupBoxOpciones
            // 
            this.groupBoxOpciones.Controls.Add(this.groupBoxPropiedades);
            this.groupBoxOpciones.Controls.Add(this.label3);
            this.groupBoxOpciones.Controls.Add(this.comboBoxHoja);
            this.groupBoxOpciones.Controls.Add(this.button1);
            this.groupBoxOpciones.Controls.Add(this.textBoxRuta);
            this.groupBoxOpciones.Controls.Add(this.label2);
            this.groupBoxOpciones.Controls.Add(this.label1);
            this.groupBoxOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxOpciones.ForeColor = System.Drawing.Color.Black;
            this.groupBoxOpciones.Location = new System.Drawing.Point(25, 47);
            this.groupBoxOpciones.Name = "groupBoxOpciones";
            this.groupBoxOpciones.Size = new System.Drawing.Size(731, 196);
            this.groupBoxOpciones.TabIndex = 8;
            this.groupBoxOpciones.TabStop = false;
            this.groupBoxOpciones.Text = "Proceso de importación";
            // 
            // groupBoxPropiedades
            // 
            this.groupBoxPropiedades.Controls.Add(this.button2);
            this.groupBoxPropiedades.Controls.Add(this.label6);
            this.groupBoxPropiedades.Controls.Add(this.label5);
            this.groupBoxPropiedades.Controls.Add(this.label4);
            this.groupBoxPropiedades.Controls.Add(this.textBoxColumna);
            this.groupBoxPropiedades.Controls.Add(this.textBoxRenglon);
            this.groupBoxPropiedades.Enabled = false;
            this.groupBoxPropiedades.Location = new System.Drawing.Point(144, 100);
            this.groupBoxPropiedades.Name = "groupBoxPropiedades";
            this.groupBoxPropiedades.Size = new System.Drawing.Size(322, 84);
            this.groupBoxPropiedades.TabIndex = 6;
            this.groupBoxPropiedades.TabStop = false;
            // 
            // button2
            // 
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.ImageIndex = 1;
            this.button2.ImageList = this.imageList1;
            this.button2.Location = new System.Drawing.Point(174, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 31);
            this.button2.TabIndex = 6;
            this.button2.Text = "Asociar celdas...";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "kghostview.ico");
            this.imageList1.Images.SetKeyName(1, "kappfinder.ico");
            this.imageList1.Images.SetKeyName(2, "kpdf.ico");
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(265, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "* Área de inicio de datos (descarte  los encabezados)";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(88, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "Columna";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(15, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Renglón";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxColumna
            // 
            this.textBoxColumna.Location = new System.Drawing.Point(88, 31);
            this.textBoxColumna.Name = "textBoxColumna";
            this.textBoxColumna.Size = new System.Drawing.Size(74, 21);
            this.textBoxColumna.TabIndex = 1;
            this.textBoxColumna.Text = "1";
            this.textBoxColumna.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxRenglon
            // 
            this.textBoxRenglon.Location = new System.Drawing.Point(15, 31);
            this.textBoxRenglon.Name = "textBoxRenglon";
            this.textBoxRenglon.Size = new System.Drawing.Size(75, 21);
            this.textBoxRenglon.TabIndex = 0;
            this.textBoxRenglon.Text = "2";
            this.textBoxRenglon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(67, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Iniciar en:";
            // 
            // comboBoxHoja
            // 
            this.comboBoxHoja.FormattingEnabled = true;
            this.comboBoxHoja.Location = new System.Drawing.Point(144, 73);
            this.comboBoxHoja.Name = "comboBoxHoja";
            this.comboBoxHoja.Size = new System.Drawing.Size(281, 23);
            this.comboBoxHoja.TabIndex = 4;
            this.comboBoxHoja.TextChanged += new System.EventHandler(this.comboBoxHoja_TextChanged_1);
            // 
            // button1
            // 
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageIndex = 1;
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(659, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ruta...";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // textBoxRuta
            // 
            this.textBoxRuta.Location = new System.Drawing.Point(144, 40);
            this.textBoxRuta.Name = "textBoxRuta";
            this.textBoxRuta.Size = new System.Drawing.Size(509, 21);
            this.textBoxRuta.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre de la hoja";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Archivo (Excel)";
            // 
            // timerAlerta
            // 
            this.timerAlerta.Interval = 500;
            // 
            // FormImportarPresupuesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 298);
            this.Controls.Add(this.buttonImportar);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.groupBoxOpciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormImportarPresupuesto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importación preliminar de presupuesto";
            this.Load += new System.EventHandler(this.FormImportarPresupuesto_Load);
            this.groupBoxOpciones.ResumeLayout(false);
            this.groupBoxOpciones.PerformLayout();
            this.groupBoxPropiedades.ResumeLayout(false);
            this.groupBoxPropiedades.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonImportar;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.OpenFileDialog openFileDialogRuta;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBoxOpciones;
        private System.Windows.Forms.GroupBox groupBoxPropiedades;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxColumna;
        private System.Windows.Forms.TextBox textBoxRenglon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxHoja;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxRuta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerAlerta;
    }
}