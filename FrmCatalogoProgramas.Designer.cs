namespace SIAPG
{
    partial class FrmCatalogoProgramas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCatalogoProgramas));
            this.treeViewProgramas = new System.Windows.Forms.TreeView();
            this.GroupPrograma = new System.Windows.Forms.GroupBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProgramaN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Boton_Cargar = new System.Windows.Forms.Button();
            this.comboPrograma = new System.Windows.Forms.ComboBox();
            this.comboTipoGasto = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IdTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdUR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Programa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.panelProgressBar = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBoxProgress = new System.Windows.Forms.PictureBox();
            this.TxtNodoTag = new System.Windows.Forms.TextBox();
            this.btnEliminarUr = new System.Windows.Forms.Button();
            this.btnModificarUR = new System.Windows.Forms.Button();
            this.btnAgregarNuevoPrograma = new System.Windows.Forms.Button();
            this.groupNProgramas = new System.Windows.Forms.GroupBox();
            this.groupComandos = new System.Windows.Forms.GroupBox();
            this.GroupPrograma.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelProgressBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).BeginInit();
            this.groupNProgramas.SuspendLayout();
            this.groupComandos.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewProgramas
            // 
            this.treeViewProgramas.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewProgramas.Location = new System.Drawing.Point(12, 12);
            this.treeViewProgramas.Name = "treeViewProgramas";
            this.treeViewProgramas.Size = new System.Drawing.Size(289, 429);
            this.treeViewProgramas.TabIndex = 0;
            this.treeViewProgramas.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewProgramas_AfterSelect);
            // 
            // GroupPrograma
            // 
            this.GroupPrograma.Controls.Add(this.btnAgregar);
            this.GroupPrograma.Controls.Add(this.button1);
            this.GroupPrograma.Controls.Add(this.label3);
            this.GroupPrograma.Controls.Add(this.txtProgramaN);
            this.GroupPrograma.Controls.Add(this.label2);
            this.GroupPrograma.Controls.Add(this.label1);
            this.GroupPrograma.Controls.Add(this.Boton_Cargar);
            this.GroupPrograma.Controls.Add(this.comboPrograma);
            this.GroupPrograma.Controls.Add(this.comboTipoGasto);
            this.GroupPrograma.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupPrograma.Location = new System.Drawing.Point(315, 49);
            this.GroupPrograma.Name = "GroupPrograma";
            this.GroupPrograma.Size = new System.Drawing.Size(510, 217);
            this.GroupPrograma.TabIndex = 2;
            this.GroupPrograma.TabStop = false;
            this.GroupPrograma.Text = "Datos de Programas:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.ImageIndex = 9;
            this.btnAgregar.ImageList = this.imageListIcons;
            this.btnAgregar.Location = new System.Drawing.Point(398, 190);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(82, 23);
            this.btnAgregar.TabIndex = 21;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // imageListIcons
            // 
            this.imageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcons.Images.SetKeyName(0, "NIVEL0.png");
            this.imageListIcons.Images.SetKeyName(1, "NIVEL1.png");
            this.imageListIcons.Images.SetKeyName(2, "NIVEL2.png");
            this.imageListIcons.Images.SetKeyName(3, "NIVEL3.png");
            this.imageListIcons.Images.SetKeyName(4, "NIVEL4.png");
            this.imageListIcons.Images.SetKeyName(5, "NIVEL5.png");
            this.imageListIcons.Images.SetKeyName(6, "MenConAgregar.png");
            this.imageListIcons.Images.SetKeyName(7, "editar 20x20.png");
            this.imageListIcons.Images.SetKeyName(8, "MenConEliminar.png");
            this.imageListIcons.Images.SetKeyName(9, "agregar.png");
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::SIAPG.Properties.Resources.MenConAgregar;
            this.button1.Location = new System.Drawing.Point(486, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 21);
            this.button1.TabIndex = 19;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 33);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nombre del Programa: \r\nUR por defecto";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProgramaN
            // 
            this.txtProgramaN.Location = new System.Drawing.Point(131, 163);
            this.txtProgramaN.Name = "txtProgramaN";
            this.txtProgramaN.Size = new System.Drawing.Size(349, 21);
            this.txtProgramaN.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 33);
            this.label2.TabIndex = 13;
            this.label2.Text = "Selecciona la UR del Programa: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tipo de Programa: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Boton_Cargar
            // 
            this.Boton_Cargar.AutoSize = true;
            this.Boton_Cargar.FlatAppearance.BorderSize = 0;
            this.Boton_Cargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Boton_Cargar.Image = global::SIAPG.Properties.Resources.MenConAgregar;
            this.Boton_Cargar.Location = new System.Drawing.Point(486, 22);
            this.Boton_Cargar.Name = "Boton_Cargar";
            this.Boton_Cargar.Size = new System.Drawing.Size(21, 21);
            this.Boton_Cargar.TabIndex = 2;
            this.Boton_Cargar.UseVisualStyleBackColor = true;
            this.Boton_Cargar.Click += new System.EventHandler(this.Boton_Cargar_Click_1);
            // 
            // comboPrograma
            // 
            this.comboPrograma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboPrograma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboPrograma.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPrograma.FormattingEnabled = true;
            this.comboPrograma.Location = new System.Drawing.Point(117, 50);
            this.comboPrograma.Name = "comboPrograma";
            this.comboPrograma.Size = new System.Drawing.Size(363, 112);
            this.comboPrograma.TabIndex = 1;
            // 
            // comboTipoGasto
            // 
            this.comboTipoGasto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboTipoGasto.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTipoGasto.FormattingEnabled = true;
            this.comboTipoGasto.Location = new System.Drawing.Point(117, 23);
            this.comboTipoGasto.Name = "comboTipoGasto";
            this.comboTipoGasto.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboTipoGasto.Size = new System.Drawing.Size(363, 21);
            this.comboTipoGasto.TabIndex = 0;
            this.comboTipoGasto.SelectedIndexChanged += new System.EventHandler(this.comboTipoGasto_SelectedIndexChanged);
            this.comboTipoGasto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboTipoGasto_KeyPress);
            this.comboTipoGasto.Leave += new System.EventHandler(this.comboTipoGasto_Leave);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdTipo,
            this.IdUR,
            this.Programa});
            this.dataGridView1.Location = new System.Drawing.Point(17, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(464, 124);
            this.dataGridView1.TabIndex = 19;
            // 
            // IdTipo
            // 
            this.IdTipo.HeaderText = "Tipo Programa";
            this.IdTipo.Name = "IdTipo";
            // 
            // IdUR
            // 
            this.IdUR.HeaderText = "UR";
            this.IdUR.Name = "IdUR";
            // 
            // Programa
            // 
            this.Programa.HeaderText = "Programa";
            this.Programa.Name = "Programa";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(176, 19);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 17;
            this.btnLimpiar.Text = "&Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(95, 19);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 16;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(16, 19);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 15;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // panelProgressBar
            // 
            this.panelProgressBar.BackColor = System.Drawing.Color.White;
            this.panelProgressBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProgressBar.Controls.Add(this.label7);
            this.panelProgressBar.Controls.Add(this.pictureBoxProgress);
            this.panelProgressBar.Location = new System.Drawing.Point(35, 356);
            this.panelProgressBar.Name = "panelProgressBar";
            this.panelProgressBar.Size = new System.Drawing.Size(266, 85);
            this.panelProgressBar.TabIndex = 9;
            this.panelProgressBar.Visible = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.HotTrack;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(258, 21);
            this.label7.TabIndex = 1;
            this.label7.Text = "Procesando desde la base...";
            // 
            // pictureBoxProgress
            // 
            this.pictureBoxProgress.BackColor = System.Drawing.Color.White;
            this.pictureBoxProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxProgress.Enabled = false;
            this.pictureBoxProgress.Image = global::SIAPG.Properties.Resources.GifProcesingBarGray;
            this.pictureBoxProgress.Location = new System.Drawing.Point(12, 39);
            this.pictureBoxProgress.Name = "pictureBoxProgress";
            this.pictureBoxProgress.Size = new System.Drawing.Size(240, 25);
            this.pictureBoxProgress.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxProgress.TabIndex = 0;
            this.pictureBoxProgress.TabStop = false;
            // 
            // TxtNodoTag
            // 
            this.TxtNodoTag.Location = new System.Drawing.Point(12, 447);
            this.TxtNodoTag.Name = "TxtNodoTag";
            this.TxtNodoTag.Size = new System.Drawing.Size(289, 20);
            this.TxtNodoTag.TabIndex = 11;
            // 
            // btnEliminarUr
            // 
            this.btnEliminarUr.FlatAppearance.BorderSize = 0;
            this.btnEliminarUr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarUr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminarUr.ImageKey = "MenConEliminar.png";
            this.btnEliminarUr.ImageList = this.imageListIcons;
            this.btnEliminarUr.Location = new System.Drawing.Point(574, 12);
            this.btnEliminarUr.Name = "btnEliminarUr";
            this.btnEliminarUr.Size = new System.Drawing.Size(125, 26);
            this.btnEliminarUr.TabIndex = 14;
            this.btnEliminarUr.Text = "Eliminar Programa";
            this.btnEliminarUr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminarUr.UseVisualStyleBackColor = true;
            // 
            // btnModificarUR
            // 
            this.btnModificarUR.FlatAppearance.BorderSize = 0;
            this.btnModificarUR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarUR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificarUR.ImageKey = "editar 20x20.png";
            this.btnModificarUR.ImageList = this.imageListIcons;
            this.btnModificarUR.Location = new System.Drawing.Point(443, 12);
            this.btnModificarUR.Name = "btnModificarUR";
            this.btnModificarUR.Size = new System.Drawing.Size(125, 26);
            this.btnModificarUR.TabIndex = 13;
            this.btnModificarUR.Text = "Modificar Programa";
            this.btnModificarUR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModificarUR.UseVisualStyleBackColor = true;
            // 
            // btnAgregarNuevoPrograma
            // 
            this.btnAgregarNuevoPrograma.FlatAppearance.BorderSize = 0;
            this.btnAgregarNuevoPrograma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarNuevoPrograma.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarNuevoPrograma.ImageKey = "MenConAgregar.png";
            this.btnAgregarNuevoPrograma.ImageList = this.imageListIcons;
            this.btnAgregarNuevoPrograma.Location = new System.Drawing.Point(312, 12);
            this.btnAgregarNuevoPrograma.Name = "btnAgregarNuevoPrograma";
            this.btnAgregarNuevoPrograma.Size = new System.Drawing.Size(125, 26);
            this.btnAgregarNuevoPrograma.TabIndex = 12;
            this.btnAgregarNuevoPrograma.Text = "Nuevo Programa";
            this.btnAgregarNuevoPrograma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarNuevoPrograma.UseVisualStyleBackColor = true;
            this.btnAgregarNuevoPrograma.Click += new System.EventHandler(this.btnAgregarNuevoPrograma_Click);
            // 
            // groupNProgramas
            // 
            this.groupNProgramas.Controls.Add(this.dataGridView1);
            this.groupNProgramas.Location = new System.Drawing.Point(315, 272);
            this.groupNProgramas.Name = "groupNProgramas";
            this.groupNProgramas.Size = new System.Drawing.Size(511, 156);
            this.groupNProgramas.TabIndex = 20;
            this.groupNProgramas.TabStop = false;
            this.groupNProgramas.Text = "Agregar Nuevos Programas: ";
            // 
            // groupComandos
            // 
            this.groupComandos.Controls.Add(this.btnLimpiar);
            this.groupComandos.Controls.Add(this.btnCancelar);
            this.groupComandos.Controls.Add(this.btnAceptar);
            this.groupComandos.Location = new System.Drawing.Point(566, 434);
            this.groupComandos.Name = "groupComandos";
            this.groupComandos.Size = new System.Drawing.Size(260, 53);
            this.groupComandos.TabIndex = 21;
            this.groupComandos.TabStop = false;
            this.groupComandos.Text = "Comandos: ";
            // 
            // FrmCatalogoProgramas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 528);
            this.Controls.Add(this.groupComandos);
            this.Controls.Add(this.groupNProgramas);
            this.Controls.Add(this.btnEliminarUr);
            this.Controls.Add(this.btnModificarUR);
            this.Controls.Add(this.btnAgregarNuevoPrograma);
            this.Controls.Add(this.panelProgressBar);
            this.Controls.Add(this.GroupPrograma);
            this.Controls.Add(this.treeViewProgramas);
            this.Controls.Add(this.TxtNodoTag);
            this.Name = "FrmCatalogoProgramas";
            this.Text = "Catálogo de Programas";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.GroupPrograma.ResumeLayout(false);
            this.GroupPrograma.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelProgressBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).EndInit();
            this.groupNProgramas.ResumeLayout(false);
            this.groupComandos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewProgramas;
        private System.Windows.Forms.GroupBox GroupPrograma;
        private System.Windows.Forms.ComboBox comboPrograma;
        private System.Windows.Forms.ComboBox comboTipoGasto;
        private System.Windows.Forms.Button Boton_Cargar;
        private System.Windows.Forms.Panel panelProgressBar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBoxProgress;
        private System.Windows.Forms.TextBox TxtNodoTag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEliminarUr;
        private System.Windows.Forms.Button btnModificarUR;
        private System.Windows.Forms.Button btnAgregarNuevoPrograma;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ImageList imageListIcons;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProgramaN;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdUR;
        private System.Windows.Forms.DataGridViewTextBoxColumn Programa;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupNProgramas;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox groupComandos;
    }
}