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

namespace SIAPG
{
    public partial class FormReportesConcentrado : Form
    {

        ClassParametros _ClassParametros = new ClassParametros();
        ClassTipoProceso _ClassTipoProceso = new ClassTipoProceso();

        


        public FormReportesConcentrado()
        {
            InitializeComponent();
            
        }

        private void FormReportesConcentrado_Load(object sender, EventArgs e)
        {

//              this.reportViewerConcentrado.RefreshReport();
            CargarConcentradosHojasDeTrabajo();
          //    this.reportViewer1.RefreshReport();
        }

        private void CargarConcentradosHojasDeTrabajo()
        {
            if (_ClassTipoProceso.TipoProceso == (int)ClassTipoProceso.ProcesoAejecutar.EjecutarCocentradoPorUr)
            {
                CargarConcetradoHojaPresupuesto("ConcentradoHojaTrabajoXUR");
                this.Text = "Informe concentrado por Unidad Responsable";
            }
            else if (_ClassTipoProceso.TipoProceso == (int)ClassTipoProceso.ProcesoAejecutar.EjecutarConcentradoPorOrigenIngreso)
            {
                CargarConcetradoHojaPresupuesto("ConcentradoHojaTrabajoXOrigenIngreso");
                this.Text = "Informe concentrado por Origen de Ingreso";
            }
            else if (_ClassTipoProceso.TipoProceso == (int)ClassTipoProceso.ProcesoAejecutar.EjecutarConcentradoPorCOG)
            {
                CargarConcetradoHojaPresupuesto("ConcentradoHojaTrabajoXCOG");
                this.Text = "Informe concentrado por COG";
            }

        }

        private void CargarConcetradoHojaPresupuesto(string NombreProcedimiento)
        {
            string TipoReporte = "";
            string TituloReporte = "";

            if (NombreProcedimiento == "ConcentradoHojaTrabajoXUR")
                TipoReporte = "UNIDAD RESPONSABLE";
            else if (NombreProcedimiento == "ConcentradoHojaTrabajoXOrigenIngreso")
                TipoReporte = "ORIGEN DEL INGRESO";
            else if (NombreProcedimiento == "ConcentradoHojaTrabajoXCOG")
                TipoReporte = "CATALOGO OBJETO DEL GASTO";


            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable = new DataTable();
            DateTime FehaImpresion = new DateTime();
            FehaImpresion = DateTime.Now;
            ReportParameter ReportParameter1 = new ReportParameter("ReportParameter1", FehaImpresion.ToShortDateString());
            ReportParameter ReportParameter2 = new ReportParameter("TipoConcentrado", TipoReporte);
            ReportParameter ReportParameter3; 

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@IdHojaPresupuesto", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@IdHojaPresupuesto"].Value = _ClassParametros.IdHojaPresupuesto;

                sqldataadapter.Fill(datatable);
                reportViewerConcentrado.Visible = true;


                if (NombreProcedimiento == "ConcentradoHojaTrabajoXUR")
                {
                    datatable.Columns["UR"].ColumnName = "DESCRIPCION";
                    TituloReporte = "CONCENTRADO PRESUPUESTAL POR UNIDAD RESPONSABLE";
                }
                else if (NombreProcedimiento == "ConcentradoHojaTrabajoXOrigenIngreso")
                {
                    datatable.Columns["ORIGEN_INGRESO"].ColumnName = "DESCRIPCION";
                    TituloReporte = "CONCENTRADO PRESUPUESTAL POR ORIGEN DE INGRESO";
                }
                else if (NombreProcedimiento == "ConcentradoHojaTrabajoXCOG")
                {
                    datatable.Columns["COG"].ColumnName = "DESCRIPCION";
                    TituloReporte = "CONCENTRADO PRESUPUESTAL POR CLASIFICACION OBJETO DEL GASTO";
                }

                ReportParameter3 = new ReportParameter("ReportParameterSubtitulo", TituloReporte);

                //  reportViewerConcentrado.LocalReport.ReportPath = "ReportConcentradoPresupuesto.rdlc";
                reportViewerConcentrado.LocalReport.DataSources.Clear();
                reportViewerConcentrado.LocalReport.DataSources.Add(new ReportDataSource("DataSetConcentrado", datatable));

                reportViewerConcentrado.LocalReport.SetParameters(new ReportParameter[] { ReportParameter1, ReportParameter2, ReportParameter3 });

                reportViewerConcentrado.RefreshReport();
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
