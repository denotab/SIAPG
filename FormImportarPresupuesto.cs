using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Threading;

using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System.Data.SqlClient;

namespace SIAPG
{
    public partial class FormImportarPresupuesto : Form
    {
        ClassBaseDeDatos ClassConexionBaseDeDatos = new ClassBaseDeDatos();
        ClassParametros _ClassParametros = new ClassParametros();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();

        public FormImportarPresupuesto()
        {
            InitializeComponent();
        }

  
        private void buttonImportar_Click(object sender, EventArgs e)
        {
            if (textBoxRuta.Text.ToString() != "" && comboBoxHoja.Text != "")
            {
                _ClassParametros.RutaArchivoPresupuesto = textBoxRuta.Text.ToString();
                _ClassParametros.NombreHojaPresupuesto = comboBoxHoja.Text;
                _ClassParametros.ProcesoCancelado = false;
                _ClassParametros.ProcesoEjecutado = true;
                this.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialogRuta.Multiselect = false;
            if (openFileDialogRuta.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                textBoxRuta.Text = openFileDialogRuta.FileName.ToString();
                comboBoxHoja.Items.Clear();
                if (textBoxRuta.Text.ToString().Trim() != "")
                    if (CargarHojasExcel()) comboBoxHoja.DroppedDown = true;

                this.Cursor = Cursors.Default;
            }

        }

        private bool CargarHojasExcel()
        {
            Boolean ProcesoCorrecto = true;
            var AppExcel = new Excel.Application();
            Excel.Workbook LibroDeTrabajoExcel;

            try
            {
                LibroDeTrabajoExcel = AppExcel.Workbooks.Open(textBoxRuta.Text.ToString());
                foreach (Microsoft.Office.Interop.Excel.Worksheet Hojas in LibroDeTrabajoExcel.Worksheets)
                {
                    comboBoxHoja.Items.Add(Hojas.Name.ToString());
                }
            }

            catch (Exception MyException)
            {
                MessageBox.Show(this, "Ha ocurrido un error al obtener las propiedades del archivo; verifique el siguiente error: \n\r\n\r" +
                MyException.Message.ToString(), "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                ProcesoCorrecto = false;
            }
            finally
            {
                AppExcel = null;
                LibroDeTrabajoExcel = null;
            }
            return ProcesoCorrecto;
        }
        private void comboBoxHoja_TextChanged_1(object sender, EventArgs e)
        {
            VerificarSeleccionHoja();
        }

        private void VerificarSeleccionHoja()
        {
            if (comboBoxHoja.Text.ToString().Trim() != "")
            {
                buttonImportar.Enabled = true;
                groupBoxPropiedades.Enabled = true;
            }
            else
            {
                buttonImportar.Enabled = false;
                groupBoxPropiedades.Enabled = false;

            }

        }
        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            _ClassParametros.ProcesoCancelado = true;
            _ClassParametros.ProcesoEjecutado = false; 

            this.Close();
        }

        private void FormImportarPresupuesto_Load(object sender, EventArgs e)
        {

        }
    }
}
