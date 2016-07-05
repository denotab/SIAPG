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
using System.Security.Cryptography;

namespace SIAPG
{
    public partial class FormCaracteristicasHOJAPRESUPUESTO : Form
    {
        ClassParametros _ClassParametros = new ClassParametros();
        int IdHojaPresupuesto = 0;
        bool ButtonAceptarPress = false; 

        public FormCaracteristicasHOJAPRESUPUESTO()
        {
            InitializeComponent();
        }

        private void FormCaracteristicasHOJAPRESUPUESTO_Load(object sender, EventArgs e)
        {
            if (_ClassParametros.NuevaHojaDePresupuesto || _ClassParametros.GuardarComo )
            {
                DateTime NowTime = DateTime.Now;
                textBoxFechaCreacion.Text = NowTime.ToString("dd/MM/yyyy HH:mm:ss tt");
                textBoxFechaModificacion.Text = NowTime.ToString("dd/MM/yyyy HH:mm:ss tt");
                textBoxNombreHoja.Focus();  
            }
            else
            {

            }
            timerFecha.Enabled = true;
            LlenarComboHojas();
        }

        private void ConfiguracionFechaActual()
        {
            DateTime NowTime = DateTime.Now;
            textBoxFechaModificacion.Text = NowTime.ToString("dd/MM/yyyy HH:mm:ss tt");

            if (_ClassParametros.NuevaHojaDePresupuesto)
            {
                textBoxFechaCreacion.Text = NowTime.ToString("dd/MM/yyyy HH:mm:ss tt");
            }
        }

        private void timerFecha_Tick(object sender, EventArgs e)
        {
            ConfiguracionFechaActual();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DatosCorrectos())
            {
                ButtonAceptarPress = true; 
                if (_ClassParametros.NuevaHojaDePresupuesto || _ClassParametros.GuardarComo)
                {
                    timerFecha.Enabled = false;
                    if (IdHojaPresupuesto == 0)
                    {
                        if (_ClassParametros.NuevaHojaDePresupuesto)
                        {
                            if (GuardarHojaPresupuesto())
                            {
                                _ClassParametros.ProcesoCancelado = false;
                                this.Close();
                            }
                            else
                                timerFecha.Enabled = true;
                        }
                        else    // Guardar como..
                        {
                            if (GuardarHojaPresupuesto())
                            {
                                _ClassParametros.ProcesoCancelado = false;
                                _ClassParametros.NuevaHojaDePresupuesto = true; 
                                this.Close();
                            }
                            else
                                timerFecha.Enabled = true;
                        }
                    }
                    else   // Agregar sobre una hoja existente.
                    {
                        if (_ClassParametros.NuevaHojaDePresupuesto)
                        {
                            if (GuardarFechaDeModificacion())
                            {
                                _ClassParametros.IdHojaPresupuesto = IdHojaPresupuesto;
                                _ClassParametros.ProcesoCancelado = false;
                                _ClassParametros.ProcesoCorrecto = true;
                                this.Close();
                            }
                        }
                        else if (_ClassParametros.GuardarComo)
                        {
                            if (IdHojaPresupuesto != _ClassParametros.IdHojaPresupuesto)
                            {
                                if (GuardarFechaDeModificacion())
                                {
                                    _ClassParametros.NuevaHojaDePresupuesto = true;
                                    _ClassParametros.IdHojaPresupuesto = IdHojaPresupuesto;
                                    _ClassParametros.ProcesoCancelado = false;
                                    _ClassParametros.ProcesoCorrecto = true;
                                    this.Close();
                                }
                            }
                            else
                            {
                                if (GuardarFechaDeModificacion())
                                {
                                    _ClassParametros.NuevaHojaDePresupuesto = false;
                                    _ClassParametros.ProcesoCancelado = false;
                                    _ClassParametros.ProcesoCorrecto = true;
                                    this.Close();
                                }
                            }
                        }
                    }
                }
                else
                {

                }
            }
        }

