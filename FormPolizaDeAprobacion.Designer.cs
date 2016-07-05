namespace SIAPG
{
    partial class FormPolizaDeAprobacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPolizaDeAprobacion));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageListOpciones = new System.Windows.Forms.ImageList(this.components);
            this.panelopciones = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.GridviewCargaPresupuestal = new System.Windows.Forms.DataGridView();
            this.CodigoCOG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PresupuestoAsignado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_CUENTA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelProceso = new System.Windows.Forms.Panel();
            this.labelFinal = new System.Windows.Forms.Label();
            this.labelInicial = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBarProceso = new System.Windows.Forms.ProgressBar();
            this.panelopciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridviewCargaPresupuestal)).BeginInit();
            this.panelProceso.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListOpciones
            // 
            this.imageListOpciones.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListOpciones.ImageStream")));
            this.imageListOpciones.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListOpciones.Images.SetKeyName(0, "Abrir.png");
            this.imageListOpciones.Images.SetKeyName(1, "Eliminar.png");
            this.imageListOpciones.Images.SetKeyName(2, "Imprimir.png");
            this.imageListOpciones.Images.SetKeyName(3, "Exportar.png");
            this.imageListOpciones.Images.SetKeyName(4, "Generar.png");
            // 
            // panelopciones
            // 
            this.panelopciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(170)))), ((int)(((byte)(250)))));
            this.panelopciones.Controls.Add(this.button5);
            this.panelopciones.Controls.Add(this.buttonGuardar);
            this.panelopciones.Controls.Add(this.button2);
            this.panelopciones.Controls.Add(this.button1);
            this.panelopciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelopciones.Location = new System.Drawing.Point(0, 0);
            this.panelopciones.Name = "panelopciones";
            this.panelopciones.Size = new System.Drawing.Size(674, 43);
            this.panelopciones.TabIndex = 19;
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.ImageIndex = 2;
            this.button5.ImageList = this.imageListOpciones;
            this.button5.Location = new System.Drawing.Point(426, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(85, 28);
            this.button5.TabIndex = 15;
            this.button5.Text = "       Imprmir";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonGuardar.FlatAppearance.BorderSize = 0;
            this.buttonGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.buttonGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGuardar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGuardar.ImageIndex = 4;
            this.buttonGuardar.ImageList = this.imageListOpciones;
            this.buttonGuardar.Location = new System.Drawing.Point(177, 12);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(85, 28);
            this.buttonGuardar.TabIndex = 14;
            this.buttonGuardar.Text = "       Generar...";
            this.buttonGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.ImageIndex = 1;
            this.button2.ImageList = this.imageListOpciones;
            this.button2.Location = new System.Drawing.Point(311, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 28);
            this.button2.TabIndex = 13;
            this.button2.Text = "       Eliminar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageIndex = 0;
            this.button1.ImageList = this.imageListOpciones;
            this.button1.Location = new System.Drawing.Point(33, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "       Presupuesto";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.GridviewCargaPresupuestal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoCOG,
            this.COG,
            this.PresupuestoAsignado,
            this.ID_CUENTA});
            this.GridviewCargaPresupuestal.EnableHeadersVisualStyles = false;
            this.GridviewCargaPresupuestal.Location = new System.Drawing.Point(0, 61);
            this.GridviewCargaPresupuestal.Name = "GridviewCargaPresupuestal";
            this.GridviewCargaPresupuestal.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.GridviewCargaPresupuestal.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.GridviewCargaPresupuestal.RowHeadersWidth = 60;
            this.GridviewCargaPresupuestal.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GridviewCargaPresupuestal.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.GridviewCargaPresupuestal.Size = new System.Drawing.Size(671, 450);
            this.GridviewCargaPresupuestal.TabIndex = 20;
            this.GridviewCargaPresupuestal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridviewCargaPresupuestal_CellContentClick);
            // 
            // CodigoCOG
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CodigoCOG.DefaultCellStyle = dataGridViewCellStyle2;
            this.CodigoCOG.HeaderText = "CODIGO";
            this.CodigoCOG.Name = "CodigoCOG";
            // 
            // COG
            // 
            this.COG.HeaderText = "COG";
            this.COG.Name = "COG";
            this.COG.Width = 350;
            // 
            // PresupuestoAsignado
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.PresupuestoAsignado.DefaultCellStyle = dataGridViewCellStyle3;
            this.PresupuestoAsignado.HeaderText = "MONTO";
            this.PresupuestoAsignado.Name = "PresupuestoAsignado";
            this.PresupuestoAsignado.Width = 140;
            // 
            // ID_CUENTA
            // 
            this.ID_CUENTA.HeaderText = "ID_CUENTA";
            this.ID_CUENTA.Name = "ID_CUENTA";
            this.ID_CUENTA.Visible = false;
            // 
            // panelProceso
            // 
            this.panelProceso.BackColor = System.Drawing.Color.LightGray;
            this.panelProceso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProceso.Controls.Add(this.labelFinal);
            this.panelProceso.Controls.Add(this.labelInicial);
            this.panelProceso.Controls.Add(this.label1);
            this.panelProceso.Controls.Add(this.progressBarProceso);
            this.panelProceso.Location = new System.Drawing.Point(163, 205);
            this.panelProceso.Name = "panelProceso";
            this.panelProceso.Size = new System.Drawing.Size(349, 101);
            this.panelProceso.TabIndex = 21;
            // 
            // labelFinal
            // 
            this.labelFinal.AutoSize = true;
            this.labelFinal.Location = new System.Drawing.Point(314, 50);
            this.labelFinal.Name = "labelFinal";
            this.labelFinal.Size = new System.Drawing.Size(21, 13);
            this.labelFinal.TabIndex = 16;
            this.labelFinal.Text = "No";
            this.labelFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelInicial
            // 
            this.labelInicial.Location = new System.Drawing.Point(3, 47);
            this.labelInicial.Name = "labelInicial";
            this.labelInicial.Size = new System.Drawing.Size(33, 18);
            this.labelInicial.TabIndex = 15;
            this.labelInicial.Text = "0";
            this.labelInicial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "   Procesado...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBarProceso
            // 
            this.progressBarProceso.Location = new System.Drawing.Point(43, 45);
            this.progressBarProceso.Name = "progressBarProceso";
            this.progressBarProceso.Size = new System.Drawing.Size(266, 23);
            this.progressBarProceso.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarProceso.TabIndex = 13;
            // 
            // FormPolizaDeAprobacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 511);
            this.Controls.Add(this.panelProceso);
            this.Controls.Add(this.GridviewCargaPresupuestal);
            this.Controls.Add(this.panelopciones);
            this.Name = "FormPolizaDeAprobacion";
            this.Text = "Concentrado COG";
            this.MaximumSizeChanged += new System.EventHandler(this.FormPolizaDeAprobacion_MaximumSizeChanged);
            this.MinimumSizeChanged += new System.EventHandler(this.FormPolizaDeAprobacion_MinimumSizeChanged);
            this.Load += new System.EventHandler(this.FormPolizaDeAprobacion_Load);
            this.ResizeEnd += new System.EventHandler(this.FormPolizaDeAprobacion_ResizeEnd);
            this.panelopciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridviewCargaPresupuestal)).EndInit();
            this.panelProceso.ResumeLayout(false);
            this.panelProceso.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListOpciones;
        private System.Windows.Forms.Panel panelopciones;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView GridviewCargaPresupuestal;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoCOG;
        private System.Windows.Forms.DataGridViewTextBoxColumn COG;
        private System.Windows.Forms.DataGridViewTextBoxColumn PresupuestoAsignado;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_CUENTA;
        private System.Windows.Forms.Panel panelProceso;
        private System.Windows.Forms.Label labelFinal;
        private System.Windows.Forms.Label labelInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBarProceso;
    }
}