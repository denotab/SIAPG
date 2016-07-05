using System;

namespace SIAPG
{
    partial class FrmCatalogoUR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCatalogoUR));
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.treeViewCatUR = new System.Windows.Forms.TreeView();
            this.btnAgregarNuevaUR = new System.Windows.Forms.Button();
            this.groupAgregarNuevaUR = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdUr3 = new System.Windows.Forms.TextBox();
            this.txtIdUr2 = new System.Windows.Forms.TextBox();
            this.txtIdUr1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtId_URPadre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPadreURNueva = new System.Windows.Forms.Label();
            this.txtURN = new System.Windows.Forms.TextBox();
            this.lblNuevaUR = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblURPadre = new System.Windows.Forms.Label();
            this.TxtNodoTag = new System.Windows.Forms.TextBox();
            this.btnModificarUR = new System.Windows.Forms.Button();
            this.btnEliminarUr = new System.Windows.Forms.Button();
            this.groupAgregarNuevaUR.SuspendLayout();
            this.SuspendLayout();
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
            // 
            // treeViewCatUR
            // 
            this.treeViewCatUR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewCatUR.HideSelection = false;
            this.treeViewCatUR.HotTracking = true;
            this.treeViewCatUR.Location = new System.Drawing.Point(5, 12);
            this.treeViewCatUR.Name = "treeViewCatUR";
            this.treeViewCatUR.Size = new System.Drawing.Size(484, 350);
            this.treeViewCatUR.TabIndex = 0;
            this.treeViewCatUR.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCatUR_AfterSelect);
            // 
            // btnAgregarNuevaUR
            // 
            this.btnAgregarNuevaUR.FlatAppearance.BorderSize = 0;
            this.btnAgregarNuevaUR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarNuevaUR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarNuevaUR.ImageKey = "MenConAgregar.png";
            this.btnAgregarNuevaUR.ImageList = this.imageListIcons;
            this.btnAgregarNuevaUR.Location = new System.Drawing.Point(495, 12);
            this.btnAgregarNuevaUR.Name = "btnAgregarNuevaUR";
            this.btnAgregarNuevaUR.Size = new System.Drawing.Size(125, 26);
            this.btnAgregarNuevaUR.TabIndex = 2;
            this.btnAgregarNuevaUR.Text = "Agregar Nueva UR";
            this.btnAgregarNuevaUR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarNuevaUR.UseVisualStyleBackColor = true;
            this.btnAgregarNuevaUR.Click += new System.EventHandler(this.btnAgregarNuevaUR_Click);
            // 
            // groupAgregarNuevaUR
            // 
            this.groupAgregarNuevaUR.Controls.Add(this.label5);
            this.groupAgregarNuevaUR.Controls.Add(this.label4);
            this.groupAgregarNuevaUR.Controls.Add(this.txtIdUr3);
            this.groupAgregarNuevaUR.Controls.Add(this.txtIdUr2);
            this.groupAgregarNuevaUR.Controls.Add(this.txtIdUr1);
            this.groupAgregarNuevaUR.Controls.Add(this.label2);
            this.groupAgregarNuevaUR.Controls.Add(this.txtId_URPadre);
            this.groupAgregarNuevaUR.Controls.Add(this.label1);
            this.groupAgregarNuevaUR.Controls.Add(this.lblPadreURNueva);
            this.groupAgregarNuevaUR.Controls.Add(this.txtURN);
            this.groupAgregarNuevaUR.Controls.Add(this.lblNuevaUR);
            this.groupAgregarNuevaUR.Controls.Add(this.btnLimpiar);
            this.groupAgregarNuevaUR.Controls.Add(this.btnCancelar);
            this.groupAgregarNuevaUR.Controls.Add(this.btnAceptar);
            this.groupAgregarNuevaUR.Controls.Add(this.lblURPadre);
            this.groupAgregarNuevaUR.Location = new System.Drawing.Point(495, 44);
            this.groupAgregarNuevaUR.Name = "groupAgregarNuevaUR";
            this.groupAgregarNuevaUR.Size = new System.Drawing.Size(413, 238);
            this.groupAgregarNuevaUR.TabIndex = 3;
            this.groupAgregarNuevaUR.TabStop = false;
            this.groupAgregarNuevaUR.Text = "Unidad Responsable: ";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(226, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(8, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = ".";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(197, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(8, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = ".";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtIdUr3
            // 
            this.txtIdUr3.Enabled = false;
            this.txtIdUr3.Location = new System.Drawing.Point(234, 92);
            this.txtIdUr3.MaxLength = 2;
            this.txtIdUr3.Name = "txtIdUr3";
            this.txtIdUr3.Size = new System.Drawing.Size(20, 20);
            this.txtIdUr3.TabIndex = 6;
            this.txtIdUr3.Enter += new System.EventHandler(this.txtIdUr3_Enter);
            this.txtIdUr3.Leave += new System.EventHandler(this.txtIdUr3_Leave);
            // 
            // txtIdUr2
            // 
            this.txtIdUr2.Enabled = false;
            this.txtIdUr2.Location = new System.Drawing.Point(206, 92);
            this.txtIdUr2.MaxLength = 2;
            this.txtIdUr2.Name = "txtIdUr2";
            this.txtIdUr2.Size = new System.Drawing.Size(20, 20);
            this.txtIdUr2.TabIndex = 5;
            this.txtIdUr2.TextChanged += new System.EventHandler(this.txtIdUr2_TextChanged);
            this.txtIdUr2.Enter += new System.EventHandler(this.txtIdUr2_Enter);
            this.txtIdUr2.Leave += new System.EventHandler(this.txtIdUr2_Leave);
            // 
            // txtIdUr1
            // 
            this.txtIdUr1.Enabled = false;
            this.txtIdUr1.Location = new System.Drawing.Point(176, 92);
            this.txtIdUr1.MaxLength = 2;
            this.txtIdUr1.Name = "txtIdUr1";
            this.txtIdUr1.Size = new System.Drawing.Size(20, 20);
            this.txtIdUr1.TabIndex = 4;
            this.txtIdUr1.TextChanged += new System.EventHandler(this.txtIdUr1_TextChanged);
            this.txtIdUr1.Enter += new System.EventHandler(this.txtIdUr1_Enter);
            this.txtIdUr1.Leave += new System.EventHandler(this.txtIdUr1_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Clave de UR Nueva:";
            // 
            // txtId_URPadre
            // 
            this.txtId_URPadre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtId_URPadre.Enabled = false;
            this.txtId_URPadre.Location = new System.Drawing.Point(9, 92);
            this.txtId_URPadre.Name = "txtId_URPadre";
            this.txtId_URPadre.Size = new System.Drawing.Size(157, 20);
            this.txtId_URPadre.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Clave de UR Padre:";
            // 
            // lblPadreURNueva
            // 
            this.lblPadreURNueva.BackColor = System.Drawing.SystemColors.Window;
            this.lblPadreURNueva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPadreURNueva.Location = new System.Drawing.Point(9, 43);
            this.lblPadreURNueva.Name = "lblPadreURNueva";
            this.lblPadreURNueva.Size = new System.Drawing.Size(398, 21);
            this.lblPadreURNueva.TabIndex = 10;
            this.lblPadreURNueva.Text = "lblPadreURNueva";
            this.lblPadreURNueva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtURN
            // 
            this.txtURN.Location = new System.Drawing.Point(9, 133);
            this.txtURN.Name = "txtURN";
            this.txtURN.Size = new System.Drawing.Size(398, 20);
            this.txtURN.TabIndex = 6;
            // 
            // lblNuevaUR
            // 
            this.lblNuevaUR.AutoSize = true;
            this.lblNuevaUR.Location = new System.Drawing.Point(6, 117);
            this.lblNuevaUR.Name = "lblNuevaUR";
            this.lblNuevaUR.Size = new System.Drawing.Size(95, 13);
            this.lblNuevaUR.TabIndex = 8;
            this.lblNuevaUR.Text = "Nombre de la  UR:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(332, 183);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "&Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(251, 183);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(172, 183);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 7;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblURPadre
            // 
            this.lblURPadre.AutoSize = true;
            this.lblURPadre.Location = new System.Drawing.Point(6, 27);
            this.lblURPadre.Name = "lblURPadre";
            this.lblURPadre.Size = new System.Drawing.Size(119, 13);
            this.lblURPadre.TabIndex = 3;
            this.lblURPadre.Text = "UR Padre Selecionada:";
            // 
            // TxtNodoTag
            // 
            this.TxtNodoTag.Location = new System.Drawing.Point(5, 368);
            this.TxtNodoTag.Name = "TxtNodoTag";
            this.TxtNodoTag.Size = new System.Drawing.Size(484, 20);
            this.TxtNodoTag.TabIndex = 4;
            // 
            // btnModificarUR
            // 
            this.btnModificarUR.FlatAppearance.BorderSize = 0;
            this.btnModificarUR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarUR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificarUR.ImageKey = "editar 20x20.png";
            this.btnModificarUR.ImageList = this.imageListIcons;
            this.btnModificarUR.Location = new System.Drawing.Point(626, 12);
            this.btnModificarUR.Name = "btnModificarUR";
            this.btnModificarUR.Size = new System.Drawing.Size(125, 26);
            this.btnModificarUR.TabIndex = 5;
            this.btnModificarUR.Text = "Modificar la UR";
            this.btnModificarUR.UseVisualStyleBackColor = true;
            this.btnModificarUR.Click += new System.EventHandler(this.btnModificarUR_Click);
            // 
            // btnEliminarUr
            // 
            this.btnEliminarUr.FlatAppearance.BorderSize = 0;
            this.btnEliminarUr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarUr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminarUr.ImageKey = "MenConEliminar.png";
            this.btnEliminarUr.ImageList = this.imageListIcons;
            this.btnEliminarUr.Location = new System.Drawing.Point(757, 12);
            this.btnEliminarUr.Name = "btnEliminarUr";
            this.btnEliminarUr.Size = new System.Drawing.Size(125, 26);
            this.btnEliminarUr.TabIndex = 6;
            this.btnEliminarUr.Text = "Eliminar la UR";
            this.btnEliminarUr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminarUr.UseVisualStyleBackColor = true;
            this.btnEliminarUr.Click += new System.EventHandler(this.btnEliminarUr_Click);
            // 
            // FrmCatalogoUR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 394);
            this.Controls.Add(this.btnEliminarUr);
            this.Controls.Add(this.btnModificarUR);
            this.Controls.Add(this.TxtNodoTag);
            this.Controls.Add(this.groupAgregarNuevaUR);
            this.Controls.Add(this.btnAgregarNuevaUR);
            this.Controls.Add(this.treeViewCatUR);
            this.Name = "FrmCatalogoUR";
            this.Text = "Catálogo de Unidades Responsables (UR)";
            this.Load += new System.EventHandler(this.FrmCatalogoUR_Load);
            this.groupAgregarNuevaUR.ResumeLayout(false);
            this.groupAgregarNuevaUR.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion

        private System.Windows.Forms.ImageList imageListIcons;
        private System.Windows.Forms.TreeView treeViewCatUR;
        private System.Windows.Forms.Button btnAgregarNuevaUR;
        private System.Windows.Forms.GroupBox groupAgregarNuevaUR;
        private System.Windows.Forms.Label lblURPadre;
        private System.Windows.Forms.TextBox TxtNodoTag;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtURN;
        private System.Windows.Forms.Label lblNuevaUR;
        private System.Windows.Forms.Label lblPadreURNueva;
        private System.Windows.Forms.TextBox txtId_URPadre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIdUr3;
        private System.Windows.Forms.TextBox txtIdUr2;
        private System.Windows.Forms.TextBox txtIdUr1;
        private System.Windows.Forms.Button btnModificarUR;
        private System.Windows.Forms.Button btnEliminarUr;
    }
}