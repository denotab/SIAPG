namespace SIAPG
{
    partial class FormImportarCatalogos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportarCatalogos));
            this.groupBoxOpciones = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
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
            this.panelProgressBar = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelRegistrosAlmacenados = new System.Windows.Forms.Label();
            this.labelRegistrosProcesados = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBoxProgress = new System.Windows.Forms.PictureBox();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonImportar = new System.Windows.Forms.Button();
            this.openFileDialogRuta = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.catálogosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiposDeCuentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.géneroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grupoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rubroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clasificaciónFuncionalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.funciónToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.subfunciónToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.unidadResponsableURToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fuentesDeFinanciamientoORToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clasificToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAlerta = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxOpciones.SuspendLayout();
            this.groupBoxPropiedades.SuspendLayout();
            this.panelProgressBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOpciones
            // 
            this.groupBoxOpciones.Controls.Add(this.textBox1);
            this.groupBoxOpciones.Controls.Add(this.checkBox1);
            this.groupBoxOpciones.Controls.Add(this.groupBoxPropiedades);
            this.groupBoxOpciones.Controls.Add(this.label3);
            this.groupBoxOpciones.Controls.Add(this.comboBoxHoja);
            this.groupBoxOpciones.Controls.Add(this.button1);
            this.groupBoxOpciones.Controls.Add(this.textBoxRuta);
            this.groupBoxOpciones.Controls.Add(this.label2);
            this.groupBoxOpciones.Controls.Add(this.label1);
            this.groupBoxOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxOpciones.ForeColor = System.Drawing.Color.Black;
            this.groupBoxOpciones.Location = new System.Drawing.Point(17, 34);
            this.groupBoxOpciones.Name = "groupBoxOpciones";
            this.groupBoxOpciones.Size = new System.Drawing.Size(731, 196);
            this.groupBoxOpciones.TabIndex = 0;
            this.groupBoxOpciones.TabStop = false;
            this.groupBoxOpciones.Text = "Proceso de importación";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(659, -22);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(96, 28);
            this.textBox1.TabIndex = 8;
            this.textBox1.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(472, 165);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(253, 19);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Eliminar los registros previos en la tabla ?";
            this.checkBox1.UseVisualStyleBackColor = true;
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
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.comboBoxHoja.SelectionChangeCommitted += new System.EventHandler(this.comboBoxHoja_SelectionChangeCommitted);
            this.comboBoxHoja.TextChanged += new System.EventHandler(this.comboBoxHoja_TextChanged);
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
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // panelProgressBar
            // 
            this.panelProgressBar.BackColor = System.Drawing.Color.White;
            this.panelProgressBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProgressBar.Controls.Add(this.label9);
            this.panelProgressBar.Controls.Add(this.label8);
            this.panelProgressBar.Controls.Add(this.labelRegistrosAlmacenados);
            this.panelProgressBar.Controls.Add(this.labelRegistrosProcesados);
            this.panelProgressBar.Controls.Add(this.label7);
            this.panelProgressBar.Controls.Add(this.pictureBoxProgress);
            this.panelProgressBar.Location = new System.Drawing.Point(361, 12);
            this.panelProgressBar.Name = "panelProgressBar";
            this.panelProgressBar.Size = new System.Drawing.Size(387, 95);
            this.panelProgressBar.TabIndex = 7;
            this.panelProgressBar.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(320, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Importados";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Procesados";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelRegistrosAlmacenados
            // 
            this.labelRegistrosAlmacenados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegistrosAlmacenados.Location = new System.Drawing.Point(323, 53);
            this.labelRegistrosAlmacenados.Name = "labelRegistrosAlmacenados";
            this.labelRegistrosAlmacenados.Size = new System.Drawing.Size(64, 17);
            this.labelRegistrosAlmacenados.TabIndex = 3;
            this.labelRegistrosAlmacenados.Text = "1,999";
            // 
            // labelRegistrosProcesados
            // 
            this.labelRegistrosProcesados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegistrosProcesados.Location = new System.Drawing.Point(17, 53);
            this.labelRegistrosProcesados.Name = "labelRegistrosProcesados";
            this.labelRegistrosProcesados.Size = new System.Drawing.Size(49, 15);
            this.labelRegistrosProcesados.TabIndex = 2;
            this.labelRegistrosProcesados.Text = "1,999";
            this.labelRegistrosProcesados.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(381, 21);
            this.label7.TabIndex = 1;
            this.label7.Text = "Procesando ....";
            // 
            // pictureBoxProgress
            // 
            this.pictureBoxProgress.BackColor = System.Drawing.Color.White;
            this.pictureBoxProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxProgress.Enabled = false;
            this.pictureBoxProgress.Image = global::SIAPG.Properties.Resources.GifProcesingBarGray;
            this.pictureBoxProgress.Location = new System.Drawing.Point(75, 45);
            this.pictureBoxProgress.Name = "pictureBoxProgress";
            this.pictureBoxProgress.Size = new System.Drawing.Size(240, 21);
            this.pictureBoxProgress.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxProgress.TabIndex = 0;
            this.pictureBoxProgress.TabStop = false;
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Location = new System.Drawing.Point(156, 249);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(131, 23);
            this.buttonCancelar.TabIndex = 4;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // buttonImportar
            // 
            this.buttonImportar.Enabled = false;
            this.buttonImportar.Location = new System.Drawing.Point(511, 249);
            this.buttonImportar.Name = "buttonImportar";
            this.buttonImportar.Size = new System.Drawing.Size(131, 23);
            this.buttonImportar.TabIndex = 5;
            this.buttonImportar.Text = "Importar";
            this.buttonImportar.UseVisualStyleBackColor = true;
            this.buttonImportar.Click += new System.EventHandler(this.buttonImportar_Click);
            // 
            // openFileDialogRuta
            // 
            this.openFileDialogRuta.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Lavender;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.catálogosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(769, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // catálogosToolStripMenuItem
            // 
            this.catálogosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiposDeCuentaToolStripMenuItem,
            this.clasificaciónFuncionalToolStripMenuItem,
            this.unidadResponsableURToolStripMenuItem,
            this.fuentesDeFinanciamientoORToolStripMenuItem,
            this.programasToolStripMenuItem,
            this.clasificToolStripMenuItem});
            this.catálogosToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.catálogosToolStripMenuItem.Name = "catálogosToolStripMenuItem";
            this.catálogosToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.catálogosToolStripMenuItem.Text = "Catálogos";
            // 
            // tiposDeCuentaToolStripMenuItem
            // 
            this.tiposDeCuentaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.géneroToolStripMenuItem,
            this.grupoToolStripMenuItem,
            this.rubroToolStripMenuItem});
            this.tiposDeCuentaToolStripMenuItem.Name = "tiposDeCuentaToolStripMenuItem";
            this.tiposDeCuentaToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.tiposDeCuentaToolStripMenuItem.Tag = "1";
            this.tiposDeCuentaToolStripMenuItem.Text = "Tipos de cuenta";
            this.tiposDeCuentaToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // géneroToolStripMenuItem
            // 
            this.géneroToolStripMenuItem.Name = "géneroToolStripMenuItem";
            this.géneroToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.géneroToolStripMenuItem.Tag = "10";
            this.géneroToolStripMenuItem.Text = "Género";
            this.géneroToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // grupoToolStripMenuItem
            // 
            this.grupoToolStripMenuItem.Name = "grupoToolStripMenuItem";
            this.grupoToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.grupoToolStripMenuItem.Tag = "11";
            this.grupoToolStripMenuItem.Text = "Grupo";
            this.grupoToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // rubroToolStripMenuItem
            // 
            this.rubroToolStripMenuItem.Name = "rubroToolStripMenuItem";
            this.rubroToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.rubroToolStripMenuItem.Tag = "12";
            this.rubroToolStripMenuItem.Text = "Rubro";
            this.rubroToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // clasificaciónFuncionalToolStripMenuItem
            // 
            this.clasificaciónFuncionalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fioToolStripMenuItem,
            this.funciónToolStripMenuItem1,
            this.subfunciónToolStripMenuItem1});
            this.clasificaciónFuncionalToolStripMenuItem.Name = "clasificaciónFuncionalToolStripMenuItem";
            this.clasificaciónFuncionalToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.clasificaciónFuncionalToolStripMenuItem.Tag = "2";
            this.clasificaciónFuncionalToolStripMenuItem.Text = "Clasificación funcional";
            this.clasificaciónFuncionalToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // fioToolStripMenuItem
            // 
            this.fioToolStripMenuItem.Name = "fioToolStripMenuItem";
            this.fioToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.fioToolStripMenuItem.Tag = "7";
            this.fioToolStripMenuItem.Text = "Finalidad";
            this.fioToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // funciónToolStripMenuItem1
            // 
            this.funciónToolStripMenuItem1.Name = "funciónToolStripMenuItem1";
            this.funciónToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.funciónToolStripMenuItem1.Tag = "8";
            this.funciónToolStripMenuItem1.Text = "Función";
            this.funciónToolStripMenuItem1.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // subfunciónToolStripMenuItem1
            // 
            this.subfunciónToolStripMenuItem1.Name = "subfunciónToolStripMenuItem1";
            this.subfunciónToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.subfunciónToolStripMenuItem1.Tag = "9";
            this.subfunciónToolStripMenuItem1.Text = "Subfunción";
            this.subfunciónToolStripMenuItem1.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // unidadResponsableURToolStripMenuItem
            // 
            this.unidadResponsableURToolStripMenuItem.Name = "unidadResponsableURToolStripMenuItem";
            this.unidadResponsableURToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.unidadResponsableURToolStripMenuItem.Tag = "3";
            this.unidadResponsableURToolStripMenuItem.Text = "Unidad responsable (UR)";
            this.unidadResponsableURToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // fuentesDeFinanciamientoORToolStripMenuItem
            // 
            this.fuentesDeFinanciamientoORToolStripMenuItem.Name = "fuentesDeFinanciamientoORToolStripMenuItem";
            this.fuentesDeFinanciamientoORToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.fuentesDeFinanciamientoORToolStripMenuItem.Tag = "4";
            this.fuentesDeFinanciamientoORToolStripMenuItem.Text = "Fuentes de financiamiento (OR)";
            this.fuentesDeFinanciamientoORToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // programasToolStripMenuItem
            // 
            this.programasToolStripMenuItem.Name = "programasToolStripMenuItem";
            this.programasToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.programasToolStripMenuItem.Tag = "5";
            this.programasToolStripMenuItem.Text = "Programas";
            this.programasToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // clasificToolStripMenuItem
            // 
            this.clasificToolStripMenuItem.Name = "clasificToolStripMenuItem";
            this.clasificToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.clasificToolStripMenuItem.Tag = "6";
            this.clasificToolStripMenuItem.Text = "Clasificación objeto del gasto (COG)";
            this.clasificToolStripMenuItem.Click += new System.EventHandler(this.Clic_MenuCatalogo);
            // 
            // timerAlerta
            // 
            this.timerAlerta.Interval = 500;
            this.timerAlerta.Tick += new System.EventHandler(this.timerAlerta_Tick);
            // 
            // FormImportarCatalogos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 286);
            this.Controls.Add(this.panelProgressBar);
            this.Controls.Add(this.buttonImportar);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.groupBoxOpciones);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormImportarCatalogos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importación de catálogos";
            this.Load += new System.EventHandler(this.FormImportarCatalogoFinalidad_Load);
            this.groupBoxOpciones.ResumeLayout(false);
            this.groupBoxOpciones.PerformLayout();
            this.groupBoxPropiedades.ResumeLayout(false);
            this.groupBoxPropiedades.PerformLayout();
            this.panelProgressBar.ResumeLayout(false);
            this.panelProgressBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOpciones;
        private System.Windows.Forms.ComboBox comboBoxHoja;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxRuta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonImportar;
        private System.Windows.Forms.OpenFileDialog openFileDialogRuta;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBoxPropiedades;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxColumna;
        private System.Windows.Forms.TextBox textBoxRenglon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem catálogosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiposDeCuentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clasificaciónFuncionalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unidadResponsableURToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuentesDeFinanciamientoORToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clasificToolStripMenuItem;
        private System.Windows.Forms.Timer timerAlerta;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panelProgressBar;
        private System.Windows.Forms.PictureBox pictureBoxProgress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelRegistrosAlmacenados;
        private System.Windows.Forms.Label labelRegistrosProcesados;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem fioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subfunciónToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem funciónToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem géneroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grupoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rubroToolStripMenuItem;
    }
}