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
    public partial class FormCapturaPorcentajeCOG : Form
    {
        private FormCargaPresupuestaria FormCargaPresupuestariaCalendarizacion;
        bool CapturePorcentaje = false; 
        ClassParametros _ClassParametros = new ClassParametros();

        Properties.Settings PropiedadesAPP = new Properties.Settings();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassOpcionesSeleccionadas OpcionesSeleccionadas = new ClassOpcionesSeleccionadas();

        private bool NuevoRegistro = false;

        string PorcentajeEnero = "0";
        string PorcentajeFebrero = "0";
        string PorcentajeMarzo = "0";
        string PorcentajeAbril = "0";
        string PorcentajeMayo = "0";
        string PorcentajeJunio = "0";
        string PorcentajeJulio = "0";
        string PorcentajeAgosto = "0";
        string PorcentajeSeptiembre = "0";
        string PorcentajeOctubre = "0";
        string PorcentajeNoviembre = "0";
        string PorcentajeDiciembre = "0";


        public FormCapturaPorcentajeCOG()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        public FormCapturaPorcentajeCOG(FormCargaPresupuestaria _FormCargaPresupuestariaCalendarizacion)
        {
            FormCargaPresupuestariaCalendarizacion = _FormCargaPresupuestariaCalendarizacion;
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormCapturaPorcentajeCOG_Load(object sender, EventArgs e)
        {
            PausaBackGround.RunWorkerAsync();
            configurarformulario();
        }


        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(1500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {
            this.Cursor = Cursors.WaitCursor;

            this.labelStatus.Parent = this;
            this.labelStatus.BringToFront();

            if (!_ClassParametros.IngresoAlternativo)
            {
                LlenarGridUR();
            }
            configurarformulario();

            AsignarValoresCOG();
            AsignarCalendarizacioPorCOG();
            textBoxEnero.Focus(); 
            this.Cursor = Cursors.Default;
        }
        private void LlenarGridUR()
        {
            int TotalRegistros = 0;
            int RegistroActual = 0;
            string NombreProcedimiento = "ConsultarCatalogoCOG";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@COGNivel1", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@COGNivel1"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@COGNivel2", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@COGNivel2"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@COGNivel3", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@COGNivel3"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@COG_COMPLEMENTO", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@COG_COMPLEMENTO"].Value = -1;

            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "CatalogoCOG");
            DataTable datatable = dataset.Tables["CatalogoCOG"];


            string CodigoNivel1 = "";
            string CodigoNivel2 = "";
            string CodigoNivel3 = "";
            string CodigoNivel4 = "";
            string COG = "";
            string IdCOG = "";

            if (datatable.Rows.Count >= 0)
            {
                TotalRegistros = datatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {


                    DataRow registro = datatable.Rows[RegistroActual];
                    CodigoNivel1 = registro["COG_NIVEL1"].ToString().Trim();
                    CodigoNivel2 = registro["COG_NIVEL2"].ToString().Trim();
                    CodigoNivel3 = registro["COG_NIVEL3"].ToString().Trim();
                    CodigoNivel4 = registro["COG_COMPLEMENTO"].ToString().Trim();
                    COG = registro["CUENTA"].ToString().Trim();
                    IdCOG = registro["ID_COG"].ToString().Trim();
                    InsertarNodos(IdCOG, CodigoNivel1, CodigoNivel2, CodigoNivel3, CodigoNivel4, COG);
                    RegistroActual++;
                }

                treeViewCatalogo.EndUpdate();
                //  treeViewConsulta.ExpandAll();
            }
        }
        private void InsertarNodos(string Codigo, string nivel1, string nivel2, string nivel3, string nivel4, string COG)
        {
            string NombreNodo = "";
            string NombreNodoPadre = "";
            byte NumImagen = 0;
            string EtiquetaNodo = "";

            if (nivel4 == "") nivel4 = "0";
            if (nivel3 == "") nivel3 = "0";
            if (nivel2 == "") nivel2 = "0";
            if (nivel1 == "") nivel1 = "0";


            EtiquetaNodo = COG;



            if (nivel4 != "0")
            {
                NombreNodoPadre = "nodo" + nivel1 + nivel2 + nivel3;
                TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2 + nivel3 + nivel4;
                    NumImagen = 4;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = Codigo;
                }
            }

            else if (nivel3 != "0")
            {
                NombreNodoPadre = "nodo" + nivel1 + nivel2;
                TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2 + nivel3;
                    NumImagen = 3;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = Codigo;
                }
            }

            else if (nivel2 != "0")
            {
                NombreNodoPadre = "nodo" + nivel1;
                TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2;
                    NumImagen = 2;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = Codigo;
                    NuevoNodo.Expand();
                }
            }
            else if (nivel1 != "0")
            {
                NombreNodo = "nodo" + nivel1.ToString();
                NumImagen = 1;
                TreeNode NuevoNodo = treeViewCatalogo.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                NuevoNodo.Tag = Codigo;
                NuevoNodo.Expand();

            }
        }



        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (CapturePorcentaje)
                {
                    CapturePorcentaje = false;
                }
                GuardarCalendarizacionCapturada();
                AsignarCalendarizacionEquitativa();
            }
            else
                CancelarCalendarizacion();

        }

        private void AsignarCalendarizacionEquitativa()
        {
            float PorcentajeEquitativo = 100f / 12f;
            float PorcentajeTrunck4 = (float)Math.Round(PorcentajeEquitativo, 4);
            float[] PorcentajeMensual = new float[12];
            float SumaRealPorcentaje = 0;

            for (int Elem = 0; Elem < 12; Elem++)
            {
                PorcentajeMensual[Elem] = PorcentajeTrunck4;
                SumaRealPorcentaje += PorcentajeTrunck4;
            }

            double Diferencia = 100 - SumaRealPorcentaje;

            textBoxEnero.Text = PorcentajeMensual[0].ToString();
            textBoxFebrero.Text = PorcentajeMensual[1].ToString();
            textBoxMarzo.Text = PorcentajeMensual[2].ToString();
            textBoxAbril.Text = PorcentajeMensual[3].ToString();
            textBoxMayo.Text = PorcentajeMensual[4].ToString();
            textBoxJunio.Text = PorcentajeMensual[5].ToString();
            textBoxJulio.Text = PorcentajeMensual[6].ToString();
            textBoxAgosto.Text = PorcentajeMensual[7].ToString();
            textBoxSeptiembre.Text = PorcentajeMensual[8].ToString();
            textBoxOctubre.Text = PorcentajeMensual[9].ToString();
            textBoxNoviembre.Text = PorcentajeMensual[10].ToString();
            textBoxDiciembre.Text = PorcentajeMensual[11].ToString();

            if (Diferencia != 0)
            {
                PorcentajeMensual[11] = (float)Math.Round(PorcentajeMensual[11] + Diferencia, 4);
                textBoxDiciembre.Text = PorcentajeMensual[11].ToString();
            }


            SumarTotalPorcentaje(); 

        }

        private void CancelarCalendarizacion()
        {
            textBoxEnero.Text = PorcentajeEnero;
            textBoxFebrero.Text = PorcentajeFebrero;
            textBoxMarzo.Text = PorcentajeMarzo;
            textBoxAbril.Text = PorcentajeAbril;
            textBoxMayo.Text = PorcentajeMayo;
            textBoxJunio.Text = PorcentajeJunio;
            textBoxJulio.Text = PorcentajeJulio;
            textBoxAgosto.Text = PorcentajeAgosto;
            textBoxSeptiembre.Text = PorcentajeSeptiembre;
            textBoxOctubre.Text = PorcentajeOctubre;
            textBoxNoviembre.Text = PorcentajeNoviembre;
            textBoxDiciembre.Text = PorcentajeDiciembre;
            SumarTotalPorcentaje(); 
        }

        private void GuardarCalendarizacionCapturada()
        {
            PorcentajeEnero = textBoxEnero.Text.ToString();
            PorcentajeFebrero = textBoxFebrero.Text.ToString();
            PorcentajeMarzo = textBoxMarzo.Text.ToString();
            PorcentajeAbril = textBoxAbril.Text.ToString();
            PorcentajeMayo = textBoxMayo.Text.ToString();
            PorcentajeJunio = textBoxJunio.Text.ToString();
            PorcentajeJulio = textBoxJulio.Text.ToString();
            PorcentajeAgosto = textBoxAgosto.Text.ToString();
            PorcentajeSeptiembre = textBoxSeptiembre.Text.ToString();
            PorcentajeOctubre = textBoxOctubre.Text.ToString();
            PorcentajeNoviembre = textBoxNoviembre.Text.ToString();
            PorcentajeDiciembre = textBoxDiciembre.Text.ToString(); 

        }
        private void treeViewCatalogo_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void AsignarCalendarizacioPorCOG()
        {
            NuevoRegistro = true;
            string NombreProcedimiento = "ConsultarCatalogoCalendarioCOG";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@id_cog", SqlDbType.VarChar,20);
                sqldataadapter.SelectCommand.Parameters["@id_cog"].Value = _ClassParametros.IDCOGCELDA;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "ConsultaporcentajeCOG");
                datatable = dataset.Tables["ConsultaporcentajeCOG"];

                if (datatable.Rows.Count > 0)
                {
                    DataRow registro = datatable.Rows[0];
                    NuevoRegistro = false;

                    PorcentajeEnero = registro["ENERO"].ToString();
                    PorcentajeFebrero = registro["FEBRERO"].ToString();
                    PorcentajeMarzo = registro["MARZO"].ToString();
                    PorcentajeAbril = registro["ABRIL"].ToString();
                    PorcentajeMayo = registro["MAYO"].ToString();
                    PorcentajeJunio = registro["JUNIO"].ToString();
                    PorcentajeJulio = registro["JULIO"].ToString();
                    PorcentajeAgosto = registro["AGOSTO"].ToString();
                    PorcentajeSeptiembre = registro["SEPTIEMBRE"].ToString();
                    PorcentajeOctubre = registro["OCTUBRE"].ToString();
                    PorcentajeNoviembre = registro["NOVIEMBRE"].ToString();
                    PorcentajeNoviembre = registro["DICIEMBRE"].ToString();
                    labelStatus.Text = "Asignado";
                }
                else
                {
                    labelStatus.Text = "Sin asignar";
                    InicializarPorcentaje();
                    NuevoRegistro = true;
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
        private void InicializarPorcentaje()
        {
            PorcentajeEnero = "0";
            PorcentajeFebrero = "0";
            PorcentajeMarzo = "0";
            PorcentajeAbril = "0";
            PorcentajeMayo = "0";
            PorcentajeJunio = "0";
            PorcentajeJulio = "0";
            PorcentajeAgosto = "0";
            PorcentajeSeptiembre = "0";
            PorcentajeOctubre = "0";
            PorcentajeNoviembre = "0";
            PorcentajeDiciembre = "0";
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            buttonGuardar.Enabled = false; 
            if (textBoxTotal.Text != "100")
            {
                MessageBox.Show(this, "La suma de la calendarización mensual presupuetal debe ser de 100%.", "Dato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                configurartextos(); 
                if (GuardarRegistro())
                {
                    if (_ClassParametros.IngresoAlternativo)   // este parametro es para cuando se llama a este for
                    {
                        ActualizarParametros();
                        FormCargaPresupuestariaCalendarizacion.CalendarizacioCOGProcesada();
                        this.Hide();
                    }
                    else
                        MessageBox.Show(this, "El registro se ha guardado correctamente.", "Proceso finalizado", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            buttonGuardar.Enabled = true;
        }

        private void configurartextos()
        {
            foreach (Control textporcentaje in panelPorcentaje.Controls)
            {
                if (textporcentaje is TextBox)
                {
                    if (textporcentaje.Name != "textBoxTotal")
                    {
                        if (textporcentaje.Text.Trim() == "")
                        {
                            textporcentaje.Text = "0";
                        }
                    }
                }
            }


        }

        private void treeViewCatalogo_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)   // si es un nódo de último nivel unicamente. 
            {
                _ClassParametros.IDCOGCELDA = e.Node.Tag.ToString();
                _ClassParametros.COGCELDA = e.Node.Text;
                AsignarValoresCOG();
                AsignarCalendarizacioPorCOG();
            }
        }

        private void ConsultarNodoCOG()
        {

        }

        private void SumarTotalPorcentaje()
        {
            double TotalPorcentaje = 0;
            bool HayValoresNoNumericos = false;
            foreach (Control textporcentaje in panelPorcentaje.Controls)
            {
                if (textporcentaje is TextBox)
                {
                    if (textporcentaje.Name != "textBoxTotal")
                    {
                        if (textporcentaje.Text.Trim() != "")
                        {
                            if (_ClassParametros.IsNumeric(textporcentaje.Text.ToString()))
                            {
                                TotalPorcentaje += Convert.ToDouble(textporcentaje.Text.ToString());
                            }
                            else
                                HayValoresNoNumericos = true;
                        }
                    }
                }
            }

            textBoxTotal.Text = TotalPorcentaje.ToString();

            if (textBoxTotal.Text == "100")
                pictureBoxCheck.Image = imageListIcons.Images[7];
            else
                pictureBoxCheck.Image = imageListIcons.Images[6];

            if (HayValoresNoNumericos)
            {
                MessageBox.Show(this, "Algunos valores indicados enl la calendarización porcentual podrían no ser númericos.", "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTotal.Text = ""; 
            }
        }

        private void textBoxEnero_Validating(object sender, CancelEventArgs e)
        {

        }


        private void TextBoxLeave(object sender, EventArgs e)
        {
            TextBox textvalue = sender as TextBox;
            if (textvalue.Text.Trim() != "")
            {
                if (!_ClassParametros.IsNumeric(textvalue.Text.Trim()))
                {
                    MessageBox.Show(this, "Sólo se aceptan valores numéricos", "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textvalue.Focus();
                }
                else
                {
                    CapturePorcentaje = true;
                    SumarTotalPorcentaje();
                }
            }
        }

        private void textBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Tab)
            {
                TextBox textvalue = sender as TextBox;
                SendKeys.Send("{TAB}");
            }
        }

        private bool GuardarRegistro()
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
                CommandInsertCatalogo.Parameters["@ID_COG"].Value = _ClassParametros.IDCOGCELDA;
                CommandInsertCatalogo.Parameters.Add("@ENERO", SqlDbType.Float, 4);
                CommandInsertCatalogo.Parameters["@ENERO"].Value = textBoxEnero.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@FEBRERO", SqlDbType.Float, 4);
                CommandInsertCatalogo.Parameters["@FEBRERO"].Value = textBoxFebrero.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@MARZO", SqlDbType.Float, 4);
                CommandInsertCatalogo.Parameters["@MARZO"].Value = textBoxMarzo.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@ABRIL", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@ABRIL"].Value = textBoxAbril.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@MAYO", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@MAYO"].Value = textBoxMayo.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@JUNIO", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@JUNIO"].Value = textBoxJunio.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@JULIO", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@JULIO"].Value = textBoxJulio.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@AGOSTO", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@AGOSTO"].Value = textBoxAgosto.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@SEPTIEMBRE", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@SEPTIEMBRE"].Value = textBoxSeptiembre.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@OCTUBRE", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@OCTUBRE"].Value = textBoxOctubre.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@NOVIEMBRE", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@NOVIEMBRE"].Value = textBoxNoviembre.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@DICIEMBRE", SqlDbType.Float);
                CommandInsertCatalogo.Parameters["@DICIEMBRE"].Value = textBoxDiciembre.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@NUEVO_REGISTRO", SqlDbType.Bit);
                CommandInsertCatalogo.Parameters["@NUEVO_REGISTRO"].Value = NuevoRegistro;

                CommandInsertCatalogo.ExecuteNonQuery();


            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al guardar los registros en el catálogo ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }
            finally
            {
                CommandInsertCatalogo = null;
            }
            this.Cursor = Cursors.Default;
            return ProcesoCorrecto;
        }

        public void AsignarValoresCOG()
        {
            textBoxCodigo.Text = _ClassParametros.IDCOGCELDA;
            textBoxDescripcion.Text = _ClassParametros.COGCELDA;
        }
        private void ActualizarParametros()
        {
            _ClassParametros.PorcentajeEnero = Convert.ToDouble(textBoxEnero.Text.ToString());
            _ClassParametros.PorcentajeFebrero = Convert.ToDouble(textBoxFebrero.Text.ToString());
            _ClassParametros.PorcentajeMarzo = Convert.ToDouble(textBoxMarzo.Text.ToString());
            _ClassParametros.PorcentajeAbril = Convert.ToDouble(textBoxAbril.Text.ToString());
            _ClassParametros.PorcentajeMayo = Convert.ToDouble(textBoxMayo.Text.ToString());
            _ClassParametros.PorcentajeJunio = Convert.ToDouble(textBoxJunio.Text.ToString());
            _ClassParametros.PorcentajeJulio = Convert.ToDouble(textBoxJulio.Text.ToString());
            _ClassParametros.PorcentajeAgosto = Convert.ToDouble(textBoxAgosto.Text.ToString());
            _ClassParametros.PorcentajeSeptiembre = Convert.ToDouble(textBoxSeptiembre.Text.ToString());
            _ClassParametros.PorcentajeOctubre = Convert.ToDouble(textBoxOctubre.Text.ToString());
            _ClassParametros.PorcentajeNoviembre = Convert.ToDouble(textBoxNoviembre.Text.ToString());
            _ClassParametros.PorcentajeDiciembre = Convert.ToDouble(textBoxDiciembre.Text.ToString());


        }

        private void FormCapturaPorcentajeCOG_ResizeEnd(object sender, EventArgs e)
        {
            labelStatus.Top = panelPorcentaje.Top - (labelStatus.Height / 2);
            labelStatus.Left = panelPorcentaje.Left + (panelPorcentaje.Width / 2) - (labelStatus.Width / 2);
        }

        private void configurarformulario()
        {
            if (_ClassParametros.IngresoAlternativo)
            {
                treeViewCatalogo.Visible = false;
                int movetop = treeViewCatalogo.Height * -1;
                this.Height += movetop;
                textBoxCodigo.Enabled = false;
                textBoxDescripcion.Enabled = false;

            }
            else
            {
                textBoxCodigo.Enabled = true;
                textBoxDescripcion.Enabled = true; 
            }

            labelStatus.Top = panelPorcentaje.Top - (labelStatus.Height / 2);
            labelStatus.Left = panelPorcentaje.Left + (panelPorcentaje.Width / 2) - (labelStatus.Width / 2);

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FormCapturaPorcentajeCOG_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (_ClassParametros.IngresoAlternativo)
                {
                    if (!_ClassParametros.CerrandoCargaPresupuestaria)
                    {
                        e.Cancel = true; this.Hide();
                    }
                }
            }
        }

        private void labelStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