        private bool GuardarFechaDeModificacion()
        {
            bool ProcesoCorrecto = true;

            DateTime NowTime = DateTime.Now;
            textBoxFechaModificacion.Text = NowTime.ToString("dd/MM/yyyy HH:mm:ss tt");

            try
            {
                string MySql = "UPDATE HOJAS_PROPIEDADES SET HOJAS_PROPIEDADES.FECHA_MODIFICACION = '" + textBoxFechaModificacion.Text.ToString() + "', OBSERVACIONES = '" + textBoxObservaciones.Text.ToString()   + "'  WHERE HOJAS_PROPIEDADES.ID_HOJA_PRESUPUESTO = " + IdHojaPresupuesto;
                SqlCommand CommandVerificacion = new SqlCommand(MySql, ClassBaseDeDatos.MyConnectionDB);
                CommandVerificacion.CommandType = CommandType.Text;
                CommandVerificacion.ExecuteNonQuery();
            }
            catch (Exception MyException)
            {
                MessageBox.Show(this, MyException.Message.ToString(), "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcesoCorrecto = false;
            }

            return ProcesoCorrecto;
        }

        private bool DatosCorrectos()
        {
            if (textBoxNombreHoja.Text.Trim().Length <= 3)
            {
                MessageBox.Show(this, "Es necesario que especifíque un nombre para la hoja de presupuesto que esta trabajando ", "Dato no especificado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else 
                return true;
        }
        private bool GuardarHojaPresupuesto()
        {
            string IdHojaPresupuesto;
            _ClassParametros.ProcesoCorrecto = true;


            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            string NombreProcedimiento = "IngresarRegistrosHojaPropiedadesPresupuesto";

            IdHojaPresupuesto = HashingHOJA();
            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@ID_HOJA", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@ID_HOJA"].Value = 0;

                CommandInsertCatalogo.Parameters.Add("@ID_USUARIO_CREA", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@ID_USUARIO_CREA"].Value = -1;  // Modificar al valor correcto si fuere necesario

                CommandInsertCatalogo.Parameters.Add("@ID_USUARIO_MODIFICA", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@ID_USUARIO_MODIFICA"].Value = -1;  // Modificar al valor correcto si fuere necesario

                CommandInsertCatalogo.Parameters.Add("@FECHA_CREACION", SqlDbType.DateTime);
                CommandInsertCatalogo.Parameters["@FECHA_CREACION"].Value = textBoxFechaCreacion.Text.ToString();


                CommandInsertCatalogo.Parameters.Add("@FECHA_MODIFICACION", SqlDbType.DateTime);
                CommandInsertCatalogo.Parameters["@FECHA_MODIFICACION"].Value = textBoxFechaModificacion.Text.ToString();

                CommandInsertCatalogo.Parameters.Add("@NOMBRE_HOJA", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@NOMBRE_HOJA"].Value = textBoxNombreHoja.Text.ToString();

                CommandInsertCatalogo.Parameters.Add("@OBSERVACIONES", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@OBSERVACIONES"].Value = textBoxObservaciones.Text.ToString();

                CommandInsertCatalogo.Parameters.Add("@CODIGO_GENERACION", SqlDbType.VarChar); 
                CommandInsertCatalogo.Parameters["@CODIGO_GENERACION"].Value = IdHojaPresupuesto;

                CommandInsertCatalogo.Parameters.Add("@NUEVO_REGISTRO", SqlDbType.Bit); ;
                CommandInsertCatalogo.Parameters["@NUEVO_REGISTRO"].Value = 1;


                Object IdNumRecord = CommandInsertCatalogo.ExecuteScalar();

                _ClassParametros.IdHojaPresupuesto = (long)(decimal)IdNumRecord;
                _ClassParametros.ProcesoCorrecto = true; 

            }
            catch (Exception exception)
            { 
                ProcesoCorrecto = false;
                MessageBox.Show(this, "Ha ocurrido un error al guardar el registro; verifique el siguiente error: \n\r\n\r" + exception.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            finally
            {
                CommandInsertCatalogo = null;
            }
            return ProcesoCorrecto;
        }

        private string HashingHOJA()
        {
            string Codigo = "";
            byte[] HashValor;            
            string CadenaHash = "";
            CadenaHash = textBoxFechaCreacion.Text.ToString() + textBoxNombreHoja.Text.ToString();
            UnicodeEncoding Encript = new UnicodeEncoding();
            byte[] MensajeEnBytes = Encript.GetBytes(CadenaHash);
            SHA1Managed SHhash = new SHA1Managed();
            HashValor = SHhash.ComputeHash(MensajeEnBytes);

            Codigo = Encoding.Default.GetString(HashValor, 0, HashValor.Length - 1).ToString();
        
            return Codigo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonAceptarPress = false;
            _ClassParametros.ProcesoCancelado = true;
            this.Close(); 

        }

        private void LlenarComboHojas()
        {
            string NombreProcedimiento = "ConsultarHojasDePresupuestoPropiedades";
            SqlDataAdapter sqldataadapter;
            DataSet dataset = new DataSet();
            DataTable datatable;
            comboBoxHojas.DataSource = null;

            try
            {
                sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqldataadapter.SelectCommand.Parameters.Add("@ID_HOJA_PRESUPUESTO", SqlDbType.Int);
                sqldataadapter.SelectCommand.Parameters["@ID_HOJA_PRESUPUESTO"].Value = -1;
                dataset = new DataSet();
                sqldataadapter.Fill(dataset, "HojasPresupuesto");
                datatable = dataset.Tables["HojasPresupuesto"];

                comboBoxHojas.Items.Clear();
                comboBoxHojas.DataSource = datatable;
                comboBoxHojas.ValueMember = datatable.Columns["ID_HOJA_PRESUPUESTO"].ToString();
                comboBoxHojas.DisplayMember = datatable.Columns["NOMBRE_HOJA"].ToString();

            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
            }

        }

        private void comboBoxHojas_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Object SelectedValue = comboBoxHojas.SelectedValue;
            if (SelectedValue != null)
            {
                IdHojaPresupuesto = Convert.ToInt32(SelectedValue.ToString());
                comboBoxHojas.Visible = false; 

                LlenarFormulario(IdHojaPresupuesto);
            }

            comboBoxHojas.Visible = false; 
        }

        private void LlenarFormulario(int IdHojaPresupuesto)
        {
            string NombreProcedimiento = "ConsultarHojasDePresupuestoPropiedades";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@ID_HOJA_PRESUPUESTO", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@ID_HOJA_PRESUPUESTO"].Value = IdHojaPresupuesto;
            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "HojasPresupuesto");
            DataTable datatable = dataset.Tables["HojasPresupuesto"];

            if (datatable.Rows.Count > 0)
            {
                timerFecha.Enabled = false; 
                string[] registrogrid = new string[5];
                DataRow registro = datatable.Rows[0];
                textBoxNombreHoja.Text  = registro["NOMBRE_HOJA"].ToString().Trim();
                textBoxFechaCreacion.Text = registro["FECHA_CREACION"].ToString().Trim();
                textBoxFechaModificacion.Text = registro["FECHA_MODIFICACION"].ToString().Trim();
                textBoxObservaciones.Text  = registro["OBSERVACIONES"].ToString().Trim();

                textBoxNombreHoja.Enabled = false;
                textBoxFechaCreacion.Enabled = false;
                textBoxFechaModificacion.Enabled = false;
                textBoxObservaciones.Enabled = true;
                textBoxObservaciones.Focus(); 

            }
            else
            {
                MessageBox.Show(this, "No se han elaborado hojas de trabajo presupuestales", "0 hojas encontradas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            label5.Visible = false;
            comboBoxHojas.Visible = true;
            comboBoxHojas.DroppedDown = true; 
        }

        private void LimpiarCampos()
        {
            textBoxNombreHoja.Text = "";
            textBoxFechaCreacion.Text = "";
            textBoxFechaModificacion.Text = "";
            textBoxObservaciones.Text = "";

        }

        private void FormCaracteristicasHOJAPRESUPUESTO_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!ButtonAceptarPress)
                   _ClassParametros.ProcesoCancelado = true;
            }
        }
    }
}
