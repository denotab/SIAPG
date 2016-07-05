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

    public partial class FormImportarCatalogos : Form
    {
        ClassBaseDeDatos ClassConexionBaseDeDatos = new ClassBaseDeDatos();
        SqlTransaction TransactionCatalogo;

        DbDataReader DataRecordExcel;
        ClassParametros _ClassParametros = new ClassParametros();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        string CatalogoEnProceso = "";
        int NumIntermiciones = 0;
        int NumCatalogoAImportar = 0;
        public FormImportarCatalogos()
        {
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialogRuta.Multiselect = false;
            if (openFileDialogRuta.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                textBoxRuta.Text = openFileDialogRuta.FileName.ToString();
                comboBoxHoja.Items.Clear();
                if (textBoxRuta.Text.ToString().Trim() != "")
                    if (CargarHojasExcel()) comboBoxHoja.DroppedDown = true;

                this.Cursor = Cursors.Default;
            }
        }

        private bool CargarHojasExcel()
        {
            Boolean ProcesoCorrecto = true;
            var AppExcel = new Excel.Application();
            Excel.Workbook LibroDeTrabajoExcel;

            try
            {
                LibroDeTrabajoExcel = AppExcel.Workbooks.Open(textBoxRuta.Text.ToString());
                foreach (Microsoft.Office.Interop.Excel.Worksheet Hojas in LibroDeTrabajoExcel.Worksheets)
                {
                    comboBoxHoja.Items.Add(Hojas.Name.ToString());
                }
            }

            catch (Exception MyException)
            {
                MessageBox.Show(this, "Ha ocurrido un error al obtener las propiedades del archivo; verifique el siguiente error: \n\r\n\r" +
                MyException.Message.ToString(), "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                ProcesoCorrecto = false;
            }
            finally
            {
                AppExcel = null;
                LibroDeTrabajoExcel = null; 
            }
            return ProcesoCorrecto;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pendiente de aplicar");
        }

        private void comboBoxHoja_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void comboBoxHoja_TextChanged(object sender, EventArgs e)
        {
            VerificarSeleccionHoja();
        }
        private void VerificarSeleccionHoja()
        {
            if (comboBoxHoja.Text.ToString().Trim() != "")
            {
                buttonImportar.Enabled = true;
                groupBoxPropiedades.Enabled = true;
            }
            else
            {
                buttonImportar.Enabled = false;
                groupBoxPropiedades.Enabled = false;

            }

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormImportarCatalogoFinalidad_Load(object sender, EventArgs e)
        {
            PausaBackGround.RunWorkerAsync();
            ConfigurarPanelProgressBar();
        }
        private void DesplegarMenu()
        {
            catálogosToolStripMenuItem.ShowDropDown();
        }

        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(1500);
        }
        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {
            DesplegarMenu();            
        }

        private void buttonImportar_Click(object sender, EventArgs e)
        {
            panelProgressBar.Visible = true;
            pictureBoxProgress.Enabled = true;
            groupBoxOpciones.Enabled = false;
            buttonImportar.Enabled = false;
            if (ImportarCatalogo())
            {
                string Alerta = "El proceso ha terminado correctamente. \n\r\n\r" + "Desea ver los registros importados ?";
                DialogResult ContinuarProceso = new DialogResult();

                ContinuarProceso = MessageBox.Show(this, Alerta, "Proceso terminado", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (ContinuarProceso == DialogResult.Yes)
                {
                    FormCatalogoDeSubfunciones _FormCatalogoDeSubfunciones = new FormCatalogoDeSubfunciones();
                    _FormCatalogoDeSubfunciones.ShowDialog();
                }

                PausaBackGround.RunWorkerAsync();

               
            }
            buttonImportar.Enabled = true; 
            groupBoxOpciones.Enabled = true;
            panelProgressBar.Visible = false;
        }



        private void timerAlerta_Tick(object sender, EventArgs e)
        {
            NumIntermiciones++;
            if (groupBoxOpciones.Text != "")
                groupBoxOpciones.Text = "";
            else
            {
                groupBoxOpciones.Text = CatalogoEnProceso;
                if (NumIntermiciones >= 10)
                    timerAlerta.Enabled = false;
            }

        }

        private void Clic_MenuCatalogo(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem ItemMenuCatalogo = sender as ToolStripMenuItem;
                int IdCatalogo = Convert.ToInt32(ItemMenuCatalogo.Tag.ToString());

                CatalogoEnProceso = "Se importará el catálogo " + ItemMenuCatalogo.Text.ToString();
                NumIntermiciones = 0;
                timerAlerta.Enabled = true;
                NumCatalogoAImportar = Convert.ToInt32(ItemMenuCatalogo.Tag.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message.ToString(), "Ha ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private bool ImportarCatalogo()
        {
            bool ProcesoCorrecto = ConexionCatalogoExcelEstablecida();


            if (ProcesoCorrecto)
            {

                switch (NumCatalogoAImportar)
                {
                    case (Int32)ClassParametros._Catalogo.Finalidad:
                    case (Int32)ClassParametros._Catalogo.Funcion:
                    case (Int32)ClassParametros._Catalogo.Subfuncion:
                        ProcesoCorrecto = ImportarCatalogoFinalidadFuncionSubfuncion();
                        break;

                    case (Int32)ClassParametros._Catalogo.UnidadResponsable_UR:
                        ProcesoCorrecto = ImportarCAtalogoUR();
                        break;

                    case (Int32)ClassParametros._Catalogo.CuentaGenero:
                        ProcesoCorrecto = ImportarCatalogoDeCuentaGENERO(); 
                        break;

                    case (Int32)ClassParametros._Catalogo.CuentaGrupo:
                        ProcesoCorrecto = ImportarCatalogoDeCuentaGRUPO();
                        break;

                    case (Int32)ClassParametros._Catalogo.CuentaRubro:
                        ProcesoCorrecto = ImportarCatalogoDeCuentaRUBRO();
                        break;



                }
            }
            pictureBoxProgress.Enabled = false;
            return ProcesoCorrecto;
        }


        private bool ImportarCatalogoFinalidadFuncionSubfuncion()
        {
            bool ImportacionCorrecta = true;
            
            int NumRecord = 0;
            string Finalidad = "";
            string Funcion = "";
            string SubFuncion = "";
            string Concepto = "";
            int NumColumnaExcel = Convert.ToInt32(textBoxColumna.Text.ToString()) - 1;
            int NumRenglonExcel = Convert.ToInt32(textBoxRenglon.Text.ToString()) - 1;
            int IdCodigoFuncion = 0;
            int IdCodigoSubFuncion = 0;
            int RegistrosAlmacenados = 0;
      //      TransactionCatalogo = ClassBaseDeDatos.MyConnectionDB.BeginTransaction();
            try
            {
                while (DataRecordExcel.Read())
                {
                    Application.DoEvents();
                    NumRecord++;
                    labelRegistrosProcesados.Text = NumRecord.ToString("#,###");
                    if (NumRecord >= NumRenglonExcel)
                    {

                        Finalidad = DataRecordExcel[NumColumnaExcel].ToString();
                        if (NumCatalogoAImportar == (Int32)ClassParametros._Catalogo.Funcion || NumCatalogoAImportar == (Int32)ClassParametros._Catalogo.Subfuncion)
                            Funcion = DataRecordExcel[NumColumnaExcel + 1].ToString();

                        if (NumCatalogoAImportar == (Int32)ClassParametros._Catalogo.Subfuncion)
                            SubFuncion = DataRecordExcel[NumColumnaExcel + 2].ToString();


                        if (NumCatalogoAImportar == (Int32)ClassParametros._Catalogo.Finalidad)
                            Concepto = DataRecordExcel[NumColumnaExcel + 1].ToString();
                        else if (NumCatalogoAImportar == (Int32)ClassParametros._Catalogo.Funcion)
                            Concepto = DataRecordExcel[NumColumnaExcel + 2].ToString();
                         else if (NumCatalogoAImportar == (Int32)ClassParametros._Catalogo.Subfuncion)
                            Concepto = DataRecordExcel[NumColumnaExcel + 3].ToString();

                        if (NumCatalogoAImportar == (Int32)ClassParametros._Catalogo.Finalidad)
                        {
                            if (Finalidad.Trim() != "" && Concepto != "")
                            {
                                if (!InsertarRegistrosCatalogoDeFinalidad(Convert.ToInt32(Finalidad), Concepto))
                                {
                                    ImportacionCorrecta = false;
                                    break;
                                }
                                else
                                    RegistrosAlmacenados++;
                            }

                        }


                        if (NumCatalogoAImportar == (Int32)ClassParametros._Catalogo.Funcion)
                        {
                            if (Finalidad.Trim() != "" && Funcion != "" && Concepto != "")
                            {
                                IdCodigoFuncion = Convert.ToInt32(Finalidad + Funcion);
                                if (!InsertarRegistrosCatalogoDefunciones(IdCodigoFuncion, Convert.ToInt32(Finalidad), Convert.ToInt32(Funcion), Concepto))
                                {
                                    ImportacionCorrecta = false;
                                    break;
                                }
                                else
                                    RegistrosAlmacenados++;
                            }

                        }
                        else if (NumCatalogoAImportar == (Int32)ClassParametros._Catalogo.Subfuncion)
                        {
                            if (Finalidad.Trim() != "" && Funcion != "" && SubFuncion != "" && Concepto != "")
                            {
                                IdCodigoSubFuncion = Convert.ToInt32(Finalidad + Funcion + SubFuncion);
                                if (!InsertarRegistrosCatalogoDeSubfunciones(IdCodigoSubFuncion, Convert.ToInt32(Finalidad), Convert.ToInt32(Funcion), Convert.ToInt32(SubFuncion), Concepto))
                                {
                                    ImportacionCorrecta = false;
                                    break;
                                }
                                else
                                    RegistrosAlmacenados++;
                            }
                        }
                    }
                    labelRegistrosAlmacenados.Text = RegistrosAlmacenados.ToString("#,###");

                }
            }

            catch (Exception exception)
            {
                MessageBox.Show(this, "Ha ocurrido un error al obtener los datos del archivo excel: \n\r\n\r" +
                exception.Message.ToString(), "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ImportacionCorrecta = false; 
            }


        //    if (ImportacionCorrecta)
          //      TransactionCatalogo.Commit();
         //   else
          //       TransactionCatalogo.Rollback();

       //     TransactionCatalogo = null;
            return ImportacionCorrecta;
        }




        private bool ConexionCatalogoExcelEstablecida()
        {
            //            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source= " + NameDocumento + @" ;Extended Properties= ""Excel 8.0;HDR=No;IMEX=1""";                        

            bool ConexionEstablecida = true;
            try
            {
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source= " + textBoxRuta.Text.ToString() + @" ;Extended Properties= ""Excel 12.0 Xml;HDR=Yes;IMEX=1""";
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                DbConnection conexion = factory.CreateConnection();
                DbCommand comando = conexion.CreateCommand();
                conexion.ConnectionString = connectionString;
                comando.CommandText = "SELECT * FROM [" + comboBoxHoja.Text.ToString() + "$]";
                conexion.Open();
                DataRecordExcel = comando.ExecuteReader();
            }
            catch (Exception exception)
            {
                ConexionEstablecida = false;
                MessageBox.Show(this, "No ha sido posible  acceder al archivo excel para la importación de sus registros; verifique el siguiente error: " +
                    "\n\r\n\r" + exception.Message.ToString(), "Ha ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return ConexionEstablecida;
        }

        // Insertar registros en catalogo de finalidad
        private bool InsertarRegistrosCatalogoDeFinalidad(int CodigoFinalidad, string Concepto)
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            //     CommandInsertCatalogo.Transaction = TransactionCatalogo;

            try
            {
                CommandInsertCatalogo = new SqlCommand("IngresarRegistrosCatalogoDeFinalidad", ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@CodFinalidad", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFinalidad"].Value = CodigoFinalidad;
                CommandInsertCatalogo.Parameters.Add("@Concepto", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@Concepto"].Value = Concepto;
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

        // Insertar registros en catalogo de Funciones
        private bool InsertarRegistrosCatalogoDefunciones(int IdFuncion, int Finalidad, int Funcion, string Concepto)
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            //     CommandInsertCatalogo.Transaction = TransactionCatalogo;

            try
            {

                CommandInsertCatalogo = new SqlCommand("IngresarRegistrosCatalogoDefunciones", ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@IdFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@IdFuncion"].Value = IdFuncion;

                CommandInsertCatalogo.Parameters.Add("@CodFinalidad", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFinalidad"].Value = Finalidad;
                CommandInsertCatalogo.Parameters.Add("@CodFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFuncion"].Value = Funcion;
                CommandInsertCatalogo.Parameters.Add("@Concepto", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@Concepto"].Value = Concepto;
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

        // Insertar registros en catalogo de subfunciones
        private bool InsertarRegistrosCatalogoDeSubfunciones(int IdCodigoSubFuncion, int Finalidad, int Funcion, int SubFuncion, string Concepto)
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
       //     CommandInsertCatalogo.Transaction = TransactionCatalogo;

            try
            {

                CommandInsertCatalogo = new SqlCommand("IngresarRegistrosCatalogoDeSubfunciones", ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@IdSubFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@IdSubFuncion"].Value = IdCodigoSubFuncion;

                CommandInsertCatalogo.Parameters.Add("@CodFinalidad", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFinalidad"].Value = Finalidad;
                CommandInsertCatalogo.Parameters.Add("@CodFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodFuncion"].Value = Funcion;
                CommandInsertCatalogo.Parameters.Add("@CodSubFuncion", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodSubFuncion"].Value = SubFuncion;
                CommandInsertCatalogo.Parameters.Add("@Concepto", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@Concepto"].Value = Concepto;
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



        private bool ImportarCAtalogoUR()
        {
            bool ImportacionCorrecta = true;
            int NumRecord = 0;
            int NumColumnaExcel = Convert.ToInt32(textBoxColumna.Text.ToString()) - 1;
            int NumRenglonExcel = Convert.ToInt32(textBoxRenglon.Text.ToString()) - 1;
            int RegistrosAlmacenados = 0; 

            string Nivel1 = "";
            string Nivel2 = "";
            string Nivel3 = "";
            string Nivel4 = "";
            string Nivel5 = "";
            string UR = "";
            string IdUr = "";
            string CodigoUR = "";
            textBox1.Visible = true; 
            try
            {
                while (DataRecordExcel.Read())
                {
                    NumRecord++;
                    if (NumRecord >= NumRenglonExcel)
                    {
                        IdUr = "0";
                        Nivel1 = DataRecordExcel[NumColumnaExcel].ToString();
                        Nivel2 = DataRecordExcel[NumColumnaExcel + 1].ToString();
                        Nivel3 = DataRecordExcel[NumColumnaExcel + 2].ToString();
                        Nivel4 = DataRecordExcel[NumColumnaExcel + 3].ToString(); 
                        Nivel5 = DataRecordExcel[NumColumnaExcel + 4].ToString();

                        Nivel4 += Nivel5;
                        Nivel5 = "0";

                        UR = DataRecordExcel[NumColumnaExcel + 5].ToString();
                        CodigoUR = DataRecordExcel[2].ToString(); // Nivel1.Trim() + Nivel2.Trim() + Nivel3.Trim() + Nivel4.Trim() + Nivel5.Trim();


                        if (!InsertarRegistrosUR(CodigoUR,  Convert.ToInt32(Nivel1), Convert.ToInt32(Nivel2), Convert.ToInt32(Nivel3), Convert.ToInt32(Nivel4), Convert.ToInt32(Nivel5), UR))
                        {
                            ImportacionCorrecta = false;
                            break;
                        }
                        else
                            RegistrosAlmacenados++;
                    }
                    labelRegistrosAlmacenados.Text = RegistrosAlmacenados.ToString("#,###");
        
                }
            }

            catch (Exception exception)
            {
                MessageBox.Show(this, "Ha ocurrido un error al obtener los datos del archivo excel: \n\r\n\r" +
                exception.Message.ToString(), "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ImportacionCorrecta = false;
            }

            return ImportacionCorrecta; 
        }

        private bool ImportarCatalogoDeCuentaGENERO()
        {
            bool ImportacionCorrecta = true;
            int NumRecord = 0;
            int NumColumnaExcel = Convert.ToInt32(textBoxColumna.Text.ToString()) - 1;
            int NumRenglonExcel = Convert.ToInt32(textBoxRenglon.Text.ToString()) - 1;
            int RegistrosAlmacenados = 0;

            string CodigoGenero = "";
            string TipoCuentaGenero = "";
            textBox1.Visible = true;
            try
            {
                while (DataRecordExcel.Read())
                {
                    NumRecord++;
                    if (NumRecord >= NumRenglonExcel)
                    {
                        CodigoGenero = DataRecordExcel[NumColumnaExcel].ToString();
                        TipoCuentaGenero = DataRecordExcel[NumColumnaExcel + 1].ToString();


                        if (!InsertarRegistrosTipoCuentaGENERO(Convert.ToInt32(CodigoGenero), TipoCuentaGenero))
                        {
                            ImportacionCorrecta = false;
                            break;
                        }
                        else
                            RegistrosAlmacenados++;
                    }
                    labelRegistrosAlmacenados.Text = RegistrosAlmacenados.ToString("#,###");

                }
            }

            catch (Exception exception)
            {
                MessageBox.Show(this, "Ha ocurrido un error al obtener los datos del archivo excel: \n\r\n\r" +
                exception.Message.ToString(), "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ImportacionCorrecta = false;
            }
        
            return ImportacionCorrecta;
        }

        private bool ImportarCatalogoDeCuentaGRUPO()
        {
            bool ImportacionCorrecta = true;
            int NumRecord = 0;
            int NumColumnaExcel = Convert.ToInt32(textBoxColumna.Text.ToString()) - 1;
            int NumRenglonExcel = Convert.ToInt32(textBoxRenglon.Text.ToString()) - 1;
            int RegistrosAlmacenados = 0;

            string CodigoGenero = "";
            string CodigoGrupo = "";
            string TipoCuentaGrupo = "";

            textBox1.Visible = true;
            try
            {
                while (DataRecordExcel.Read())
                {
                    NumRecord++;
                    if (NumRecord >= NumRenglonExcel)
                    {
                        CodigoGenero = DataRecordExcel[NumColumnaExcel].ToString();
                        CodigoGrupo =  DataRecordExcel[NumColumnaExcel+1].ToString();
                        TipoCuentaGrupo = DataRecordExcel[NumColumnaExcel + 4].ToString();
                        if (!InsertarRegistrosTipoCuentaGRUPO(Convert.ToInt32(CodigoGenero), Convert.ToInt32(CodigoGrupo), TipoCuentaGrupo))
                        {
                            ImportacionCorrecta = false;
                            break;
                        }
                        else
                            RegistrosAlmacenados++;
                    }
                    labelRegistrosAlmacenados.Text = RegistrosAlmacenados.ToString("#,###");

                }
            }

            catch (Exception exception)
            {
                MessageBox.Show(this, "Ha ocurrido un error al obtener los datos del archivo excel: \n\r\n\r" +
                exception.Message.ToString(), "Error de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ImportacionCorrecta = false;
            }

            return ImportacionCorrecta;
        }


        private bool ImportarCatalogoDeCuentaRUBRO()
        {
            bool ImportacionCorrecta = true;
            int NumRecord = 0;
            int NumColumnaExcel = Convert.ToInt32(textBoxColumna.Text.ToString()) - 1;
            int NumRenglonExcel = Convert.ToInt32(textBoxRenglon.Text.ToString()) - 1;
            int RegistrosAlmacenados = 0;

            string CodigoGenero = "";
            string CodigoGrupo = "";
            string CodigoRubro = ""; 
            string TipoCuentaRubro = "";

            textBox1.Visible = true;
            try
            {
                while (DataRecordExcel.Read())
                {
                    NumRecord++;
                    if (NumRecord >= NumRenglonExcel)
                    {
                        CodigoGenero = DataRecordExcel[NumColumnaExcel].ToString();
                        CodigoGrupo = DataRecordExcel[NumColumnaExcel + 1].ToString();
                        CodigoRubro = DataRecordExcel[NumColumnaExcel + 2].ToString();
                        TipoCuentaRubro = DataRecordExcel[NumColumnaExcel + 4].ToString();
                        if (!InsertarRegistrosTipoCuentaRUBRO(Convert.ToInt32(CodigoGenero), Convert.ToInt32(CodigoGrupo), Convert.ToInt32(CodigoRubro), TipoCuentaRubro))
                        {
                            ImportacionCorrecta = false;
                            break;
                        }
                        else
                            RegistrosAlmacenados++;
                    }
                    labelRegistrosAlmacenados.Text = RegistrosAlmacenados.ToString("#,###");

                }
            }

            catch (Exception exception)
            {
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
            panelProgressBar.Top = (this.Height - panelProgressBar.Height)-60;
            pictureBoxProgress.Enabled = false;
            labelRegistrosProcesados.Text = "";
            labelRegistrosAlmacenados.Text = "";
        }

 //                               if (!InsertarRegistrosCatalogoDeSubfunciones(IdCodigoSubFuncion, Convert.ToInt32(Finalidad), Connvert.ToInt32(Funcion), Convert.ToInt32(SubFuncion), Concepto))

        private bool InsertarRegistrosUR(string COD_UR, int Nivel1, int Nivel2, int Nivel3, int Nivel4, int Nivel5,   string UR)
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            string NombreProcedimiento = "IngresarRegistrosCatalogoUR";
            //     CommandInsertCatalogo.Transaction = TransactionCatalogo;

            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;

                CommandInsertCatalogo.Parameters.Add("@CodigoUR", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@CodigoUR"].Value = COD_UR;
                CommandInsertCatalogo.Parameters.Add("@Nivel1", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@Nivel1"].Value = Nivel1;
                CommandInsertCatalogo.Parameters.Add("@Nivel2", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@Nivel2"].Value = Nivel2;
                CommandInsertCatalogo.Parameters.Add("@Nivel3", SqlDbType.Int);
                CommandInsertCatalogo.Parameters["@Nivel3"].Value = Nivel3;
                CommandInsertCatalogo.Parameters.Add("@Nivel4", SqlDbType.Int);
                CommandInsertCatalogo.Parameters["@Nivel4"].Value = Nivel4;
                CommandInsertCatalogo.Parameters.Add("@Nivel5", SqlDbType.Int);
                CommandInsertCatalogo.Parameters["@Nivel5"].Value = Nivel5;
                CommandInsertCatalogo.Parameters.Add("@UR", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@UR"].Value = UR;
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

        private bool InsertarRegistrosTipoCuentaGENERO(int CodigoCuenta, string NombreCuenta)
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            string NombreProcedimiento = "IngresarRegistrosCatalogoTipoDeCuentaGenero";
            //     CommandInsertCatalogo.Transaction = TransactionCatalogo;

            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@CodigoCuenta", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoCuenta"].Value = CodigoCuenta;
                CommandInsertCatalogo.Parameters.Add("@CuentaGenero", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@CuentaGenero"].Value = NombreCuenta;
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



        private bool InsertarRegistrosTipoCuentaGRUPO(int CodigoCuentaGenero, int CodigoCuentaGrupo, string CuentaGrupo)
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            string NombreProcedimiento = "IngresarRegistrosCatalogoTipoDeCuentaGrupo";
            //     CommandInsertCatalogo.Transaction = TransactionCatalogo;

            string CodigoGrupo = CodigoCuentaGenero.ToString() + CodigoCuentaGrupo.ToString();
            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@CodigoGrupo", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@CodigoGrupo"].Value = CodigoGrupo;
                CommandInsertCatalogo.Parameters.Add("@CodigoCuentaGenero", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoCuentaGenero"].Value = CodigoCuentaGenero;
                CommandInsertCatalogo.Parameters.Add("@CodigoCuentaGrupo", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoCuentaGrupo"].Value = CodigoCuentaGrupo;
                CommandInsertCatalogo.Parameters.Add("@CuentaGrupo", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@CuentaGrupo"].Value = CuentaGrupo;
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

        private bool InsertarRegistrosTipoCuentaRUBRO(int CodigoCuentaGenero, int CodigoCuentaGrupo, int CodigoCuentaRubro,    string CuentaRubro)
        {
            bool ProcesoCorrecto = true;
            SqlCommand CommandInsertCatalogo;
            string NombreProcedimiento = "IngresarRegistrosCatalogoTipoDeCuentaRubro";
            //     CommandInsertCatalogo.Transaction = TransactionCatalogo;

            string CodigoRubro = CodigoCuentaGenero.ToString() + CodigoCuentaGrupo.ToString() + CodigoCuentaRubro.ToString(); 
            try
            {

                CommandInsertCatalogo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                CommandInsertCatalogo.CommandType = CommandType.StoredProcedure;
                CommandInsertCatalogo.Parameters.Add("@CodigoRubro", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@CodigoRubro"].Value = CodigoRubro;
                CommandInsertCatalogo.Parameters.Add("@CodigoCuentaGenero", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoCuentaGenero"].Value = CodigoCuentaGenero;
                CommandInsertCatalogo.Parameters.Add("@CodigoCuentaGrupo", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoCuentaGrupo"].Value = CodigoCuentaGrupo;
                CommandInsertCatalogo.Parameters.Add("@CodigoCuentaRubro", SqlDbType.Int, 4);
                CommandInsertCatalogo.Parameters["@CodigoCuentaRubro"].Value = CodigoCuentaRubro;
                CommandInsertCatalogo.Parameters.Add("@CuentaRubro", SqlDbType.VarChar);
                CommandInsertCatalogo.Parameters["@CuentaRubro"].Value = CuentaRubro;
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
    }
}