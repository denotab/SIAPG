using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using MyExcel = Microsoft.Office.Interop.Excel;


namespace SIAPG
{
    public partial class FormCuadroComparativoPresupuestoIngreso : Form
    {


        ClassTipoProceso _ClassTipoProceso = new ClassTipoProceso();
        ClassParametros _ClassParametros = new ClassParametros();
        string AlternatingRowStyleBackColor = "#dce6f1";
        string AlternatingRowStyleForeColor = "#090909";
        Color DefaultRowStyleBackColor;
        Color DefaultRowStyleForeColor;

        private MyExcel.Application MyApplicationExcel;
        private MyExcel.Worksheet MyWorkSheetExcel;
        SaveFileDialog SaveFileExcel = new SaveFileDialog();
        int RenglonHeadExcel = 0;
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();

        public FormCuadroComparativoPresupuestoIngreso()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormCuadroComparativoPresupuestoIngreso_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            PausaBackGround.RunWorkerAsync();

        }

        private void ConfigurarGrid()
        {
            GridviewCargaPresupuestal.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml(AlternatingRowStyleBackColor);
            GridviewCargaPresupuestal.AlternatingRowsDefaultCellStyle.ForeColor = ColorTranslator.FromHtml(AlternatingRowStyleForeColor);

            DefaultRowStyleBackColor = GridviewCargaPresupuestal.DefaultCellStyle.BackColor;
            DefaultRowStyleForeColor = GridviewCargaPresupuestal.DefaultCellStyle.ForeColor;
        }


        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {

            CargarConcentradoComparativo();

        }

        private void CargarConcentradoComparativo()
        {
            if (_ClassTipoProceso.TipoProceso == (int)ClassTipoProceso.ProcesoAejecutar.CuadroComparativoPresupuestoIngresoConAjustes ||
                _ClassTipoProceso.TipoProceso == (int)ClassTipoProceso.ProcesoAejecutar.CuadroComparativoPresupuestoIngreso)
            {
                CargarConcetradoHojaPresupuesto("CuadroComparativoPresupuestoIngreso");
                this.Text = "Cuadro comparativo de Ingreso y Presupuesto";
            }
        }

