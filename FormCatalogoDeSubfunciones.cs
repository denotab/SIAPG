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


namespace SIAPG
{
    public partial class FormCatalogoDeSubfunciones : Form
    {
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassBaseDeDatos _ClassBaseDeDatos = new ClassBaseDeDatos();
        Properties.Settings PropiedadesAPP = new Properties.Settings();

        public TreeNode MySelectedNode = new TreeNode();
        public TreeNode MySelectedNodeAnterior = new TreeNode();
        bool ModuloAgregarActivo = false;
        bool NuevoRegistro = false; 

        int NodoSelectedFinalidad = -1;
        int NodoSelectedFuncion = -1;
        int NodoSelectedSubfuncion = -1;


        public FormCatalogoDeSubfunciones()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormCatalogoDeSubfunciones_Load(object sender, EventArgs e)
        {
            PausaBackGround.RunWorkerAsync();
            ConfigurarPanelProgressBar();

            dataGridViewCatalogoFuncionalidad.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridViewCatalogoFuncionalidad.DefaultCellStyle.BackColor = Color.AliceBlue;
            dataGridViewCatalogoFuncionalidad.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewCatalogoFuncionalidad.RowHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dataGridViewCatalogoFuncionalidad.RowHeadersDefaultCellStyle.BackColor = Color.AliceBlue;
        }

        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {
            ActivarProgressBar();
            LlenarCatalogoDeFinalidad();
            CargarRegistros();
            DesactivarProgressBar();

        }

        private void ActivarProgressBar()
        {
            panelProgressBar.Visible = true;
            pictureBoxProgress.Enabled = true; 
        }

        private void DesactivarProgressBar()
        {
            panelProgressBar.Visible = false;
            pictureBoxProgress.Enabled = false;
        }
        private void ConfigurarPanelProgressBar()
        {
            panelProgressBar.Parent = this;
            panelProgressBar.Visible = false;
            panelProgressBar.BringToFront();
            panelProgressBar.Left = (this.Width - panelProgressBar.Width) / 2;
            panelProgressBar.Top = (this.Height - panelProgressBar.Height) - 60;
            pictureBoxProgress.Enabled = false;
        }

        private void CargarRegistros()
        {
            int TotalRegistros = 0;
            int RegistroActual = 0;
            string NombreProcedimiento = "ConsultarCatalogoDeSubfunciones";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoFuncion", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoFuncion"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoSubFuncion", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoSubFuncion"].Value = -1;
            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "ConsultaSubfunciones");
            DataTable datatable = dataset.Tables["ConsultaSubfunciones"];


            string CodigoFinalidad = "";
            string CodigoFuncion = "";
            string CodigoSubFuncion = "";

            

