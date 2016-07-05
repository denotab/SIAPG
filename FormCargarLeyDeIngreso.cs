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
    public partial class FormCargarLeyDeIngreso : Form
    {

        SqlTransaction TransactionIngreso;
        SqlCommand CommandTransactionHojas;
        DbDataReader DataRecordExcel;
        ClassDetallesPresupuestales _ClassDetallesPresupuestales = new ClassDetallesPresupuestales();


        Properties.Settings PropiedadesAPP = new Properties.Settings();
        ClassParametros _ClassParametros = new ClassParametros();
        ClassTipoProceso _ClassTipoProceso = new ClassTipoProceso();

        FormCapturaPorcentajeCOG _FormCapturaPorcentajeCOG;

        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassOpcionesSeleccionadas OpcionesSeleccionadas = new ClassOpcionesSeleccionadas();
        int RenglonActual = 0;
        int ColumnaActual = 0;
        int NumRegistros = 0;
        bool Insertar = false;
        bool Agregar = false;
        int NumRegistrosSeleccionados = 0;
        int NumColumnasSeleccionadas = 0;
        int CatalogoSeleccionado = 0;
        bool Multiselect = false;
        int TipoDeOperacionSeleccionada = 0;
        int leftbuttonFunciones = 0;
        int topbuttonFunciones = 0;
        int NumRegistrosEliminados;

        Point PosicionFormularioUR = new Point();
        Size TamanioFormulario = new Size();

        string[,] BloqueSeleccionado;
        string AlternatingRowStyleBackColor = "#dce6f1";
        string AlternatingRowStyleForeColor = "#090909";
        Color DefaultRowStyleBackColor;
        Color DefaultRowStyleForeColor;

        double[] CalendarizacionAnual = new double[12];
        double TotalCalendarizacionAnual = 0;

        bool Lahojasehamodificado = false;
        int NumErrores = 0;


        enum TipoCatalogo
        {
            CatalogoUR = 1,
            CatalogoFFS = 2,
            CatalogoCOG = 3,
            ClasificacionProgramatica = 4,
            FuenteDeFinanciamiento = 5,
            ProgramaPresupuestal = 6
        }

        enum TipoOperacion
        {
            AgregarUR = 1,
            InsertarUR = 2,
            DuplicarBloqueUR = 3,
            EliminarBloqueUR = 4,
            CopiarBloqueUR = 5,
            RelacionarURyCOG = 6,

            AgregarFFS = 7,
            InsertarFFS = 8,
            DuplicarBloqueFFS = 9,
            EliminarBloqueFFS = 10,
            CopiarBloqueFFS = 11,

            AgregarCOG = 12,
            InsertarCOG = 13,
            DuplicarBloqueCOG = 14,
            EliminarBloqueCOG = 15,
            CopiarBloqueCOG = 16,
            RelacionarCOGyUR = 17,

            AgregarClasificacionProgramatica = 18,
            InsertarClasificacionProgramatica = 19,
            DuplicarClasificacionProgramatica = 20,
            EliminarClasificacionProgramatica = 21,
            CopiarClasificacionProgramatica = 22,
            RelacionarClasificacionProgramatica = 23,

            AgregarFuenteDeFinanciamiento = 24,
            InsertarFuenteDeFinanciamiento = 25,
            DuplicarFuenteDeFinanciamiento = 26,
            EliminarFuenteDeFinanciamiento = 28,
            CopiarFuenteDeFinanciamiento = 28,
            RelacionarFuenteDeFinanciamiento = 29

        }
        public FormCargarLeyDeIngreso()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;
        }

        private void FormCargarLeyDeIngreso_Load(object sender, EventArgs e)
        {
            configurarEtiquetasgridview();
            PausaBackGround.RunWorkerAsync();
            _ClassParametros.NuevaHojaDeIngreso = true;
            panelProceso.Parent = this; panelProceso.Visible = false;
            ConfigurarPanelProgressBar();

        }

      
        private void ConfigurarGrid()
        {
            GridviewCargaIngreso.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml(AlternatingRowStyleBackColor);
            GridviewCargaIngreso.AlternatingRowsDefaultCellStyle.ForeColor = ColorTranslator.FromHtml(AlternatingRowStyleForeColor);

            DefaultRowStyleBackColor = GridviewCargaIngreso.DefaultCellStyle.BackColor;
            DefaultRowStyleForeColor = GridviewCargaIngreso.DefaultCellStyle.ForeColor;
        }

        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {
            ConfigurarGrid();
            CargarListaCOGPorcentual();

        }

        private void GridviewCargaPresupuestal_SelectionChanged(object sender, EventArgs e)
        {
            NumRegistrosSeleccionados = GridviewCargaIngreso.SelectedRows.Count;
            if (NumRegistrosSeleccionados == 0)
            {
                NumColumnasSeleccionadas = GridviewCargaIngreso.SelectedCells.Count;
                if (NumColumnasSeleccionadas > 1)
                {
                    buttonFunciones.Visible = false;
                    Multiselect = true;
                }
                else
                    Multiselect = false;
            }
            else
            {
                NumRegistrosEliminados = 0;
                NumColumnasSeleccionadas = 0;
                buttonFunciones.Visible = false;
                Multiselect = true;
            }


        }

        private void GridviewCargaPresupuestal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if ((Array.IndexOf(new String[] { "CodigoUR", "DescripcionUR", "CodigoCOG", "COG", "CodigoCF", "ClasificadorCF", "CodigoPrograma", "Programa", "codigoFuente", "FuenteFinanciamiento" }, GridviewCargaIngreso.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                {
                    Rectangle rectangledisplay = GridviewCargaIngreso.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                    int formWidthBorder = this.Width - this.ClientSize.Width;
                    int formBorderWidth = (int)((this.Width - this.ClientSize.Width)); // / 2);
                    int formTitleBarHeight = (int)(this.Height - this.ClientSize.Height); // - 2 * formBorderWidth);

                    // panel1.Left +
                    // panel1.Top +

                    leftbuttonFunciones = formBorderWidth + GridviewCargaIngreso.Left + rectangledisplay.Left + rectangledisplay.Width - (buttonFunciones.Width * 2); //  + this.Left; 
                    topbuttonFunciones = formBorderWidth + GridviewCargaIngreso.Top + rectangledisplay.Top + rectangledisplay.Height - (buttonFunciones.Height * 2); // + this.Top;

                    Point point = new Point(leftbuttonFunciones, topbuttonFunciones);
                    buttonFunciones.Location = point;
                    buttonFunciones.Visible = true;

                    if ((Array.IndexOf(new String[] { "CodigoUR", "DescripcionUR" }, GridviewCargaIngreso.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                        CatalogoSeleccionado = (int)TipoCatalogo.CatalogoUR;
                    else if ((Array.IndexOf(new String[] { "CodigoCF", "ClasificadorCF" }, GridviewCargaIngreso.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                        CatalogoSeleccionado = (int)TipoCatalogo.CatalogoFFS;
                    else if ((Array.IndexOf(new String[] { "CodigoCOG", "COG" }, GridviewCargaIngreso.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                        CatalogoSeleccionado = (int)TipoCatalogo.CatalogoCOG;
                    else if ((Array.IndexOf(new String[] { "CodigoPrograma", "Programa" }, GridviewCargaIngreso.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                        CatalogoSeleccionado = (int)TipoCatalogo.ClasificacionProgramatica;
                    else if ((Array.IndexOf(new String[] { "codigoFuente", "FuenteFinanciamiento" }, GridviewCargaIngreso.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                        CatalogoSeleccionado = (int)TipoCatalogo.FuenteDeFinanciamiento;


                }
                else
                {
                    buttonFunciones.Visible = false;
                }
            }
            catch
            {

            }
        }

        private void buttonFunciones_Click(object sender, EventArgs e)
        {
            if (CatalogoSeleccionado == (int)TipoCatalogo.CatalogoUR)
            {
                contextMenuStripUR.Items["duplicarBloqueURSToolStripMenuItem"].Enabled = false;
                contextMenuStripUR.Items["eliminarURSToolStripMenuItem"].Enabled = false;
                contextMenuStripUR.Items["AgregarURSToolStripMenuItem"].Enabled = true;
                contextMenuStripUR.Items["insertarURSToolStripMenuItem"].Enabled = true;
                contextMenuStripUR.Items["relacionarCOGToolStripMenuItem"].Enabled = false;

                contextMenuStripUR.Show(PosicionMenu());
            }
            else if (CatalogoSeleccionado == (int)TipoCatalogo.CatalogoFFS)
            {
                contextMenuStripFFS.Show(PosicionMenu());
            }
            else if (CatalogoSeleccionado == (int)TipoCatalogo.CatalogoCOG)
            {
                contextMenuStripCOG.Show(PosicionMenu());
            }
            else if (CatalogoSeleccionado == (int)TipoCatalogo.ClasificacionProgramatica)
            {
                contextMenuStripClasificacionProgramatica.Show(PosicionMenu());
            }

            else if (CatalogoSeleccionado == (int)TipoCatalogo.FuenteDeFinanciamiento)
            {
                contextMenuStripFuenteDeFinanciamiento.Show(PosicionMenu());
            }

        }

        private Point PosicionMenu()
        {
            Point PosicionMenu = new Point();
            PosicionMenu.X = buttonFunciones.Left;
            PosicionMenu.Y = buttonFunciones.Top + buttonFunciones.Height;

            return PosicionMenu;
        }

        private Point UbicacionFormulario(Size TamañoFormulario)
        {
            // TamanioFormulario
            Point PosicionFormulario = new Point();

            #region   // Configuracion de la posicion formulario UR
            if (CatalogoSeleccionado == (int)TipoCatalogo.CatalogoUR)
            {
                if (PropiedadesAPP.PosicionFormularioUR.Y <= 0)   // significa que el formulario no se ha visualizado en ninguna ocasion
                {
                    if (buttonFunciones.Top + TamanioFormulario.Height < Screen.PrimaryScreen.WorkingArea.Height)
                    {
                        PosicionFormulario.Y = buttonFunciones.Top;
                    }
                    else
                    {
                        PosicionFormulario.Y = Screen.PrimaryScreen.WorkingArea.Height - TamañoFormulario.Height - buttonFunciones.Height;
                    }
                }
                else
                    PosicionFormulario.Y = PropiedadesAPP.PosicionFormularioUR.Y;


                if (PropiedadesAPP.PosicionFormularioUR.X <= 0)   // significa que el formulario no se ha visualizado en ninguna ocasion
                {
                    if (buttonFunciones.Left + TamanioFormulario.Width < Screen.PrimaryScreen.WorkingArea.Width)
                    {
                        PosicionFormulario.X = buttonFunciones.Left;
                    }
                    else
                    {
                        PosicionFormulario.X = Screen.PrimaryScreen.WorkingArea.Width - TamañoFormulario.Width - buttonFunciones.Width;
                    }
                }
                else
                    PosicionFormulario.X = PropiedadesAPP.PosicionFormularioUR.X;
            }
            #endregion

            #region   // Configuracion de la posicion del formulario FFS
            else
            {
                if (CatalogoSeleccionado == (int)TipoCatalogo.CatalogoFFS)
                {
                    if (PropiedadesAPP.PosicionFormularioFFS.Y <= 0)   // significa que el formulario no se ha visualizado en ninguna ocasion
                    {
                        if (buttonFunciones.Top + TamanioFormulario.Height < Screen.PrimaryScreen.WorkingArea.Height)
                        {
                            PosicionFormulario.Y = buttonFunciones.Top;
                        }
                        else
                        {
                            PosicionFormulario.Y = Screen.PrimaryScreen.WorkingArea.Height - TamañoFormulario.Height - buttonFunciones.Height;
                        }
                    }
                    else
                        PosicionFormulario.Y = PropiedadesAPP.PosicionFormularioFFS.Y;


                    if (PropiedadesAPP.PosicionFormularioFFS.X <= 0)   // significa que el formulario no se ha visualizado en ninguna ocasion
                    {
                        if (buttonFunciones.Left + TamanioFormulario.Width < Screen.PrimaryScreen.WorkingArea.Width)
                        {
                            PosicionFormulario.X = buttonFunciones.Left;
                        }
                        else
                        {
                            PosicionFormulario.X = Screen.PrimaryScreen.WorkingArea.Width - TamañoFormulario.Width - buttonFunciones.Width;
                        }
                    }
                    else
                        PosicionFormulario.X = PropiedadesAPP.PosicionFormularioFFS.X;
                }
            }
            #endregion

            return PosicionFormulario;
        }

        private void GridviewCargaPresupuestal_MouseUp(object sender, MouseEventArgs e)
        {
            if (Multiselect)
            {

                if (NumRegistrosSeleccionados == 0 && NumColumnasSeleccionadas > 1)
                {

                    DataGridViewSelectedCellCollection CelldasSeleccionadas = GridviewCargaIngreso.SelectedCells;
                    string NombreCelda = GridviewCargaIngreso.Columns[CelldasSeleccionadas[CelldasSeleccionadas.Count - 1].ColumnIndex].Name.ToString();

                    if (Array.IndexOf(new String[] { "DescripcionUR", "CodigoUR" }, NombreCelda) >= 0)
                    {
                        contextMenuStripUR.Items["duplicarBloqueURSToolStripMenuItem"].Enabled = true;
                        contextMenuStripUR.Items["eliminarURSToolStripMenuItem"].Enabled = true;
                        contextMenuStripUR.Items["AgregarURSToolStripMenuItem"].Enabled = false;
                        contextMenuStripUR.Items["insertarURSToolStripMenuItem"].Enabled = false;
                        contextMenuStripUR.Items["relacionarCOGToolStripMenuItem"].Enabled = true;

                        Rectangle rectangledisplay = GridviewCargaIngreso.GetCellDisplayRectangle(CelldasSeleccionadas[CelldasSeleccionadas.Count - 1].ColumnIndex, CelldasSeleccionadas[CelldasSeleccionadas.Count - 1].RowIndex, false);

                        int formWidthBorder = this.Width - this.ClientSize.Width;
                        int formBorderWidth = (int)((this.Width - this.ClientSize.Width) / 2);
                        int formTitleBarHeight = (int)(this.Height - this.ClientSize.Height - 2 * formBorderWidth);


                        //  panel1.Left
                        // panel1.Top +
                        int left = this.Left + formBorderWidth + GridviewCargaIngreso.Left + rectangledisplay.Left + rectangledisplay.Width - buttonFunciones.Width;
                        int top = this.Top + formBorderWidth + GridviewCargaIngreso.Top + rectangledisplay.Top + rectangledisplay.Height - buttonFunciones.Height;

                        Point point = new Point(left, top);
                        buttonFunciones.Location = point;
                        buttonFunciones.Visible = true;

                        contextMenuStripUR.Show(PosicionMenu());
                    }
                }

            }
        }

        private void configurarEtiquetasgridview()
        {
            string colorpaneletiqueta = "#D0CECE";
            string colorpictureEtiqueta = "#7F7F7F";

            string Coloretiqueta0 = "#00B0F0";
            string Coloretiqueta1 = "#00B0F0";
            string Coloretiqueta2 = "#FFC519";
            string Coloretiqueta3 = "#C55A11";


            panelEtiquetas.Left = GridviewCargaIngreso.Left;
            panelEtiquetas.Width = GridviewCargaIngreso.Width;
            pictureBoxEtiquetas.Parent = panelEtiquetas;


            pictureBoxEtiquetas.Left = 0;
            pictureBoxEtiquetas.Top = 2;

            Etiqueta0.Parent = pictureBoxEtiquetas;
            Etiqueta1.Parent = pictureBoxEtiquetas;
            Etiqueta2.Parent = pictureBoxEtiquetas;
            Etiqueta3.Parent = pictureBoxEtiquetas;

            Etiqueta0.Top = 2;
            Etiqueta1.Top = 2;
            Etiqueta2.Top = 2;
            Etiqueta3.Top = 2;


            Etiqueta0.Height = 26;
            Etiqueta1.Height = 26;
            Etiqueta2.Height = 26;
            Etiqueta3.Height = 26;


            panelEtiquetas.BackColor = ColorTranslator.FromHtml(colorpaneletiqueta);
            pictureBoxEtiquetas.BackColor = ColorTranslator.FromHtml(colorpictureEtiqueta);
            Etiqueta0.BackColor = ColorTranslator.FromHtml(Coloretiqueta0);
            Etiqueta1.BackColor = ColorTranslator.FromHtml(Coloretiqueta1);
            Etiqueta2.BackColor = ColorTranslator.FromHtml(Coloretiqueta2);
            Etiqueta3.BackColor = ColorTranslator.FromHtml(Coloretiqueta3);

            pictureBoxEtiquetas.Height = Etiqueta0.Height + 2;
            panelEtiquetas.Height = pictureBoxEtiquetas.Height + 2;
            panelEtiquetas.Top = GridviewCargaIngreso.Top - panelEtiquetas.Height;

            Etiqueta0.Left = 2;
            ajustartamañoEqituetas();
        }

        private void ajustartamañoEqituetas()
        {


            Etiqueta0.Width = GridviewCargaIngreso.RowHeadersWidth;
            Etiqueta1.Width = GridviewCargaIngreso.Columns[0].Width + GridviewCargaIngreso.Columns[1].Width;

            Etiqueta2.Width = GridviewCargaIngreso.Columns[2].Width + GridviewCargaIngreso.Columns[3].Width;
            Etiqueta3.Width = GridviewCargaIngreso.Columns[4].Width + GridviewCargaIngreso.Columns[5].Width;

            Etiqueta3.Width = 0;
            for (int col = 4; col < GridviewCargaIngreso.Columns.Count; col++)
            {
                if (GridviewCargaIngreso.Columns[col].Visible)
                   Etiqueta3.Width += GridviewCargaIngreso.Columns[col].Width;
            }


            pictureBoxEtiquetas.Width = Etiqueta0.Width + Etiqueta1.Width + Etiqueta2.Width + Etiqueta3.Width  + 2;


            Etiqueta1.Left = Etiqueta0.Left + Etiqueta0.Width;
            Etiqueta2.Left = Etiqueta1.Left + Etiqueta1.Width;
            Etiqueta3.Left = Etiqueta2.Left + Etiqueta2.Width;

        }

        private void GridviewCargaPresupuestal_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                int ValorScroll = GridviewCargaIngreso.HorizontalScrollingOffset;
                pictureBoxEtiquetas.Left = ValorScroll * -1;

                buttonFunciones.Visible = false;

            }

        }

        private void GridviewCargaPresupuestal_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string NombreCelda = GridviewCargaIngreso.Columns[e.ColumnIndex].Name.ToString();
            // En este apartado vamos a validar 

            Lahojasehamodificado = true;

            try
            {
                if (Array.IndexOf(new String[] { "PresupuestoAsignado", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" }, NombreCelda) >= 0)
                {
                    if (GridviewCargaIngreso.Rows[e.RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Value != null)
                    {
                        if (NombreCelda == "PresupuestoAsignado")
                        {
                            if (GridviewCargaIngreso.Rows[e.RowIndex].Cells[GridviewCargaIngreso.Columns["CodigoCOG"].Index].Value != null)
                            {
                                string IDCOGCelda = GridviewCargaIngreso.Rows[e.RowIndex].Cells[GridviewCargaIngreso.Columns["CodigoCOG"].Index].Value.ToString();
                                string COGCelda = GridviewCargaIngreso.Rows[e.RowIndex].Cells[GridviewCargaIngreso.Columns["COG"].Index].Value.ToString();

                                if (_ClassParametros.IDCOC == IDCOGCelda)
                                    AsignarPresupuestoPorcentual(e.RowIndex);
                                else if (ExisteCOGEnListaPreliminar(IDCOGCelda))
                                    AsignarPresupuestoPorcentual(e.RowIndex);
                                else
                                {
                                    _ClassParametros.IngresoAlternativo = true;
                                    _ClassParametros.IdRowGrid = e.RowIndex;
                                    _ClassParametros.IDCOGCELDA = IDCOGCelda;
                                    _ClassParametros.COGCELDA = COGCelda;
                                    ActivarFormularioCapturaPorcentajePresupuestal();
                                }
                                decimal Presupuesto = Convert.ToDecimal(GridviewCargaIngreso.Rows[e.RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Value.ToString());
                                GridviewCargaIngreso.Rows[e.RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Value = Presupuesto.ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                            }
                        }
                    }

                    CompararPresupuestoYCalendarizacion(e.RowIndex);
                }
                else
                {
                    if (Array.IndexOf(new String[] { "CodigoCRI", "DescripcionCRI" }, NombreCelda) >= 0)
                    {


                    }
                }


                if (GridviewCargaIngreso.Rows[e.RowIndex].Cells[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index].Value != null)
                {
                    if (Convert.ToInt32(GridviewCargaIngreso.Rows[e.RowIndex].Cells[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index].Value.ToString()) > 0)
                    {
                        GridviewCargaIngreso.Rows[e.RowIndex].Cells[GridviewCargaIngreso.Columns["rowmodificado"].Index].Value = "1";   // registro posiblemente modificado. 
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message.ToString());
            }
        }

        private void CompararPresupuestoYCalendarizacion(int RowIndex)
        {
            double Presupuesto = Convert.ToDouble(GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Value.ToString());
            TotalCalendarizacionAnual = 0;
            int colenero = GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns["ENERO"].Index].ColumnIndex;
            for (int col = colenero; col < GridviewCargaIngreso.ColumnCount; col++)
            {
                string NombreCelda = GridviewCargaIngreso.Columns[col].Name.ToString();
                if (Array.IndexOf(new String[] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" }, NombreCelda) >= 0)
                {
                    if (GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns[col].Index].Value != null)
                    {
                        string valuecell = GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns[col].Index].Value.ToString();
                        if (_ClassParametros.IsNumeric(valuecell))
                        {
                            TotalCalendarizacionAnual += Convert.ToDouble(valuecell);
                        }
                    }
                }
            }
            Presupuesto = Math.Round(Presupuesto, 2);
            TotalCalendarizacionAnual = Math.Round(TotalCalendarizacionAnual, 2);

            if (TotalCalendarizacionAnual != Presupuesto)
            {
                GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Style.BackColor = Color.DarkRed;
                GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Style.ForeColor = Color.White;
            }
            else
            {
                if (RowIndex % 2 > 0)
                {
                    GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Style.BackColor = ColorTranslator.FromHtml(AlternatingRowStyleBackColor);
                    GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Style.ForeColor = ColorTranslator.FromHtml(AlternatingRowStyleForeColor);
                }
                else
                {
                    GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Style.BackColor = DefaultBackColor;
                    GridviewCargaIngreso.Rows[RowIndex].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Style.ForeColor = DefaultForeColor;
                }
            }
        }


        private void ActivarFormularioCapturaPorcentajePresupuestal()
        {
            Point Position = new Point();


            if (Application.OpenForms["FormCapturaPorcentajeCOG"] != null)
            {
                _FormCapturaPorcentajeCOG.AsignarValoresCOG();
                _FormCapturaPorcentajeCOG.AsignarCalendarizacioPorCOG();
                Application.OpenForms["FormCapturaPorcentajeCOG"].Activate();
                Application.OpenForms["FormCapturaPorcentajeCOG"].Show();
            }
            else
            {
            //    _FormCapturaPorcentajeCOG = new FormCapturaPorcentajeCOG(this);
                Position.Y = Screen.PrimaryScreen.WorkingArea.Height - _FormCapturaPorcentajeCOG.Height - 4;
                Position.X = (Screen.PrimaryScreen.WorkingArea.Width / 2) + (_FormCapturaPorcentajeCOG.Width / 2);
                _FormCapturaPorcentajeCOG.Location = Position;
                _FormCapturaPorcentajeCOG.Show();
            }
        }

        private bool IdCogActivo(string IDCOGCelda)
        {

            if (_ClassParametros.IDCOC == IDCOGCelda)
                return true;
            else
                return false;
        }

        private bool ExisteCOGEnListaPreliminar(string IDCOGCelda)
        {
            bool CogRegistrado = false;

            foreach (ClassParametros._PorcentajeCOG ListaCOG in _ClassParametros.PORCENTAJECOG)
            {
                if (ListaCOG.IDCOG == IDCOGCelda)
                {
                    _ClassParametros.IDCOC = IDCOGCelda;
                    _ClassParametros.PorcentajeEnero = Convert.ToDouble(ListaCOG.ENERO);
                    _ClassParametros.PorcentajeFebrero = Convert.ToDouble(ListaCOG.FEBRERO);
                    _ClassParametros.PorcentajeMarzo = Convert.ToDouble(ListaCOG.MARZO);
                    _ClassParametros.PorcentajeAbril = Convert.ToDouble(ListaCOG.ABRIL);
                    _ClassParametros.PorcentajeMayo = Convert.ToDouble(ListaCOG.MAYO);
                    _ClassParametros.PorcentajeJunio = Convert.ToDouble(ListaCOG.JUNIO);
                    _ClassParametros.PorcentajeJulio = Convert.ToDouble(ListaCOG.JULIO);
                    _ClassParametros.PorcentajeAgosto = Convert.ToDouble(ListaCOG.AGOSTO);
                    _ClassParametros.PorcentajeSeptiembre = Convert.ToDouble(ListaCOG.SEPTIEMBRE);
                    _ClassParametros.PorcentajeOctubre = Convert.ToDouble(ListaCOG.OCTUBRE);
                    _ClassParametros.PorcentajeNoviembre = Convert.ToDouble(ListaCOG.NOVIEMBRE);
                    _ClassParametros.PorcentajeDiciembre = Convert.ToDouble(ListaCOG.DICIEMBRE);
                    CogRegistrado = true;
                    break;
                }
            }
            return CogRegistrado;
        }

        private void AsignarPresupuestoPorcentual(int NumRow)
        {
            double presupuestoCRI = 0;
            double totalceldas = 0;
            double[] porcentajeceldas = new double[12];
            double diferencia = 0;



            presupuestoCRI = Convert.ToDouble(GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Value.ToString());

            porcentajeceldas[0] = Math.Round((_ClassParametros.PorcentajeEnero / 100 * presupuestoCRI), 2);
            porcentajeceldas[1] = Math.Round((_ClassParametros.PorcentajeFebrero / 100 * presupuestoCRI), 2);
            porcentajeceldas[2] = Math.Round((_ClassParametros.PorcentajeMarzo / 100 * presupuestoCRI), 2);
            porcentajeceldas[3] = Math.Round((_ClassParametros.PorcentajeAbril / 100 * presupuestoCRI), 2);
            porcentajeceldas[4] = Math.Round((_ClassParametros.PorcentajeMayo / 100 * presupuestoCRI), 2);
            porcentajeceldas[5] = Math.Round((_ClassParametros.PorcentajeJunio / 100 * presupuestoCRI), 2);
            porcentajeceldas[6] = Math.Round((_ClassParametros.PorcentajeJulio / 100 * presupuestoCRI), 2);
            porcentajeceldas[7] = Math.Round((_ClassParametros.PorcentajeAgosto / 100 * presupuestoCRI), 2);
            porcentajeceldas[8] = Math.Round((_ClassParametros.PorcentajeSeptiembre / 100 * presupuestoCRI), 2);
            porcentajeceldas[9] = Math.Round((_ClassParametros.PorcentajeOctubre / 100 * presupuestoCRI), 2);
            porcentajeceldas[10] = Math.Round((_ClassParametros.PorcentajeNoviembre / 100 * presupuestoCRI), 2);
            porcentajeceldas[11] = Math.Round((_ClassParametros.PorcentajeDiciembre / 100 * presupuestoCRI), 2);


            for (int elem = 0; elem < porcentajeceldas.Length; elem++)
            {
                totalceldas += porcentajeceldas[elem];
            }

            diferencia = presupuestoCRI - totalceldas;
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


            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["ENERO"].Index].Value = porcentajeceldas[0].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["FEBRERO"].Index].Value = porcentajeceldas[1].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["MARZO"].Index].Value = porcentajeceldas[2].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["ABRIL"].Index].Value = porcentajeceldas[3].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["MAYO"].Index].Value = porcentajeceldas[4].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["JUNIO"].Index].Value = porcentajeceldas[5].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["JULIO"].Index].Value = porcentajeceldas[6].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["AGOSTO"].Index].Value = porcentajeceldas[7].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["SEPTIEMBRE"].Index].Value = porcentajeceldas[8].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["OCTUBRE"].Index].Value = porcentajeceldas[9].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["NOVIEMBRE"].Index].Value = porcentajeceldas[10].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaIngreso.Rows[NumRow].Cells[GridviewCargaIngreso.Columns["DICIEMBRE"].Index].Value = porcentajeceldas[11].ToString("#,##0.00", _ClassParametros.MiRegionProvider);

        }

        public void CalendarizacioCOGProcesada()
        {

            _ClassParametros.PORCENTAJECOG.Add(new ClassParametros._PorcentajeCOG(_ClassParametros.IDCOGCELDA, _ClassParametros.PorcentajeEnero.ToString(), _ClassParametros.PorcentajeFebrero.ToString(),
            _ClassParametros.PorcentajeMarzo.ToString(), _ClassParametros.PorcentajeAbril.ToString(), _ClassParametros.PorcentajeMayo.ToString(), _ClassParametros.PorcentajeJunio.ToString(),
            _ClassParametros.PorcentajeJulio.ToString(), _ClassParametros.PorcentajeAgosto.ToString(), _ClassParametros.PorcentajeSeptiembre.ToString(), _ClassParametros.PorcentajeOctubre.ToString(),
            _ClassParametros.PorcentajeNoviembre.ToString(), _ClassParametros.PorcentajeDiciembre.ToString()));

            string idcogfocus = GridviewCargaIngreso.Rows[_ClassParametros.IdRowGrid].Cells[GridviewCargaIngreso.Columns["CodigoCOG"].Index].Value.ToString();
            if (_ClassParametros.IDCOGCELDA == idcogfocus)
            {
                AsignarPresupuestoPorcentual(_ClassParametros.IdRowGrid);
                _ClassParametros.IDCOC = _ClassParametros.IDCOGCELDA;
                CompararPresupuestoYCalendarizacion(_ClassParametros.IdRowGrid);
            }
        }

        private void CargarListaCOGPorcentual()
        {

            _ClassParametros.PORCENTAJECOG.Clear();

            string NombreProcedimiento = "ConsultarCatalogoCalendarioCOG";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@id_cog", SqlDbType.VarChar, 20);
                sqldataadapter.SelectCommand.Parameters["@id_cog"].Value = "";
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "ConsultaporcentajeCOG");
                datatable = dataset.Tables["ConsultaporcentajeCOG"];

                if (datatable.Rows.Count > 0)
                {
                    int RegistroActual = 0;
                    int RegistrosTotales = datatable.Rows.Count;
                    while (RegistroActual < RegistrosTotales)
                    {
                        DataRow registro = datatable.Rows[0];
                        _ClassParametros.PORCENTAJECOG.Add(new ClassParametros._PorcentajeCOG(registro["ID_COG"].ToString(), registro["ENERO"].ToString(), registro["FEBRERO"].ToString(),
                            registro["MARZO"].ToString(), registro["ABRIL"].ToString(), registro["MAYO"].ToString(), registro["JUNIO"].ToString(), registro["JULIO"].ToString(),
                           registro["AGOSTO"].ToString(), registro["SEPTIEMBRE"].ToString(), registro["OCTUBRE"].ToString(), registro["NOVIEMBRE"].ToString(), registro["DICIEMBRE"].ToString()));
                        RegistroActual++;
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

        private bool GuardarHojaDeTrabajo()
        {
            bool ProcesoCorrecto = true;
            if (_ClassParametros.NuevaHojaDeIngreso)
            {
                _ClassParametros.ProcesoCancelado = false; _ClassParametros.ProcesoCorrecto = false;
                GuardarPropiedadesHojaIngreso();
                if (!_ClassParametros.ProcesoCancelado && _ClassParametros.ProcesoCorrecto)
                {
                    ProcesoCorrecto = Guardar();
                }
            }
            else
                ProcesoCorrecto = Guardar();

            return ProcesoCorrecto;
        }


        private bool GuardarHojaIngreso()
        {
            bool NuevoRegistro = false;
            int IdRegistroHojaIngreso = 0;
            bool ProcesoCorrecto = true;
            int RegistroActual = 0;
            int RegistroFinal = GridviewCargaIngreso.Rows.Count - 1;
            bool EjecutarQuery = false;

            labelInicial.Text = "0";
            labelFinal.Text = RegistroFinal.ToString("#,###");
            panelProceso.Visible = true;

            string CodigoCRI = "";
            string codigoFuente = "";                      // Origen del recurso  
            string PresupuestoAsignado = "";
            string ENERO = "";
            string FEBRERO = "";
            string MARZO = "";
            string ABRIL = "";
            string MAYO = "";
            string JUNIO = "";
            string JULIO = "";
            string AGOSTO = "";
            string SEPTIEMBRE = "";
            string OCTUBRE = "";
            string NOVIEMBRE = "";
            string DICIEMBRE = "";


            progressBarProceso.Value = 0;
            progressBarProceso.Maximum = RegistroFinal;
            CommandTransactionHojas.CommandText = "INSERTAR_REGISTROS_HOJAS_DE_INGRESO";


            try
            {
                CommandTransactionHojas.Parameters.Add("@ID_HOJA_INGRESO", SqlDbType.Int, 4);
                CommandTransactionHojas.Parameters.Add("@ID_CRI", SqlDbType.VarChar);
                CommandTransactionHojas.Parameters.Add("@ID_ORIGEN_INGRESO", SqlDbType.VarChar);
                CommandTransactionHojas.Parameters.Add("@PRESUPUESTO", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@ENERO", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@FEBRERO", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@MARZO", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@ABRIL", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@MAYO", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@JUNIO", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@JULIO", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@AGOSTO", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@SEPTIEMBRE", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@OCTUBRE", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@NOVIEMBRE", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@DICIEMBRE", SqlDbType.Money);
                CommandTransactionHojas.Parameters.Add("@ID_REGISTRO_INGRESO", SqlDbType.Int);
                CommandTransactionHojas.Parameters.Add("@NUEVO_REGISTRO", SqlDbType.Bit);


                foreach (DataGridViewRow rowingreso in GridviewCargaIngreso.Rows)
                {
                    Application.DoEvents();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["CodigoCRI"].Index].Value != null)
                        CodigoCRI = rowingreso.Cells[GridviewCargaIngreso.Columns["CodigoCRI"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["codigoFuente"].Index].Value != null)
                        codigoFuente = rowingreso.Cells[GridviewCargaIngreso.Columns["codigoFuente"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Value != null)
                        PresupuestoAsignado = rowingreso.Cells[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["ENERO"].Index].Value != null)
                        ENERO = rowingreso.Cells[GridviewCargaIngreso.Columns["ENERO"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["FEBRERO"].Index].Value != null)
                        FEBRERO = rowingreso.Cells[GridviewCargaIngreso.Columns["FEBRERO"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["MARZO"].Index].Value != null)
                        MARZO = rowingreso.Cells[GridviewCargaIngreso.Columns["MARZO"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["ABRIL"].Index].Value != null)
                        ABRIL = rowingreso.Cells[GridviewCargaIngreso.Columns["ABRIL"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["MAYO"].Index].Value != null)
                        MAYO = rowingreso.Cells[GridviewCargaIngreso.Columns["MAYO"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["JUNIO"].Index].Value != null)
                        JUNIO = rowingreso.Cells[GridviewCargaIngreso.Columns["JUNIO"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["JULIO"].Index].Value != null)
                        JULIO = rowingreso.Cells[GridviewCargaIngreso.Columns["JULIO"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["AGOSTO"].Index].Value != null)
                        AGOSTO = rowingreso.Cells[GridviewCargaIngreso.Columns["AGOSTO"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["SEPTIEMBRE"].Index].Value != null)
                        SEPTIEMBRE = rowingreso.Cells[GridviewCargaIngreso.Columns["SEPTIEMBRE"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["OCTUBRE"].Index].Value != null)
                        OCTUBRE = rowingreso.Cells[GridviewCargaIngreso.Columns["OCTUBRE"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["NOVIEMBRE"].Index].Value != null)
                        NOVIEMBRE = rowingreso.Cells[GridviewCargaIngreso.Columns["NOVIEMBRE"].Index].Value.ToString();
                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["DICIEMBRE"].Index].Value != null)
                        DICIEMBRE = rowingreso.Cells[GridviewCargaIngreso.Columns["DICIEMBRE"].Index].Value.ToString();


                    if (PresupuestoAsignado.Trim() == "") PresupuestoAsignado = "0";
                    if (ENERO.Trim() == "") ENERO = "0";
                    if (FEBRERO.Trim() == "") FEBRERO = "0";
                    if (MARZO.Trim() == "") MARZO = "0";
                    if (ABRIL.Trim() == "") ABRIL = "0";
                    if (MAYO.Trim() == "") MAYO = "0";
                    if (JUNIO.Trim() == "") JUNIO = "0";
                    if (JULIO.Trim() == "") JULIO = "0";
                    if (AGOSTO.Trim() == "") AGOSTO = "0";
                    if (SEPTIEMBRE.Trim() == "") SEPTIEMBRE = "0";
                    if (OCTUBRE.Trim() == "") OCTUBRE = "0";
                    if (NOVIEMBRE.Trim() == "") NOVIEMBRE = "0";
                    if (DICIEMBRE.Trim() == "") DICIEMBRE = "0";


                    CommandTransactionHojas.Parameters["@ID_HOJA_INGRESO"].Value = _ClassParametros.IdHojaIngreso;

                    CommandTransactionHojas.Parameters["@ID_CRI"].Value = CodigoCRI;

                    CommandTransactionHojas.Parameters["@ID_ORIGEN_INGRESO"].Value = codigoFuente;

                    CommandTransactionHojas.Parameters["@PRESUPUESTO"].Value = PresupuestoAsignado;

                    CommandTransactionHojas.Parameters["@ENERO"].Value = ENERO;

                    CommandTransactionHojas.Parameters["@FEBRERO"].Value = FEBRERO;

                    CommandTransactionHojas.Parameters["@MARZO"].Value = MARZO;

                    CommandTransactionHojas.Parameters["@ABRIL"].Value = ABRIL;

                    CommandTransactionHojas.Parameters["@MAYO"].Value = MAYO;

                    CommandTransactionHojas.Parameters["@JUNIO"].Value = JUNIO;

                    CommandTransactionHojas.Parameters["@JULIO"].Value = JULIO;

                    CommandTransactionHojas.Parameters["@AGOSTO"].Value = AGOSTO;

                    CommandTransactionHojas.Parameters["@SEPTIEMBRE"].Value = SEPTIEMBRE;

                    CommandTransactionHojas.Parameters["@OCTUBRE"].Value = OCTUBRE;

                    CommandTransactionHojas.Parameters["@NOVIEMBRE"].Value = NOVIEMBRE;

                    CommandTransactionHojas.Parameters["@DICIEMBRE"].Value = DICIEMBRE;


                    if (_ClassParametros.NuevaHojaDeIngreso)
                    {
                        if (!rowingreso.IsNewRow)
                        {
                            CommandTransactionHojas.Parameters["@ID_REGISTRO_INGRESO"].Value = -1;
                            CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 1;
                            EjecutarQuery = true;
                            NuevoRegistro = true;
                        }
                        else
                            EjecutarQuery = false;
                    }
                    else
                    {
                        if (!rowingreso.IsNewRow)
                        {

                            if (rowingreso.Cells[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index].Value != null)
                            {

                                // si no existe se agrega, si existe se verifica que no haya sufrido modificacion. 
                                if (!ExisteRegistroEnLaTablaHojaDeTrabajo(rowingreso.Cells[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index].Value.ToString()))
                                {
                                    CommandTransactionHojas.Parameters["@ID_REGISTRO_INGRESO"].Value = -1;
                                    CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 1;
                                    EjecutarQuery = true; NuevoRegistro = true;
                                }
                                else
                                {
                                    if (rowingreso.Cells[GridviewCargaIngreso.Columns["rowmodificado"].Index].Value != null)
                                    {
                                        if (rowingreso.Cells[GridviewCargaIngreso.Columns["rowmodificado"].Index].Value.ToString().Trim() == "1")
                                        {
                                            CommandTransactionHojas.Parameters["@ID_REGISTRO_INGRESO"].Value = Convert.ToInt32(rowingreso.Cells[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index].Value.ToString());
                                            CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 0;    // Registro que existe en la tabla y ha sido modificado. 
                                            EjecutarQuery = true; NuevoRegistro = false;
                                        }
                                        else
                                            EjecutarQuery = false;
                                    }
                                    else
                                    {
                                        CommandTransactionHojas.Parameters["@ID_REGISTRO_INGRESO"].Value = Convert.ToInt32(rowingreso.Cells[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index].Value.ToString());
                                        CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 0;    // Registro que existe en la tabla y no se sabe a ciencia cierta si fue modificado o no.   (se actualiza) 
                                        EjecutarQuery = true; NuevoRegistro = false;
                                    }
                                }
                            }
                            else   // Nuevo registro (agregado despues de haber guardado una parte de la hoja de trabajo o despues de abrir un proyecto
                                   // Esta opcion es poco probable que suceda, sin embargo se tiene en consieracion por algun descuido al agregar nuevos registros. 
                            {
                                CommandTransactionHojas.Parameters["@ID_REGISTRO_INGRESO"].Value = -1;
                                CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 1;
                                EjecutarQuery = true; NuevoRegistro = true;
                            }
                        }
                        else
                            EjecutarQuery = false;
                    }


                    if (EjecutarQuery)
                    {
                        if (NuevoRegistro)   //  modificacion de registro. 
                        {
                            Object IdNumregistro = CommandTransactionHojas.ExecuteScalar();
                            IdRegistroHojaIngreso = (int)(decimal)IdNumregistro;
                            rowingreso.Cells[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index].Value = IdRegistroHojaIngreso.ToString();
                        }
                        else
                        {
                            CommandTransactionHojas.ExecuteNonQuery();
                        }
                    }
                    if (RegistroActual < RegistroFinal)
                        RegistroActual++;

                    labelInicial.Text = RegistroActual.ToString("#,###");
                    CodigoCRI = "";  codigoFuente = ""; PresupuestoAsignado = "";
                    ENERO = ""; FEBRERO = ""; MARZO = ""; ABRIL = ""; MAYO = ""; JUNIO = ""; JULIO = ""; AGOSTO = ""; SEPTIEMBRE = ""; OCTUBRE = ""; NOVIEMBRE = ""; DICIEMBRE = "";

                    progressBarProceso.Value = RegistroActual;

                }



            }
            catch (Exception exception)
            {
                ProcesoCorrecto = false;
                MessageBox.Show(this, "Ha ocurrido un error al guardar los registros; verifique: \n\r\n\r" + exception.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            panelProceso.Visible = false;
            return ProcesoCorrecto;
        }

        private void GuardarPropiedadesHojaIngreso()
        {

            _ClassParametros.ProcesoCancelado = false;
            FormCaracteristicasHOJAINGRESO _FormCaracteristicasHOJAINGRESO = new FormCaracteristicasHOJAINGRESO();
            _FormCaracteristicasHOJAINGRESO.ShowDialog();

        }

        private void FormCargarLeyDeIngreso_ResizeEnd(object sender, EventArgs e)
        {
            panelProceso.Left = (this.Width / 2) - (panelProceso.Width / 2);
            panelProceso.Top = (this.Height / 2) - (panelProceso.Height / 2);

            panelProgressBar.Left = (this.Width - panelProgressBar.Width) / 2;
            panelProgressBar.Top = (this.Height - panelProgressBar.Height) / 2;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ClassParametros.ProcesoCancelado = false;
            _ClassParametros.ProcesoCorrecto = false;

            FormAbrirHojasDeTrabajoIngreso _FormAbrirHojasDeTrabajoIngreso = new FormAbrirHojasDeTrabajoIngreso();
            _FormAbrirHojasDeTrabajoIngreso.ShowDialog();

            if (!_ClassParametros.ProcesoCancelado && _ClassParametros.ProcesoCorrecto)
            {
                this.Cursor = Cursors.WaitCursor;
                if (CargarHojaIngreso())
                {
                    _ClassParametros.NuevaHojaDeIngreso = false;
                }
                this.Cursor = Cursors.Default;
            }        
        }

        private bool CargarHojaIngreso()
        {
            bool ProcesoCorrecto = true;
            double[] MontoCeldas = new double[13];
            GridviewCargaIngreso.Rows.Clear();
            int RegistroActual = 0;
            int RegistroFinal = 0;

            labelInicial.Text = "0";
            string NombreProcedimiento = "ConsultarHojasDeTrabajoIngreso";


            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@ID_HOJA_INGRESO", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@ID_HOJA_INGRESO"].Value = Convert.ToInt32(_ClassParametros.IdHojaIngreso);
            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "HojaDeTrabajoIngreso");
            DataTable datatable = dataset.Tables["HojaDeTrabajoIngreso"];


            try
            {
                if (datatable.Rows.Count >= 0)
                {
                    panelProceso.Visible = true;
                    RegistroFinal = GridviewCargaIngreso.Rows.Count - 1;
                    labelFinal.Text = RegistroFinal.ToString("#,###");

                    RegistroFinal = datatable.Rows.Count;

                    progressBarProceso.Value = 0;
                    progressBarProceso.Maximum = RegistroFinal;

                    while (RegistroActual < RegistroFinal)
                    {


                        string[] registrogrid = new string[27];
                        DataRow registro = datatable.Rows[RegistroActual];
                        registrogrid[GridviewCargaIngreso.Columns["CodigoCRI"].Index] = registro["ID_CRI"].ToString().Trim();
                        registrogrid[GridviewCargaIngreso.Columns["CRI"].Index] = registro["CRI"].ToString().Trim();
                        registrogrid[GridviewCargaIngreso.Columns["codigoFuente"].Index] = registro["ID_ORIGEN_INGRESO"].ToString().Trim();
                        registrogrid[GridviewCargaIngreso.Columns["FuenteFinanciamiento"].Index] = registro["ORIGEN_INGRESO"].ToString().Trim();


                        if (_ClassParametros.IsNumeric(registro["Presupuesto"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index] = Math.Round(Convert.ToDecimal(registro["Presupuesto"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["ENERO"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["ENERO"].Index] = Math.Round(Convert.ToDecimal(registro["ENERO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["ENERO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["FEBRERO"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["FEBRERO"].Index] = Math.Round(Convert.ToDecimal(registro["FEBRERO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["FEBRERO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["MARZO"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["MARZO"].Index] = Math.Round(Convert.ToDecimal(registro["MARZO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["MARZO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["ABRIL"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["ABRIL"].Index] = Math.Round(Convert.ToDecimal(registro["ABRIL"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["ABRIL"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["MAYO"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["MAYO"].Index] = Math.Round(Convert.ToDecimal(registro["MAYO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["MAYO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["JUNIO"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["JUNIO"].Index] = Math.Round(Convert.ToDecimal(registro["JUNIO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["JUNIO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["JULIO"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["JULIO"].Index] = Math.Round(Convert.ToDecimal(registro["JULIO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["JULIO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["AGOSTO"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["AGOSTO"].Index] = Math.Round(Convert.ToDecimal(registro["AGOSTO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["AGOSTO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["SEPTIEMBRE"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["SEPTIEMBRE"].Index] = Math.Round(Convert.ToDecimal(registro["SEPTIEMBRE"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["SEPTIEMBRE"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["OCTUBRE"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["OCTUBRE"].Index] = Math.Round(Convert.ToDecimal(registro["OCTUBRE"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["OCTUBRE"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["NOVIEMBRE"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["NOVIEMBRE"].Index] = Math.Round(Convert.ToDecimal(registro["NOVIEMBRE"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["NOVIEMBRE"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["DICIEMBRE"].ToString()))
                            registrogrid[GridviewCargaIngreso.Columns["DICIEMBRE"].Index] = Math.Round(Convert.ToDecimal(registro["DICIEMBRE"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaIngreso.Columns["DICIEMBRE"].Index] = "";


                        registrogrid[GridviewCargaIngreso.Columns["rowmodificado"].Index] = "0";
                        registrogrid[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index] = registro["ID_REGISTRO_PRESUPUESTO_INGRESO"].ToString();

                        GridviewCargaIngreso.Rows.Add(registrogrid);
                        CompararPresupuestoYCalendarizacion(RegistroActual);
                        GridviewCargaIngreso.Rows[RegistroActual].HeaderCell.Value = (RegistroActual + 1).ToString();
                        RegistroActual++;


                        labelInicial.Text = RegistroActual.ToString("#,###");
                        progressBarProceso.Value = RegistroActual;

                    }

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
            panelProceso.Visible = false;
            sqldataadapter = null;
            return ProcesoCorrecto;

        }

        private void RenumerarRenglones()
        {
            try
            {
                for (int row = 0; row < GridviewCargaIngreso.Rows.Count; row++)
                {
                    GridviewCargaIngreso.Rows[row].HeaderCell.Value = (row + 1).ToString();
                }
            }
            catch
            {
            }
        }

        private void GridviewCargaPresupuestal_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            NumRegistrosEliminados++;
            if (NumRegistrosEliminados >= NumRegistrosSeleccionados)
            {
                RenumerarRenglones();
            }

        }

        private void GridviewCargaPresupuestal_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataGridViewRow row = e.Row;
            if (row.Cells[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index].Value != null)
            {
                int IdRegistro = Convert.ToInt32(row.Cells[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index].Value.ToString());  // indica que es un registro previamente almacenado
                if (Convert.ToInt64(IdRegistro) > 0)
                {
                    _ClassParametros.IdRegistrosEliminados.Add(IdRegistro);
                }
            }
            Lahojasehamodificado = true;

        }

        private bool RegistrosEliminados()
        {

            bool ProcesoCorrecto = true;
            string RegistrosAEliminar = "";
            try
            {
                for (int elem = 0; elem < _ClassParametros.IdRegistrosEliminados.Count; elem++)
                {
                    if (RegistrosAEliminar == "")
                        RegistrosAEliminar = _ClassParametros.IdRegistrosEliminados[elem].ToString();
                    else
                        RegistrosAEliminar += ", " + _ClassParametros.IdRegistrosEliminados[elem].ToString();
                }

                CommandTransactionHojas.Parameters.Add("@ListaRegistros", SqlDbType.VarChar);
                CommandTransactionHojas.Parameters["@ListaRegistros"].Value = RegistrosAEliminar;
                CommandTransactionHojas.ExecuteNonQuery();
                CommandTransactionHojas.Parameters.RemoveAt("@ListaRegistros");
            }
            catch (Exception exception)
            {
                ProcesoCorrecto = false;
                MessageBox.Show(this, exception.Message.ToString(), "Error al eliminar registros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ProcesoCorrecto;
        }

        private void ModificarRegistros()
        {

        }

        private bool ExisteRegistroEnLaTablaHojaDeTrabajo(string IdRegistroIngreso)
        {
            bool ExisteRegistro = false;
            string MySql = "SELECT ID_REGISTRO_INGRESO FROM HOJAS_DE_INGRESO WHERE ID_REGISTRO_INGRESO = " + IdRegistroIngreso + "; SELECT SCOPE_IDENTITY() ";
            SqlCommand CommandVerificacion = new SqlCommand(MySql, ClassBaseDeDatos.MyConnectionDB);
            CommandVerificacion.CommandType = CommandType.Text;
            CommandVerificacion.Transaction = TransactionIngreso;
            Object RegistroEncontrado = CommandVerificacion.ExecuteScalar();
            if (RegistroEncontrado != null)
            {
                int IdRegistroEncontrado = (int)RegistroEncontrado;
                if (IdRegistroEncontrado >= 0)
                    ExisteRegistro = true;

            }
            return ExisteRegistro;
        }

        private void buttonNuevo_Click(object sender, EventArgs e)
        {
            bool RealizarAccion = true;

            if (Lahojasehamodificado)
            {
                DialogResult EstablecerConexion = new DialogResult();
                string Alerta = "Los cambios realizados en la hoja actual no se han guardado. " +
                    "\n\r\n\r" + "Desea iniciar una hoja de trabajo de todas maeras ?";
                EstablecerConexion = MessageBox.Show(this, Alerta, "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (EstablecerConexion == DialogResult.No)
                {
                    RealizarAccion = false;
                }
            }

            if (RealizarAccion)
            {

                Lahojasehamodificado = false;
                _ClassParametros.NuevaHojaDeIngreso = true;
                NumRegistrosEliminados = 0;
                _ClassParametros.IdRegistrosEliminados.Clear();
                _ClassParametros.NuevaHojaDeIngreso = true;
                GridviewCargaIngreso.Rows.Clear();
                if (TransactionIngreso != null)
                    TransactionIngreso.Dispose();
                if (CommandTransactionHojas != null)
                    CommandTransactionHojas.Dispose();
            }
        }

        private void buttonGuardarcomo_Click(object sender, EventArgs e)
        {
            buttonGuardar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            _ClassParametros.GuardarSobreHojaExistente = false;
            _ClassParametros.GuardarComo = true;

            GuardarPropiedadesHojaIngreso();
            if (!_ClassParametros.ProcesoCancelado && _ClassParametros.ProcesoCorrecto)
            {
                Guardar();
            }
            buttonGuardar.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private bool Guardar()
        {
            bool ProcesoCorrecto = true;

            TransactionIngreso = ClassBaseDeDatos.MyConnectionDB.BeginTransaction();
            CommandTransactionHojas = ClassBaseDeDatos.MyConnectionDB.CreateCommand();
            CommandTransactionHojas.CommandType = CommandType.StoredProcedure;
            CommandTransactionHojas.Transaction = TransactionIngreso;

            if (_ClassParametros.NuevaHojaDeIngreso)
            {

                if (GuardarHojaIngreso())
                {
                    TransactionIngreso.Commit();
                }
                else
                {
                    TransactionIngreso.Rollback();
                    ProcesoCorrecto = false;
                }
            }
            else
            {

                if (_ClassParametros.IdRegistrosEliminados.Count > 0)
                {
                    CommandTransactionHojas.CommandText = "ELIMINAR_REGISTROS_HOJAS_INGRESO";
                    if (RegistrosEliminados())
                    {
                        if (GuardarHojaIngreso())
                        {
                            TransactionIngreso.Commit();
                        }
                        else
                        {
                            TransactionIngreso.Rollback();
                            ProcesoCorrecto = false;
                        }
                    }
                    else
                    {
                        TransactionIngreso.Rollback();
                        ProcesoCorrecto = false;
                    }


                }
                else
                {
                    if (GuardarHojaIngreso())
                    {
                        TransactionIngreso.Commit();
                    }
                    else
                    {
                        TransactionIngreso.Rollback();
                        ProcesoCorrecto = false;
                    }

                }
            }
            if (ProcesoCorrecto)
            {
                _ClassParametros.NuevaHojaDePresupuesto = false;
                MessageBox.Show(this, "La hoja de ingreso se ha guardado correctamente: \n\r\n\r", "Proceso terminado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            TransactionIngreso.Dispose();
            CommandTransactionHojas.Dispose();
            return ProcesoCorrecto;
        }



        private void FormCargarLeyDeIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _ClassParametros.IngresoAlternativo = false;
                if (_FormCapturaPorcentajeCOG != null)
                {
                    _FormCapturaPorcentajeCOG.Close();
                    _FormCapturaPorcentajeCOG.Dispose();
                }
            }
            catch
            {
            }
        }



        private bool ConexionCatalogoExcelEstablecida()
        {

            bool ConexionEstablecida = true;
            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source= " + _ClassParametros.RutaArchivoIngreso.ToString() + @" ;Extended Properties= ""Excel 12.0 Xml;HDR=Yes;IMEX=1""";
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                DbConnection conexion = factory.CreateConnection();
                DbCommand comando = conexion.CreateCommand();
                conexion.ConnectionString = connectionString;
                comando.CommandText = "SELECT * FROM [" + _ClassParametros.NombreHojaIngreso.ToString() + "$]";
                conexion.Open();
                DataRecordExcel = comando.ExecuteReader();
            }
            catch (Exception exception)
            {
                DeshabilitarProgressProcesando();
                ConexionEstablecida = false;
                MessageBox.Show(this, "No ha sido posible  acceder al archivo excel para la importación de sus registros; verifique el siguiente error: " +
                    "\n\r\n\r" + exception.Message.ToString(), "Ha ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return ConexionEstablecida;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _ClassParametros.ProcesoCancelado = false;
            _ClassParametros.ProcesoEjecutado = false;
            FormImportarIngreso _FormImportarIngreso = new FormImportarIngreso();
            _FormImportarIngreso.ShowDialog();
            if (!_ClassParametros.ProcesoCancelado && _ClassParametros.ProcesoEjecutado)
            {
                HabilitarProgressProcesando();

                if (ConexionCatalogoExcelEstablecida())
                {
                    if (ImportarIngresoExcel())
                    {
                        DeshabilitarProgressProcesando();
                        string Alerta = "El proceso ha terminado correctamente. \n\r\n\r";
                        MessageBox.Show(this, Alerta, "Proceso terminado", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                        _ClassDetallesPresupuestales.RealizarAjustes = false;
                        FormInformativo _FormInformativo = new FormInformativo();
                        _FormInformativo.ShowDialog();
                        if (_ClassDetallesPresupuestales.RealizarAjustes)
                        {

                            DialogResult AjustarDecimales = new DialogResult();
                            string Alerta2 = "Antes de relizar los ajustes decimales en el COG, es necesario que guarde los registros importados.  " +
                                "\n\r\n\r" + "Desea guardar los registros en este momento ?";
                            AjustarDecimales = MessageBox.Show(this, Alerta2, "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (AjustarDecimales == DialogResult.Yes)
                            {
                                if (GuardarHojaDeTrabajo())
                                {
                                    _ClassTipoProceso.TipoProceso = (int)ClassTipoProceso.ProcesoAejecutar.CuadroComparativoPresupuestoIngresoConAjustes;
                                    FormCuadroComparativoPresupuestoIngreso _FormCuadroComparativoPresupuestoIngreso = new FormCuadroComparativoPresupuestoIngreso();
                                    _FormCuadroComparativoPresupuestoIngreso.ShowDialog();
                                }
                            }
                            else
                            {
                                string Alerta3 = "En la ópción de [Consultas] podrá verificar el monto asignado por origen de ingreso y el presupuestado. ";
                                MessageBox.Show(this, Alerta3, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            }
                        }
                        else if (_ClassDetallesPresupuestales.MontoDiferencia > 0)
                        {
                            string Alerta4 = "En la ópción de [Consultas] podrá verificar el monto asignado por origen de ingreso y el presupuestado. ";
                            MessageBox.Show(this, Alerta4, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }

                    }
                }
            }
        }


        private void HabilitarProgressProcesando()
        {
            this.Cursor = Cursors.WaitCursor;
            panelProgressBar.Visible = true;
            pictureBoxProgress.Enabled = true;
        }

        private void DeshabilitarProgressProcesando()
        {
            this.Cursor = Cursors.Default;
            panelProgressBar.Visible = false;
            pictureBoxProgress.Enabled = false;

        }


        private bool ImportarIngresoExcel()
        {
            bool ImportacionCorrecta = true;
            int NumRecord = 0;
            int NumRenglonExcel = 1;

            string OrigenRecurso = "";
            string CodigoCRI = "";

            double[] porcentajeceldas = new double[12];

            double presupuestoCRI = 0;
            double totalceldas = 0;
            double diferencia = 0;
            string[] registrogrid = new string[27];
            int RegistroActual = 0;
            string IdOrigenRecursoORFIS = "";

            double MontoTotalImportacion = 0;
            double MontoTotalAjustado = 0;

            double[] PorcentajeMensual = new double[12];
            double SumaRealPorcentaje = 0;


            try
            {
                while (DataRecordExcel.Read())
                {
                    Application.DoEvents();
                    NumRecord++;
                    labelRegistrosProcesados.Text = NumRecord.ToString("#,###");

                    if (NumRecord >= NumRenglonExcel)
                    {
                        OrigenRecurso = DataRecordExcel[2].ToString();
                        CodigoCRI = DataRecordExcel[1].ToString();

                        presupuestoCRI = 0;
                        for (int Elem = 0; Elem < porcentajeceldas.Length; Elem++)
                        {
                            porcentajeceldas[Elem] = 0;     // Porcentaje monetario
                            PorcentajeMensual[Elem] = 0;    // Porcentaje porciento

                        }

                        // presupuesto
                        if (DataRecordExcel[3] != null)
                        {
                            if (_ClassParametros.IsNumeric(DataRecordExcel[3].ToString()))
                            {
                                MontoTotalImportacion += Convert.ToDouble(DataRecordExcel[3].ToString());
                                presupuestoCRI = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[3].ToString()), 2));
                                MontoTotalAjustado += presupuestoCRI;
                            }
                        }

                        // enero
                        if (DataRecordExcel[4] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[4].ToString()))
                                porcentajeceldas[0] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[4].ToString()), 2));

                        // febrero
                        if (DataRecordExcel[5] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[5].ToString()))
                                porcentajeceldas[1] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[5].ToString()), 2));

                        // marzo
                        if (DataRecordExcel[6] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[6].ToString()))
                                porcentajeceldas[2] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[6].ToString()), 2));

                        // abril 
                        if (DataRecordExcel[7] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[7].ToString()))
                                porcentajeceldas[3] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[7].ToString()), 2));

                        // mayo
                        if (DataRecordExcel[8] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[8].ToString()))
                                porcentajeceldas[4] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[8].ToString()), 2));

                        // junio
                        if (DataRecordExcel[9] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[9].ToString()))
                                porcentajeceldas[5] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[9].ToString()), 2));

                        // julio
                        if (DataRecordExcel[10] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[10].ToString()))
                                porcentajeceldas[6] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[10].ToString()), 2));

                        // agosto
                        if (DataRecordExcel[11] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[11].ToString()))
                                porcentajeceldas[7] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[11].ToString()), 2));


                        // septiembre
                        if (DataRecordExcel[12] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[12].ToString()))
                                porcentajeceldas[8] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[12].ToString()), 2));

                        // octubre
                        if (DataRecordExcel[13] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[13].ToString()))
                                porcentajeceldas[9] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[13].ToString()), 2));

                        // noviembre
                        if (DataRecordExcel[14] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[14].ToString()))
                                porcentajeceldas[10] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[14].ToString()), 2));

                        // diciembre
                        if (DataRecordExcel[15] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[15].ToString()))
                                porcentajeceldas[11] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[15].ToString()), 2));




                        totalceldas = 0;

                        for (int elem = 0; elem < porcentajeceldas.Length; elem++)
                        {
                            totalceldas += porcentajeceldas[elem];
                        }

                        diferencia = presupuestoCRI - totalceldas;

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


                        PorcentajeMensual[0] = Math.Round(porcentajeceldas[0] / presupuestoCRI * 100, 4);  // Enero
                        PorcentajeMensual[1] = Math.Round(porcentajeceldas[1] / presupuestoCRI * 100, 4);  // Febrero
                        PorcentajeMensual[2] = Math.Round(porcentajeceldas[2] / presupuestoCRI * 100, 4);  // Marzo
                        PorcentajeMensual[3] = Math.Round(porcentajeceldas[3] / presupuestoCRI * 100, 4);  // Abril
                        PorcentajeMensual[4] = Math.Round(porcentajeceldas[4] / presupuestoCRI * 100, 4);  // Mayo
                        PorcentajeMensual[5] = Math.Round(porcentajeceldas[5] / presupuestoCRI * 100, 4);  // Junio
                        PorcentajeMensual[6] = Math.Round(porcentajeceldas[6] / presupuestoCRI * 100, 4);  // Julio
                        PorcentajeMensual[7] = Math.Round(porcentajeceldas[7] / presupuestoCRI * 100, 4);  // Agosto
                        PorcentajeMensual[8] = Math.Round(porcentajeceldas[8] / presupuestoCRI * 100, 4);  // Septiembre
                        PorcentajeMensual[9] = Math.Round(porcentajeceldas[9] / presupuestoCRI * 100, 4);  // Octubre
                        PorcentajeMensual[10] = Math.Round(porcentajeceldas[10] / presupuestoCRI * 100, 4);  // Noviembre
                        PorcentajeMensual[11] = Math.Round(porcentajeceldas[11] / presupuestoCRI * 100, 4);  // Diciembre



                        SumaRealPorcentaje = 0;
                        for (int Elem = 0; Elem < PorcentajeMensual.Length; Elem++)
                        {

                            SumaRealPorcentaje += PorcentajeMensual[Elem];
                        }

                        diferencia = 100 - SumaRealPorcentaje;

                        if (diferencia != 0)
                        {
                            for (int elem = PorcentajeMensual.Length - 1; elem > 0; elem--)      // se resta o suma la diferencia en el ultimo mes con valor > 0
                            {
                                if (PorcentajeMensual[elem] > 0)
                                {
                                    PorcentajeMensual[elem] = PorcentajeMensual[elem] + diferencia;
                                    PorcentajeMensual[elem] = Math.Round(PorcentajeMensual[elem], 4);
                                    break;
                                }
                            }
                        }


                        _ClassParametros.PorcentajeEnero = PorcentajeMensual[0];
                        _ClassParametros.PorcentajeFebrero = PorcentajeMensual[1];
                        _ClassParametros.PorcentajeMarzo = PorcentajeMensual[2];
                        _ClassParametros.PorcentajeAbril = PorcentajeMensual[3];
                        _ClassParametros.PorcentajeMayo = PorcentajeMensual[4];
                        _ClassParametros.PorcentajeJunio = PorcentajeMensual[5];
                        _ClassParametros.PorcentajeJulio = PorcentajeMensual[6];
                        _ClassParametros.PorcentajeAgosto = PorcentajeMensual[7];
                        _ClassParametros.PorcentajeSeptiembre = PorcentajeMensual[8];
                        _ClassParametros.PorcentajeOctubre = PorcentajeMensual[9];
                        _ClassParametros.PorcentajeNoviembre = PorcentajeMensual[10];
                        _ClassParametros.PorcentajeDiciembre = PorcentajeMensual[11];



                        IdOrigenRecursoORFIS = ExisteFuenteDeFinanciamiento(OrigenRecurso);
                        if (IdOrigenRecursoORFIS == "") IdOrigenRecursoORFIS = OrigenRecurso;

                        registrogrid[GridviewCargaIngreso.Columns["CodigoCRI"].Index] = CodigoCRI;
                        registrogrid[GridviewCargaIngreso.Columns["CRI"].Index] = "";
                        registrogrid[GridviewCargaIngreso.Columns["codigoFuente"].Index] = IdOrigenRecursoORFIS;
                        registrogrid[GridviewCargaIngreso.Columns["FuenteFinanciamiento"].Index] = "";


                        registrogrid[GridviewCargaIngreso.Columns["PresupuestoAsignado"].Index] = presupuestoCRI.ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["ENERO"].Index] = porcentajeceldas[0].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["FEBRERO"].Index] = porcentajeceldas[1].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["MARZO"].Index] = porcentajeceldas[2].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["ABRIL"].Index] = porcentajeceldas[3].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["MAYO"].Index] = porcentajeceldas[4].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["JUNIO"].Index] = porcentajeceldas[5].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["JULIO"].Index] = porcentajeceldas[6].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["AGOSTO"].Index] = porcentajeceldas[7].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["SEPTIEMBRE"].Index] = porcentajeceldas[8].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["OCTUBRE"].Index] = porcentajeceldas[9].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["NOVIEMBRE"].Index] = porcentajeceldas[10].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["DICIEMBRE"].Index] = porcentajeceldas[11].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaIngreso.Columns["rowmodificado"].Index] = "0";
                        registrogrid[GridviewCargaIngreso.Columns["IdRegistroPresupuestal"].Index] = "-1";

                        GridviewCargaIngreso.Rows.Add(registrogrid);
                        CompararPresupuestoYCalendarizacion(RegistroActual);
                        GridviewCargaIngreso.Rows[RegistroActual].HeaderCell.Value = (RegistroActual + 1).ToString();
                        RegistroActual++;
                        _ClassDetallesPresupuestales.RegistrosImportados = RegistroActual;
                        labelRegistrosAlmacenados.Text = RegistroActual.ToString("#,###");


                        if (ExisteCalendarioPorcentualCRI(CodigoCRI))
                        {
                            GuardarCalendarizacionCRI(false, CodigoCRI, _ClassParametros.PorcentajeEnero.ToString(), _ClassParametros.PorcentajeFebrero.ToString(), _ClassParametros.PorcentajeMarzo.ToString(), _ClassParametros.PorcentajeAbril.ToString(),
                            _ClassParametros.PorcentajeMayo.ToString(), _ClassParametros.PorcentajeJunio.ToString(), _ClassParametros.PorcentajeJulio.ToString(), _ClassParametros.PorcentajeAgosto.ToString(), _ClassParametros.PorcentajeSeptiembre.ToString(),
                            _ClassParametros.PorcentajeOctubre.ToString(), _ClassParametros.PorcentajeNoviembre.ToString(), _ClassParametros.PorcentajeDiciembre.ToString());
                        }
                        else
                        {
                            GuardarCalendarizacionCRI(true, CodigoCRI, _ClassParametros.PorcentajeEnero.ToString(), _ClassParametros.PorcentajeFebrero.ToString(), _ClassParametros.PorcentajeMarzo.ToString(), _ClassParametros.PorcentajeAbril.ToString(),
                            _ClassParametros.PorcentajeMayo.ToString(), _ClassParametros.PorcentajeJunio.ToString(), _ClassParametros.PorcentajeJulio.ToString(), _ClassParametros.PorcentajeAgosto.ToString(), _ClassParametros.PorcentajeSeptiembre.ToString(),
                            _ClassParametros.PorcentajeOctubre.ToString(), _ClassParametros.PorcentajeNoviembre.ToString(), _ClassParametros.PorcentajeDiciembre.ToString());
                        }

                        if (NumErrores >= 3)
                        {
                            DialogResult CancelarOperacion = new DialogResult();

                            CancelarOperacion = MessageBox.Show(this, "Han ocurrido demasiados errores durante el proceso.  \n\r\n\r" +
                            "desea detener la operacion en este momento ?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (CancelarOperacion == DialogResult.Yes)
                                break;
                        }

                    }
                }

                _ClassDetallesPresupuestales.MontoTotalOrigen = MontoTotalImportacion;
                _ClassDetallesPresupuestales.MontoTotalImportado = MontoTotalAjustado;
                _ClassDetallesPresupuestales.MontoDiferencia = MontoTotalImportacion - MontoTotalAjustado;
             //   CargarListaCOGPorcentual();


            }

            catch (Exception exception)
            {
                DeshabilitarProgressProcesando();
                MessageBox.Show(this, "Ha ocurrido un error al obtener los datos del archivo excel: \n\r\n\r" +
                exception.Message.ToString(), "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ImportacionCorrecta = false;
            }

            return ImportacionCorrecta;
        }

        private void ConfigurarPanelProgressBar()
        {
            panelProgressBar.Parent = this;
            panelProgressBar.Visible = false;
            panelProgressBar.BringToFront();
            panelProgressBar.Left = (this.Width - panelProgressBar.Width) / 2;
            panelProgressBar.Top = (this.Height - panelProgressBar.Height) / 2;
            pictureBoxProgress.Enabled = false;
            labelRegistrosProcesados.Text = "";
            labelRegistrosAlmacenados.Text = "";
        }

        private string ExisteFuenteDeFinanciamiento(String IdOrigenORFIS)
        {
            string IdRegistroEncontrado = "";
            string MySql = "SELECT ID_ORIGEN_INGRESO FROM CAT_ORIGEN_INGRESO WHERE ID_ORFIS = " + IdOrigenORFIS;
            try
            {
                SqlCommand CommandVerificacion = new SqlCommand(MySql, ClassBaseDeDatos.MyConnectionDB);
                CommandVerificacion.CommandType = CommandType.Text;
                Object RegistroEncontrado = CommandVerificacion.ExecuteScalar();
                if (RegistroEncontrado != null)
                {
                    IdRegistroEncontrado = RegistroEncontrado.ToString();

                }
            }
            catch
            { }

            return IdRegistroEncontrado;
        }

        private void GridviewCargaPresupuestal_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ajustartamañoEqituetas();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            buttonGuardar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            GuardarHojaDeTrabajo();


            buttonGuardar.Enabled = true;
            this.Cursor = Cursors.Default;
        }


        // Guardar calendarizacion COG
        private bool GuardarCalendarizacionCRI(bool NuevoRegistro, string ID_CRI, string ENERO, string FEBRERO, string MARZO, string ABRIL, string MAYO, string JUNIO, string JULIO, string AGOSTO,
            string SEPTIEMBRE, string OCTUBRE, string NOVIEMBRE, string DICIEMBRE)
        {
            this.Cursor = Cursors.WaitCursor;
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            string NombreProcedimiento = "IngresarRegistrosCalendarizacionCRI";

            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@ID_CRI", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@ID_CRI"].Value = ID_CRI;
                CommandInsertCatalogo.Parameters.Add("@ENERO", SqlDbType.Float, 4);
                CommandInsertCatalogo.Parameters["@ENERO"].Value = ENERO;
                CommandInsertCatalogo.Parameters.Add("@FEBRERO", SqlDbType.Float, 4);
                CommandInsertCatalogo.Parameters["@FEBRERO"].Value = FEBRERO;
                CommandInsertCatalogo.Parameters.Add("@MARZO", SqlDbType.Float, 4);
                CommandInsertCatalogo.Parameters["@MARZO"].Value = MARZO;
                CommandInsertCatalogo.Parameters.Add("@ABRIL", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@ABRIL"].Value = ABRIL;
                CommandInsertCatalogo.Parameters.Add("@MAYO", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@MAYO"].Value = MAYO;
                CommandInsertCatalogo.Parameters.Add("@JUNIO", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@JUNIO"].Value = JUNIO;
                CommandInsertCatalogo.Parameters.Add("@JULIO", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@JULIO"].Value = JULIO;
                CommandInsertCatalogo.Parameters.Add("@AGOSTO", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@AGOSTO"].Value = AGOSTO;
                CommandInsertCatalogo.Parameters.Add("@SEPTIEMBRE", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@SEPTIEMBRE"].Value = SEPTIEMBRE;
                CommandInsertCatalogo.Parameters.Add("@OCTUBRE", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@OCTUBRE"].Value = OCTUBRE;
                CommandInsertCatalogo.Parameters.Add("@NOVIEMBRE", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@NOVIEMBRE"].Value = NOVIEMBRE;
                CommandInsertCatalogo.Parameters.Add("@DICIEMBRE", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@DICIEMBRE"].Value = DICIEMBRE;
                CommandInsertCatalogo.Parameters.Add("@NUEVO_REGISTRO", SqlDbType.Bit);
                CommandInsertCatalogo.Parameters["@NUEVO_REGISTRO"].Value = NuevoRegistro;
                CommandInsertCatalogo.ExecuteNonQuery();


            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al guardar los registros en el catálogo CALENDARIO_CRI ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
                NumErrores++;
            }
            finally
            {
                CommandInsertCatalogo = null;
            }
            this.Cursor = Cursors.Default;
            return ProcesoCorrecto;
        }


        private bool ExisteCalendarioPorcentualCRI(string ID_CRI)
        {
            bool ExisteCOG = false;
            string MySql = "SELECT ID_CRI FROM CALENDARIO_CRI WHERE ID_CRI = " + ID_CRI;
            try
            {
                SqlCommand CommandVerificacion = new SqlCommand(MySql, ClassBaseDeDatos.MyConnectionDB);
                CommandVerificacion.CommandType = CommandType.Text;
                Object RegistroEncontrado = CommandVerificacion.ExecuteScalar();
                if (RegistroEncontrado != null)
                {
                    if (ID_CRI == RegistroEncontrado.ToString().Trim())
                        ExisteCOG = true;
                }
            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al consultar los registros en el catálogo CALENDARIO_CRI ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NumErrores++;
            }
            return ExisteCOG;
        }

    }
}
