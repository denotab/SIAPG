using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;


namespace SIAPG
{
    public partial class FormAbrirHojasDeTrabajoPresupuesto : Form
    {
        string IdHojaDeTrabajo = "";

        ClassParametros _ClassParametros = new ClassParametros();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassBaseDeDatos _ClassBaseDeDatos = new ClassBaseDeDatos();
        Properties.Settings PropiedadesAPP = new Properties.Settings();

        public FormAbrirHojasDeTrabajoPresupuesto()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormAbrirHojasDeTrabajoPresupuesto_Load(object sender, EventArgs e)
        {
            GridviewHojas.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            GridviewHojas.DefaultCellStyle.BackColor = Color.AliceBlue;
            GridviewHojas.DefaultCellStyle.SelectionForeColor = Color.White;
            GridviewHojas.RowHeadersDefaultCellStyle.ForeColor = Color.Gray;
            GridviewHojas.RowHeadersDefaultCellStyle.BackColor = Color.AliceBlue;
            PausaBackGround.RunWorkerAsync();


        }
        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs rpuesunworkercompletedeventargs)
        {
            CargarHojasPresupuesto();

        }

        private void CargarHojasPresupuesto()
        {

            int TotalRegistros = 0;
            int RegistroActual = 0;
            string NombreProcedimiento = "ConsultarHojasDePresupuestoPropiedades";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@ID_HOJA_PRESUPUESTO", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@ID_HOJA_PRESUPUESTO"].Value = -1;
            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "HojasPresupuesto");
            DataTable datatable = dataset.Tables["HojasPresupuesto"];
            GridviewHojas.Rows.Clear();


            if (datatable.Rows.Count >= 0)
            {
                TotalRegistros = datatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {

                    string[] registrogrid = new string[5];
                    DataRow registro = datatable.Rows[RegistroActual];
                    registrogrid[0] = registro["NOMBRE_HOJA"].ToString().Trim();
                    registrogrid[1] = registro["FECHA_CREACION"].ToString().Trim();
                    registrogrid[2] = registro["FECHA_MODIFICACION"].ToString().Trim();
                    registrogrid[3] = registro["OBSERVACIONES"].ToString().Trim();
                    registrogrid[4] = registro["ID_HOJA_PRESUPUESTO"].ToString().Trim();
                    GridviewHojas.Rows.Add(registrogrid);
                    RegistroActual++;
                }
            }
            else
            {
                buttonGuardar.Enabled = false;
                MessageBox.Show(this, "No se han elaborado hojas de trabajo presupuestales", "0 hojas encontradas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GridviewHojas_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            IdHojaDeTrabajo = GridviewHojas.Rows[e.RowIndex].Cells[GridviewHojas.Columns["IdHojaPresupuesto"].Index].Value.ToString();

            if (_ClassParametros.IsNumeric(IdHojaDeTrabajo))
                _ClassParametros.IdHojaPresupuesto = Convert.ToInt32(IdHojaDeTrabajo);
            else
                _ClassParametros.IdHojaPresupuesto = -1;


        }

        private void GridviewHojas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            IdHojaDeTrabajo = GridviewHojas.Rows[e.RowIndex].Cells[GridviewHojas.Columns["IdHojaPresupuesto"].Index].Value.ToString().Trim();
            if (IdHojaDeTrabajo != "")
            {
                if (_ClassParametros.IsNumeric(IdHojaDeTrabajo))
                {
                    _ClassParametros.IdHojaPresupuesto = Convert.ToInt32(IdHojaDeTrabajo);
                    ClassFunciones.IdHojaPresupuesto = Convert.ToInt32(_ClassParametros.IdHojaPresupuesto);
                    _ClassParametros.ProcesoCorrecto = true;
                    this.Close();
                }
                else
                    _ClassParametros.IdHojaPresupuesto = -1;

            }

        }

        /// <summary>
        /// BOTON QUE CARGA LA HOJA DE PROPIEDADES EN EL GRID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (IdHojaDeTrabajo != "")
            {
                if (_ClassParametros.IsNumeric(IdHojaDeTrabajo))
                {
                    _ClassParametros.IdHojaPresupuesto = Convert.ToInt32(IdHojaDeTrabajo);
                    ClassFunciones.IdHojaPresupuesto = Convert.ToInt32(_ClassParametros.IdHojaPresupuesto);
                    _ClassParametros.ProcesoCorrecto = true;
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ClassParametros.ProcesoCancelado = true;
            this.Close(); 
        }
    }
}