            if (datatable.Rows.Count >= 0)
            {
                TotalRegistros = datatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {


                    DataRow registro = datatable.Rows[RegistroActual];
                    if (registro["COD_FINALIDAD"].ToString().Trim() != CodigoFinalidad)
                    {
                        CodigoFinalidad = registro["COD_FINALIDAD"].ToString().Trim();
                        InsertarNodoPrimero(Convert.ToInt32(CodigoFinalidad), registro["FINALIDAD"].ToString().Trim());
                        CodigoFuncion = "";
                        CodigoSubFuncion = "";
                    }

                    if (registro["COD_FUNCION"].ToString().Trim() != CodigoFuncion)
                    {
                        CodigoFuncion = registro["COD_FUNCION"].ToString().Trim();
                        InsertarNodoSecundario(Convert.ToInt32(CodigoFinalidad), Convert.ToInt32(CodigoFuncion), registro["FUNCION"].ToString().Trim());
                        CodigoSubFuncion = "";
                    }

                    if (registro["COD_SUBFUNCION"].ToString().Trim() != CodigoSubFuncion)
                    {
                        CodigoSubFuncion = registro["COD_SUBFUNCION"].ToString().Trim();
                        InsertarNodoTercero(Convert.ToInt32(CodigoFinalidad), Convert.ToInt32(CodigoFuncion), Convert.ToInt32(CodigoSubFuncion), registro["SUBFUNCION"].ToString().Trim());
                    }


                    RegistroActual++;
                }

                treeViewConsulta.EndUpdate();
              //  treeViewConsulta.ExpandAll();
            }
        }

  
  
        private void InsertarNodoPrimero(int CodigoFinalidad, string Finalidad)
        {
            string IdNameNodoPrimario = "Finalidad" + CodigoFinalidad.ToString();
            string EtiquetaNodo = CodigoFinalidad.ToString() + ". " + Finalidad;
            int NumImage = 3;
            TreeNode NodeParent = treeViewConsulta.Nodes.Add(IdNameNodoPrimario, EtiquetaNodo, NumImage);
            NodeParent.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
            NodeParent.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
            NodeParent.Tag = CodigoFinalidad.ToString();                            
        }

        private void InsertarNodoSecundario(int CodigoFinalidad, int CodigoFuncion,  string Funcion)
        {
            string IdNameNodoPrimario = "Finalidad" + CodigoFinalidad.ToString();
            string IdNameNodoSecundario = "Funcion" + CodigoFinalidad.ToString() + CodigoFuncion.ToString();
            string EtiquetaNodo = CodigoFinalidad.ToString() + "." + CodigoFuncion.ToString() + ". " + Funcion;

            int NumImage = 10;
            TreeNode[] IdNodoPrimario = treeViewConsulta.Nodes.Find(IdNameNodoPrimario, true);
            
            if (IdNodoPrimario.Length != 0)
            {
                TreeNode NodoPrimario = IdNodoPrimario[0];
                TreeNode IdNodoSegundo = NodoPrimario.Nodes.Add(IdNameNodoSecundario, EtiquetaNodo, NumImage);

                IdNodoSegundo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                IdNodoSegundo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                IdNodoSegundo.Tag = CodigoFinalidad.ToString() + ";" + CodigoFuncion.ToString(); 

            }
        }

        private void InsertarNodoTercero(int CodigoFinalidad, int CodigoFuncion, int CodigoSubFuncion, string SubFuncion)
        {
            string IdNameNodoPrimario = "Finalidad" + CodigoFinalidad.ToString();
            string IdNameNodoSecundario = "Funcion" + CodigoFinalidad.ToString() + CodigoFuncion.ToString();
            string IdNameNodoTercero = "SubFuncion" + CodigoFinalidad.ToString() + CodigoFuncion.ToString() + CodigoSubFuncion.ToString();
            string EtiquetaNodo = CodigoFinalidad.ToString() + "." + CodigoFuncion.ToString() + "." + CodigoSubFuncion.ToString() + ". " + SubFuncion;

            int NumImage = 1;
            TreeNode[] IdNodoSecundario = treeViewConsulta.Nodes.Find(IdNameNodoSecundario, true);
            if (IdNodoSecundario.Length != 0)
            {
                TreeNode NodoSecundario = IdNodoSecundario[0];
                TreeNode IdNodoTercero = NodoSecundario.Nodes.Add(IdNameNodoTercero, EtiquetaNodo, NumImage);

                IdNodoTercero.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                IdNodoTercero.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                IdNodoTercero.Tag = CodigoFinalidad.ToString() + ";" + CodigoFuncion.ToString() + ";" + CodigoSubFuncion.ToString();
            }
        }

        private void treeViewConsulta_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void treeViewConsulta_Click(object sender, EventArgs e)
        {
           

        
    
        }

        private void LlenarCatalogoDeFinalidad()
        {
            string NombreProcedimiento = "ConsultarCatalogoDeFinalidad";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            comboBoxFinalidad.DataSource = null;
            NodoSelectedFuncion = -1;
            NodoSelectedSubfuncion = 1;

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = -1;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "ConsultaFinalidad");
                datatable = dataset.Tables["ConsultaFinalidad"];

                comboBoxFinalidad.Items.Clear();
                comboBoxFinalidad.DataSource = datatable;
                comboBoxFinalidad.ValueMember = datatable.Columns[0].ToString();
                comboBoxFinalidad.DisplayMember = datatable.Columns[1].ToString();

            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
            }
        }

        private void comboBoxFinalidad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Object SelectedValue = comboBoxFinalidad.SelectedValue;
            if (SelectedValue != null)
            {
                NodoSelectedFinalidad = Convert.ToInt32(SelectedValue.ToString());               
                LlenarComboFuncion();
                NodoSelectedFuncion = -1;
                NodoSelectedSubfuncion = -1; 
                comboBoxFuncion.Text = "";
                comboBoxSubFuncion.Text = "";
                comboBoxSubFuncion.Enabled = false; 
                comboBoxFuncion.DroppedDown = true;
                Llenargridfuncionalidad();
            }
            else
            {
                NodoSelectedFinalidad = -1;
                comboBoxFuncion.Enabled = false;
                comboBoxFuncion.Items.Clear();
            }            
        }

        private void LlenarComboFuncion()
        {
            string NombreProcedimiento = "ConsultarCatalogoDeFunciones";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            comboBoxFuncion.DataSource = null;
            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = NodoSelectedFinalidad;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFuncion", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFuncion"].Value = -1;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "Consultafunciones");
                datatable = dataset.Tables["Consultafunciones"];

                comboBoxFuncion.DataSource = datatable;
                comboBoxFuncion.ValueMember = datatable.Columns[1].ToString();
                comboBoxFuncion.DisplayMember = datatable.Columns[3].ToString();
                comboBoxFuncion.Enabled = true;

            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
            }
            sqldataadapter = null;
            dataset = null;
            datatable = null;

        }

        private void LlenarComboSubFuncion()
        {
            string NombreProcedimiento = "ConsultarCatalogoDeSubfunciones";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            comboBoxSubFuncion.DataSource = null;
            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = NodoSelectedFinalidad;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFuncion", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFuncion"].Value = NodoSelectedFuncion;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoSubFuncion", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoSubFuncion"].Value = -1;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "Consultasubfunciones");
                datatable = dataset.Tables["Consultasubfunciones"];

                comboBoxSubFuncion.DataSource = datatable;
                comboBoxSubFuncion.ValueMember = datatable.Columns[2].ToString();
                comboBoxSubFuncion.DisplayMember = datatable.Columns[5].ToString();
                comboBoxSubFuncion.Enabled = true;
            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
                comboBoxSubFuncion.Enabled = false;

            }
            sqldataadapter = null;
            dataset = null;
            datatable = null;

        }

        private void comboBoxFuncion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Object SelectedValue = comboBoxFuncion.SelectedValue;
            if (SelectedValue != null)
            {
                NodoSelectedFuncion = Convert.ToInt32(SelectedValue.ToString());


                LlenarComboSubFuncion();
                NodoSelectedSubfuncion = -1;
                comboBoxSubFuncion.Text = "";
                comboBoxSubFuncion.Enabled = true;
                comboBoxSubFuncion.DroppedDown = true;
                Llenargridfuncionalidad();

            }
            else
            {
                NodoSelectedFuncion = -1;
                comboBoxSubFuncion.Enabled = false;
                comboBoxSubFuncion.Items.Clear();
            }

        }

        private void comboBoxSubFuncion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Object SelectedValue = comboBoxFuncion.SelectedValue;
            if (SelectedValue != null)
            {
                NodoSelectedSubfuncion = Convert.ToInt32(SelectedValue.ToString());
                LlencamposSubfuncion();
                Llenargridfuncionalidad();

            }
            else
            {
                NodoSelectedSubfuncion = -1;
                groupBoxDatosSubfuncion.Enabled = false;
                textBoxCodigoSubfuncion.Text = "";
                textBoxConceptoSubfuncion.Text = "";
            }
        }

        private void LlencamposSubfuncion()
        {

            string NombreProcedimiento = "ConsultarCatalogoDeSubfunciones";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            textBoxCodigoSubfuncion.Text = "";
            textBoxConceptoSubfuncion.Text = "";

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = NodoSelectedFinalidad;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFuncion", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFuncion"].Value = NodoSelectedFuncion;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoSubFuncion", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoSubFuncion"].Value = NodoSelectedSubfuncion;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "Consultasubfunciones");
                datatable = dataset.Tables["Consultasubfunciones"];

                if (datatable.Rows.Count > 0)
                {
                    DataRow registro = datatable.Rows[0];
                    textBoxCodigoSubfuncion.Text = registro["COD_SUBFUNCION"].ToString();
                    textBoxConceptoSubfuncion.Text = registro["SUBFUNCION"].ToString();

                    buttonModificar.Enabled = true;
                    buttonAgregar.Enabled = true;
                    buttonEliminar.Enabled = true;  
                    buttonGuardar.Enabled = false;
                    buttonModificar.Focus();  
                }
            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
                groupBoxDatosSubfuncion.Enabled = false;
            }

            sqldataadapter = null;
            dataset = null;
            datatable = null; 

        }

        private void Llenargridfuncionalidad()
        {

            int TotalRegistros = 0;
            int RegistroActual = 0;
            string NombreProcedimiento = "ConsultarCatalogoDeSubfunciones";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = NodoSelectedFinalidad;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoFuncion", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoFuncion"].Value = NodoSelectedFuncion;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoSubFuncion", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoSubFuncion"].Value = NodoSelectedSubfuncion;
            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "ConsultaSubfunciones");
            DataTable datatable = dataset.Tables["ConsultaSubfunciones"];

            dataGridViewCatalogoFuncionalidad.Rows.Clear(); 

            if (datatable.Rows.Count >= 0)
            {
                TotalRegistros = datatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {

                    string[] registrogrid = new string[4]; 
                    DataRow registro = datatable.Rows[RegistroActual];
                    registrogrid[0] = registro["COD_FINALIDAD"].ToString().Trim();
                    registrogrid[1] = registro["COD_FUNCION"].ToString().Trim();
                    registrogrid[2] = registro["COD_SUBFUNCION"].ToString().Trim();
                    registrogrid[3] = registro["SUBFUNCION"].ToString().Trim();
                    dataGridViewCatalogoFuncionalidad.Rows.Add(registrogrid);

                    RegistroActual++;
                }

            }

        }

        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            string RegistroAEliminar = NodoSelectedFinalidad.ToString() + "." + NodoSelectedFuncion.ToString() + "." + NodoSelectedSubfuncion.ToString() + ". " +
                textBoxConceptoSubfuncion.Text.ToString(); 

            string Alerta = "Se eliminará el registro:  " + "\n\r\n\r" + RegistroAEliminar + "\n\r\n\r" + "Desea Continuar con la operación ?";
            DialogResult CerrarProceso = new DialogResult();
            CerrarProceso = MessageBox.Show(this, Alerta, "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (CerrarProceso == DialogResult.Yes)
            {
                if (EliminarRegistroSubfunciones())
                {
                    MessageBox.Show(this,"El registro se ha eliminado correctamente.", "Proceso corecto", MessageBoxButtons.OK,MessageBoxIcon.Information);

                    ActivarProgressBar();
                    CargarRegistros();
                    DesactivarProgressBar();

                    comboBoxFuncion.Enabled = false;
                    comboBoxSubFuncion.Enabled = false;
                    buttonAgregar.Enabled = false;
                    buttonEliminar.Enabled = false;
                    buttonModificar.Enabled = false;
                }
            }
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            buttonModificar.Enabled = false;
            buttonGuardar.Enabled = true;
            buttonEliminar.Enabled = false;
            buttonAgregar.Enabled = false;
            buttonCancelar.Enabled = true; 

            groupBoxDatosSubfuncion.Enabled = true;
            NuevoRegistro = false;
            textBoxCodigoSubfuncion.Enabled = true;
            textBoxConceptoSubfuncion.Enabled = true;
            textBoxConceptoSubfuncion.Focus();
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            ModuloAgregarActivo = true;
            NuevoRegistro = true;

            textBoxCodigoSubfuncion.Enabled  = true;
            textBoxConceptoSubfuncion.Enabled  = true;
            textBoxCodigoSubfuncion.Text = "";
            textBoxConceptoSubfuncion.Text = "";
            textBoxCodigoSubfuncion.Focus();

            buttonAgregar.Enabled = false;
            buttonCancelar.Enabled = true;
            buttonEliminar.Enabled = false;
            buttonModificar.Enabled = false;
            buttonGuardar.Enabled = true; 
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (NuevoRegistro)
            {
                if (AgregarRegistro())
                {

                    textBoxCodigoSubfuncion.Enabled = false;
                    textBoxConceptoSubfuncion.Enabled = false;
                    buttonAgregar.Enabled = true;
                    buttonEliminar.Enabled = true;
                    buttonCancelar.Enabled = false;
                    buttonGuardar.Enabled = false;
                    buttonModificar.Enabled = true;
                    MessageBox.Show(this, "El registro se ha guardado correctamente.", "Proceso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NuevoRegistro = false;
                    NodoSelectedSubfuncion = Convert.ToInt32(textBoxCodigoSubfuncion.Text.ToString());
                }
            }
            else
            {
                if (ModificarRegistro())
                {
                    textBoxCodigoSubfuncion.Enabled = false;
                    textBoxConceptoSubfuncion.Enabled = false;
                    buttonAgregar.Enabled = true;
                    buttonEliminar.Enabled = true;
                    buttonCancelar.Enabled = false;
                    buttonGuardar.Enabled = false;
                    buttonModificar.Enabled = true;
                    MessageBox.Show(this, "El registro se ha modificado correctamente.", "Proceso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool AgregarRegistro()
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            int IdCodigoSubFuncion = Convert.ToInt32(NodoSelectedFinalidad.ToString() + NodoSelectedFuncion.ToString() + textBoxCodigoSubfuncion.Text.ToString());
            string NombreProcedimiento = "IngresarRegistrosCatalogoDeSubfunciones";

            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@IdSubFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@IdSubFuncion"].Value = IdCodigoSubFuncion;

                CommandInsertCatalogo.Parameters.Add("@CodFinalidad", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFinalidad"].Value = NodoSelectedFinalidad;
                CommandInsertCatalogo.Parameters.Add("@CodFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFuncion"].Value = NodoSelectedFuncion;
                CommandInsertCatalogo.Parameters.Add("@CodSubFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodSubFuncion"].Value = textBoxCodigoSubfuncion.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@Concepto", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@Concepto"].Value = textBoxConceptoSubfuncion.Text.ToString();
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
            return ProcesoCorrecto;
        }
        private bool ModificarRegistro()
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            int IdCodigoSubFuncion = Convert.ToInt32(NodoSelectedFinalidad.ToString() + NodoSelectedFuncion.ToString() + textBoxCodigoSubfuncion.Text.ToString());
            string NombreProcedimiento = "ModificarCatalogoDeSubfunciones";

            try
            {
                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@IdSubFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@IdSubFuncion"].Value = IdCodigoSubFuncion;

                CommandInsertCatalogo.Parameters.Add("@CodigoFinalidad", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoFinalidad"].Value = NodoSelectedFinalidad;
                CommandInsertCatalogo.Parameters.Add("@CodigoFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoFuncion"].Value = NodoSelectedFuncion;
                CommandInsertCatalogo.Parameters.Add("@CodigoSubFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoSubFuncion"].Value = NodoSelectedSubfuncion;

                CommandInsertCatalogo.Parameters.Add("@NuevoCodigoSubFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@NuevoCodigoSubFuncion"].Value =  Convert.ToInt32(textBoxCodigoSubfuncion.Text.ToString());

                CommandInsertCatalogo.Parameters.Add("@Concepto", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@Concepto"].Value = textBoxConceptoSubfuncion.Text.ToString();
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
            return ProcesoCorrecto;
        }

        private bool EliminarRegistroSubfunciones()
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            string NombreProcedimiento = "EliminarCatalogoDeSubfunciones";

            try
            {
                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;

                CommandInsertCatalogo.Parameters.Add("@CodigoFinalidad", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoFinalidad"].Value = NodoSelectedFinalidad;
                CommandInsertCatalogo.Parameters.Add("@CodigoFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoFuncion"].Value = NodoSelectedFuncion;
                CommandInsertCatalogo.Parameters.Add("@CodigoSubFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoSubFuncion"].Value = NodoSelectedSubfuncion;
                CommandInsertCatalogo.ExecuteNonQuery();
            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }
            finally
            {
                CommandInsertCatalogo = null;
            }
            return ProcesoCorrecto;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {

            groupBoxDatosSubfuncion.Enabled = false;
            textBoxCodigoSubfuncion.Enabled = false;
            textBoxConceptoSubfuncion.Enabled = false;
            buttonAgregar.Enabled = true;
            buttonEliminar.Enabled = true;
            buttonGuardar.Enabled = false;
            buttonModificar.Enabled = true;
            buttonCancelar.Enabled = false;

            if (ModuloAgregarActivo)
            {
                if (NodoSelectedFinalidad != -1 && NodoSelectedFuncion != -1 && NodoSelectedSubfuncion != -1)
                    LlencamposSubfuncion();
                ModuloAgregarActivo = false;
            }

        }

        private void treeViewConsulta_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
 

        }

        private void InhabilitarCaptura()
        {
            textBoxCodigoSubfuncion.Enabled = false;
            textBoxConceptoSubfuncion.Enabled = false;
            buttonAgregar.Enabled = false;
            buttonCancelar.Enabled = false;
            buttonEliminar.Enabled = false;
            buttonModificar.Enabled = false;
            buttonGuardar.Enabled = false;

            textBoxCodigoSubfuncion.Text = "";
            textBoxConceptoSubfuncion.Text = ""; 
        }

        private void HabilitarCaptura()
        {
            buttonAgregar.Enabled = true;
            buttonEliminar.Enabled = true;
            buttonModificar.Enabled = true;
        }

        private void treeViewConsulta_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {

                if (treeViewConsulta.SelectedNode.Tag != null)
                {
                    textBox1.Text = ""; 
                    String[] campos = treeViewConsulta.SelectedNode.Tag.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int elem = 0; elem < campos.Length; elem++)
                    {
                        if (textBox1.Text == "")
                           textBox1.Text = campos[elem].ToString();
                        else
                            textBox1.Text +=  "; " + campos[elem].ToString();
                    }

                    if (campos.Length == 1)
                    {
                        NodoSelectedFinalidad = Convert.ToInt32(campos[0].ToString());
                        comboBoxFinalidad.SelectedValue = NodoSelectedFinalidad;
                        NodoSelectedFuncion = -1;
                        NodoSelectedSubfuncion = -1;
                        LlenarComboFuncion();
                        InhabilitarCaptura();
                        comboBoxFuncion.Text = "";
                        comboBoxSubFuncion.Text = "";
                        comboBoxSubFuncion.Enabled = false;

                    }
                    else if (campos.Length == 2)
                    {

                        if (Convert.ToInt32(campos[0].ToString()) != NodoSelectedFinalidad)
                        {
                            NodoSelectedFinalidad = Convert.ToInt32(campos[0].ToString());
                            comboBoxFinalidad.SelectedValue = NodoSelectedFinalidad;
                            LlenarComboFuncion();
                        }

                        NodoSelectedFuncion = Convert.ToInt32(campos[1].ToString());
                        comboBoxFuncion.SelectedValue = NodoSelectedFuncion;
                        NodoSelectedSubfuncion = -1;
                        LlenarComboSubFuncion();
                        comboBoxSubFuncion.Text = ""; 
                        InhabilitarCaptura();
                        comboBoxSubFuncion.Enabled = true;
                    }
                    else if (campos.Length == 3)
                    {
                        if (Convert.ToInt32(campos[0].ToString()) != NodoSelectedFinalidad)
                        {
                            NodoSelectedFinalidad = Convert.ToInt32(campos[0].ToString());
                            comboBoxFinalidad.SelectedValue = NodoSelectedFinalidad;
                            NodoSelectedFuncion = -1;
                            NodoSelectedSubfuncion = -1; 
                            LlenarComboFuncion();
                        }

                        if (Convert.ToInt32(campos[1].ToString()) != NodoSelectedFuncion)
                        {
                            LlenarComboFuncion();
                            NodoSelectedFuncion = Convert.ToInt32(campos[1].ToString());
                            comboBoxFuncion.SelectedValue = NodoSelectedFuncion;
                            LlenarComboSubFuncion();
                        }
                        NodoSelectedSubfuncion = Convert.ToInt32(campos[2].ToString());
                        comboBoxSubFuncion.SelectedValue = NodoSelectedSubfuncion;
                        LlencamposSubfuncion();
                        HabilitarCaptura();
                    }
                    Llenargridfuncionalidad(); 
                }
            }
            catch (Exception MyException)
            {
                // Continuamos con la obra.. 
                MessageBox.Show(MyException.Message);
            }
        }

        private void dataGridViewCatalogoFuncionalidad_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int rowfinalidad = Convert.ToInt32(dataGridViewCatalogoFuncionalidad.Rows[e.RowIndex].Cells[0].Value.ToString());
                int rowfuncion = Convert.ToInt32(dataGridViewCatalogoFuncionalidad.Rows[e.RowIndex].Cells[1].Value.ToString());
                int rowsubfuncion = Convert.ToInt32(dataGridViewCatalogoFuncionalidad.Rows[e.RowIndex].Cells[2].Value.ToString());


                if (rowfinalidad != NodoSelectedFinalidad)
                {
                    NodoSelectedFinalidad = rowfinalidad;
                    comboBoxFinalidad.SelectedValue = NodoSelectedFinalidad;
                    NodoSelectedFuncion = -1;
                    NodoSelectedSubfuncion = -1;
                    LlenarComboFuncion();
                }

                if (rowfuncion != NodoSelectedFuncion)
                {
                    LlenarComboFuncion();
                    NodoSelectedFuncion = rowfuncion;
                    comboBoxFuncion.SelectedValue = NodoSelectedFuncion;
                    NodoSelectedSubfuncion = -1;
                }

                if (rowsubfuncion != NodoSelectedSubfuncion)
                {
                    NodoSelectedSubfuncion = rowsubfuncion;
                    LlenarComboSubFuncion();
                }

                   
                comboBoxSubFuncion.SelectedValue = NodoSelectedSubfuncion;
                LlencamposSubfuncion();
                HabilitarCaptura();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message.ToString());
            }
        }                    
    }
}


