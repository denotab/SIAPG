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
using System.Text.RegularExpressions;

namespace SIAPG
{
    public partial class FormPolizaDeAprobacion : Form
    {
        ClassParametros _ClassParametros = new ClassParametros();
        SqlTransaction TransactionPresupuesto;
        SqlCommand CommandInsertCatalogo;
        SqlCommand CommandInsertarCuentasPresupuestales;
        SqlDataAdapter sqldataadapter;


        string[,] CuentaGenero;
        public FormPolizaDeAprobacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ClassParametros.ProcesoCancelado = false;
            _ClassParametros.ProcesoCorrecto = false;

            FormAbrirHojasDeTrabajoPresupuesto _FormAbrirHojasDeTrabajoPresupuesto = new FormAbrirHojasDeTrabajoPresupuesto();
            _FormAbrirHojasDeTrabajoPresupuesto.ShowDialog();

            if (!_ClassParametros.ProcesoCancelado && _ClassParametros.ProcesoCorrecto)
            {
                this.Cursor = Cursors.WaitCursor;
                if (CargarConcentradoPresupuestoPorCog())
                {
                    
                }
                this.Cursor = Cursors.Default;
            }
        }


        private bool CargarConcentradoPresupuestoPorCog()
        {

            bool ProcesoCorrecto = true;

            GridviewCargaPresupuestal.Rows.Clear();
            int RegistroActual = 0;
            int RegistroFinal = 0;

            string NombreProcedimiento = "ConcentradoPresupuestoPorCOGparaGenerarPolizaAprobacion";


            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@ID_HOJA_PRESUPUESTO", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@ID_HOJA_PRESUPUESTO"].Value = Convert.ToInt32(_ClassParametros.IdHojaPresupuesto);
            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "ConcentradoPresupuestoCOG");
            DataTable datatable = dataset.Tables["ConcentradoPresupuestoCOG"];

            double TotalPresupuesto = 0; 
            try
            {
                if (datatable.Rows.Count >= 0)
                {
                    RegistroFinal = datatable.Rows.Count;
                    string[] registrogrid = new string[4];

                    labelInicial.Text = "0"; 
                    labelFinal.Text  = RegistroFinal.ToString("#,###", _ClassParametros.MiRegionProvider);
                    progressBarProceso.Maximum = RegistroFinal;
                    panelProceso.Visible = true; 
                    while (RegistroActual < RegistroFinal)
                    {

                        Application.DoEvents();
                        DataRow registro = datatable.Rows[RegistroActual];

                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index] = registro["ID_COG"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["COG"].Index] = registro["COG"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index] = registro["MONTO_PRESUPUESTO_COG"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["ID_CUENTA"].Index] = registro["ID_CUENTA"].ToString().Trim();

                        if (_ClassParametros.IsNumeric(registro["MONTO_PRESUPUESTO_COG"].ToString()))
                        {
                            registrogrid[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index] = Math.Round(Convert.ToDecimal(registro["MONTO_PRESUPUESTO_COG"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                            TotalPresupuesto += Convert.ToDouble(registro["MONTO_PRESUPUESTO_COG"].ToString());
                        }
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index] = "";

                        

                        GridviewCargaPresupuestal.Rows.Add(registrogrid);
                        GridviewCargaPresupuestal.Rows[RegistroActual].HeaderCell.Value = (RegistroActual + 1).ToString();
                        RegistroActual++;
                    }

                    registrogrid[0] = ""; registrogrid[1] = ""; registrogrid[2] = ""; registrogrid[3] = "";
                    registrogrid[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index] = Math.Round(Convert.ToDecimal(TotalPresupuesto), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                    GridviewCargaPresupuestal.Rows.Add(registrogrid);

                    GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Style.BackColor = ColorTranslator.FromHtml("#990000");
                    GridviewCargaPresupuestal.Rows[RegistroActual].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Style.ForeColor = ColorTranslator.FromHtml("#ffffff");

                    progressBarProceso.Value = RegistroActual;
                    labelInicial.Text = RegistroActual.ToString("#,###", _ClassParametros.MiRegionProvider);

                }
                else
                {
                    MessageBox.Show(this, "No se encontraron registros en el nombre de hoja seleccionada", "0 Registros encontrados.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(this, excepcion.Message.ToString(), "Ha ocurrido un  error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }
            sqldataadapter = null;
            panelProceso.Visible = false;
            return ProcesoCorrecto;
        }

        private void GridviewCargaPresupuestal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void  buttonGuardar_Click(object sender, EventArgs e)
        {
            bool ProcesoCorrecto = true; 
            string CUENTA_PRESUPUESTO = ""; 
            string CodigoCOG = "";
            string ID_CUENTA = "";
            string ID_GENERO = "";
            string ID_GRUPO = "";
            string ID_RUBRO = "";
            string CARGO = "";
            string ABONO = "";
            Regex SignoMonedamoneda = new Regex(";,$%");
            int RegistroFinal = 0;
            int RegistroInicial = 0;
            this.Cursor = Cursors.WaitCursor;

            if (ExtraerTipoDeCuentas())
            {

                labelInicial.Text = "0";
                RegistroFinal = (CuentaGenero.GetLength(0) * GridviewCargaPresupuestal.Rows.Count);
                labelFinal.Text = RegistroFinal.ToString("#,###", _ClassParametros.MiRegionProvider);
                progressBarProceso.Maximum = RegistroFinal;
                panelProceso.Visible = true;
                TransactionPresupuesto = ClassBaseDeDatos.MyConnectionDB.BeginTransaction();
                CommandInsertCatalogo = new SqlCommand();


                sqldataadapter = new SqlDataAdapter(); // );
                sqldataadapter.SelectCommand = new SqlCommand();
                sqldataadapter.SelectCommand.CommandType = CommandType.Text;
                sqldataadapter.SelectCommand.Connection = ClassBaseDeDatos.MyConnectionDB;
                sqldataadapter.SelectCommand.Transaction = TransactionPresupuesto;

                //  este apartado es para crear el command que guardarà las cuentas de orden presupuestal en la tabla [CUENTAS_ORDEN_PRESUPUESTAL]
                CommandInsertarCuentasPresupuestales = new SqlCommand();
                CommandInsertarCuentasPresupuestales.Transaction = TransactionPresupuesto;
                CommandInsertarCuentasPresupuestales.CommandType = CommandType.StoredProcedure;
                CommandInsertarCuentasPresupuestales.CommandText = "INSERTAR_CUENTAS_ORDEN_PRESUPUESTAL";
                CommandInsertarCuentasPresupuestales.Connection = ClassBaseDeDatos.MyConnectionDB;
                CommandInsertarCuentasPresupuestales.Parameters.Add("@ID_COG", SqlDbType.VarChar);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@ID_UR", SqlDbType.VarChar);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@APROBADO_ABONO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@APROBADO_CARGO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@COMPROMETIDO_ABONO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@COMPROMETIDO_CARGO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@EJERCER_ABONO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@EJERCER_CARGO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@DEVENGADO_ABONO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@DEVENGADO_CARGO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@EJERCIDO_ABONO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@EJERCIDO_CARGO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@PAGADO_ABONO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@PAGADO_CARGO", SqlDbType.Money);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@NUEVO_REGISTRO", SqlDbType.Bit);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@ID_REGISTRO_PRESUPUESTO", SqlDbType.Int);
                CommandInsertarCuentasPresupuestales.Parameters.Add("@ID_CUENTA_PRESUPUESTAL", SqlDbType.Int);    // cambiar a su valor autonumerico 



                //  TERMINA EL MODULO ANTERIOR. 
                string NombreProcedimiento = "INSERTAR_CUENTAS_PRESUPUESTALES";

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.Transaction = TransactionPresupuesto;
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@NUEVO_REGISTRO", SqlDbType.Bit);
                CommandInsertCatalogo.Parameters.Add("@ID_COG", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters.Add("@ID_HOJA_PRESUPUESTO", SqlDbType.Int);
                CommandInsertCatalogo.Parameters.Add("@ID_CUENTA_GENERO", SqlDbType.Int);
                CommandInsertCatalogo.Parameters.Add("@ID_CUENTA_GRUPO", SqlDbType.Int);
                CommandInsertCatalogo.Parameters.Add("@ID_CUENTA_RUBRO", SqlDbType.Int);
                CommandInsertCatalogo.Parameters.Add("@CUENTA_DE_PRESUPUESTO", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters.Add("@CARGO", SqlDbType.Money);
                CommandInsertCatalogo.Parameters.Add("@ABONO", SqlDbType.Money);
                CommandInsertCatalogo.Parameters.Add("@ID_CUENTA_PRESUPUESTAL", SqlDbType.Int);    // cambiar a su valor autonumerico 



                for (int row = 0; row < CuentaGenero.GetLength(0); row++)
                {
                    ID_GENERO = CuentaGenero[row, 0].ToString();
                    ID_GRUPO = CuentaGenero[row, 1].ToString();
                    ID_RUBRO = CuentaGenero[row, 2].ToString();

                    for (int Elem = 0; Elem < GridviewCargaPresupuestal.Rows.Count; Elem++)
                    {
                        Application.DoEvents();

                        if (!GridviewCargaPresupuestal.Rows[Elem].IsNewRow)
                        {

                            if (GridviewCargaPresupuestal.Rows[Elem].Cells[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index].Value != null)
                            {
                                CodigoCOG = GridviewCargaPresupuestal.Rows[Elem].Cells[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index].Value.ToString();
                                ID_CUENTA = GridviewCargaPresupuestal.Rows[Elem].Cells[GridviewCargaPresupuestal.Columns["ID_CUENTA"].Index].Value.ToString();
                                CARGO = GridviewCargaPresupuestal.Rows[Elem].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Value.ToString();
                                ABONO = CARGO = SignoMonedamoneda.Replace(CARGO.Trim(), "");

                                if (CodigoCOG.Trim() != "")
                                {
                                    if (ID_GENERO == "8")
                                    {
                                        if (ID_GRUPO == "2")
                                        {
                                            if (ID_RUBRO == "1")
                                                CARGO = "0";
                                            else if (ID_RUBRO == "2")
                                                ABONO = "0";
                                            else
                                            {
                                                CARGO = "0";
                                                ABONO = "0";
                                            }
                                        }

                                        CUENTA_PRESUPUESTO = ID_GENERO + "." + ID_GRUPO + "." + ID_RUBRO + "." + ID_CUENTA;

                                        ProcesoCorrecto = GenerarCuentas(ID_GENERO, ID_GRUPO, ID_RUBRO, CodigoCOG, CARGO, ABONO, CUENTA_PRESUPUESTO);
                                        if (!ProcesoCorrecto)
                                            break;
                                        else
                                        {
                                            if (row == 0)  // LAS CUENTAS POR  UR Y COG SE GENERAN EN UN SOLO RECORRIDO DEL DATAGRIDVIEW.   TABLA  CUENTAS_DE_ORDEN_PRESUPUESTAL
                                            {
                                                ProcesoCorrecto = GenerarCuentasDeOrdenPresupuestal(CodigoCOG);
                                                if (!ProcesoCorrecto)
                                                    break;
                                            }
                                        }

                                    }
                                }
                            }

                            RegistroInicial++;
                            progressBarProceso.Value = RegistroInicial;
                            labelInicial.Text = RegistroInicial.ToString("#,###", _ClassParametros.MiRegionProvider);

                        }
                    }
                    if (!ProcesoCorrecto) break;
                }

                if (ProcesoCorrecto)
                    TransactionPresupuesto.Commit();
                else
                    TransactionPresupuesto.Rollback();                
            }
            else
                ProcesoCorrecto = false; 
            this.Cursor = Cursors.Hand;
            panelProceso.Visible = false;
            if (ProcesoCorrecto)
            {
                MessageBox.Show(this, "El proceso ha terminado correctamente", "Proceso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (CommandInsertCatalogo != null) CommandInsertCatalogo.Dispose();
            if (TransactionPresupuesto != null) TransactionPresupuesto.Dispose();

        }

        private bool ExtraerTipoDeCuentas()
        {
            bool ProcesoCorrecto = true;
            string MySql = "SELECT DISTINCT ID_CUENTA_GENERO, ID_CUENTA_GRUPO, ID_CUENTA_RUBRO FROM CAT_CUENTA " +
            " WHERE (ID_CUENTA_GENERO = 8) AND (ID_CUENTA_GRUPO = 2) AND (ID_CUENTA_RUBRO <> 0) " +
            " ORDER BY ID_CUENTA_GENERO, ID_CUENTA_GRUPO, ID_CUENTA_RUBRO";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;

            try
            {
                sqldataadapter = new SqlDataAdapter(MySql, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.Text;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "TiposDeCuenta");
                datatable = dataset.Tables["TiposDeCuenta"];
                int RegistroActual = 0;
                int NumRegistros = datatable.Rows.Count;
                if (datatable.Rows.Count > 0)
                {
                    CuentaGenero = new string[datatable.Rows.Count, 3];
                    while (RegistroActual < NumRegistros)
                    {
                        DataRow registro = datatable.Rows[RegistroActual];

                        CuentaGenero[RegistroActual, 0] = registro["ID_CUENTA_GENERO"].ToString();
                        CuentaGenero[RegistroActual, 1] = registro["ID_CUENTA_GRUPO"].ToString();
                        CuentaGenero[RegistroActual, 2] = registro["ID_CUENTA_RUBRO"].ToString();
                        RegistroActual++;
                    }
                }

            }
            catch (Exception MyException)
            {
                MessageBox.Show(MyException.Message.ToString());
                ProcesoCorrecto = false;
            }


            return ProcesoCorrecto;
        }


        private bool GenerarCuentas(string ID_GENERO, string ID_GRUPO, string ID_RUBRO, string ID_COG, string CARGO, string ABONO, string CUENTA_PRESUPUESTO)
        {
            bool ProcesoCorrecto = true;

            try
            {

                CommandInsertCatalogo.Parameters["@ID_COG"].Value = ID_COG;
                CommandInsertCatalogo.Parameters["@ID_HOJA_PRESUPUESTO"].Value = _ClassParametros.IdHojaPresupuesto;
                CommandInsertCatalogo.Parameters["@ID_CUENTA_GENERO"].Value = Convert.ToInt32(ID_GENERO);
                CommandInsertCatalogo.Parameters["@ID_CUENTA_GRUPO"].Value = Convert.ToInt32(ID_GRUPO);
                CommandInsertCatalogo.Parameters["@ID_CUENTA_RUBRO"].Value = Convert.ToInt32(ID_RUBRO);
                CommandInsertCatalogo.Parameters["@CUENTA_DE_PRESUPUESTO"].Value = CUENTA_PRESUPUESTO;
                CommandInsertCatalogo.Parameters["@CARGO"].Value = Math.Round(Convert.ToDouble(CARGO), 2);
                CommandInsertCatalogo.Parameters["@ABONO"].Value = Math.Round(Convert.ToDouble(ABONO), 2);
                CommandInsertCatalogo.Parameters["@ID_CUENTA_PRESUPUESTAL"].Value = -1;
                CommandInsertCatalogo.Parameters["@NUEVO_REGISTRO"].Value = 1;
                CommandInsertCatalogo.ExecuteNonQuery();
            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al guardar los registros en el catálogo CALENDARIO_COG ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }
            return ProcesoCorrecto;
        }

        private void FormPolizaDeAprobacion_Load(object sender, EventArgs e)
        {
            panelProceso.Parent = this; panelProceso.Visible = false;
            panelProceso.BringToFront();
            labelInicial.Text = "";
            labelFinal.Text = "";
            ReubicarPanelProgressBar();

        }

        private void ReubicarPanelProgressBar()
        {
            panelProceso.Left = (this.Width - panelProceso.Width) / 2;
            panelProceso.Top = (this.Height - panelProceso.Height) / 2;
        }

        private void FormPolizaDeAprobacion_ResizeEnd(object sender, EventArgs e)
        {
            ReubicarPanelProgressBar();
        }

        private void FormPolizaDeAprobacion_MaximumSizeChanged(object sender, EventArgs e)
        {
            ReubicarPanelProgressBar();
        }

        private void FormPolizaDeAprobacion_MinimumSizeChanged(object sender, EventArgs e)
        {
            ReubicarPanelProgressBar();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormReportePolizaDeAprobacion _FormReportePolizaDeAprobacion = new FormReportePolizaDeAprobacion();
            _FormReportePolizaDeAprobacion.ShowDialog();
        }

        private bool GenerarCuentasDeOrdenPresupuestal(string ID_COG)
        {
            bool ProcesoCorrecto = true; 

            string MySql = "SELECT ID_HOJA_PRESUPUESTO, ID_REGISTRO_PRESUPUESTO, ID_UR, ID_COG, PRESUPUESTO  FROM HOJAS_DE_PRESUPUESTO " +
            " WHERE ID_COG = '" + ID_COG.ToString() + "'  AND ID_HOJA_PRESUPUESTO = " + _ClassParametros.IdHojaPresupuesto;


            DataSet dataset = new DataSet();
            DataTable datatable;
            int ID_REGISRO_PRESUPUESTO = 0;
            string ID_UR = "";
            double PRESUPUESTO = 0;

            try
            {

                sqldataadapter.SelectCommand.CommandText = MySql;
                sqldataadapter.Fill(dataset, "CUENTASURCOG");
                datatable = dataset.Tables["CUENTASURCOG"];
                int RegistroActual = 0;
                int NumRegistros = datatable.Rows.Count;
                if (datatable.Rows.Count > 0)
                {
                    while (RegistroActual < NumRegistros)
                    {
                        DataRow registro = datatable.Rows[RegistroActual];
                        ID_REGISRO_PRESUPUESTO = Convert.ToInt32(registro["ID_REGISTRO_PRESUPUESTO"].ToString());
                        ID_UR = registro["ID_UR"].ToString();
                        PRESUPUESTO = Math.Round(Convert.ToDouble(registro["PRESUPUESTO"].ToString()), 2);

                        if (!InsertarCuentasDeOrdenPresupuestal(ID_COG, ID_UR, PRESUPUESTO, ID_REGISRO_PRESUPUESTO))
                        {
                            ProcesoCorrecto = false; break;
                        }

                        RegistroActual++;
                    }
                }
            }
            catch (Exception MyException)
            {
                ProcesoCorrecto = false;
                MessageBox.Show(MyException.Message.ToString());
            }
            return ProcesoCorrecto;
        }


        private bool InsertarCuentasDeOrdenPresupuestal(string ID_COG, string ID_UR, double PRESUPUESTO, int ID_REGISRO_PRESUPUESTO)
        {
            bool ProcesoCorrecto = true;
            try
            {
                CommandInsertarCuentasPresupuestales.Parameters["@ID_COG"].Value = ID_COG;
                CommandInsertarCuentasPresupuestales.Parameters["@ID_UR"].Value = ID_UR;
                CommandInsertarCuentasPresupuestales.Parameters["@APROBADO_ABONO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@APROBADO_CARGO"].Value = PRESUPUESTO;
                CommandInsertarCuentasPresupuestales.Parameters["@COMPROMETIDO_ABONO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@COMPROMETIDO_CARGO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@EJERCER_ABONO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@EJERCER_CARGO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@DEVENGADO_ABONO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@DEVENGADO_CARGO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@EJERCIDO_ABONO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@EJERCIDO_CARGO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@PAGADO_ABONO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@PAGADO_CARGO"].Value = 0;
                CommandInsertarCuentasPresupuestales.Parameters["@NUEVO_REGISTRO"].Value = 1;
                CommandInsertarCuentasPresupuestales.Parameters["@ID_REGISTRO_PRESUPUESTO"].Value = ID_REGISRO_PRESUPUESTO;
                CommandInsertarCuentasPresupuestales.Parameters["@ID_CUENTA_PRESUPUESTAL"].Value = -1;  // CUANDO ES UN INSERT, ESTE VALOR SE DESCARTA DENTRO DEL PROCEDIMIENTO ALMACENADO.
                CommandInsertarCuentasPresupuestales.ExecuteNonQuery();
            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al guardar los registros en el catálogo CALENDARIO_COG ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }
            return ProcesoCorrecto;
        }
    }
}
