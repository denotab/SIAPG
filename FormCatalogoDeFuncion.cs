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
    public partial class FormCatalogoDeFuncion : Form
    {
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassBaseDeDatos _ClassBaseDeDatos = new ClassBaseDeDatos();
        Properties.Settings PropiedadesAPP = new Properties.Settings();
        int IdFinalidad = -1;
        int IdFuncion = -1;
        bool NuevoRegistro = true;
        SqlTransaction TransactionCatalogo;


        public FormCatalogoDeFuncion()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormCatalogoDeFuncion_Load(object sender, EventArgs e)
        {
            PausaBackGround.RunWorkerAsync();

        }

        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {
            LlenarCatalogoDeFinalidad();
            comboBoxFinalidad.Text = "";
            IdFinalidad = -1;
            ActivarControlesDeCaptura(0);

        }

        private void LlenarCatalogoDeFinalidad()
        {
            string NombreProcedimiento = "ConsultarCatalogoDeFinalidad";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            comboBoxFinalidad.DataSource = null;

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
                comboBoxFinalidad.DisplayMember = datatable.Columns[2].ToString();


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
                IdFinalidad = Convert.ToInt32(SelectedValue.ToString());
                if (LlenarComboFuncion())
                {
                    ActivarControlesDeCaptura(1);
                    comboBoxFuncion.Text = "";
                    comboBoxFuncion.Focus(); 
                }
            }
            else
            {
                IdFinalidad = -1;
            }
        }

  

        private bool LlenarComboFuncion()
        {
            bool ProcesoCorrecto = true;
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
                sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = IdFinalidad;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFuncion", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFuncion"].Value = -1;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "Consultafunciones");
                datatable = dataset.Tables["Consultafunciones"];

                comboBoxFuncion.DataSource = datatable;
                comboBoxFuncion.ValueMember = datatable.Columns[1].ToString();
                comboBoxFuncion.DisplayMember = datatable.Columns[4].ToString();
                comboBoxFuncion.Enabled = true;

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

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            NuevoRegistro = true;
            ActivarControlesDeCaptura(2);

        }

        private void ActivarControlesDeCaptura(int status)
        {
            /*
                0 =  indicando  en el catalogo de finalidad  
                1 =  Se ha elegido la opcion finalidad y se puede agregar una subfuncion. 
                2 = Se activan los controles de agregar y posibilidad de guardar y cancelar. 
                3 = Se activan los controles para agregar, modificar y eliminar. 
                4 = modificar registro; 
                5 = button cancelar. 
            */


            if (status == 0)
            {
                foreach (Control controlbutton in this.Controls)
                {
                    if (controlbutton is Button)
                        controlbutton.Enabled = false;
                }
                comboBoxFuncion.Enabled = false;
                groupBoxDatosCaptura.Enabled = false;
                comboBoxFuncion.Text = "";
                foreach (Control controltext in groupBoxDatosCaptura.Controls)
                {
                    if (controltext is TextBox)
                        controltext.Text = "";
                }
                NuevoRegistro = true; 

            }
            else if (status == 1)
            {
                foreach (Control controlbutton in this.Controls)
                {
                    if (controlbutton is Button)
                        controlbutton.Enabled = false;
                }

                comboBoxFuncion.Enabled = true;
                buttonAgregar.Enabled = true;
                groupBoxDatosCaptura.Enabled = false;
                foreach (Control controltext in groupBoxDatosCaptura.Controls)
                {
                    if (controltext is TextBox)
                        controltext.Text = "";
                }
            }


            else if (status == 2)  // se activa el proceso de captura de un nuevo registro.  (presiono el boton agregar)
            {
                buttonAgregar.Enabled = false;
                buttonCancelar.Enabled = true;
                buttonGuardar.Enabled = true;
                buttonModificar.Enabled = false;
                buttonEliminar.Enabled = false;

                groupBoxDatosCaptura.Enabled = true;
                textBoxCodigoFuncion.Focus();
                textBoxCodigoFuncion.Text = "";
                textBoxFuncion.Text = "";
            }

            else if (status == 3)  // se activa el proceso de modificar, agregar y eliminar. seleccino una subfuncion. 
            {
                buttonAgregar.Enabled = true;
                buttonEliminar.Enabled = true;
                buttonModificar.Enabled = true;
                groupBoxDatosCaptura.Enabled = false;
                buttonCancelar.Enabled = false;
                buttonGuardar.Enabled = false; 
            }
            else if (status == 4)  // bugtton modificar. 
            {
                buttonAgregar.Enabled = false;
                buttonEliminar.Enabled = false;
                buttonModificar.Enabled = false;
                buttonCancelar.Enabled = true;
                buttonGuardar.Enabled = true;
                groupBoxDatosCaptura.Enabled = true;
                textBoxCodigoFuncion.Focus(); 
            }
            else if (status == 5)  // bugtton cancelar. 
            {
                buttonAgregar.Enabled = true;               
                buttonCancelar.Enabled = false;
                buttonGuardar.Enabled = false;
                groupBoxDatosCaptura.Enabled = false;
                if (NuevoRegistro)
                {
                    buttonEliminar.Enabled = false;
                    buttonModificar.Enabled = false;
                }
                else
                {
                    buttonEliminar.Enabled = true;
                    buttonModificar.Enabled = true;
                }
            }


        }

        private void comboBoxFuncion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Object SelectedValue = comboBoxFuncion.SelectedValue;
            if (SelectedValue != null)
            {
                IdFuncion = Convert.ToInt32(SelectedValue.ToString());
                ActivarControlesDeCaptura(3);
                LlencamposFuncion();
                NuevoRegistro = false;
                buttonModificar.Focus(); 
            }
            else
            {
                IdFuncion = -1;
            }
        }

        private void LlencamposFuncion()
        {
            string NombreProcedimiento = "ConsultarCatalogoDeFunciones";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            textBoxCodigoFuncion.Text = "";
            textBoxFuncion.Text = "";

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = IdFinalidad;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFuncion", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFuncion"].Value = IdFuncion;

                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "Consultafunciones");
                datatable = dataset.Tables["Consultafunciones"];

                if (datatable.Rows.Count > 0)
                {
                    DataRow registro = datatable.Rows[0];
                    textBoxCodigoFuncion.Text = registro["COD_FUNCION"].ToString();
                    textBoxFuncion.Text = registro["FUNCION"].ToString();
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

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            string RegistroAEliminar = IdFinalidad.ToString() + "." + IdFuncion.ToString() + "." + textBoxFuncion.Text.ToString();

            string Alerta = "Se eliminará el registro:  " + "\n\r" + RegistroAEliminar + "; y los registros relacionados en la tabla  [SUBFUNCION]" + "\n\r\n\r" + "Desea Continuar con la operación ?";
            DialogResult EliminarRegistro = new DialogResult();
            EliminarRegistro = MessageBox.Show(this, Alerta, "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (EliminarRegistro == DialogResult.Yes)
            {
                if (EliminarRegistrosFuncionalidad())
                {
                    MessageBox.Show(this, "El registro se ha eliminado correctamente.", "Proceso corecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NuevoRegistro = true;
                    ActivarControlesDeCaptura(0);
                    comboBoxFinalidad.Focus(); 
                }
            }
        }

        private bool EliminarRegistrosFuncionalidad()
        {
            bool ProcesoCorrecto = true;
            string MySql = "DELETE CAT_FUNCION WHERE COD_FINALIDAD = " + IdFinalidad.ToString() + " AND COD_FUNCION = " + IdFuncion.ToString(); 
            TransactionCatalogo = ClassBaseDeDatos.MyConnectionDB.BeginTransaction();

            SqlCommand CommandEliminarCatalogo;
            try
            {

                CommandEliminarCatalogo = new SqlCommand(MySql, ClassBaseDeDatos.MyConnectionDB);
                CommandEliminarCatalogo.Transaction = TransactionCatalogo;
                CommandEliminarCatalogo.CommandType = CommandType.Text;
                CommandEliminarCatalogo.ExecuteNonQuery();

                MySql = "DELETE CAT_SUBFUNCION WHERE COD_FINALIDAD = " + IdFinalidad.ToString() + " AND COD_FUNCION = " + IdFuncion.ToString();
                CommandEliminarCatalogo.CommandText = MySql;
                CommandEliminarCatalogo.ExecuteNonQuery();
                TransactionCatalogo.Commit();
            }
            catch (SqlException sqlexception)
            {
                TransactionCatalogo.Rollback();
                MessageBox.Show(this, "Ha ocurrido un error al borrar el registro  y sus relaciones en los catálogos." + "\n\r\n\r" + sqlexception.Message.ToString(), "error al ejecutar el proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }
            finally
            {
                CommandEliminarCatalogo = null;
            }
            return ProcesoCorrecto;
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            NuevoRegistro = false; 
            ActivarControlesDeCaptura(4);
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            ActivarControlesDeCaptura(5);
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            bool ProcesoCorrecto = false;
            if (NuevoRegistro)
            {
                if (ProcesoCorrecto = AgregarRegistro())
                {
                    MessageBox.Show(this, "El registro se ha guardado correctamente.", "Proceso terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (ProcesoCorrecto = ModificarRegistro())
                {
                    MessageBox.Show(this, "El registro se ha modificado correctamente.", "Proceso terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            ActivarControlesDeCaptura(0);
            comboBoxFinalidad.Focus();
        }


        private bool AgregarRegistro()
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            int IdCodigoFuncion = Convert.ToInt32(IdFinalidad.ToString() + textBoxCodigoFuncion.Text.ToString());
            string NombreProcedimiento = "IngresarRegistrosCatalogoDefunciones";

            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@IdFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@IdFuncion"].Value = IdCodigoFuncion;

                CommandInsertCatalogo.Parameters.Add("@CodFinalidad", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFinalidad"].Value = IdFinalidad;
                CommandInsertCatalogo.Parameters.Add("@CodFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFuncion"].Value = textBoxCodigoFuncion.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@Concepto", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@Concepto"].Value = textBoxFuncion.Text.ToString();
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
            SqlCommand CommandModificarCatalogo;
            string NombreProcedimiento = "ModificarCatalogoDeFuncion";

            try
            {
                CommandModificarCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandModificarCatalogo.CommandType = CommandType.StoredProcedure;

                CommandModificarCatalogo.Parameters.Add("@CodigoFinalidad", SqlDbType.Int, 4);
                CommandModificarCatalogo.Parameters["@CodigoFinalidad"].Value = IdFinalidad;

                CommandModificarCatalogo.Parameters.Add("@CodigoFuncion", SqlDbType.Int, 4);
                CommandModificarCatalogo.Parameters["@CodigoFuncion"].Value = IdFuncion;

                CommandModificarCatalogo.Parameters.Add("@NuevoCodigoCodigoFuncion", SqlDbType.Int, 4);
                CommandModificarCatalogo.Parameters["@NuevoCodigoCodigoFuncion"].Value = textBoxCodigoFuncion.Text.ToString();

                CommandModificarCatalogo.Parameters.Add("@Funcion", SqlDbType.VarChar);
                CommandModificarCatalogo.Parameters["@Funcion"].Value = textBoxFuncion.Text.ToString();
                CommandModificarCatalogo.ExecuteNonQuery();
            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al modificar los registros en el catálogo ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }
            finally
            {
                CommandModificarCatalogo = null;
            }
            return ProcesoCorrecto;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
