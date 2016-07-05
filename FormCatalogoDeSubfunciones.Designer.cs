namespace SIAPG
{
    partial class FormCatalogoDeSubfunciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCatalogoDeSubfunciones));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.treeViewConsulta = new System.Windows.Forms.TreeView();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.dataGridViewCatalogoFuncionalidad = new System.Windows.Forms.DataGridView();
            this.CodFinalidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodFuncion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subfuncion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonAgregar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxSubFuncion = new System.Windows.Forms.ComboBox();
            this.buttonEliminar = new System.Windows.Forms.Button();
            this.buttonModificar = new System.Windows.Forms.Button();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.textBoxConceptoSubfuncion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCodigoSubfuncion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxFuncion = new System.Windows.Forms.ComboBox();
            this.comboBoxFinalidad = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxDatosSubfuncion = new System.Windows.Forms.GroupBox();
            this.panelProgressBar = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBoxProgress = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCatalogoFuncionalidad)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panelProgressBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewConsulta
            // 
            this.treeViewConsulta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewConsulta.ImageIndex = 0;
            this.treeViewConsulta.ImageList = this.imageListIcons;
            this.treeViewConsulta.LineColor = System.Drawing.Color.SteelBlue;
            this.treeViewConsulta.Location = new System.Drawing.Point(0, 2);
            this.treeViewConsulta.Name = "treeViewConsulta";
            this.treeViewConsulta.SelectedImageIndex = 9;
            this.treeViewConsulta.ShowNodeToolTips = true;
            this.treeViewConsulta.Size = new System.Drawing.Size(270, 364);
            this.treeViewConsulta.TabIndex = 0;
            this.treeViewConsulta.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewConsulta_AfterSelect);
            this.treeViewConsulta.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewConsulta_NodeMouseClick);
            this.treeViewConsulta.Click += new System.EventHandler(this.treeViewConsulta_Click);
            // 
            // imageListIcons
            // 
            this.imageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcons.Images.SetKeyName(0, "Acuerdos.gif");
            this.imageListIcons.Images.SetKeyName(1, "Documento.gif");
            this.imageListIcons.Images.SetKeyName(2, "Expediente.gif");
            this.imageListIcons.Images.SetKeyName(3, "Expediente2.gif");
            this.imageListIcons.Images.SetKeyName(4, "Folios.gif");
            this.imageListIcons.Images.SetKeyName(5, "KeyClose.gif");
            this.imageListIcons.Images.SetKeyName(6, "KeyOpen.gif");
            this.imageListIcons.Images.SetKeyName(7, "ViñetaFlecha.gif");
            this.imageListIcons.Images.SetKeyName(8, "ViñetaPunto.gif");
            this.imageListIcons.Images.SetKeyName(9, "Edicion.gif");
            this.imageListIcons.Images.SetKeyName(10, "Documento2.gif");
            // 
            // dataGridViewCatalogoFuncionalidad
            // 
            this.dataGridViewCatalogoFuncionalidad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCatalogoFuncionalidad.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCatalogoFuncionalidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCatalogoFuncionalidad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodFinalidad,
            this.CodFuncion,
            this.Cod,
            this.subfuncion});
            this.dataGridViewCatalogoFuncionalidad.Location = new System.Drawing.Point(276, 281);
            this.dataGridViewCatalogoFuncionalidad.Name = "dataGridViewCatalogoFuncionalidad";
            this.dataGridViewCatalogoFuncionalidad.Size = new System.Drawing.Size(614, 174);
            this.dataGridViewCatalogoFuncionalidad.TabIndex = 1;
            this.dataGridViewCatalogoFuncionalidad.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCatalogoFuncionalidad_CellMouseClick);
            // 
            // CodFinalidad
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CodFinalidad.DefaultCellStyle = dataGridViewCellStyle2;
            this.CodFinalidad.HeaderText = "Código Finalidad";
            this.CodFinalidad.Name = "CodFinalidad";
            this.CodFinalidad.ReadOnly = true;
            this.CodFinalidad.Width = 80;
            // 
            // CodFuncion
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CodFuncion.DefaultCellStyle = dataGridViewCellStyle3;
            this.CodFuncion.HeaderText = "Código Función";
            this.CodFuncion.Name = "CodFuncion";
            this.CodFuncion.ReadOnly = true;
            this.CodFuncion.Width = 80;
            // 
            // Cod
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Cod.DefaultCellStyle = dataGridViewCellStyle4;
            this.Cod.HeaderText = "Código Subfunción";
            this.Cod.Name = "Cod";
            this.Cod.ReadOnly = true;
            this.Cod.Width = 80;
            // 
            // subfuncion
            // 
            this.subfuncion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.subfuncion.HeaderText = "Subfunción";
            this.subfuncion.Name = "subfuncion";
            this.subfuncion.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonCancelar);
            this.groupBox1.Controls.Add(this.buttonAgregar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBoxSubFuncion);
            this.groupBox1.Controls.Add(this.buttonEliminar);
            this.groupBox1.Controls.Add(this.buttonModificar);
            this.groupBox1.Controls.Add(this.buttonGuardar);
            this.groupBox1.Controls.Add(this.textBoxConceptoSubfuncion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxCodigoSubfuncion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboBoxFuncion);
            this.groupBox1.Controls.Add(this.comboBoxFinalidad);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBoxDatosSubfuncion);
            this.groupBox1.Location = new System.Drawing.Point(276, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 263);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos subfunción";
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancelar.Enabled = false;
            this.buttonCancelar.Location = new System.Drawing.Point(269, 225);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelar.TabIndex = 17;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // buttonAgregar
            // 
            this.buttonAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAgregar.Enabled = false;
            this.buttonAgregar.Location = new System.Drawing.Point(145, 225);
            this.buttonAgregar.Name = "buttonAgregar";
            this.buttonAgregar.Size = new System.Drawing.Size(75, 23);
            this.buttonAgregar.TabIndex = 16;
            this.buttonAgregar.Text = "Agregar";
            this.buttonAgregar.UseVisualStyleBackColor = true;
            this.buttonAgregar.Click += new System.EventHandler(this.buttonAgregar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(61, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Subfunción";
            // 
            // comboBoxSubFuncion
            // 
            this.comboBoxSubFuncion.Enabled = false;
            this.comboBoxSubFuncion.FormattingEnabled = true;
            this.comboBoxSubFuncion.Location = new System.Drawing.Point(139, 84);
            this.comboBoxSubFuncion.Name = "comboBoxSubFuncion";
            this.comboBoxSubFuncion.Size = new System.Drawing.Size(456, 21);
            this.comboBoxSubFuncion.TabIndex = 13;
            this.comboBoxSubFuncion.SelectionChangeCommitted += new System.EventHandler(this.comboBoxSubFuncion_SelectionChangeCommitted);
            // 
            // buttonEliminar
            // 
            this.buttonEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonEliminar.Enabled = false;
            this.buttonEliminar.Location = new System.Drawing.Point(21, 225);
            this.buttonEliminar.Name = "buttonEliminar";
            this.buttonEliminar.Size = new System.Drawing.Size(75, 23);
            this.buttonEliminar.TabIndex = 12;
            this.buttonEliminar.Text = "Eliminar...";
            this.buttonEliminar.UseVisualStyleBackColor = true;
            this.buttonEliminar.Click += new System.EventHandler(this.buttonCerrar_Click);
            // 
            // buttonModificar
            // 
            this.buttonModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonModificar.Enabled = false;
            this.buttonModificar.Location = new System.Drawing.Point(393, 225);
            this.buttonModificar.Name = "buttonModificar";
            this.buttonModificar.Size = new System.Drawing.Size(75, 23);
            this.buttonModificar.TabIndex = 11;
            this.buttonModificar.Text = "Modificar";
            this.buttonModificar.UseVisualStyleBackColor = true;
            this.buttonModificar.Click += new System.EventHandler(this.buttonModificar_Click);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGuardar.Enabled = false;
            this.buttonGuardar.Location = new System.Drawing.Point(517, 225);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(75, 23);
            this.buttonGuardar.TabIndex = 10;
            this.buttonGuardar.Text = "Guardar";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // textBoxConceptoSubfuncion
            // 
            this.textBoxConceptoSubfuncion.Enabled = false;
            this.textBoxConceptoSubfuncion.Location = new System.Drawing.Point(139, 162);
            this.textBoxConceptoSubfuncion.Multiline = true;
            this.textBoxConceptoSubfuncion.Name = "textBoxConceptoSubfuncion";
            this.textBoxConceptoSubfuncion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxConceptoSubfuncion.Size = new System.Drawing.Size(439, 40);
            this.textBoxConceptoSubfuncion.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sunfunción";
            // 
            // textBoxCodigoSubfuncion
            // 
            this.textBoxCodigoSubfuncion.Enabled = false;
            this.textBoxCodigoSubfuncion.Location = new System.Drawing.Point(139, 136);
            this.textBoxCodigoSubfuncion.Name = "textBoxCodigoSubfuncion";
            this.textBoxCodigoSubfuncion.Size = new System.Drawing.Size(108, 20);
            this.textBoxCodigoSubfuncion.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Código subfunción";
            // 
            // comboBoxFuncion
            // 
            this.comboBoxFuncion.Enabled = false;
            this.comboBoxFuncion.FormattingEnabled = true;
            this.comboBoxFuncion.Location = new System.Drawing.Point(139, 57);
            this.comboBoxFuncion.Name = "comboBoxFuncion";
            this.comboBoxFuncion.Size = new System.Drawing.Size(456, 21);
            this.comboBoxFuncion.TabIndex = 4;
            this.comboBoxFuncion.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFuncion_SelectionChangeCommitted);
            // 
            // comboBoxFinalidad
            // 
            this.comboBoxFinalidad.FormattingEnabled = true;
            this.comboBoxFinalidad.Location = new System.Drawing.Point(139, 27);
            this.comboBoxFinalidad.Name = "comboBoxFinalidad";
            this.comboBoxFinalidad.Size = new System.Drawing.Size(456, 21);
            this.comboBoxFinalidad.TabIndex = 3;
            this.comboBoxFinalidad.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFinalidad_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Función";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Finalidad";
            // 
            // groupBoxDatosSubfuncion
            // 
            this.groupBoxDatosSubfuncion.Enabled = false;
            this.groupBoxDatosSubfuncion.Location = new System.Drawing.Point(21, 126);
            this.groupBoxDatosSubfuncion.Name = "groupBoxDatosSubfuncion";
            this.groupBoxDatosSubfuncion.Size = new System.Drawing.Size(574, 93);
            this.groupBoxDatosSubfuncion.TabIndex = 15;
            this.groupBoxDatosSubfuncion.TabStop = false;
            // 
            // panelProgressBar
            // 
            this.panelProgressBar.BackColor = System.Drawing.Color.White;
            this.panelProgressBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProgressBar.Controls.Add(this.label7);
            this.panelProgressBar.Controls.Add(this.pictureBoxProgress);
            this.panelProgressBar.Location = new System.Drawing.Point(12, 190);
            this.panelProgressBar.Name = "panelProgressBar";
            this.panelProgressBar.Size = new System.Drawing.Size(266, 85);
            this.panelProgressBar.TabIndex = 8;
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
            this.label7.Text = "Procesando ....";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 372);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(270, 34);
            this.textBox1.TabIndex = 0;
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
            // FormCatalogoDeSubfunciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 456);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panelProgressBar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewCatalogoFuncionalidad);
            this.Controls.Add(this.treeViewConsulta);
            this.Name = "FormCatalogoDeSubfunciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálogo Subfunciones";
            this.Load += new System.EventHandler(this.FormCatalogoDeSubfunciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCatalogoFuncionalidad)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelProgressBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewConsulta;
        private System.Windows.Forms.DataGridView dataGridViewCatalogoFuncionalidad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonEliminar;
        private System.Windows.Forms.Button buttonModificar;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.TextBox textBoxConceptoSubfuncion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCodigoSubfuncion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxFuncion;
        private System.Windows.Forms.ComboBox comboBoxFinalidad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelProgressBar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBoxProgress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxSubFuncion;
        private System.Windows.Forms.GroupBox groupBoxDatosSubfuncion;
        private System.Windows.Forms.Button buttonAgregar;
        private System.Windows.Forms.ImageList imageListIcons;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn subfuncion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cod;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodFuncion;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodFinalidad;
    }
}