using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAPG
{
    public partial class FormInformativo : Form
    {
        ClassDetallesPresupuestales _ClassDetallesPresupuestales = new ClassDetallesPresupuestales();
        public FormInformativo()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormInformativo_Load(object sender, EventArgs e)
        {
            this.labelRegistrosImportados.Text = _ClassDetallesPresupuestales.RegistrosImportados.ToString("#,###");
            this.labelMontoOrigen.Text = _ClassDetallesPresupuestales.MontoTotalOrigen.ToString("$ #,###.########");
            this.labelMontoImportado.Text = _ClassDetallesPresupuestales.MontoTotalImportado.ToString("$ #,###.########");
            this.labelDiferencia.Text = _ClassDetallesPresupuestales.MontoDiferencia.ToString("$ #,##0.########");

            if (_ClassDetallesPresupuestales.MontoDiferencia > 0)
                button2.Enabled = true; 
            else
                button2.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ClassDetallesPresupuestales.RealizarAjustes = true;
            this.Close();
        }
    }
}
