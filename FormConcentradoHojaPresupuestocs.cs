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

using MyExcel = Microsoft.Office.Interop.Excel;


namespace SIAPG
{
    public partial class FormConcentradoHojaPresupuestocs : Form
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

        public FormConcentradoHojaPresupuestocs()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormConcentradoHojaPresupuestocs_Load(object sender, EventArgs e)
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

            CargarConcentradosHojasDeTrabajo();

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
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@IdHojaPresupuesto", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@IdHojaPresupuesto"].Value = _ClassParametros.IdHojaPresupuesto; 
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "concentradohojadetrabajo");
                datatable = dataset.Tables["concentradohojadetrabajo"];

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
                        if (Array.IndexOf(new String[] { "CODIGO", "TOTAL", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" }, NombreCelda) >= 0)
                        {
                            columgrid.Width = 100;
                            if (NombreCelda != "CODIGO")
                            {
                                cellgrid.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                cellgrid.Style.Padding = paddingmonto;
                            }
                            else
                                cellgrid.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                            if (Array.IndexOf(new String[] { "TOTAL", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" }, NombreCelda) >= 0)
                                columnasvalue[elem] = Math.Round(Convert.ToDecimal(registro[elem].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                            else
                                columnasvalue[elem] = registro[elem].ToString();
                        }                           

                        GridviewCargaPresupuestal.Rows.Add(columnasvalue);
                        registrogrid[GridviewCargaPresupuestal.Columns["TOTAL"].Index] += Convert.ToDouble(registro["TOTAL"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["ENERO"].Index] += Convert.ToDouble(registro["ENERO"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["FEBRERO"].Index] += Convert.ToDouble(registro["FEBRERO"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["MARZO"].Index] += Convert.ToDouble(registro["MARZO"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["ABRIL"].Index] += Convert.ToDouble(registro["ABRIL"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["MAYO"].Index] += Convert.ToDouble(registro["MAYO"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["JUNIO"].Index] += Convert.ToDouble(registro["JUNIO"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["JULIO"].Index] += Convert.ToDouble(registro["JULIO"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["AGOSTO"].Index] += Convert.ToDouble(registro["AGOSTO"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["SEPTIEMBRE"].Index] += Convert.ToDouble(registro["SEPTIEMBRE"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["OCTUBRE"].Index] += Convert.ToDouble(registro["OCTUBRE"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["NOVIEMBRE"].Index] += Convert.ToDouble(registro["NOVIEMBRE"].ToString().Trim());
                        registrogrid[GridviewCargaPresupuestal.Columns["DICIEMBRE"].Index] += Convert.ToDouble(registro["DICIEMBRE"].ToString().Trim());
                        GridviewCargaPresupuestal.Rows[RegistroActual].HeaderCell.Value = (RegistroActual + 1).ToString();

                        RegistroActual++;

                    }


                    for (int elem = 0; elem < datatable.Columns.Count; elem++)
                    {
                        columnasvalue[elem] = "";
                        string NombreCelda = GridviewCargaPresupuestal.Columns[elem].Name.ToString();
                        if (Array.IndexOf(new String[] { "TOTAL", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" }, NombreCelda) >= 0)
                            columnasvalue[elem] = Math.Round(Convert.ToDecimal(registrogrid[elem].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                    }
                    GridviewCargaPresupuestal.Rows.Add(columnasvalue);


                    for (int elem = 0; elem < datatable.Columns.Count; elem++)
                    {
                        string NombreCelda = GridviewCargaPresupuestal.Columns[elem].Name.ToString();
                        if (Array.IndexOf(new String[] { "TOTAL", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" }, NombreCelda) >= 0)
                        {
                            columnasvalue[elem] = Math.Round(Convert.ToDecimal(registrogrid[elem].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
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

            sqldataadapter = null;
            dataset = null;
            datatable = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileExcel = new SaveFileDialog();
            SaveFileExcel.Title = "Exportar a Excel";
            SaveFileExcel.Filter  = "Excel |*.xls";
            SaveFileExcel.CheckPathExists = true;
            SaveFileExcel.OverwritePrompt = true; 
            SaveFileExcel.ShowDialog();
            {
                if (SaveFileExcel.FileName != "")
                {
                    IniciarExportarAExel();  
                }
            }

        }

        private void IniciarExportarAExel()
        {
            ConfigurarInicioExcel();
            if (MyApplicationExcel == null)
            {
                MessageBox.Show(this, "Verifique que la instalación del Microsoft Office este correcta, e intente de nuevo.", "No ha sido posible generar el archivo Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            object NewFileName = SaveFileExcel.FileName.ToString();
            MyExcel.Workbook MyWorkBookExcel = MyApplicationExcel.Workbooks.Add(MyExcel.XlWBATemplate.xlWBATWorksheet); // xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            MyWorkSheetExcel = (MyExcel.Worksheet)MyWorkBookExcel.Worksheets[1];
            ConfigurarPaginaExcel();
            PrepararencabezadoExcel();

            if (ExportarRegistros())
            {
                ConfigurarActivarExcel(true);
                MyApplicationExcel.DisplayAlerts = false;
                MyWorkBookExcel.DisplayInkComments = false;
                MyApplicationExcel.ActiveWorkbook.SaveAs(NewFileName, MyExcel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, false, Type.Missing, MyExcel.XlSaveAsAccessMode.xlNoChange, MyExcel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                ((Microsoft.Office.Interop.Excel._Workbook)MyWorkBookExcel).Activate();
            }
        }

        private bool ExportarRegistros()
        {
            bool ProcesoCorrecto = true;
            Padding paddingmonto = new Padding(0, 0, 2, 0);
            Font font = new Font(GridviewCargaPresupuestal.DefaultCellStyle.Font.FontFamily, 8, FontStyle.Regular);


            string[] columnasexcel = _ClassParametros.ColumnasExcel;

            string NombreCelda = "";
            string ContenidoCelda = "";

            foreach (DataGridViewRow row in GridviewCargaPresupuestal.Rows)
            {

                foreach (DataGridViewColumn colum in GridviewCargaPresupuestal.Columns)
                {
                    DataGridViewColumn columgrid = new DataGridViewColumn();
                    DataGridViewTextBoxCell cellgrid = new DataGridViewTextBoxCell();
                    ContenidoCelda = "";
                    NombreCelda = colum.Name.ToString();
                    if (row.Cells[colum.Index].Value != null)
                        ContenidoCelda = row.Cells[colum.Index].Value.ToString();

                    columgrid.HeaderText = NombreCelda;
                    columgrid.Name = NombreCelda;
                    cellgrid.Style.Font = font;
                    MyExcel.Range RangoFormato = MyWorkSheetExcel.get_Range(columnasexcel[colum.Index] + RenglonHeadExcel.ToString() + ":" + columnasexcel[colum.Index] + RenglonHeadExcel.ToString(), Type.Missing);
                    RangoFormato.Value = ContenidoCelda;
                    RangoFormato.Font.Name = "Calibri";
                    RangoFormato.Font.Size = 10;

                    if (Array.IndexOf(new String[] { "CODIGO" }, NombreCelda) >= 0)
                    {
                        RangoFormato.HorizontalAlignment = MyExcel.XlHAlign.xlHAlignCenter;
                    }
                }
                RenglonHeadExcel++;

            }
            MyExcel.Range RangoFormatoBorder = MyWorkSheetExcel.get_Range(columnasexcel[0] + "1:" + columnasexcel[GridviewCargaPresupuestal.Columns.Count-1] + (RenglonHeadExcel-2).ToString(), Type.Missing);
            RangoFormatoBorder.Borders.Color = ColorTranslator.FromHtml("#707070");

            MyExcel.Range RangoFormatoFoot = MyWorkSheetExcel.get_Range(columnasexcel[0] + (RenglonHeadExcel-2).ToString() + ":" + columnasexcel[GridviewCargaPresupuestal.Columns.Count - 1] + (RenglonHeadExcel - 2).ToString(), Type.Missing);
            RangoFormatoFoot.Font.Color = ColorTranslator.FromHtml("#ffffff");
            RangoFormatoFoot.Interior.Color = ColorTranslator.FromHtml("#aa0000");
            return ProcesoCorrecto;
        }
        private void ConfigurarInicioExcel()
        {
            MyApplicationExcel = new MyExcel.Application();
            MyApplicationExcel.EnableEvents = true;
            MyApplicationExcel.Visible = true;
            MyApplicationExcel.Interactive = false;
            MyApplicationExcel.ScreenUpdating = false;           
        }

        private void ConfigurarPaginaExcel()
        {
            try
            {
                MyWorkSheetExcel.PageSetup.PaperSize = MyExcel.XlPaperSize.xlPaperLetter;
                MyWorkSheetExcel.PageSetup.Orientation = MyExcel.XlPageOrientation.xlLandscape;
                MyWorkSheetExcel.PageSetup.LeftMargin = 1;
                MyWorkSheetExcel.PageSetup.RightMargin = 1;
                MyWorkSheetExcel.PageSetup.TopMargin = 1.4;
                MyWorkSheetExcel.PageSetup.BottomMargin = 1.4;
                MyWorkSheetExcel.PageSetup.CenterHorizontally = true;
                MyWorkSheetExcel.PageSetup.FooterMargin = 0.8;
                MyWorkSheetExcel.PageSetup.HeaderMargin = 0.8;
                MyWorkSheetExcel.PageSetup.Zoom = 78;
            }
            catch
            {
            }
        }
        private void ConfigurarActivarExcel(bool StatusActivacion)
        {
            if (StatusActivacion)
            {
                if (MyApplicationExcel != null)
                {
                    MyApplicationExcel.Visible = true;
                    MyApplicationExcel.EnableEvents = false;
                    MyApplicationExcel.Interactive = true;
                    MyApplicationExcel.ScreenUpdating = true;
                    MyApplicationExcel.Calculation = MyExcel.XlCalculation.xlCalculationAutomatic;
                }
            }
        }


        private void PrepararencabezadoExcel()
        {
            double PorcentajeWidth = 6.5;
            RenglonHeadExcel = 1;
            string[] columnasexcel = _ClassParametros.ColumnasExcel;
        
            for (int Elem = 0; Elem < GridviewCargaPresupuestal.ColumnCount; Elem++)
            {
                MyWorkSheetExcel.get_Range(columnasexcel[Elem] + RenglonHeadExcel.ToString(), columnasexcel[Elem] + RenglonHeadExcel.ToString()).ColumnWidth = (int) GridviewCargaPresupuestal.Columns[Elem].Width / PorcentajeWidth;
                MyWorkSheetExcel.get_Range(columnasexcel[Elem] + RenglonHeadExcel.ToString(), columnasexcel[Elem] + RenglonHeadExcel.ToString()).Value2 = GridviewCargaPresupuestal.Columns[Elem].Name;
            }

            MyExcel.Range RangoFormato = MyWorkSheetExcel.get_Range(columnasexcel[0] + RenglonHeadExcel.ToString() + ":" + columnasexcel[GridviewCargaPresupuestal.ColumnCount-1] + RenglonHeadExcel.ToString(), Type.Missing);

            RangoFormato.Font.Size = 11;
            RangoFormato.Font.Name = "Calibri";
            RangoFormato.Font.Bold = false;
            RangoFormato.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            RangoFormato.Interior.Color = ColorTranslator.FromHtml("#0889f2");   // System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
            RangoFormato.Interior.Pattern = Microsoft.Office.Interop.Excel.XlPattern.xlPatternSolid;
            RangoFormato.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            RangoFormato.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            RangoFormato.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);          
            RenglonHeadExcel++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormReportesConcentrado _FormReportesConcentrado = new FormReportesConcentrado();
            _FormReportesConcentrado.ShowDialog(); 
        }
    }
}
