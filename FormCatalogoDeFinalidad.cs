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
    public partial class FormCatalogoDeFinalidad : Form
    {
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassBaseDeDatos _ClassBaseDeDatos = new ClassBaseDeDatos();
        Properties.Settings PropiedadesAPP = new Properties.Settings();
        int IdFinalidad = -1;
        bool NuevoRegistro = false;
        SqlTransaction TransactionCatalogo;


        public FormCatalogoDeFinalidad()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormCatalogoDeFinalidad_Load(object sender, EventArgs e)
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
                comboBoxFinalidad.DisplayMember =  datatable.Columns[2].ToString();


            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
            }
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            NuevoRegistro = true;
            buttonAgregar.Enabled = false;
            buttonModificar.Enabled = false;
            buttonEliminar.Enabled = false;
            buttonGuardar.Enabled = true;
            buttonCancelar.Enabled = true;
            groupBoxDatosCaptura.Enabled = true;
            textBoxCodigoFinalidad.Enabled = true;
            textBoxFinalidad.Enabled = true; 
            textBoxCodigoFinalidad.Text = "";
            textBoxFinalidad.Text = "";
            textBoxCodigoFinalidad.Focus();
            comboBoxFinalidad.Enabled = false;
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            NuevoRegistro = false;
            buttonAgregar.Enabled = false;
            buttonModificar.Enabled = false;
            buttonEliminar.Enabled = false;
            buttonGuardar.Enabled = true;
            buttonCancelar.Enabled = true; 
            groupBoxDatosCaptura.Enabled = true;
            textBoxCodigoFinalidad.Enabled = true;
            textBoxFinalidad.Enabled = true;
            textBoxCodigoFinalidad.Focus(); 
            comboBoxFinalidad.Enabled = false;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            NuevoRegistro = false;
            buttonAgregar.Enabled = true;
            buttonModificar.Enabled = false;
            buttonEliminar.Enabled = false;
            buttonGuardar.Enabled = false;
            buttonCancelar.Enabled = false;
            groupBoxDatosCaptura.Enabled = false;
            textBoxCodigoFinalidad.Enabled = false;
            textBoxFinalidad.Enabled = false;
            comboBoxFinalidad.Enabled = true; 
        }

        private void comboBoxFinalidad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Object SelectedValue = comboBoxFinalidad.SelectedValue;
            if (SelectedValue != null)
            {
                IdFinalidad = Convert.ToInt32(SelectedValue.ToString());
                LlencamposFinalidad();
                groupBoxDatosCaptura.Enabled = false;
                textBoxCodigoFinalidad.Text = IdFinalidad.ToString();
                buttonAgregar.Enabled = true;
                buttonCancelar.Enabled = false;
                buttonModificar.Enabled = true;
                buttonEliminar.Enabled = true;
                buttonGuardar.Enabled = false;
                buttonModificar.Focus(); 
                NuevoRegistro = false; 
            }
            else 
            {
                IdFinalidad = -1;
                groupBoxDatosCaptura.Enabled = false;
                textBoxCodigoFinalidad.Text = "";
                textBoxFinalidad.Text = "";
                buttonEliminar.Enabled = false; 
                buttonGuardar.Enabled = false;
                buttonModificar.Enabled = false;
                buttonAgregar.Enabled = true; 

            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            string RegistroAEliminar =  IdFinalidad.ToString() + ". "  +  textBoxFinalidad.Text.ToString();

            string Alerta = "Se eliminará el registro:  " + "\n\r" + RegistroAEliminar + "; y los registros relacionados en las tablas [FUNCION] y [SUBFUNCION]"  + "\n\r\n\r" + "Desea Continuar con la operación ?";
            DialogResult EliminarRegistro = new DialogResult();
            EliminarRegistro = MessageBox.Show(this, Alerta, "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (EliminarRegistro == DialogResult.Yes)
            {
                if (EliminarRegistrosFuncionalidad())
                {
                    MessageBox.Show(this, "El registro se ha eliminado correctamente.", "Proceso corecto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    NuevoRegistro = true;
                    buttonAgregar.Enabled = true;
                    buttonModificar.Enabled = false;
                    buttonEliminar.Enabled = false;
                    buttonGuardar.Enabled = false;
                    buttonCancelar.Enabled = false;
                    groupBoxDatosCaptura.Enabled = false;
                    textBoxCodigoFinalidad.Enabled = false;
                    textBoxFinalidad.Enabled = false;
                    textBoxCodigoFinalidad.Text = "";
                    textBoxFinalidad.Text = "";
                    LlenarCatalogoDeFinalidad();
                    comboBoxFinalidad.Text = "";
                    IdFinalidad = -1;

                }
            }
        }


        private bool EliminarRegistrosFuncionalidad()
        {
            bool ProcesoCorrecto = true;
            string MySql = "DELETE CAT_FINALIDAD WHERE COD_FINALIDAD = " + IdFinalidad.ToString(); 
            TransactionCatalogo = ClassBaseDeDatos.MyConnectionDB.BeginTransaction();

            SqlCommand CommandInsertCatalogo;
            try
            {

                CommandInsertCatalogo = new SqlCommand(MySql, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.Transaction = TransactionCatalogo;
                CommandInsertCatalogo.CommandType = CommandType.Text;
                CommandInsertCatalogo.ExecuteNonQuery();

                MySql = "DELETE CAT_FUNCION WHERE COD_FINALIDAD = " + IdFinalidad.ToString();
                CommandInsertCatalogo.CommandText = MySql;
                CommandInsertCatalogo.ExecuteNonQuery();

                MySql = "DELETE CAT_SUBFUNCION WHERE COD_FINALIDAD = " + IdFinalidad.ToString();
                CommandInsertCatalogo.CommandText = MySql;
                CommandInsertCatalogo.ExecuteNonQuery();
                TransactionCatalogo.Commit();
            }
            catch (SqlException sqlexception)
            {
                TransactionCatalogo.Rollback();
                MessageBox.Show(this, "Ha ocurrido un error al borrar el registro  y sus relaciones en los catálogos." + "\n\r\n\r" +  sqlexception.Message.ToString(), "error al ejecutar el proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }
            finally
            {
                CommandInsertCatalogo = null;
            }
            return ProcesoCorrecto;
        }

        private void comboBoxFinalidad_DropDownClosed(object sender, EventArgs e)
        {


        }

        private void comboBoxFinalidad_SelectedIndexChanged(object sender, EventArgs e)
        {                        
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
            if (ProcesoCorrecto)
            {
                NuevoRegistro = true;
                buttonAgregar.Enabled = true;
                buttonModificar.Enabled = false;
                buttonEliminar.Enabled = false;
                buttonGuardar.Enabled = false;
                buttonCancelar.Enabled = false;
                groupBoxDatosCaptura.Enabled = false;
                textBoxCodigoFinalidad.Enabled = false;
                textBoxFinalidad.Enabled = false;
                textBoxCodigoFinalidad.Text = "";
                textBoxFinalidad.Text = "";
                LlenarCatalogoDeFinalidad();
                comboBoxFinalidad.Text = "";
                comboBoxFinalidad.Enabled = true; 
                comboBoxFinalidad.Focus();
                IdFinalidad = -1;
            }
        }

        private bool AgregarRegistro()
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            string NombreProcedimiento = "IngresarRegistrosCatalogoDeFinalidad";

            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;

                CommandInsertCatalogo.Parameters.Add("@CodFinalidad", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFinalidad"].Value = textBoxCodigoFinalidad.Text.ToString();
                CommandInsertCatalogo.Parameters.Add("@Concepto", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@Concepto"].Value = textBoxFinalidad.Text.ToString();
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
            string NombreProcedimiento = "ModificarCatalogoDeFinalidad";

            try
            {
                CommandModificarCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandModificarCatalogo.CommandType = CommandType.StoredProcedure;

                CommandModificarCatalogo.Parameters.Add("@CodigoFinalidad", SqlDbType.Int, 4);
                CommandModificarCatalogo.Parameters["@CodigoFinalidad"].Value = IdFinalidad;

                CommandModificarCatalogo.Parameters.Add("@NuevoCodigoFinalidad", SqlDbType.Int, 4);
                CommandModificarCatalogo.Parameters["@NuevoCodigoFinalidad"].Value =  textBoxCodigoFinalidad.Text.ToString();

                CommandModificarCatalogo.Parameters.Add("@Finalidad", SqlDbType.VarChar);
                CommandModificarCatalogo.Parameters["@Finalidad"].Value =  textBoxFinalidad.Text.ToString();
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

        private void LlencamposFinalidad()
        {
            string NombreProcedimiento = "ConsultarCatalogoDeFinalidad";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            textBoxCodigoFinalidad.Text = "";
            textBoxFinalidad.Text = "";

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = IdFinalidad;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "Consultafinalidades");
                datatable = dataset.Tables["Consultafinalidades"];

                if (datatable.Rows.Count > 0)
                {
                    DataRow registro = datatable.Rows[0];
                    textBoxCodigoFinalidad.Text = registro["COD_FINALIDAD"].ToString();
                    textBoxFinalidad.Text = registro["FINALIDAD"].ToString();
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
    }
}
