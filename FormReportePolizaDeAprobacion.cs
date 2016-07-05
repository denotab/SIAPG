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
using Microsoft.Reporting.WinForms;
using System.Threading;

namespace SIAPG
{
    public partial class FormReportePolizaDeAprobacion : Form
    {


        ClassParametros _ClassParametros = new ClassParametros();
        ClassTipoProceso _ClassTipoProceso = new ClassTipoProceso();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();


        public FormReportePolizaDeAprobacion()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormReportePolizaDeAprobacion_Load(object sender, EventArgs e)
        {
            PausaBackGround.RunWorkerAsync();

            // this.reportPolizaAprobacion.RefreshReport();

        }

        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {
            string NombreProcedimiento = "CONSULTAR_POLIZA_APROBACION_PRESUPUESTAL";
            CargarPolizaAprobacion(NombreProcedimiento);
        }

        private void CargarPolizaAprobacion(string NombreProcedimiento)
        {


            DateTime FehaImpresion = new DateTime();
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable = new DataTable();

            FehaImpresion = DateTime.Now;
            ReportParameter ReportParameter1 = new ReportParameter("ReportParameter1", FehaImpresion.ToShortDateString());
            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@ID_HOJA_PRESUPUESTO", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@ID_HOJA_PRESUPUESTO"].Value = _ClassParametros.IdHojaPresupuesto;
                sqldataadapter.SelectCommand.Parameters.Add("@ID_CUENTA_GENERO", SqlDbType.VarChar);
                sqldataadapter.SelectCommand.Parameters["@ID_CUENTA_GENERO"].Value = "8";
                sqldataadapter.SelectCommand.Parameters.Add("@ID_CUENTA_GRUPO", SqlDbType.VarChar);
                sqldataadapter.SelectCommand.Parameters["@ID_CUENTA_GRUPO"].Value = "2";
                sqldataadapter.SelectCommand.Parameters.Add("@ID_CUENTA_RUBRO", SqlDbType.VarChar);
                sqldataadapter.SelectCommand.Parameters["@ID_CUENTA_RUBRO"].Value = "1,2";

                sqldataadapter.Fill(datatable);
                reportPolizaAprobacion.Visible = true;
                //  reportViewerConcentrado.LocalReport.ReportPath = "ReportConcentradoPresupuesto.rdlc";
                reportPolizaAprobacion.LocalReport.DataSources.Clear();
                reportPolizaAprobacion.LocalReport.DataSources.Add(new ReportDataSource("DataSetCP", datatable));

                reportPolizaAprobacion.LocalReport.SetParameters(new ReportParameter[] { ReportParameter1 });

                reportPolizaAprobacion.RefreshReport();

            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
            }

            sqldataadapter = null;
            dataset = null;
            datatable = null;
        }
    }
}
