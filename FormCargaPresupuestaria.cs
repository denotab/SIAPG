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
using System.Diagnostics;
using SIAPG.Polizas;

namespace SIAPG
{
    public partial class FormCargaPresupuestaria : Form
    {

        SqlTransaction TransactionPresupuesto;
        SqlCommand CommandTransactionHojas;


        DbDataReader DataRecordExcel;
        ClassDetallesPresupuestales _ClassDetallesPresupuestales = new ClassDetallesPresupuestales();
        int NumErrores = 0;

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
        bool ImportacionDesdeExcel = false; 
        

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

        public FormCargaPresupuestaria()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }


        private void FormCargaPresupuestaria_Load(object sender, EventArgs e)
        {
            configurarEtiquetasgridview();
            PausaBackGround.RunWorkerAsync();
            _ClassParametros.NuevaHojaDePresupuesto = true;
            panelProceso.Parent = this; panelProceso.Visible = false;
            ConfigurarPanelProgressBar();



        }



        private void ConfigurarGrid()
        {
            GridviewCargaPresupuestal.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml(AlternatingRowStyleBackColor);
            GridviewCargaPresupuestal.AlternatingRowsDefaultCellStyle.ForeColor = ColorTranslator.FromHtml(AlternatingRowStyleForeColor);

            DefaultRowStyleBackColor = GridviewCargaPresupuestal.DefaultCellStyle.BackColor;
            DefaultRowStyleForeColor = GridviewCargaPresupuestal.DefaultCellStyle.ForeColor;


        }

        private void dataGridViewCatalogoFuncionalidad_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ajustartamañoEqituetas();
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

        private void button1_Click(object sender, EventArgs e)
        {
        }