        private void CargarConcetradoHojaPresupuesto(string NombreProcedimiento)
        {
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            double Diferencia = 0;
            bool ExisteDiferencia = false;
            GridviewCargaPresupuestal.Rows.Clear();
            GridviewCargaPresupuestal.Columns.Clear();
            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@IdHojaPresupuesto", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@IdHojaPresupuesto"].Value = _ClassParametros.IdHojaPresupuesto;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "concentradocomparativo");
                datatable = dataset.Tables["concentradocomparativo"];

                if (datatable.Rows.Count > 0)
                {
                    int RegistroActual = 0;
                    int RegistrosTotales = datatable.Rows.Count;
                    double[] registrogrid = new double[datatable.Columns.Count];

                    Padding paddingmonto = new Padding(0, 0, 2, 0);
                    Font font = new Font(GridviewCargaPresupuestal.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular);
                    foreach (DataColumn colum in datatable.Columns)
                    {
                        DataGridViewColumn columgrid = new DataGridViewColumn();
                        DataGridViewTextBoxCell cellgrid = new DataGridViewTextBoxCell();

                        string NombreCelda = colum.ColumnName.ToString();
                        columgrid.HeaderText = NombreCelda;
                        columgrid.Name = NombreCelda;
                        cellgrid.Style.Font = font;
                        if (Array.IndexOf(new String[] { "ID_CODIGO", "TOTAL_PRESUPUESTO", "TOTAL_INGRESO", "DIFERENCIA" }, NombreCelda) >= 0)
                        {
                            if (NombreCelda != "ID_CODIGO")
                            {
                                columgrid.Width = 140;
                                cellgrid.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                cellgrid.Style.Padding = paddingmonto;
                            }
                            else
                            {
                                columgrid.Width = 100;
                                cellgrid.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                        }
                        else
                        {
                            columgrid.Width = 150;
                            cellgrid.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        }

                        columgrid.CellTemplate = cellgrid;
                        GridviewCargaPresupuestal.Columns.Add(columgrid);
                    }


                    string[] columnasvalue = new string[datatable.Columns.Count];
                    while (RegistroActual < RegistrosTotales)
                    {
                        DataRow registro = datatable.Rows[RegistroActual];

                        for (int elem = 0; elem < datatable.Columns.Count; elem++)
                        {
                            string NombreCelda = GridviewCargaPresupuestal.Columns[elem].Name.ToString();
                            if (Array.IndexOf(new String[] { "TOTAL_PRESUPUESTO", "TOTAL_INGRESO", "DIFERENCIA" }, NombreCelda) >= 0)
                                columnasvalue[elem] = Math.Round(Convert.ToDecimal(registro[elem].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                            else
                                columnasvalue[elem] = registro[elem].ToString();
                        }

                        GridviewCargaPresupuestal.Rows.Add(columnasvalue);
                        registrogrid[GridviewCargaPresupuestal.Columns["TOTAL_PRESUPUESTO"].Index] += Convert.ToDouble(registro["TOTAL_PRESUPUESTO"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["TOTAL_INGRESO"].Index] += Convert.ToDouble(registro["TOTAL_INGRESO"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index] += Convert.ToDouble(registro["DIFERENCIA"].ToString().Trim());
                        GridviewCargaPresupuestal.Rows[RegistroActual].HeaderCell.Value = (RegistroActual + 1).ToString();

                        Diferencia = Convert.ToDouble(registro["DIFERENCIA"].ToString().Trim());

                        if (Diferencia < 0)   // presupuesto asignado mayor que el origen del recurso
                        {
                            ExisteDiferencia = true;
                            GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Style.BackColor = ColorTranslator.FromHtml("#990000");
                            GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Style.ForeColor = ColorTranslator.FromHtml("#ffffff");
                        }
                        else if (Diferencia > 0)   // suficiencia presupuestaria  (origen de ingreso es mayor que el presupuestado)
                        {
                            ExisteDiferencia = true;
                            GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Style.BackColor = ColorTranslator.FromHtml("#006000");
                            GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Style.ForeColor = ColorTranslator.FromHtml("#ffffff");
                        }

                        else
                        {
                            if (RegistroActual % 2 > 0)
                            {
                                GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Style.BackColor = ColorTranslator.FromHtml(AlternatingRowStyleBackColor);
                                GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Style.ForeColor = ColorTranslator.FromHtml(AlternatingRowStyleForeColor);
                            }
                            else
                            {
                                GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Style.BackColor = DefaultBackColor;
                                GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Style.ForeColor = DefaultForeColor;
                            }
                        }



                        RegistroActual++;

                    }


                    for (int elem = 0; elem < datatable.Columns.Count; elem++)
                    {
                        columnasvalue[elem] = "";
                        string NombreCelda = GridviewCargaPresupuestal.Columns[elem].Name.ToString();
                        if (Array.IndexOf(new String[] { "TOTAL_PRESUPUESTO", "TOTAL_INGRESO", "DIFERENCIA" }, NombreCelda) >= 0)
                            columnasvalue[elem] = Math.Round(Convert.ToDecimal(registrogrid[elem].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                    }
                    GridviewCargaPresupuestal.Rows.Add(columnasvalue);


                    for (int elem = 0; elem < datatable.Columns.Count; elem++)
                    {
                        string NombreCelda = GridviewCargaPresupuestal.Columns[elem].Name.ToString();
                        if (Array.IndexOf(new String[] { "TOTAL_PRESUPUESTO", "TOTAL_INGRESO" }, NombreCelda) >= 0)
                        {
                            // columnasvalue[elem] = Math.Round(Convert.ToDecimal(registrogrid[elem].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                            GridviewCargaPresupuestal.Rows[RegistroActual].Cells[elem].Style.BackColor = ColorTranslator.FromHtml("#990000");
                            GridviewCargaPresupuestal.Rows[RegistroActual].Cells[elem].Style.ForeColor = ColorTranslator.FromHtml("#ffffff");
                        }
                    }
                }
            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
            }

            if (ExisteDiferencia)
                buttonAjustes.Enabled = true;
            else
                buttonAjustes.Enabled = false;

            sqldataadapter = null;
            dataset = null;
            datatable = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonAjustes.Enabled = false;
            RealizarAjusteAutomatico();
        }

        private void RealizarAjusteAutomatico()
        {
            double ValueDiferencia = 0;
            string diferencia = "";
            string ID_CODIGO = "";
            bool AjusteCorrecto = true;

            for (int Elem = 0; Elem < GridviewCargaPresupuestal.Rows.Count; Elem++)
            {
                ID_CODIGO = "";
                diferencia = "";

                if (GridviewCargaPresupuestal.Rows[Elem].Cells[GridviewCargaPresupuestal.Columns["ID_CODIGO"].Index].Value != null)
                {
                    ID_CODIGO = GridviewCargaPresupuestal.Rows[Elem].Cells[GridviewCargaPresupuestal.Columns["ID_CODIGO"].Index].Value.ToString();
                }

                if (GridviewCargaPresupuestal.Rows[Elem].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Value != null)
                {
                    diferencia = GridviewCargaPresupuestal.Rows[Elem].Cells[GridviewCargaPresupuestal.Columns["DIFERENCIA"].Index].Value.ToString();
                }

                if (ID_CODIGO != "")
                {
                    Regex SignoMonedamoneda = new Regex(";,$%");
                    diferencia = SignoMonedamoneda.Replace(diferencia.Trim(), "");
                    ValueDiferencia = Convert.ToDouble(diferencia);

                    if (ValueDiferencia != 0)
                    {
                        if (!ActualizarDiferenciaCog(ID_CODIGO, ValueDiferencia))
                        {
                            AjusteCorrecto = false;
                            break;
                        }
                    }
                }
            }
            if (AjusteCorrecto)
            {
                MessageBox.Show(this, "El ajuste se ha realizado correctamente", "Proceso terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarConcetradoHojaPresupuesto("CuadroComparativoPresupuestoIngreso");
            }
            else
                buttonAjustes.Enabled = true;
        }

        private bool ActualizarDiferenciaCog(string Id_codigo_Ingreso, double Diferencia)
        {
            int Id_Registro_Presupuesto = 0;
            bool ProcesoCorrecto = true;
            string MySql = "SELECT TOP (1) ID_REGISTRO_PRESUPUESTO, ID_COG, PRESUPUESTO, ENERO, FEBRERO, MARZO, ABRIL, MAYO, JUNIO, JULIO, AGOSTO, SEPTIEMBRE, OCTUBRE, NOVIEMBRE, DICIEMBRE " +
            " FROM HOJAS_DE_PRESUPUESTO WHERE ID_ORIGEN_INGRESO = " + Id_codigo_Ingreso + " ORDER BY PRESUPUESTO DESC ";


            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            Double Presupuesto = 0;
            string ID_COG = "";

            double[] porcentajeceldas = new double[12];
            double[] PorcentajeMensual = new double[12];
            double totalceldas = 0;
            double diferencia = 0; 

            try
            {
                sqldataadapter = new SqlDataAdapter(MySql, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.Text;
                dataset = new DataSet();

                sqldataadapter.Fill(dataset, "ajustespresupuestales");
                datatable = dataset.Tables["ajustespresupuestales"];

                if (datatable.Rows.Count > 0)
                {
                    DataRow registro = datatable.Rows[0];
                    Id_Registro_Presupuesto = Convert.ToInt32(registro["ID_REGISTRO_PRESUPUESTO"].ToString());
                    ID_COG = registro["ID_COG"].ToString();
                    Presupuesto = Convert.ToDouble(registro["PRESUPUESTO"].ToString());
                    Presupuesto += Diferencia;


                    for (int elem = 0; elem < porcentajeceldas.Length; elem++)
                    {
                        porcentajeceldas[elem] = 0;
                    }


                    // enero
                    if (registro["ENERO"] != null)
                        if (_ClassParametros.IsNumeric(registro["ENERO"].ToString()))
                            porcentajeceldas[0] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["ENERO"].ToString()), 2));

                    // febrero
                    if (registro["FEBRERO"] != null)
                        if (_ClassParametros.IsNumeric(registro["FEBRERO"].ToString()))
                            porcentajeceldas[1] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["FEBRERO"].ToString()), 2));

                    // marzo
                    if (registro["MARZO"] != null)
                        if (_ClassParametros.IsNumeric(registro["MARZO"].ToString()))
                            porcentajeceldas[2] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["MARZO"].ToString()), 2));


                    // abril 
                    if (registro["ABRIL"] != null)
                        if (_ClassParametros.IsNumeric(registro["ABRIL"].ToString()))
                            porcentajeceldas[3] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["ABRIL"].ToString()), 2));

                    // mayo
                    if (registro["MAYO"] != null)
                        if (_ClassParametros.IsNumeric(registro["MAYO"].ToString()))
                            porcentajeceldas[4] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["MAYO"].ToString()), 2));

                    // junio
                    if (registro["JUNIO"] != null)
                        if (_ClassParametros.IsNumeric(registro["JUNIO"].ToString()))
                            porcentajeceldas[5] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["JUNIO"].ToString()), 2));

                    // julio
                    if (registro["JULIO"] != null)
                        if (_ClassParametros.IsNumeric(registro["JULIO"].ToString()))
                            porcentajeceldas[6] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["JULIO"].ToString()), 2));

                    // agosto
                    if (registro["AGOSTO"] != null)
                        if (_ClassParametros.IsNumeric(registro["AGOSTO"].ToString()))
                            porcentajeceldas[7] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["AGOSTO"].ToString()), 2));


                    // septiembre
                    if (registro["SEPTIEMBRE"] != null)
                        if (_ClassParametros.IsNumeric(registro["SEPTIEMBRE"].ToString()))
                            porcentajeceldas[8] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["SEPTIEMBRE"].ToString()), 2));

                    // octubre
                    if (registro["OCTUBRE"] != null)
                        if (_ClassParametros.IsNumeric(registro["OCTUBRE"].ToString()))
                            porcentajeceldas[9] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["OCTUBRE"].ToString()), 2));

                    // noviembre
                    if (registro["NOVIEMBRE"] != null)
                        if (_ClassParametros.IsNumeric(registro["NOVIEMBRE"].ToString()))
                            porcentajeceldas[10] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["NOVIEMBRE"].ToString()), 2));

                    // diciembre
                    if (registro["DICIEMBRE"] != null)
                        if (_ClassParametros.IsNumeric(registro["DICIEMBRE"].ToString()))
                            porcentajeceldas[11] = Convert.ToDouble(Math.Round(Convert.ToDecimal(registro["DICIEMBRE"].ToString()), 2));



                    RecalcularPorcentajeMensual(ID_COG, Presupuesto, ref porcentajeceldas);


                    totalceldas = 0;
                    for (int elem = 0; elem < porcentajeceldas.Length; elem++)
                    {
                        totalceldas += porcentajeceldas[elem];
                    }
                    diferencia = Presupuesto - totalceldas;
                    if (diferencia != 0)
                    {
                        for (int elem = porcentajeceldas.Length - 1; elem > 0; elem--)      // se resta o suma la diferencia en el ultimo mes con valor > 0
                        {
                            if (porcentajeceldas[elem] > 0)
                            {
                                porcentajeceldas[elem] = porcentajeceldas[elem] + diferencia;
                                porcentajeceldas[elem] = Math.Round(porcentajeceldas[elem], 2);
                                break;
                            }
                        }

                    }

                    if (!ActualizarAjustesPresupuesto(Presupuesto, Id_Registro_Presupuesto, Diferencia, porcentajeceldas))
                        ProcesoCorrecto = false;
                }
            }
            catch (Exception MyException)
            {
                MessageBox.Show(this, MyException.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }

            return ProcesoCorrecto;
        }

        private bool ActualizarAjustesPresupuesto(double NuevoPresupuesto, int Id_Registro_Presupuesto, double Diferencia, double[] porcentajeceldas)
        {
            bool ProcesoCorrecto = true;
            string Query = " WHERE ID_REGISTRO_PRESUPUESTO = " + Id_Registro_Presupuesto.ToString();
            string MySql = "UPDATE  HOJAS_DE_PRESUPUESTO SET Presupuesto = " + NuevoPresupuesto.ToString() +   ", " + 
            " ENERO = " + porcentajeceldas[0].ToString() + ", " +
            " FEBRERO = " + porcentajeceldas[1].ToString() + ", " +
            " MARZO = " + porcentajeceldas[2].ToString() + ", " +
            " ABRIL = " + porcentajeceldas[3].ToString() + ", " +
            " MAYO = " + porcentajeceldas[4].ToString() + ", " +
            " JUNIO = " + porcentajeceldas[5].ToString() + ", " +
            " JULIO = " + porcentajeceldas[6].ToString() + ", " +
            " AGOSTO = " + porcentajeceldas[7].ToString() + ", " +
            " SEPTIEMBRE = " + porcentajeceldas[8].ToString() + ", " +
            " OCTUBRE = " + porcentajeceldas[9].ToString() + ", " +
            " NOVIEMBRE = " + porcentajeceldas[10].ToString() + ", " +
            " DICIEMBRE = " + porcentajeceldas[11].ToString() + ", " +
            " STATUS = 1, DIFERENCIA = " + Diferencia.ToString() + Query;
            try
            {
                SqlCommand CommandVerificacion = new SqlCommand(MySql, ClassBaseDeDatos.MyConnectionDB);
                CommandVerificacion.CommandType = CommandType.Text;
                CommandVerificacion.ExecuteNonQuery();
            }
            catch (Exception MyException)
            {
                MessageBox.Show(this, MyException.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }

            return ProcesoCorrecto;
        }

        private bool RecalcularPorcentajeMensual(string ID_COG, double Presupuesto, ref double[] porcentajeceldas)
        {
            bool ProcesoCorrecto = true;
            string NombreProcedimiento = "ConsultarCatalogoCalendarioCOG";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@id_cog", SqlDbType.VarChar);
                sqldataadapter.SelectCommand.Parameters["@id_cog"].Value = ID_COG;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "ConsultaporcentajeCOG");
                datatable = dataset.Tables["ConsultaporcentajeCOG"];

                if (datatable.Rows.Count > 0)
                {
                    DataRow registro = datatable.Rows[0];
             
                    _ClassParametros.PorcentajeEnero = Math.Round(Convert.ToDouble(registro["ENERO"].ToString()));
                    _ClassParametros.PorcentajeFebrero = Math.Round(Convert.ToDouble(registro["FEBRERO"].ToString()));
                    _ClassParametros.PorcentajeMarzo = Math.Round(Convert.ToDouble(registro["MARZO"].ToString()));
                    _ClassParametros.PorcentajeAbril = Math.Round(Convert.ToDouble(registro["ABRIL"].ToString()));
                    _ClassParametros.PorcentajeMayo = Math.Round(Convert.ToDouble(registro["MAYO"].ToString()));
                    _ClassParametros.PorcentajeJunio = Math.Round(Convert.ToDouble(registro["JUNIO"].ToString()));
                    _ClassParametros.PorcentajeJulio = Math.Round(Convert.ToDouble(registro["JULIO"].ToString()));
                    _ClassParametros.PorcentajeAgosto = Math.Round(Convert.ToDouble(registro["AGOSTO"].ToString()));
                    _ClassParametros.PorcentajeSeptiembre = Math.Round(Convert.ToDouble(registro["SEPTIEMBRE"].ToString()));
                    _ClassParametros.PorcentajeOctubre = Math.Round(Convert.ToDouble(registro["OCTUBRE"].ToString()));
                    _ClassParametros.PorcentajeNoviembre = Math.Round(Convert.ToDouble(registro["NOVIEMBRE"].ToString()));
                    _ClassParametros.PorcentajeDiciembre = Math.Round(Convert.ToDouble(registro["DICIEMBRE"].ToString()));

                    porcentajeceldas[0] = Math.Round((_ClassParametros.PorcentajeEnero / 100 * Presupuesto), 2);
                    porcentajeceldas[1] = Math.Round((_ClassParametros.PorcentajeFebrero / 100 * Presupuesto), 2);
                    porcentajeceldas[2] = Math.Round((_ClassParametros.PorcentajeMarzo / 100 * Presupuesto), 2);
                    porcentajeceldas[3] = Math.Round((_ClassParametros.PorcentajeAbril / 100 * Presupuesto), 2);
                    porcentajeceldas[4] = Math.Round((_ClassParametros.PorcentajeMayo / 100 * Presupuesto), 2);
                    porcentajeceldas[5] = Math.Round((_ClassParametros.PorcentajeJunio / 100 * Presupuesto), 2);
                    porcentajeceldas[6] = Math.Round((_ClassParametros.PorcentajeJulio / 100 * Presupuesto), 2);
                    porcentajeceldas[7] = Math.Round((_ClassParametros.PorcentajeAgosto / 100 * Presupuesto), 2);
                    porcentajeceldas[8] = Math.Round((_ClassParametros.PorcentajeSeptiembre / 100 * Presupuesto), 2);
                    porcentajeceldas[9] = Math.Round((_ClassParametros.PorcentajeOctubre / 100 * Presupuesto), 2);
                    porcentajeceldas[10] = Math.Round((_ClassParametros.PorcentajeNoviembre / 100 * Presupuesto), 2);
                    porcentajeceldas[11] = Math.Round((_ClassParametros.PorcentajeDiciembre / 100 * Presupuesto), 2);
               }
            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
                ProcesoCorrecto = false;
            }

            sqldataadapter = null;
            dataset = null;
            datatable = null;
            return ProcesoCorrecto; 
        }
    }
}
