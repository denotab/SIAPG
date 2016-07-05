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
    public partial class FormAbrirHojasDeTrabajoIngreso : Form
    {
        string IdHojaDeTrabajo = "";

        ClassParametros _ClassParametros = new ClassParametros();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassBaseDeDatos _ClassBaseDeDatos = new ClassBaseDeDatos();
        Properties.Settings PropiedadesAPP = new Properties.Settings();

        public FormAbrirHojasDeTrabajoIngreso()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;
        }

        private void FormAbrirHojasDeTrabajoIngreso_Load(object sender, EventArgs e)
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
            CargarHojasIngreso();

        }

        private void CargarHojasIngreso()
        {

            int TotalRegistros = 0;
            int RegistroActual = 0;
            string NombreProcedimiento = "ConsultarHojasDeIngresoPropiedades";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@ID_HOJA_INGRESO", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@ID_HOJA_INGRESO"].Value = -1;
            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "HojasIngreso");
            DataTable datatable = dataset.Tables["HojasIngreso"];
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
                    registrogrid[4] = registro["ID_HOJA_INGRESO"].ToString().Trim();
                    GridviewHojas.Rows.Add(registrogrid);
                    RegistroActual++;
                }
            }
            else
            {
                buttonGuardar.Enabled = false;
                MessageBox.Show(this, "No se han elaborado hojas de trabajo de ingreso", "0 hojas encontradas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GridviewHojas_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            IdHojaDeTrabajo = GridviewHojas.Rows[e.RowIndex].Cells[GridviewHojas.Columns["IdHojaIngreso"].Index].Value.ToString();

            if (_ClassParametros.IsNumeric(IdHojaDeTrabajo))
                _ClassParametros.IdHojaIngreso = Convert.ToInt32(IdHojaDeTrabajo);
            else
                _ClassParametros.IdHojaIngreso = -1;

        }

        private void GridviewHojas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            IdHojaDeTrabajo = GridviewHojas.Rows[e.RowIndex].Cells[GridviewHojas.Columns["IdHojaIngreso"].Index].Value.ToString().Trim();
            if (IdHojaDeTrabajo != "")
            {
                if (_ClassParametros.IsNumeric(IdHojaDeTrabajo))
                {
                    _ClassParametros.IdHojaIngreso = Convert.ToInt32(IdHojaDeTrabajo);
                    _ClassParametros.ProcesoCorrecto = true;
                    this.Close();
                }
                else
                    _ClassParametros.IdHojaIngreso = -1;

            }

        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (IdHojaDeTrabajo != "")
            {
                if (_ClassParametros.IsNumeric(IdHojaDeTrabajo))
                {
                    _ClassParametros.IdHojaIngreso = Convert.ToInt32(IdHojaDeTrabajo);
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