        private void GridViewCatalogoFuncionalidad_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RenglonActual = e.RowIndex;
            ColumnaActual = e.ColumnIndex;


        }


        private void RegistrosURSeleccionados()
        {
            string Clave = "";
            string Descripcion = "";

            List<ClassOpcionesSeleccionadas.ListaSeleccionada> LISTA = new List<ClassOpcionesSeleccionadas.ListaSeleccionada>();
            LISTA = OpcionesSeleccionadas.guardarlista;

            for (int Elem = 0; Elem < LISTA.Count; Elem++)
            {
                ClassOpcionesSeleccionadas.ListaSeleccionada registro = LISTA[Elem];
                Clave = registro.clave;
                Descripcion = registro.descripcion;
                AgregarUR(Clave, Descripcion);
            }
        }

        private void AgregarUR(string clave, string descripcion)
        {
            string[] registrogrid = new string[GridviewCargaPresupuestal.Columns.Count];
            registrogrid[0] = clave;
            registrogrid[1] = descripcion;
            try
            {
                GridviewCargaPresupuestal.Rows.Add(registrogrid);
                Lahojasehamodificado = true;
            }
            catch
            {

            }
        }

        private void GridviewCargaPresupuestal_SelectionChanged(object sender, EventArgs e)
        {
            NumRegistrosSeleccionados = GridviewCargaPresupuestal.SelectedRows.Count;
            if (NumRegistrosSeleccionados == 0)
            {
                NumColumnasSeleccionadas = GridviewCargaPresupuestal.SelectedCells.Count;
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void RegistrosFfsSeleccionados()
        {
            string Clave = "";
            string Descripcion = "";

            List<ClassOpcionesSeleccionadas.ListaSeleccionada> LISTA = new List<ClassOpcionesSeleccionadas.ListaSeleccionada>();
            LISTA = OpcionesSeleccionadas.guardarlista;

            for (int Elem = 0; Elem < LISTA.Count; Elem++)
            {
                ClassOpcionesSeleccionadas.ListaSeleccionada registro = LISTA[Elem];

                if (Clave == "")
                    Clave = registro.clave;
                else
                    Clave += "; " + registro.clave;

                if (Descripcion == "")
                    Descripcion = registro.descripcion;
                else
                    Descripcion += "; " + registro.descripcion;
            }

            if (Clave != "")
            {

                GridviewCargaPresupuestal.Rows[RenglonActual].Cells["CodigoCF"].Value = Clave;
                GridviewCargaPresupuestal.Rows[RenglonActual].Cells["ClasificadorCF"].Value = Descripcion;
            }
        }

        private void GridviewCargaPresupuestal_CellEnter(object sender, DataGridViewCellEventArgs e)
        {


        }



        private void GridviewCargaPresupuestal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if ((Array.IndexOf(new String[] { "CodigoUR", "DescripcionUR", "CodigoCOG", "COG", "CodigoCF", "ClasificadorCF", "CodigoPrograma", "Programa", "codigoFuente", "FuenteFinanciamiento"}, GridviewCargaPresupuestal.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                {
                    Rectangle rectangledisplay = GridviewCargaPresupuestal.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                    int formWidthBorder = this.Width - this.ClientSize.Width;
                    int formBorderWidth = (int)((this.Width - this.ClientSize.Width)); // / 2);
                    int formTitleBarHeight = (int)(this.Height - this.ClientSize.Height); // - 2 * formBorderWidth);

                    // panel1.Left +
                    // panel1.Top +

                    leftbuttonFunciones = formBorderWidth +  GridviewCargaPresupuestal.Left + rectangledisplay.Left + rectangledisplay.Width - (buttonFunciones.Width * 2); //  + this.Left; 
                    topbuttonFunciones = formBorderWidth +  GridviewCargaPresupuestal.Top + rectangledisplay.Top + rectangledisplay.Height - (buttonFunciones.Height * 2); // + this.Top;

                    Point point = new Point(leftbuttonFunciones, topbuttonFunciones);
                    buttonFunciones.Location = point;
                    buttonFunciones.Visible = true;

                    if ((Array.IndexOf(new String[] { "CodigoUR", "DescripcionUR" }, GridviewCargaPresupuestal.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                        CatalogoSeleccionado = (int)TipoCatalogo.CatalogoUR;
                    else if ((Array.IndexOf(new String[] { "CodigoCF", "ClasificadorCF" }, GridviewCargaPresupuestal.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                        CatalogoSeleccionado = (int)TipoCatalogo.CatalogoFFS;
                    else if ((Array.IndexOf(new String[] { "CodigoCOG", "COG" }, GridviewCargaPresupuestal.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                        CatalogoSeleccionado = (int)TipoCatalogo.CatalogoCOG;
                    else if ((Array.IndexOf(new String[] { "CodigoPrograma", "Programa" }, GridviewCargaPresupuestal.Columns[e.ColumnIndex].Name.ToString())) >= 0)
                        CatalogoSeleccionado = (int)TipoCatalogo.ClasificacionProgramatica;
                    else if ((Array.IndexOf(new String[] { "codigoFuente", "FuenteFinanciamiento" }, GridviewCargaPresupuestal.Columns[e.ColumnIndex].Name.ToString())) >= 0)
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



        private void insertarURSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Agregar = true;
            OpcionesSeleccionadas.Aplicar = false;
            FormUnidadResponsableSelect _FormUnidadResponsableSelect = new FormUnidadResponsableSelect();

            _FormUnidadResponsableSelect.Location = UbicacionFormulario(_FormUnidadResponsableSelect.Size);
            _FormUnidadResponsableSelect.ShowDialog();
            if (OpcionesSeleccionadas.Aplicar)
            {
                RegistrosURSeleccionados();
            }
        }

        private void agregarFFSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Agregar = true;
            OpcionesSeleccionadas.Aplicar = false;
            FormClasificadorFuncionalSelected _FormClasificadorFuncionalSelected = new FormClasificadorFuncionalSelected();

            _FormClasificadorFuncionalSelected.Location = UbicacionFormulario(_FormClasificadorFuncionalSelected.Size);

            _FormClasificadorFuncionalSelected.ShowDialog();
            if (OpcionesSeleccionadas.Aplicar)
            {
                RegistrosFfsSeleccionados();
            }
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

        private void GridviewCargaPresupuestal_MultiSelectChanged(object sender, EventArgs e)
        {

        }

        private void GridviewCargaPresupuestal_MouseUp(object sender, MouseEventArgs e)
        {
            if (Multiselect)
            {

                if (NumRegistrosSeleccionados == 0 && NumColumnasSeleccionadas > 1)
                {

                    DataGridViewSelectedCellCollection CelldasSeleccionadas = GridviewCargaPresupuestal.SelectedCells;
                    string NombreCelda = GridviewCargaPresupuestal.Columns[CelldasSeleccionadas[CelldasSeleccionadas.Count - 1].ColumnIndex].Name.ToString();

                    if (Array.IndexOf(new String[] { "DescripcionUR", "CodigoUR" }, NombreCelda) >= 0)
                    {
                        contextMenuStripUR.Items["duplicarBloqueURSToolStripMenuItem"].Enabled = true;
                        contextMenuStripUR.Items["eliminarURSToolStripMenuItem"].Enabled = true;
                        contextMenuStripUR.Items["AgregarURSToolStripMenuItem"].Enabled = false;
                        contextMenuStripUR.Items["insertarURSToolStripMenuItem"].Enabled = false;
                        contextMenuStripUR.Items["relacionarCOGToolStripMenuItem"].Enabled = true;

                        Rectangle rectangledisplay = GridviewCargaPresupuestal.GetCellDisplayRectangle(CelldasSeleccionadas[CelldasSeleccionadas.Count - 1].ColumnIndex, CelldasSeleccionadas[CelldasSeleccionadas.Count - 1].RowIndex, false);

                        int formWidthBorder = this.Width - this.ClientSize.Width;
                        int formBorderWidth = (int)((this.Width - this.ClientSize.Width) / 2);
                        int formTitleBarHeight = (int)(this.Height - this.ClientSize.Height - 2 * formBorderWidth);


                        //  panel1.Left
                        // panel1.Top +
                        int left = this.Left + formBorderWidth  + GridviewCargaPresupuestal.Left + rectangledisplay.Left + rectangledisplay.Width - buttonFunciones.Width;
                        int top = this.Top + formBorderWidth +  GridviewCargaPresupuestal.Top + rectangledisplay.Top + rectangledisplay.Height - buttonFunciones.Height;

                        Point point = new Point(left, top);
                        buttonFunciones.Location = point;
                        buttonFunciones.Visible = true;

                        contextMenuStripUR.Show(PosicionMenu());
                    }
                }

            }


        }

        private void contextMenuStripUR_Opening(object sender, CancelEventArgs e)
        {

        }

        private void agregarCOGToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Agregar = true;
            OpcionesSeleccionadas.Aplicar = false;
            FormCOGSelected _FormCOGSelected = new FormCOGSelected();
            _FormCOGSelected.Location = UbicacionFormulario(_FormCOGSelected.Size);
            _FormCOGSelected.ShowDialog();
            if (OpcionesSeleccionadas.Aplicar)
            {
                //  RegistrosURSeleccionados();
            }
        }

        private void relacionarCOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoDeOperacionSeleccionada = (int)TipoOperacion.RelacionarURyCOG;

            OpcionesSeleccionadas.Aplicar = false;
            FormCOGSelected _FormCOGSelected = new FormCOGSelected();
            _FormCOGSelected.Location = UbicacionFormulario(_FormCOGSelected.Size);
            _FormCOGSelected.ShowDialog();
            if (OpcionesSeleccionadas.Aplicar)
            {
                RegistrosCOGSeleccionados(); 
            }
        }

        private void RegistrosCOGSeleccionados()
        {
            string Clave = "";
            string Descripcion = "";

            List<ClassOpcionesSeleccionadas.ListaSeleccionada> LISTA = new List<ClassOpcionesSeleccionadas.ListaSeleccionada>();
            LISTA = OpcionesSeleccionadas.guardarlista;

            int NumRenglonesBloque = 0;
            int NumColumnasBloque = 0;
            int NumRenglonInicial = 0;
            int NumRenglonFinal = 0;

            int NumRenglonFinalBloque = 0;
            int NumRenglonInicioBloque = 0;

            if (TipoDeOperacionSeleccionada == (int)TipoOperacion.RelacionarURyCOG)
            {

                DataGridViewSelectedCellCollection CelldasSeleccionadas = GridviewCargaPresupuestal.SelectedCells;

                NumRenglonInicial = CelldasSeleccionadas[0].RowIndex;
                NumRenglonFinal = CelldasSeleccionadas[CelldasSeleccionadas.Count - 1].RowIndex;

                if (NumRenglonInicial > NumRenglonFinal)
                {
                    int numrenglonaux = NumRenglonFinal;
                    NumRenglonFinal = NumRenglonInicial;
                    NumRenglonInicial = numrenglonaux;
                }

                       
                NumRenglonesBloque =  NumRenglonFinal - NumRenglonInicial + 1;
                NumColumnasBloque = GridviewCargaPresupuestal.ColumnCount;
                BloqueSeleccionado = new String[NumRenglonesBloque, NumColumnasBloque];
                NumRenglonInicioBloque = NumRenglonInicial;

                for (int renglon = 0; renglon < NumRenglonesBloque; renglon++)
                {

                    foreach (DataGridViewColumn columnas in GridviewCargaPresupuestal.Columns)
                    {
                        

                        if (Array.IndexOf(new String[] { "DescripcionUR", "CodigoUR", "CodigoCF", "ClasificadorCF" }, columnas.Name) >= 0)
                        {
                            if (GridviewCargaPresupuestal.Rows[NumRenglonInicial + renglon].Cells[columnas.Index].Value != null)
                                BloqueSeleccionado[renglon, columnas.Index] = GridviewCargaPresupuestal.Rows[NumRenglonInicial + renglon].Cells[columnas.Index].Value.ToString();
                            else
                                BloqueSeleccionado[renglon, columnas.Index] = "";
                        }
                        else if (columnas.Name == "IdRegistroPresupuestal")
                        {
                            BloqueSeleccionado[renglon, columnas.Index] = "-1";
                        }
                        else
                            BloqueSeleccionado[renglon, columnas.Index] = "";
                    }
                }
            }

            bool HeadGroupMarcado = false;
            for (int Elem = 0; Elem < LISTA.Count; Elem++)
            {
                ClassOpcionesSeleccionadas.ListaSeleccionada registro = LISTA[Elem];

                Clave = registro.clave;
                Descripcion = registro.descripcion;

                if (Clave != "")
                {
                    if (TipoDeOperacionSeleccionada == (int)TipoOperacion.AgregarCOG)
                    {
                        string[] registrogrid = new string[GridviewCargaPresupuestal.Columns.Count];
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index] = Clave;
                        registrogrid[GridviewCargaPresupuestal.Columns["COG"].Index] = Descripcion;
                        GridviewCargaPresupuestal.Rows.Add(registrogrid);
                        Lahojasehamodificado = true;
                    }
                    else if (TipoDeOperacionSeleccionada == (int)TipoOperacion.RelacionarURyCOG)
                    {

                        for (int renglon = 0; renglon < NumRenglonesBloque; renglon++)
                        {
                            BloqueSeleccionado[renglon, GridviewCargaPresupuestal.Columns["CodigoCOG"].Index] = Clave;
                            BloqueSeleccionado[renglon, GridviewCargaPresupuestal.Columns["COG"].Index] = Descripcion;
                        }

                        HeadGroupMarcado = false;
                        for (int renglon = 0; renglon < NumRenglonesBloque; renglon++)
                        {
                            string[] registrogrid = new string[GridviewCargaPresupuestal.Columns.Count];
                            for (int col = 0; col < registrogrid.Length; col++)
                            {
                                registrogrid[col] = BloqueSeleccionado[renglon, col].ToString();
                            }

                            if (Elem == 0)
                            {
                                GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index].Value = Clave;
                                GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["COG"].Index].Value = Descripcion;
                                if (!HeadGroupMarcado)
                                {
                                    GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["CodigoUR"].Index].Style.BackColor = Color.DodgerBlue;
                                    GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["DescripcionUR"].Index].Style.BackColor = Color.DodgerBlue;

                                    GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["CodigoUR"].Index].Style.ForeColor = Color.White;
                                    GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["DescripcionUR"].Index].Style.ForeColor = Color.White;

                                    HeadGroupMarcado = true;
                                }
                                NumRenglonInicioBloque++;
                            }
                            else
                            {
                                GridviewCargaPresupuestal.Rows.Add(registrogrid);
                                Lahojasehamodificado = true;
                                if (!HeadGroupMarcado)
                                {
                                    GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["CodigoUR"].Index].Style.BackColor = Color.DodgerBlue;
                                    GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["DescripcionUR"].Index].Style.BackColor = Color.DodgerBlue;

                                    GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["CodigoUR"].Index].Style.ForeColor = Color.White;
                                    GridviewCargaPresupuestal.Rows[NumRenglonInicioBloque].Cells[GridviewCargaPresupuestal.Columns["DescripcionUR"].Index].Style.ForeColor = Color.White;

                                    HeadGroupMarcado = true;
                                }
                                NumRenglonInicioBloque++;
                            }
                        }
                                               
                    }
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void configurarEtiquetasgridview()
        {
            string colorpaneletiqueta = "#D0CECE";
            string colorpictureEtiqueta = "#7F7F7F";

            string Coloretiqueta0 = "#00B0F0";
            string Coloretiqueta1 = "#00B0F0";
            string Coloretiqueta2 = "#FFC519";
            string Coloretiqueta3 = "#C55A11";
            string Coloretiqueta4 = "#92D050";
            string Coloretiqueta5 = "#8888FF";
            string Coloretiqueta6 = "#CCCC00";
            string Coloretiqueta7 = "#990000";


            panelEtiquetas.Left = GridviewCargaPresupuestal.Left;
            panelEtiquetas.Width = GridviewCargaPresupuestal.Width;
            pictureBoxEtiquetas.Parent = panelEtiquetas;
            

            pictureBoxEtiquetas.Left = 0;
            pictureBoxEtiquetas.Top = 2;

            Etiqueta0.Parent = pictureBoxEtiquetas;
            Etiqueta1.Parent = pictureBoxEtiquetas;
            Etiqueta2.Parent = pictureBoxEtiquetas;
            Etiqueta3.Parent = pictureBoxEtiquetas;
            Etiqueta4.Parent = pictureBoxEtiquetas;
            Etiqueta5.Parent = pictureBoxEtiquetas;
            Etiqueta6.Parent = pictureBoxEtiquetas;
            Etiqueta7.Parent = pictureBoxEtiquetas;


            Etiqueta0.Top = 2;
            Etiqueta1.Top = 2;
            Etiqueta2.Top = 2;
            Etiqueta3.Top = 2;
            Etiqueta4.Top = 2;
            Etiqueta5.Top = 2;
            Etiqueta6.Top = 2;
            Etiqueta7.Top = 2;


            Etiqueta0.Height = 26;
            Etiqueta1.Height = 26;
            Etiqueta2.Height = 26;
            Etiqueta3.Height = 26;
            Etiqueta4.Height = 26;
            Etiqueta5.Height = 26;
            Etiqueta6.Height = 26;
            Etiqueta7.Height = 26;


            panelEtiquetas.BackColor =  ColorTranslator.FromHtml(colorpaneletiqueta);
            pictureBoxEtiquetas.BackColor = ColorTranslator.FromHtml(colorpictureEtiqueta);
            Etiqueta0.BackColor  = ColorTranslator.FromHtml(Coloretiqueta0);
            Etiqueta1.BackColor = ColorTranslator.FromHtml(Coloretiqueta1);
            Etiqueta2.BackColor = ColorTranslator.FromHtml(Coloretiqueta2);
            Etiqueta3.BackColor = ColorTranslator.FromHtml(Coloretiqueta3);
            Etiqueta4.BackColor = ColorTranslator.FromHtml(Coloretiqueta4);
            Etiqueta5.BackColor = ColorTranslator.FromHtml(Coloretiqueta5);
            Etiqueta6.BackColor = ColorTranslator.FromHtml(Coloretiqueta6);
            Etiqueta7.BackColor = ColorTranslator.FromHtml(Coloretiqueta7);

            pictureBoxEtiquetas.Height = Etiqueta0.Height + 2;
            panelEtiquetas.Height = pictureBoxEtiquetas.Height + 2;
            panelEtiquetas.Top = GridviewCargaPresupuestal.Top - panelEtiquetas.Height; 

            Etiqueta0.Left = 2;
            ajustartamañoEqituetas();
        }

        private void ajustartamañoEqituetas()
        {

           
            Etiqueta0.Width = GridviewCargaPresupuestal.RowHeadersWidth;
            Etiqueta1.Width = GridviewCargaPresupuestal.Columns[0].Width + GridviewCargaPresupuestal.Columns[1].Width;
          
            Etiqueta2.Width = GridviewCargaPresupuestal.Columns[2].Width + GridviewCargaPresupuestal.Columns[3].Width;
            Etiqueta3.Width = GridviewCargaPresupuestal.Columns[4].Width + GridviewCargaPresupuestal.Columns[5].Width;
            Etiqueta4.Width = GridviewCargaPresupuestal.Columns[6].Width + GridviewCargaPresupuestal.Columns[7].Width;
            Etiqueta5.Width = GridviewCargaPresupuestal.Columns[8].Width + GridviewCargaPresupuestal.Columns[9].Width;
            Etiqueta6.Width = GridviewCargaPresupuestal.Columns[10].Width + GridviewCargaPresupuestal.Columns[11].Width;

            Etiqueta7.Width = 0; 
            for (int col = 12; col < GridviewCargaPresupuestal.Columns.Count; col++)
            {
                if (GridviewCargaPresupuestal.Columns[col].Visible)
                    Etiqueta7.Width += GridviewCargaPresupuestal.Columns[col].Width;
            }
            

            pictureBoxEtiquetas.Width = Etiqueta0.Width + Etiqueta1.Width + Etiqueta2.Width + Etiqueta3.Width + Etiqueta4.Width + Etiqueta5.Width + Etiqueta6.Width + Etiqueta7.Width + 2;


            Etiqueta1.Left = Etiqueta0.Left + Etiqueta0.Width;
            Etiqueta2.Left = Etiqueta1.Left + Etiqueta1.Width;
            Etiqueta3.Left = Etiqueta2.Left + Etiqueta2.Width;
            Etiqueta4.Left = Etiqueta3.Left + Etiqueta3.Width;
            Etiqueta5.Left = Etiqueta4.Left + Etiqueta4.Width;
            Etiqueta6.Left = Etiqueta5.Left + Etiqueta5.Width;
            Etiqueta7.Left = Etiqueta6.Left + Etiqueta6.Width;
           
        }

        private void GridviewCargaPresupuestal_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                int ValorScroll = GridviewCargaPresupuestal.HorizontalScrollingOffset;
                pictureBoxEtiquetas.Left = ValorScroll * -1;

                buttonFunciones.Visible = false;

            }
        }

        private void cargarClasificaciónProgramáticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoDeOperacionSeleccionada = (int)TipoOperacion.AgregarClasificacionProgramatica;
            OpcionesSeleccionadas.Aplicar = false;
            FormClasificacionProgramaticaSelected _FormClasificacionProgramaticaSelected = new FormClasificacionProgramaticaSelected();


            _FormClasificacionProgramaticaSelected.Location = UbicacionFormulario(_FormClasificacionProgramaticaSelected.Size);

            _FormClasificacionProgramaticaSelected.ShowDialog();
            if (OpcionesSeleccionadas.Aplicar)
            {
                RegistrosFfsSeleccionados();
            }
        }

        private void RegistroClasificacionProgramatica()
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TipoDeOperacionSeleccionada = (int)TipoOperacion.AgregarFuenteDeFinanciamiento;
                
            OpcionesSeleccionadas.Aplicar = false;
            FormOrigenIngresoSelected _FormOrigenIngresoSelected = new FormOrigenIngresoSelected();
            _FormOrigenIngresoSelected.Location = UbicacionFormulario(_FormOrigenIngresoSelected.Size);

            _FormOrigenIngresoSelected.ShowDialog();
            if (OpcionesSeleccionadas.Aplicar)
            {
                RegistrosOISeleccionados(); 
            }

        }

        private void RegistrosOISeleccionados()
        {

        }
        

        private void GridviewCargaPresupuestal_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            string NombreCelda = GridviewCargaPresupuestal.Columns[e.ColumnIndex].Name.ToString();
            // En este apartado vamos a validar 

            Lahojasehamodificado = true; 

            try
            {
                if (Array.IndexOf(new String[] { "PresupuestoAsignado", "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" }, NombreCelda) >= 0)
                {
                    if (GridviewCargaPresupuestal.Rows[e.RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Value != null)
                    {
                        if (NombreCelda == "PresupuestoAsignado")
                        {
                            if (GridviewCargaPresupuestal.Rows[e.RowIndex].Cells[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index].Value != null)
                            {
                                string IDCOGCelda = GridviewCargaPresupuestal.Rows[e.RowIndex].Cells[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index].Value.ToString().Trim();
                                string COGCelda = GridviewCargaPresupuestal.Rows[e.RowIndex].Cells[GridviewCargaPresupuestal.Columns["COG"].Index].Value.ToString().Trim();

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
                                decimal Presupuesto = Convert.ToDecimal(GridviewCargaPresupuestal.Rows[e.RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Value.ToString());
                                GridviewCargaPresupuestal.Rows[e.RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Value = Presupuesto.ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                            }
                        } 
                    }

                    CompararPresupuestoYCalendarizacion(e.RowIndex); 
                }
                else
                {
                    if (Array.IndexOf(new String[] { "CodigoUR", "DescripcionUR" }, NombreCelda) >= 0)
                    {


                    }
                }


                if (GridviewCargaPresupuestal.Rows[e.RowIndex].Cells[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index].Value != null)
                {
                    if (Convert.ToInt32(GridviewCargaPresupuestal.Rows[e.RowIndex].Cells[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index].Value.ToString())  > 0)
                    {
                        GridviewCargaPresupuestal.Rows[e.RowIndex].Cells[GridviewCargaPresupuestal.Columns["rowmodificado"].Index].Value = "1";   // registro posiblemente modificado. 
                    }
                }                                       
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message.ToString()); 
            }
        }

        private void CompararPresupuestoYCalendarizacion(int  RowIndex )
        {
            double Presupuesto = Convert.ToDouble(GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Value.ToString());
            TotalCalendarizacionAnual = 0;
            int colenero = GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns["ENERO"].Index].ColumnIndex;
            for (int col = colenero; col < GridviewCargaPresupuestal.ColumnCount; col++)
            {
                string NombreCelda = GridviewCargaPresupuestal.Columns[col].Name.ToString();
                if (Array.IndexOf(new String[] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" }, NombreCelda) >= 0)
                {
                    if (GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns[col].Index].Value != null)
                    {
                        string valuecell = GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns[col].Index].Value.ToString();
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
                GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Style.BackColor = Color.DarkRed;
                GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Style.ForeColor = Color.White;
            }
            else
            {
                if (RowIndex % 2 > 0)
                {
                    GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Style.BackColor = ColorTranslator.FromHtml(AlternatingRowStyleBackColor);
                    GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Style.ForeColor = ColorTranslator.FromHtml(AlternatingRowStyleForeColor);
                }
                else
                {
                    GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Style.BackColor = DefaultBackColor;
                    GridviewCargaPresupuestal.Rows[RowIndex].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Style.ForeColor = DefaultForeColor;
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
                _FormCapturaPorcentajeCOG = new FormCapturaPorcentajeCOG(this);
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
            double presupuestoCOG = 0;
            double totalceldas = 0;
            double [] porcentajeceldas = new double[12];
            double diferencia = 0;



            presupuestoCOG = Convert.ToDouble(GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Value.ToString());

            porcentajeceldas[0] = Math.Round((_ClassParametros.PorcentajeEnero / 100 * presupuestoCOG), 2);
            porcentajeceldas[1] = Math.Round((_ClassParametros.PorcentajeFebrero / 100 * presupuestoCOG), 2);
            porcentajeceldas[2] = Math.Round((_ClassParametros.PorcentajeMarzo / 100 * presupuestoCOG), 2);
            porcentajeceldas[3] = Math.Round((_ClassParametros.PorcentajeAbril / 100 * presupuestoCOG), 2);
            porcentajeceldas[4] = Math.Round((_ClassParametros.PorcentajeMayo / 100 * presupuestoCOG), 2);
            porcentajeceldas[5] = Math.Round((_ClassParametros.PorcentajeJunio / 100 * presupuestoCOG), 2);
            porcentajeceldas[6] = Math.Round((_ClassParametros.PorcentajeJulio / 100 * presupuestoCOG), 2);
            porcentajeceldas[7] = Math.Round((_ClassParametros.PorcentajeAgosto / 100 * presupuestoCOG), 2);
            porcentajeceldas[8] = Math.Round((_ClassParametros.PorcentajeSeptiembre / 100 * presupuestoCOG), 2);
            porcentajeceldas[9] = Math.Round((_ClassParametros.PorcentajeOctubre / 100 * presupuestoCOG), 2);
            porcentajeceldas[10] = Math.Round((_ClassParametros.PorcentajeNoviembre / 100 * presupuestoCOG), 2);
            porcentajeceldas[11] = Math.Round((_ClassParametros.PorcentajeDiciembre / 100 * presupuestoCOG), 2);


            for (int elem = 0; elem < porcentajeceldas.Length; elem++)
            {
                totalceldas += porcentajeceldas[elem];
            }

            diferencia = presupuestoCOG - totalceldas;
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


            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["ENERO"].Index].Value = porcentajeceldas[0].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["FEBRERO"].Index].Value = porcentajeceldas[1].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["MARZO"].Index].Value = porcentajeceldas[2].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["ABRIL"].Index].Value = porcentajeceldas[3].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["MAYO"].Index].Value = porcentajeceldas[4].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["JUNIO"].Index].Value = porcentajeceldas[5].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["JULIO"].Index].Value = porcentajeceldas[6].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["AGOSTO"].Index].Value = porcentajeceldas[7].ToString("#,##0.00", _ClassParametros.MiRegionProvider);            
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["SEPTIEMBRE"].Index].Value = porcentajeceldas[8].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["OCTUBRE"].Index].Value = porcentajeceldas[9].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["NOVIEMBRE"].Index].Value = porcentajeceldas[10].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
            GridviewCargaPresupuestal.Rows[NumRow].Cells[GridviewCargaPresupuestal.Columns["DICIEMBRE"].Index].Value = porcentajeceldas[11].ToString("#,##0.00", _ClassParametros.MiRegionProvider);

        }

        public void CalendarizacioCOGProcesada()
        {

            _ClassParametros.PORCENTAJECOG.Add(new ClassParametros._PorcentajeCOG(_ClassParametros.IDCOGCELDA, _ClassParametros.PorcentajeEnero.ToString(), _ClassParametros.PorcentajeFebrero.ToString(),
            _ClassParametros.PorcentajeMarzo.ToString(), _ClassParametros.PorcentajeAbril.ToString(), _ClassParametros.PorcentajeMayo.ToString(), _ClassParametros.PorcentajeJunio.ToString(),
            _ClassParametros.PorcentajeJulio.ToString(), _ClassParametros.PorcentajeAgosto.ToString(), _ClassParametros.PorcentajeSeptiembre.ToString(), _ClassParametros.PorcentajeOctubre.ToString(),
            _ClassParametros.PorcentajeNoviembre.ToString(), _ClassParametros.PorcentajeDiciembre.ToString())); 

            string idcogfocus = GridviewCargaPresupuestal.Rows[_ClassParametros.IdRowGrid].Cells[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index].Value.ToString();
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
                sqldataadapter.SelectCommand.Parameters.Add("@id_cog", SqlDbType.VarChar);
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
                        DataRow registro = datatable.Rows[RegistroActual];
                        _ClassParametros.PORCENTAJECOG.Add(new ClassParametros._PorcentajeCOG(registro["ID_COG"].ToString().Trim(), registro["ENERO"].ToString(), registro["FEBRERO"].ToString(),
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

        private void button3_Click(object sender, EventArgs e)
        {
            buttonGuardar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (GuardarHojaDeTrabajo())
                Lahojasehamodificado = false;

            buttonGuardar.Enabled = true;
            this.Cursor = Cursors.Default;

        }

        private bool GuardarHojaDeTrabajo()
        {
            bool ProcesoCorrecto = true;



            if (_ClassParametros.NuevaHojaDePresupuesto)
            {
                _ClassParametros.ProcesoCancelado = false;
                _ClassParametros.ProcesoCorrecto = false;

                GuardarPropiedadesHojaPresupuestal();

                if (!_ClassParametros.ProcesoCancelado && _ClassParametros.ProcesoCorrecto)
                {
                    ProcesoCorrecto = Guardar();
                }                                 
            }
            else
                ProcesoCorrecto = Guardar();

            return ProcesoCorrecto;
        }


        private bool GuardarHojaPresupuestal()
        {
            bool NuevoRegistro = false;
            int IdRegistroHojaPresupuesto = 0; 
            bool ProcesoCorrecto = true;
            int RegistroActual = 0;
            int RegistroFinal = GridviewCargaPresupuestal.Rows.Count - 1;
            bool EjecutarQuery = false;

            labelInicial.Text = "0";
            labelFinal.Text = RegistroFinal.ToString("#,###");
            panelProceso.Visible = true;

            string CodigoUR = "";
            string CodigoCOG = "";
            string CodigoCF = "";
            string CodigoPrograma = "";                    // Clasificacion programatica
            string CodigoProgramaPresupuestal = "";        // Programa presupuestal
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
            CommandTransactionHojas.CommandText = "INSERTAR_REGISTROS_HOJAS_DE_PRESUPUESTO";

            try
            {
                CommandTransactionHojas.Parameters.Add("@ID_HOJA_PRESUPUESTO", SqlDbType.Int, 4);
                CommandTransactionHojas.Parameters.Add("@ID_UR", SqlDbType.VarChar);
                CommandTransactionHojas.Parameters.Add("@ID_COG", SqlDbType.VarChar);
                CommandTransactionHojas.Parameters.Add("@ID_FFS", SqlDbType.VarChar);
                CommandTransactionHojas.Parameters.Add("@ID_PROGRAMA", SqlDbType.Int);
                CommandTransactionHojas.Parameters.Add("@ID_TIPO_GASTO", SqlDbType.Int);
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
                CommandTransactionHojas.Parameters.Add("@ID_REGISTRO_PRESUPUESTO", SqlDbType.Int);
                CommandTransactionHojas.Parameters.Add("@NUEVO_REGISTRO", SqlDbType.Bit);

                //DUPLICO PARA GFUARDAR EN LA TABLA PRESUPUESTO_SALDO

                foreach (DataGridViewRow rowpresupuesto in GridviewCargaPresupuestal.Rows)
                {
                    Application.DoEvents();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoUR"].Index].Value != null)
                        CodigoUR = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoUR"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index].Value != null)
                        CodigoCOG = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoCF"].Index].Value != null)
                        CodigoCF = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoCF"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoPrograma"].Index].Value != null)
                        CodigoPrograma = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoPrograma"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoProgramaPresupuestal"].Index].Value != null)
                        CodigoProgramaPresupuestal = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["CodigoProgramaPresupuestal"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["codigoFuente"].Index].Value != null)
                        codigoFuente = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["codigoFuente"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Value != null)
                        PresupuestoAsignado = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["ENERO"].Index].Value != null)
                        ENERO = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["ENERO"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["FEBRERO"].Index].Value != null)
                        FEBRERO = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["FEBRERO"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["MARZO"].Index].Value != null)
                        MARZO = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["MARZO"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["ABRIL"].Index].Value != null)
                        ABRIL = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["ABRIL"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["MAYO"].Index].Value != null)
                        MAYO = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["MAYO"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["JUNIO"].Index].Value != null)
                        JUNIO = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["JUNIO"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["JULIO"].Index].Value != null)
                        JULIO = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["JULIO"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["AGOSTO"].Index].Value != null)
                        AGOSTO = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["AGOSTO"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["SEPTIEMBRE"].Index].Value != null)
                        SEPTIEMBRE = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["SEPTIEMBRE"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["OCTUBRE"].Index].Value != null)
                        OCTUBRE = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["OCTUBRE"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["NOVIEMBRE"].Index].Value != null)
                        NOVIEMBRE = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["NOVIEMBRE"].Index].Value.ToString();
                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["DICIEMBRE"].Index].Value != null)
                        DICIEMBRE = rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["DICIEMBRE"].Index].Value.ToString();


                    if (CodigoPrograma.Trim() == "") CodigoPrograma = "-1";
                    if (CodigoProgramaPresupuestal.Trim() == "") CodigoProgramaPresupuestal = "-1";
                    if (CodigoProgramaPresupuestal.Trim() == "") CodigoProgramaPresupuestal = "-1";
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


                    CommandTransactionHojas.Parameters["@ID_HOJA_PRESUPUESTO"].Value = _ClassParametros.IdHojaPresupuesto;

                    CommandTransactionHojas.Parameters["@ID_UR"].Value = CodigoUR;

                    CommandTransactionHojas.Parameters["@ID_COG"].Value = CodigoCOG;

                    CommandTransactionHojas.Parameters["@ID_FFS"].Value = CodigoCF;

                    CommandTransactionHojas.Parameters["@ID_PROGRAMA"].Value = CodigoPrograma;

                    CommandTransactionHojas.Parameters["@ID_TIPO_GASTO"].Value = CodigoProgramaPresupuestal;

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


                    if (_ClassParametros.NuevaHojaDePresupuesto)
                    {
                        if (!rowpresupuesto.IsNewRow)
                        {
                            CommandTransactionHojas.Parameters["@ID_REGISTRO_PRESUPUESTO"].Value = -1;
                            CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 1;

                            EjecutarQuery = true;
                            NuevoRegistro = true;
                        }
                        else
                            EjecutarQuery = false;
                    }
                    else
                    {
                        if (!rowpresupuesto.IsNewRow)
                        {

                            if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index].Value != null)
                            {

                                // si no existe se agrega, si existe se verifica que no haya sufrido modificacion. 
                                if (!ExisteRegistroEnLaTablaHojaDeTrabajo(rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index].Value.ToString()))
                                {
                                    CommandTransactionHojas.Parameters["@ID_REGISTRO_PRESUPUESTO"].Value = -1;
                                    CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 1;

                                    EjecutarQuery = true;
                                    NuevoRegistro = true;
                                }
                                else
                                {
                                    if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["rowmodificado"].Index].Value != null)
                                    {
                                        if (rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["rowmodificado"].Index].Value.ToString().Trim() == "1")
                                        {
                                            CommandTransactionHojas.Parameters["@ID_REGISTRO_PRESUPUESTO"].Value = Convert.ToInt32(rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index].Value.ToString());
                                            CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 0;    // Registro que existe en la tabla y ha sido modificado. 

                                            EjecutarQuery = true;
                                            NuevoRegistro = false;
                                        }
                                        else
                                            EjecutarQuery = false;
                                    }
                                    else
                                    {
                                        CommandTransactionHojas.Parameters["@ID_REGISTRO_PRESUPUESTO"].Value = Convert.ToInt32(rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index].Value.ToString());
                                        CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 0;    // Registro que existe en la tabla y no se sabe a ciencia cierta si fue modificado o no.   (se actualiza) 

                                        EjecutarQuery = true;
                                        NuevoRegistro = false;
                                    }
                                }
                            }
                            else   // Nuevo registro (agregado despues de haber guardado una parte de la hoja de trabajo o despues de abrir un proyecto
                                   // Esta opcion es poco probable que suceda, sin embargo se tiene en consieracion por algun descuido al agregar nuevos registros. 
                            {
                                CommandTransactionHojas.Parameters["@ID_REGISTRO_PRESUPUESTO"].Value = -1;
                                CommandTransactionHojas.Parameters["@NUEVO_REGISTRO"].Value = 1;

                                EjecutarQuery = true;
                                NuevoRegistro = true;
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
                            IdRegistroHojaPresupuesto = Convert.ToInt32(IdNumregistro);
                            rowpresupuesto.Cells[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index].Value = IdRegistroHojaPresupuesto;
                        }
                        else
                        {
                            CommandTransactionHojas.ExecuteNonQuery();
                        }
                    }
                    if (RegistroActual < RegistroFinal)
                        RegistroActual++;

                    labelInicial.Text = RegistroActual.ToString("#,###");
                    CodigoUR = ""; CodigoCOG = ""; CodigoCF = ""; CodigoPrograma = ""; CodigoProgramaPresupuestal = ""; codigoFuente = ""; PresupuestoAsignado = "";
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

        private void GuardarPropiedadesHojaPresupuestal()
        {

            _ClassParametros.ProcesoCancelado = false;
            FormCaracteristicasHOJAPRESUPUESTO _FormCaracteristicasHOJAPRESUPUESTO = new FormCaracteristicasHOJAPRESUPUESTO();
            _FormCaracteristicasHOJAPRESUPUESTO.ShowDialog();

        }

        private void FormCargaPresupuestaria_ResizeEnd(object sender, EventArgs e)
        {
            panelProceso.Left = (this.Width / 2) - (panelProceso.Width / 2);
            panelProceso.Top = (this.Height / 2) - (panelProceso.Height / 2);
            panelProgressBar.Left = (this.Width - panelProgressBar.Width) / 2;
            panelProgressBar.Top = (this.Height - panelProgressBar.Height) / 2;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _ClassParametros.ProcesoCancelado = false;
            _ClassParametros.ProcesoCorrecto = false;

            FormAbrirHojasDeTrabajoPresupuesto _FormAbrirHojasDeTrabajoPresupuesto = new FormAbrirHojasDeTrabajoPresupuesto();
            _FormAbrirHojasDeTrabajoPresupuesto.ShowDialog();

            if (!_ClassParametros.ProcesoCancelado && _ClassParametros.ProcesoCorrecto)
            {
                this.Cursor = Cursors.WaitCursor;
                if (CargarHojaPresupuesto())
                {
                    _ClassParametros.NuevaHojaDePresupuesto = false; 
                }
                this.Cursor = Cursors.Default;
            }
        }


        private bool CargarHojaPresupuesto()
        {
            bool ProcesoCorrecto = true; 
            double[] MontoCeldas = new double[13];
            GridviewCargaPresupuestal.Rows.Clear();
            int RegistroActual = 0;
            int RegistroFinal = 0;  

            labelInicial.Text = "0";
            string NombreProcedimiento = "ConsultarHojasDeTrabajoPresupuestal";


            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@ID_HOJA_PRESUPUESTO", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@ID_HOJA_PRESUPUESTO"].Value = Convert.ToInt32(_ClassParametros.IdHojaPresupuesto);
            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "HojaDeTrabajoPresupuestal");
            DataTable datatable = dataset.Tables["HojaDeTrabajoPresupuestal"];


            try
            {
                if (datatable.Rows.Count >= 0)
                {
                    panelProceso.Visible = true;
                    RegistroFinal = GridviewCargaPresupuestal.Rows.Count - 1;
                    labelFinal.Text = RegistroFinal.ToString("#,###");

                    RegistroFinal = datatable.Rows.Count;

                    progressBarProceso.Value = 0;
                    progressBarProceso.Maximum = RegistroFinal;

                    while (RegistroActual < RegistroFinal)
                    {
                       

                        string[] registrogrid = new string[27];
                        DataRow registro = datatable.Rows[RegistroActual];
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoUR"].Index] = registro["ID_UR"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["DescripcionUR"].Index] = registro["UR"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index] = registro["ID_COG"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["COG"].Index] = registro["COG"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoCF"].Index] = registro["ID_FFS"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["ClasificadorCF"].Index] = registro["CLASIFICACION_FUNCIONAL"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoPrograma"].Index] = registro["ID_PROGRAMA"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["Programa"].Index] = registro["PROGRAMATICO"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoProgramaPresupuestal"].Index] = registro["ID_TIPO_GASTO"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["ProgramaPresupuestal"].Index] = registro["PROGRAMA_PRESUPUESTAL"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["codigoFuente"].Index] = registro["ID_ORIGEN_INGRESO"].ToString().Trim();
                        registrogrid[GridviewCargaPresupuestal.Columns["FuenteFinanciamiento"].Index] = registro["ORIGEN_INGRESO"].ToString().Trim();


                        if (_ClassParametros.IsNumeric(registro["Presupuesto"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index] = Math.Round(Convert.ToDecimal(registro["Presupuesto"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["ENERO"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["ENERO"].Index] = Math.Round(Convert.ToDecimal(registro["ENERO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["ENERO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["FEBRERO"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["FEBRERO"].Index] = Math.Round(Convert.ToDecimal(registro["FEBRERO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["FEBRERO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["MARZO"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["MARZO"].Index] = Math.Round(Convert.ToDecimal(registro["MARZO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["MARZO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["ABRIL"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["ABRIL"].Index] = Math.Round(Convert.ToDecimal(registro["ABRIL"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["ABRIL"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["MAYO"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["MAYO"].Index] = Math.Round(Convert.ToDecimal(registro["MAYO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["MAYO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["JUNIO"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["JUNIO"].Index] = Math.Round(Convert.ToDecimal(registro["JUNIO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["JUNIO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["JULIO"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["JULIO"].Index] = Math.Round(Convert.ToDecimal(registro["JULIO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["JULIO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["AGOSTO"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["AGOSTO"].Index] = Math.Round(Convert.ToDecimal(registro["AGOSTO"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["AGOSTO"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["SEPTIEMBRE"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["SEPTIEMBRE"].Index] = Math.Round(Convert.ToDecimal(registro["SEPTIEMBRE"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["SEPTIEMBRE"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["OCTUBRE"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["OCTUBRE"].Index] = Math.Round(Convert.ToDecimal(registro["OCTUBRE"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["OCTUBRE"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["NOVIEMBRE"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["NOVIEMBRE"].Index] = Math.Round(Convert.ToDecimal(registro["NOVIEMBRE"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["NOVIEMBRE"].Index] = "";

                        if (_ClassParametros.IsNumeric(registro["DICIEMBRE"].ToString()))
                            registrogrid[GridviewCargaPresupuestal.Columns["DICIEMBRE"].Index] = Math.Round(Convert.ToDecimal(registro["DICIEMBRE"].ToString()), 2).ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        else
                            registrogrid[GridviewCargaPresupuestal.Columns["DICIEMBRE"].Index] = "";


                        registrogrid[GridviewCargaPresupuestal.Columns["rowmodificado"].Index] = "0";
                        registrogrid[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index] = registro["ID_REGISTRO_PRESUPUESTO"].ToString();

                        GridviewCargaPresupuestal.Rows.Add(registrogrid);
                        CompararPresupuestoYCalendarizacion(RegistroActual);
                        GridviewCargaPresupuestal.Rows[RegistroActual].HeaderCell.Value = (RegistroActual + 1).ToString(); 
                        RegistroActual++;


                        labelInicial.Text = RegistroActual.ToString("#,###");
                        progressBarProceso.Value = RegistroActual;

                    }

                }
                else
                {
                    MessageBox.Show(this, "No se encontraron registros en el nombre de hoja seleccionada" , "0 Registros encontrados.", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                for (int row = 0; row < GridviewCargaPresupuestal.Rows.Count; row++)
                {
                    GridviewCargaPresupuestal.Rows[row].HeaderCell.Value = (row + 1).ToString();
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
            if (row.Cells[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index].Value != null)
            {
                int IdRegistro = Convert.ToInt32(row.Cells[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index].Value.ToString());  // indica que es un registro previamente almacenado
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

        private bool ExisteRegistroEnLaTablaHojaDeTrabajo(string IdRegistroPresupuesto)
        {
            bool ExisteRegistro = false; 
            string MySql = "SELECT ID_REGISTRO_PRESUPUESTO FROM HOJAS_DE_PRESUPUESTO WHERE ID_REGISTRO_PRESUPUESTO = " + IdRegistroPresupuesto + "; SELECT SCOPE_IDENTITY() ";
            SqlCommand CommandVerificacion = new SqlCommand(MySql, ClassBaseDeDatos.MyConnectionDB);
            CommandVerificacion.CommandType = CommandType.Text;
            CommandVerificacion.Transaction = TransactionPresupuesto;
            Object RegistroEncontrado =  CommandVerificacion.ExecuteScalar();
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
                _ClassParametros.NuevaHojaDePresupuesto = true;
                NumRegistrosEliminados = 0;
                _ClassParametros.IdRegistrosEliminados.Clear();
                _ClassParametros.NuevaHojaDePresupuesto = true;
                GridviewCargaPresupuestal.Rows.Clear();
                if (TransactionPresupuesto != null)
                    TransactionPresupuesto.Dispose();
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

            GuardarPropiedadesHojaPresupuestal();
            if (!_ClassParametros.ProcesoCancelado && _ClassParametros.ProcesoCorrecto)
            {
                Guardar();
            }
            buttonGuardar.Enabled = true;
            this.Cursor = Cursors.Default;

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private bool  Guardar()
        {
            bool ProcesoCorrecto = true;

            TransactionPresupuesto = ClassBaseDeDatos.MyConnectionDB.BeginTransaction();
            CommandTransactionHojas = ClassBaseDeDatos.MyConnectionDB.CreateCommand();
            CommandTransactionHojas.CommandType = CommandType.StoredProcedure;
            CommandTransactionHojas.Transaction = TransactionPresupuesto;






            if (_ClassParametros.NuevaHojaDePresupuesto)
            {

                if (GuardarHojaPresupuestal())
                {
                    TransactionPresupuesto.Commit();
                }
                else
                {
                    TransactionPresupuesto.Rollback();
                    ProcesoCorrecto = false;
                }
            }
            else
            {

                if (_ClassParametros.IdRegistrosEliminados.Count > 0)
                {
                    CommandTransactionHojas.CommandText = "ELIMINAR_REGISTROS_HOJAS_PRESUPUESTO";
                    if (RegistrosEliminados())
                    {
                        if (GuardarHojaPresupuestal())
                        {
                            TransactionPresupuesto.Commit();
                        }
                        else
                        {
                            TransactionPresupuesto.Rollback();
                            ProcesoCorrecto = false;
                        }
                    }
                    else
                    {
                        TransactionPresupuesto.Rollback();
                        ProcesoCorrecto = false;
                    }


                }
                else
                {
                    if (GuardarHojaPresupuestal())
                    {
                        TransactionPresupuesto.Commit();
                    }
                    else
                    {
                        TransactionPresupuesto.Rollback();
                        ProcesoCorrecto = false;
                    }

                }
            }
            if (ProcesoCorrecto)
            {
                _ClassParametros.NuevaHojaDePresupuesto = false;
                MessageBox.Show(this, "La hoja de presupuesto se ha guardado correctamente: \n\r\n\r", "Proceso terminado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                obtenerDatosHojaDePresupuesto(Convert.ToInt32(_ClassParametros.IdHojaPresupuesto));  //Duplico HOJA_DE_PRESUPUESTO en HOJA_DE_PRESUPUESTO_SALDO    
            }


            TransactionPresupuesto.Dispose();
            CommandTransactionHojas.Dispose();

            return ProcesoCorrecto; 
        }

        public static void obtenerDatosHojaDePresupuesto(int codigo)
        {
            string procedimientoAlmacenado = "DUPLICAR_HOJA_PRESUPUESTO";
            SqlConnection conexion = new SqlConnection(@"Data Source=SERVIDOR-PC\SIAPG;Initial Catalog=SIAPG;Integrated Security=True");
            SqlDataAdapter miSqlDataAdapter = new SqlDataAdapter(procedimientoAlmacenado, conexion);
            conexion.Open();

            miSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            miSqlDataAdapter.SelectCommand.Parameters.Add("@IdHojaPresupuesto", SqlDbType.Int).Value = codigo;
            miSqlDataAdapter.SelectCommand.ExecuteNonQuery();

        }


        private void FormCargaPresupuestaria_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Lahojasehamodificado)
                {
                    DialogResult OpcionGuardar = new DialogResult();


                    string Alerta = "Los cambios realizados en la hoja actual no se han guardado. " +
                        "\n\r\n\r" + "Desea guardar los registros antes de salir ?";
                    OpcionGuardar = MessageBox.Show(this, Alerta, "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (OpcionGuardar == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        if (GuardarHojaDeTrabajo())
                        {
                            if (ImportacionDesdeExcel)
                            {
                                ImportacionDesdeExcel = false;   // Este módulo solamente aplica para cuando se importan los presupuestos desde excel y la hoja no tiene todos los datos necesarios. 
                                ActualizarParametrosNoImportados();
                            }
                        }

                        buttonGuardar.Enabled = true;
                        this.Cursor = Cursors.Default;
                    }
                } 


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

        private void ActualizarParametrosNoImportados()
        {
            // Este  procedimiento se ha implementado para cargar de manera automatica los valores del tipo de gasto, en base al COG y tambien el parametro de Finalidad, Funcion y Subfuncion que 
            // no han sido especificados en la hoja de importacion excel..   
            // Nota: Si en lo sucesivo se especifica en la hoja excel, probablemente no sea necesario invocar este procedimiento. 

            string MySql = "";
            try
            {
                // Gasto corriente = 1
                MySql = "UPDATE HOJAS_DE_PRESUPUESTO SET ID_TIPO_GASTO = 1 WHERE LEFT(ID_COG,1) IN ('1','2','3', '4') AND ID_HOJA_PRESUPUESTO = " + _ClassParametros.IdHojaPresupuesto.ToString();
                SqlCommand CommandVerificacion = new SqlCommand(); // MySql, ClassBaseDeDatos.MyConnectionDB);
                CommandVerificacion.Connection = ClassBaseDeDatos.MyConnectionDB;
                CommandVerificacion.CommandType = CommandType.Text;
                CommandVerificacion.CommandText = MySql;;
                CommandVerificacion.ExecuteNonQuery();

                // Gasto de capital = 2
                MySql = "UPDATE HOJAS_DE_PRESUPUESTO SET ID_TIPO_GASTO = 2 WHERE LEFT(ID_COG,1) IN ('5','6','7') AND ID_HOJA_PRESUPUESTO = " + _ClassParametros.IdHojaPresupuesto.ToString();
                CommandVerificacion.CommandText = MySql; ;
                CommandVerificacion.ExecuteNonQuery();

                // Amortización de la deuda y disminución del pasivo  = 3
                MySql = "UPDATE HOJAS_DE_PRESUPUESTO SET ID_TIPO_GASTO = 3 WHERE LEFT(ID_COG,1) IN ('8','9') AND ID_HOJA_PRESUPUESTO = " + _ClassParametros.IdHojaPresupuesto.ToString();
                CommandVerificacion.CommandText = MySql; 
                CommandVerificacion.ExecuteNonQuery();


                // Se actualiza el catalogo de Finalidad Funcion y Subfuncion (FFS)
                MySql = "UPDATE HOJAS_DE_PRESUPUESTO SET ID_FFS = CAT_PROGRAMAS.ID_FFS "  +   
                " FROM HOJAS_DE_PRESUPUESTO INNER JOIN CAT_PROGRAMAS " +
                " ON HOJAS_DE_PRESUPUESTO.ID_UR = CAT_PROGRAMAS.ID_UR " +
                " WHERE HOJAS_DE_PRESUPUESTO.ID_HOJA_PRESUPUESTO = " + _ClassParametros.IdHojaPresupuesto.ToString();
                CommandVerificacion.CommandText = MySql;
                CommandVerificacion.ExecuteNonQuery();
            }
            catch (Exception MyException)
            {
                MessageBox.Show(this, "Ha ocurrido un error al actualizar los parámetros del tipo de gasto, en la tabla [HOJAS_DE_PRESUPUESTO] " + "\n\r\n\r"  +  MyException.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ConexionCatalogoExcelEstablecida()
        {

            bool ConexionEstablecida = true;
            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source= " + _ClassParametros.RutaArchivoPresupuesto.ToString()  + @" ;Extended Properties= ""Excel 12.0 Xml;HDR=Yes;IMEX=1""";
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                DbConnection conexion = factory.CreateConnection();
                DbCommand comando = conexion.CreateCommand();
                conexion.ConnectionString = connectionString;
                comando.CommandText = "SELECT * FROM [" + _ClassParametros.NombreHojaPresupuesto.ToString() + "$]";
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


            FormImportarPresupuesto _FormImportarPresupuesto = new FormImportarPresupuesto();
            _FormImportarPresupuesto.ShowDialog();
            if (!_ClassParametros.ProcesoCancelado && _ClassParametros.ProcesoEjecutado)
            {
                HabilitarProgressProcesando();

                if (ConexionCatalogoExcelEstablecida())
                {
                    if (ImportarPresupuestoExcel())
                    {
                        ImportacionDesdeExcel = true; 
                        Lahojasehamodificado = true;
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
                                    Lahojasehamodificado = false;
                                    ActualizarParametrosNoImportados();  
                                    _ClassTipoProceso.TipoProceso =  (int)  ClassTipoProceso.ProcesoAejecutar.CuadroComparativoPresupuestoIngresoConAjustes;
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

        private bool ImportarPresupuestoExcel()
        {
            bool ImportacionCorrecta = true;
            int NumRecord = 0;
            int NumRenglonExcel = 1;
            string CodigoFFS = "";
            string AreaEjecutora = "";
            string TipoGasto = "";
            string OrigenRecurso = "";
            string CodigoCOG = "";
            string TipoDeGasto = "";
            string CodigoClasificacionProgramatica = "";
            double[] porcentajeceldas = new double[12];

            double presupuestoCOG = 0;
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
                        AreaEjecutora = DataRecordExcel[1].ToString();
                        TipoGasto = DataRecordExcel[2].ToString();
                        OrigenRecurso = DataRecordExcel[3].ToString();
                        CodigoCOG = DataRecordExcel[4].ToString();
                        CodigoFFS = DataRecordExcel[18].ToString();
                        CodigoClasificacionProgramatica = DataRecordExcel[19].ToString();
                        TipoDeGasto = DataRecordExcel[2].ToString();

                        presupuestoCOG = 0;
                        for (int Elem = 0; Elem < porcentajeceldas.Length; Elem++)
                        {
                            porcentajeceldas[Elem] = 0;     // Porcentaje monetario
                            PorcentajeMensual[Elem] = 0;    // Porcentaje porciento
                        }

                        // presupuesto
                        if (DataRecordExcel[5] != null)
                        {
                            if (_ClassParametros.IsNumeric(DataRecordExcel[5].ToString()))
                            {
                                MontoTotalImportacion += Convert.ToDouble(DataRecordExcel[5].ToString());
                                presupuestoCOG = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[5].ToString()), 2));
                                MontoTotalAjustado += presupuestoCOG;
                            }
                        }

                        // enero
                        if (DataRecordExcel[6] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[6].ToString()))
                                porcentajeceldas[0] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[6].ToString()), 2));

                        // febrero
                        if (DataRecordExcel[7] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[7].ToString()))
                                porcentajeceldas[1] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[7].ToString()), 2));

                        // marzo
                        if (DataRecordExcel[8] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[8].ToString()))
                                porcentajeceldas[2] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[8].ToString()), 2));

                        // abril 
                        if (DataRecordExcel[9] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[9].ToString()))
                                porcentajeceldas[3] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[9].ToString()), 2));

                        // mayo
                        if (DataRecordExcel[10] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[10].ToString()))
                                porcentajeceldas[4] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[10].ToString()), 2));

                        // junio
                        if (DataRecordExcel[11] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[11].ToString()))
                                porcentajeceldas[5] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[11].ToString()), 2));

                        // julio
                        if (DataRecordExcel[12] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[12].ToString()))
                                porcentajeceldas[6] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[12].ToString()), 2));

                        // agosto
                        if (DataRecordExcel[13] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[13].ToString()))
                                porcentajeceldas[7] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[13].ToString()), 2));


                        // septiembre
                        if (DataRecordExcel[14] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[14].ToString()))
                                porcentajeceldas[8] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[14].ToString()), 2));

                        // octubre
                        if (DataRecordExcel[15] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[15].ToString()))
                                porcentajeceldas[9] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[15].ToString()), 2));

                        // noviembre
                        if (DataRecordExcel[16] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[16].ToString()))
                                porcentajeceldas[10] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[16].ToString()), 2));

                        // diciembre
                        if (DataRecordExcel[17] != null)
                            if (_ClassParametros.IsNumeric(DataRecordExcel[17].ToString()))
                                porcentajeceldas[11] = Convert.ToDouble(Math.Round(Convert.ToDecimal(DataRecordExcel[17].ToString()), 2));



                        totalceldas = 0;

                        for (int elem = 0; elem < porcentajeceldas.Length; elem++)
                        {
                            totalceldas += porcentajeceldas[elem];
                        }

                        diferencia = presupuestoCOG - totalceldas;

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


                        PorcentajeMensual[0] = Math.Round(porcentajeceldas[0] / presupuestoCOG * 100, 4);  // Enero
                        PorcentajeMensual[1] = Math.Round(porcentajeceldas[1] / presupuestoCOG * 100, 4);  // Febrero
                        PorcentajeMensual[2] = Math.Round(porcentajeceldas[2] / presupuestoCOG * 100, 4);  // Marzo
                        PorcentajeMensual[3] = Math.Round(porcentajeceldas[3] / presupuestoCOG * 100, 4);  // Abril
                        PorcentajeMensual[4] = Math.Round(porcentajeceldas[4] / presupuestoCOG * 100, 4);  // Mayo
                        PorcentajeMensual[5] = Math.Round(porcentajeceldas[5] / presupuestoCOG * 100, 4);  // Junio
                        PorcentajeMensual[6] = Math.Round(porcentajeceldas[6] / presupuestoCOG * 100, 4);  // Julio
                        PorcentajeMensual[7] = Math.Round(porcentajeceldas[7] / presupuestoCOG * 100, 4);  // Agosto
                        PorcentajeMensual[8] = Math.Round(porcentajeceldas[8] / presupuestoCOG * 100, 4);  // Septiembre
                        PorcentajeMensual[9] = Math.Round(porcentajeceldas[9] / presupuestoCOG * 100, 4);  // Octubre
                        PorcentajeMensual[10] = Math.Round(porcentajeceldas[10] / presupuestoCOG * 100, 4);  // Noviembre
                        PorcentajeMensual[11] = Math.Round(porcentajeceldas[11] / presupuestoCOG * 100, 4);  // Diciembre

                        
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

                        //LLENAR LOS CAMPOS DEL GRID
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoUR"].Index] = AreaEjecutora;
                        registrogrid[GridviewCargaPresupuestal.Columns["DescripcionUR"].Index] = getDescripcionUR(AreaEjecutora);
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoCOG"].Index] = CodigoCOG;
                        registrogrid[GridviewCargaPresupuestal.Columns["COG"].Index] = getDescripcionCOG(CodigoCOG);
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoCF"].Index] = CodigoFFS;   //Tercer paso
                        registrogrid[GridviewCargaPresupuestal.Columns["ClasificadorCF"].Index] = getDescripcionFFS(CodigoFFS);
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoPrograma"].Index] = CodigoClasificacionProgramatica;
                        registrogrid[GridviewCargaPresupuestal.Columns["Programa"].Index] = "A desarrollar";
                        registrogrid[GridviewCargaPresupuestal.Columns["CodigoProgramaPresupuestal"].Index] = TipoDeGasto;
                        registrogrid[GridviewCargaPresupuestal.Columns["ProgramaPresupuestal"].Index] = getDescripcionTipoGasto(TipoDeGasto);
                        registrogrid[GridviewCargaPresupuestal.Columns["codigoFuente"].Index] = IdOrigenRecursoORFIS;
                        registrogrid[GridviewCargaPresupuestal.Columns["FuenteFinanciamiento"].Index] = getDescripcionFuenteFunanciamiento(IdOrigenRecursoORFIS);



                        registrogrid[GridviewCargaPresupuestal.Columns["PresupuestoAsignado"].Index] = presupuestoCOG.ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["ENERO"].Index] = porcentajeceldas[0].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["FEBRERO"].Index] = porcentajeceldas[1].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["MARZO"].Index] = porcentajeceldas[2].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["ABRIL"].Index] = porcentajeceldas[3].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["MAYO"].Index] = porcentajeceldas[4].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["JUNIO"].Index] = porcentajeceldas[5].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["JULIO"].Index] = porcentajeceldas[6].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["AGOSTO"].Index] = porcentajeceldas[7].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["SEPTIEMBRE"].Index] = porcentajeceldas[8].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["OCTUBRE"].Index] = porcentajeceldas[9].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["NOVIEMBRE"].Index] = porcentajeceldas[10].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["DICIEMBRE"].Index] = porcentajeceldas[11].ToString("#,##0.00", _ClassParametros.MiRegionProvider);
                        registrogrid[GridviewCargaPresupuestal.Columns["rowmodificado"].Index] = "0";
                        registrogrid[GridviewCargaPresupuestal.Columns["IdRegistroPresupuestal"].Index] = "-1";

                        GridviewCargaPresupuestal.Rows.Add(registrogrid);
                        CompararPresupuestoYCalendarizacion(RegistroActual);
                        GridviewCargaPresupuestal.Rows[RegistroActual].HeaderCell.Value = (RegistroActual + 1).ToString();
                        RegistroActual++;
                        _ClassDetallesPresupuestales.RegistrosImportados = RegistroActual;
                        labelRegistrosAlmacenados.Text = RegistroActual.ToString("#,###");



                        if (ExisteCalendarioPorcentualCOG(CodigoCOG))
                        {
                            GuardarCalendarizacionCOG(false, CodigoCOG, _ClassParametros.PorcentajeEnero.ToString(), _ClassParametros.PorcentajeFebrero.ToString(), _ClassParametros.PorcentajeMarzo.ToString(), _ClassParametros.PorcentajeAbril.ToString(),
                            _ClassParametros.PorcentajeMayo.ToString(), _ClassParametros.PorcentajeJunio.ToString(), _ClassParametros.PorcentajeJulio.ToString(), _ClassParametros.PorcentajeAgosto.ToString(), _ClassParametros.PorcentajeSeptiembre.ToString(),
                            _ClassParametros.PorcentajeOctubre.ToString(), _ClassParametros.PorcentajeNoviembre.ToString(), _ClassParametros.PorcentajeDiciembre.ToString());
                        }
                        else
                        {
                            GuardarCalendarizacionCOG(true, CodigoCOG, _ClassParametros.PorcentajeEnero.ToString(), _ClassParametros.PorcentajeFebrero.ToString(), _ClassParametros.PorcentajeMarzo.ToString(), _ClassParametros.PorcentajeAbril.ToString(),
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
                CargarListaCOGPorcentual();
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

        private  string ExisteFuenteDeFinanciamiento(String IdOrigenORFIS)
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

        private void button4_Click(object sender, EventArgs e)
        {

            int formWidthBorder = this.Width - this.ClientSize.Width;
            int formBorderWidth = (int)((this.Width - this.ClientSize.Width)); // / 2);
            int formTitleBarHeight = (int)(this.Height - this.ClientSize.Height); // - 2 * formBorderWidth);
            int left = formWidthBorder +  this.Left +  button4.Left; //  + this.Left; 
            int top = formTitleBarHeight + this.Top + button4.Top +  button4.Height; // + this.Top;
            Point point = new Point(left, top);
            contextMenuStripInforme.Show(point);

        }

        private void concentradoPorUnidadResponsableURToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ClassTipoProceso.TipoProceso = (int)ClassTipoProceso.ProcesoAejecutar.EjecutarCocentradoPorUr;
            FormConcentradoHojaPresupuestocs _FormConcentradoHojaPresupuestocs = new FormConcentradoHojaPresupuestocs();
            _FormConcentradoHojaPresupuestocs.ShowDialog();

        }

        private void cEONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ClassTipoProceso.TipoProceso = (int)ClassTipoProceso.ProcesoAejecutar.EjecutarConcentradoPorOrigenIngreso;
            FormConcentradoHojaPresupuestocs _FormConcentradoHojaPresupuestocs = new FormConcentradoHojaPresupuestocs();
            _FormConcentradoHojaPresupuestocs.ShowDialog();

        }

        private void análisisComparativoIngresoPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ClassTipoProceso.TipoProceso = (int)ClassTipoProceso.ProcesoAejecutar.CuadroComparativoPresupuestoIngresoConAjustes;
            FormCuadroComparativoPresupuestoIngreso _FormCuadroComparativoPresupuestoIngreso = new FormCuadroComparativoPresupuestoIngreso();
            _FormCuadroComparativoPresupuestoIngreso.ShowDialog();

        }


        // Guardar calendarizacion COG
        private bool GuardarCalendarizacionCOG(bool NuevoRegistro, string ID_COG,  string ENERO, string FEBRERO, string MARZO, string ABRIL, string MAYO, string JUNIO, string JULIO, string AGOSTO,
            string SEPTIEMBRE, string OCTUBRE, string NOVIEMBRE, string DICIEMBRE)
        {
            this.Cursor = Cursors.WaitCursor;
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            string NombreProcedimiento = "IngresarRegistrosCalendarizacionCOG";

            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@ID_COG", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@ID_COG"].Value = ID_COG;
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
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al guardar los registros en el catálogo CALENDARIO_COG ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private bool ExisteCalendarioPorcentualCOG(string ID_COG)
        {
            bool ExisteCOG = false; 
            string MySql = "SELECT ID_COG FROM CALENDARIO_COG WHERE ID_COG = " + ID_COG;
            try
            {
                SqlCommand CommandVerificacion = new SqlCommand(MySql, ClassBaseDeDatos.MyConnectionDB);
                CommandVerificacion.CommandType = CommandType.Text;
                Object RegistroEncontrado = CommandVerificacion.ExecuteScalar();
                if (RegistroEncontrado != null)
                {
                    if (ID_COG == RegistroEncontrado.ToString().Trim())
                        ExisteCOG = true; 
                }
            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al consultar los registros en el catálogo CALENDARIO_COG ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NumErrores++;
            }
            return ExisteCOG;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _ClassTipoProceso.TipoProceso = (int)ClassTipoProceso.ProcesoAejecutar.EjecutarConcentradoPorCOG;
            FormConcentradoHojaPresupuestocs _FormConcentradoHojaPresupuestocs = new FormConcentradoHojaPresupuestocs();
            _FormConcentradoHojaPresupuestocs.ShowDialog();

        }

        //Llamar al procedimiento almacenado para recuperar la descripción de la UR. TABLA: CAT_UR
        public static string getDescripcionUR(string codigo)
        {
            string descripcion = "";
            DataTable tablaAux = new DataTable();
            string procedimientoAlmacenado = "ConsultarDescripcionUR";
            SqlConnection conexion = new SqlConnection(@"Data Source=SERVIDOR-PC\SIAPG;Initial Catalog=SIAPG;Integrated Security=True");
            SqlDataAdapter miSqlDataAdapter = new SqlDataAdapter(procedimientoAlmacenado, conexion);
            conexion.Open();

            miSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            miSqlDataAdapter.SelectCommand.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codigo;
            miSqlDataAdapter.Fill(tablaAux);
            if (tablaAux.Rows.Count > 0)
                descripcion = tablaAux.Rows[0]["UR"].ToString();
            else
                descripcion = tablaAux.Rows[0]["UR"].ToString();

            conexion.Close();
            return descripcion;

        }

        //Llamar al procedimiento almacenado para recuperar la descripción del COG. TABLA: CAT_COG
        public static string getDescripcionCOG(string codigo)
        {
            string descripcion = "";
            DataTable tablaAux = new DataTable();
            string procedimientoAlmacenado = "ConsultarDescripcionCOG";
            SqlConnection conexion = new SqlConnection(@"Data Source=SERVIDOR-PC\SIAPG;Initial Catalog=SIAPG;Integrated Security=True");
            SqlDataAdapter miSqlDataAdapter = new SqlDataAdapter(procedimientoAlmacenado, conexion);
            conexion.Open();

            miSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            miSqlDataAdapter.SelectCommand.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codigo;
            miSqlDataAdapter.Fill(tablaAux);
            if (tablaAux.Rows.Count > 0)
                descripcion = tablaAux.Rows[0]["COG"].ToString();
            else
                descripcion = tablaAux.Rows[0]["COG"].ToString();

            conexion.Close();
            return descripcion;

        }


        //Llamar al procedimiento almacenado para recuperar la descripción de la fuente de financiamiento. TABLA: CAT_ORIGEN_INGRESO
        public static string getDescripcionFuenteFunanciamiento(string codigo)
        {
            string descripcion = "";
            DataTable tablaAux = new DataTable();
            string procedimientoAlmacenado = "ConsultarDescripcionFuenteFinanciamiento";
            SqlConnection conexion = new SqlConnection(@"Data Source=SERVIDOR-PC\SIAPG;Initial Catalog=SIAPG;Integrated Security=True");
            SqlDataAdapter miSqlDataAdapter = new SqlDataAdapter(procedimientoAlmacenado, conexion);
            conexion.Open();

            miSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            miSqlDataAdapter.SelectCommand.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codigo;
            miSqlDataAdapter.Fill(tablaAux);
            if (tablaAux.Rows.Count > 0)
                descripcion = tablaAux.Rows[0]["ORIGEN_INGRESO"].ToString();
            else
                descripcion = tablaAux.Rows[0]["ORIGEN_INGRESO"].ToString();

            conexion.Close();
            return descripcion;
        }

        //Llamar al procedimiento almacenado para recuperar la descripción de la FFS. TABLA: CAT_FFS
        public static string getDescripcionFFS(string codigo)
        {
            string descripcion = "";
            DataTable tablaAux = new DataTable();
            string procedimientoAlmacenado = "ConsultarDescripcionFFS";
            SqlConnection conexion = new SqlConnection(@"Data Source=SERVIDOR-PC\SIAPG;Initial Catalog=SIAPG;Integrated Security=True");
            SqlDataAdapter miSqlDataAdapter = new SqlDataAdapter(procedimientoAlmacenado, conexion);
            conexion.Open();

            miSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            miSqlDataAdapter.SelectCommand.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codigo;
            miSqlDataAdapter.Fill(tablaAux);
            if (tablaAux.Rows.Count > 0)
                descripcion = tablaAux.Rows[0]["CLASIFICACION_FUNCIONAL"].ToString();
            else
                descripcion = tablaAux.Rows[0]["CLASIFICACION_FUNCIONAL"].ToString();

            conexion.Close();
            return descripcion;
        }

        //Llamar al procedimiento almacenado para recuperar la descripción de la clasificación programática. TABLA: CAT_PROGRAMATICO
        public static string getDescripcionClasiPrograma(string codigo)
         {
             string descripcion = "";
             DataTable tablaAux = new DataTable();
             string procedimientoAlmacenado = "ConsultarDescripcionProgramatico";
             SqlConnection conexion = new SqlConnection(@"Data Source=SERVIDOR-PC\SIAPG;Initial Catalog=SIAPG;Integrated Security=True");
             SqlDataAdapter miSqlDataAdapter = new SqlDataAdapter(procedimientoAlmacenado, conexion);
             conexion.Open();

             miSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
             miSqlDataAdapter.SelectCommand.Parameters.Add("@codigo", SqlDbType.VarChar).Value = Convert.ToInt32(codigo);
             miSqlDataAdapter.Fill(tablaAux);
             if (tablaAux.Rows.Count > 0)
                 descripcion = tablaAux.Rows[0]["PROGRAMA"].ToString();
             else
                 descripcion = tablaAux.Rows[0]["PROGRAMA"].ToString();

             conexion.Close();
             return descripcion;
         }
         
        //Llamar al procedimiento almacenado para recuperar la descripción del tipo de gasto. TABLA: CAT_TIPO_GASTO
        public static string getDescripcionTipoGasto(string codigo)
        {
            string descripcion = "";
            DataTable tablaAux = new DataTable();
            string procedimientoAlmacenado = "ConsultarDescripcionTipoGasto";
            SqlConnection conexion = new SqlConnection(@"Data Source=SERVIDOR-PC\SIAPG;Initial Catalog=SIAPG;Integrated Security=True");
            SqlDataAdapter miSqlDataAdapter = new SqlDataAdapter(procedimientoAlmacenado, conexion);
            conexion.Open();

            miSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            miSqlDataAdapter.SelectCommand.Parameters.Add("@codigo", SqlDbType.VarChar).Value = Int32.Parse(codigo);
            miSqlDataAdapter.Fill(tablaAux);
            if (tablaAux.Rows.Count > 0)
                descripcion = tablaAux.Rows[0]["TIPO_GASTO"].ToString();
            else
                descripcion = tablaAux.Rows[0]["TIPO_GASTO"].ToString();

            conexion.Close();
            return descripcion;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            new FormListadoPolizas().ShowDialog();
        }
    }
}
