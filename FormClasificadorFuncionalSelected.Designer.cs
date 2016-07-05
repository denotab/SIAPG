﻿namespace SIAPG
{
    partial class FormClasificadorFuncionalSelected
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClasificadorFuncionalSelected));
            this.treeViewCatalogo = new System.Windows.Forms.TreeView();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.checkBoxUl = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // treeViewCatalogo
            // 
            this.treeViewCatalogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewCatalogo.CheckBoxes = true;
            this.treeViewCatalogo.FullRowSelect = true;
            this.treeViewCatalogo.ImageIndex = 0;
            this.treeViewCatalogo.ImageList = this.imageListIcons;
            this.treeViewCatalogo.Indent = 25;
            this.treeViewCatalogo.ItemHeight = 26;
            this.treeViewCatalogo.LineColor = System.Drawing.Color.RoyalBlue;
            this.treeViewCatalogo.Location = new System.Drawing.Point(1, 37);
            this.treeViewCatalogo.Name = "treeViewCatalogo";
            this.treeViewCatalogo.SelectedImageIndex = 0;
            this.treeViewCatalogo.ShowNodeToolTips = true;
            this.treeViewCatalogo.Size = new System.Drawing.Size(301, 263);
            this.treeViewCatalogo.TabIndex = 4;
            this.treeViewCatalogo.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCatalogo_AfterCheck);
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
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelar.Location = new System.Drawing.Point(12, 306);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 26);
            this.buttonCancelar.TabIndex = 6;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAceptar.Location = new System.Drawing.Point(216, 306);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(75, 26);
            this.buttonAceptar.TabIndex = 5;
            this.buttonAceptar.Text = "Aplicar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // checkBoxUl
            // 
            this.checkBoxUl.AutoSize = true;
            this.checkBoxUl.Checked = true;
            this.checkBoxUl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUl.Location = new System.Drawing.Point(12, 340);
            this.checkBoxUl.Name = "checkBoxUl";
            this.checkBoxUl.Size = new System.Drawing.Size(185, 17);
            this.checkBoxUl.TabIndex = 7;
            this.checkBoxUl.Text = "Mostrar únicamente el último nivel";
            this.checkBoxUl.UseVisualStyleBackColor = true;
            this.checkBoxUl.Click += new System.EventHandler(this.checkBoxUl_Click);
            // 
            // FormClasificadorFuncionalSelected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 369);
            this.Controls.Add(this.checkBoxUl);
            this.Controls.Add(this.treeViewCatalogo);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormClasificadorFuncionalSelected";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Clasificador funcional";
            this.Load += new System.EventHandler(this.FormClasificadorFuncionalSelected_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewCatalogo;
        private System.Windows.Forms.ImageList imageListIcons;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.CheckBox checkBoxUl;
    }
}