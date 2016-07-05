namespace SIAPG
{
    partial class FormInformativo
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelDiferencia = new System.Windows.Forms.Label();
            this.labelMontoImportado = new System.Windows.Forms.Label();
            this.labelMontoOrigen = new System.Windows.Forms.Label();
            this.labelRegistrosImportados = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 208);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Diferencia:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Monto importado:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Monto Origen:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Registros importados";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.labelDiferencia);
            this.groupBox1.Controls.Add(this.labelMontoImportado);
            this.groupBox1.Controls.Add(this.labelMontoOrigen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 122);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Presupuesto";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(9, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDiferencia
            // 
            this.labelDiferencia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(204)))), ((int)(((byte)(187)))));
            this.labelDiferencia.ForeColor = System.Drawing.Color.Black;
            this.labelDiferencia.Location = new System.Drawing.Point(112, 82);
            this.labelDiferencia.Name = "labelDiferencia";
            this.labelDiferencia.Size = new System.Drawing.Size(178, 23);
            this.labelDiferencia.TabIndex = 9;
            this.labelDiferencia.Text = "label7";
            this.labelDiferencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelMontoImportado
            // 
            this.labelMontoImportado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(187)))));
            this.labelMontoImportado.ForeColor = System.Drawing.Color.Black;
            this.labelMontoImportado.Location = new System.Drawing.Point(112, 52);
            this.labelMontoImportado.Name = "labelMontoImportado";
            this.labelMontoImportado.Size = new System.Drawing.Size(178, 23);
            this.labelMontoImportado.TabIndex = 8;
            this.labelMontoImportado.Text = "label6";
            this.labelMontoImportado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelMontoOrigen
            // 
            this.labelMontoOrigen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(190)))), ((int)(((byte)(250)))));
            this.labelMontoOrigen.ForeColor = System.Drawing.Color.Black;
            this.labelMontoOrigen.Location = new System.Drawing.Point(112, 23);
            this.labelMontoOrigen.Name = "labelMontoOrigen";
            this.labelMontoOrigen.Size = new System.Drawing.Size(178, 23);
            this.labelMontoOrigen.TabIndex = 7;
            this.labelMontoOrigen.Text = "label5";
            this.labelMontoOrigen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelRegistrosImportados
            // 
            this.labelRegistrosImportados.BackColor = System.Drawing.Color.Transparent;
            this.labelRegistrosImportados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegistrosImportados.ForeColor = System.Drawing.Color.Black;
            this.labelRegistrosImportados.Location = new System.Drawing.Point(120, 9);
            this.labelRegistrosImportados.Name = "labelRegistrosImportados";
            this.labelRegistrosImportados.Size = new System.Drawing.Size(136, 23);
            this.labelRegistrosImportados.TabIndex = 8;
            this.labelRegistrosImportados.Text = "label8";
            this.labelRegistrosImportados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(212, 208);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 27);
            this.button2.TabIndex = 9;
            this.button2.Text = "Ajustes...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(282, 31);
            this.label6.TabIndex = 10;
            this.label6.Text = "Los ajustes por decimales durante la importación, podría alterar el monto total i" +
    "mportado.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(9, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormInformativo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 246);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelRegistrosImportados);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormInformativo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalles";
            this.Load += new System.EventHandler(this.FormInformativo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelDiferencia;
        private System.Windows.Forms.Label labelMontoImportado;
        private System.Windows.Forms.Label labelMontoOrigen;
        private System.Windows.Forms.Label labelRegistrosImportados;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}