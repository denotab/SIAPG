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
    public partial class FrmCatalogoUR : Form
    {
        Properties.Settings PropiedadesAPP = new Properties.Settings();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassOpcionesSeleccionadas OpcionesSeleccionadas = new ClassOpcionesSeleccionadas();
        string comando="";

        public FrmCatalogoUR()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FrmCatalogoUR_Load(object sender, EventArgs e)
        {
            lblPadreURNueva.Text = "";
            PausaBackGround.RunWorkerAsync();
            groupAgregarNuevaUR.Enabled = false;
        }
        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {
            this.Cursor = Cursors.WaitCursor;
            LlenarTreeCatUR();
            this.Cursor = Cursors.Default;
        }

        private void LlenarTreeCatUR()
        {
            int TotalRegistros = 0;
            int RegistroActual = 0;
            string NombreProcedimiento = "CONSULTAR_CATALOGO_UR";

            SqlDataAdapter AdaptadorSql = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            AdaptadorSql.SelectCommand.CommandType = CommandType.StoredProcedure;
            AdaptadorSql.SelectCommand.Parameters.Add("@CodigoNivel1", SqlDbType.Int);            
            AdaptadorSql.SelectCommand.Parameters.Add("@CodigoNivel2", SqlDbType.Int);            
            AdaptadorSql.SelectCommand.Parameters.Add("@CodigoNivel3", SqlDbType.Int);            
            AdaptadorSql.SelectCommand.Parameters.Add("@CodigoNivel4", SqlDbType.Int);            
            AdaptadorSql.SelectCommand.Parameters.Add("@CodigoNivel5", SqlDbType.Int);            
            AdaptadorSql.SelectCommand.Parameters["@CodigoNivel1"].Value = -1;
            AdaptadorSql.SelectCommand.Parameters["@CodigoNivel2"].Value = -1;
            AdaptadorSql.SelectCommand.Parameters["@CodigoNivel3"].Value = -1;
            AdaptadorSql.SelectCommand.Parameters["@CodigoNivel4"].Value = -1;
            AdaptadorSql.SelectCommand.Parameters["@CodigoNivel5"].Value = -1;

            DataSet dataset = new DataSet();
            AdaptadorSql.Fill(dataset, "query_CatalogoUR");
            DataTable Midatatable = dataset.Tables["query_CatalogoUR"];

            string CodigoNivel1 = "";
            string CodigoNivel2 = "";
            string CodigoNivel3 = "";
            string CodigoNivel4 = "";
            string CodigoNivel5 = "";
            string UR = "";
            string IdUR = "";

            treeViewCatUR.Nodes.Clear();

            if (Midatatable.Rows.Count >= 0)
            {
                TotalRegistros = Midatatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {
                    DataRow registro = Midatatable.Rows[RegistroActual];
                    CodigoNivel1 = registro["COD_UR_NIVEL1"].ToString().Trim();
                    CodigoNivel2 = registro["COD_UR_NIVEL2"].ToString().Trim();
                    CodigoNivel3 = registro["COD_UR_NIVEL3"].ToString().Trim();
                    CodigoNivel4 = registro["COD_UR_NIVEL4"].ToString().Trim();
                    CodigoNivel5 = registro["COD_UR_NIVEL5"].ToString().Trim();
                    UR = registro["UR"].ToString().Trim();
                    IdUR = registro["ID_UR"].ToString().Trim();
                    InsertarNodos(IdUR, CodigoNivel1, CodigoNivel2, CodigoNivel3, CodigoNivel4, CodigoNivel5, UR);
                    RegistroActual++;
                }
                treeViewCatUR.EndUpdate();
                treeViewCatUR.ExpandAll();
            }
        }
        private void InsertarNodos(string IdUR, string nivel1, string nivel2, string nivel3, string nivel4, string nivel5, string ur)
        {
            string NombreNodo = "";
            string NombreNodoPadre = "";
            byte NumImagen = 0;
            string EtiquetaNodo = "";

            if (nivel1 == "") nivel1 = "0";
            if (nivel2 == "") nivel2 = "0";
            if (nivel3 == "") nivel3 = "0";
            if (nivel4 == "") nivel4 = "0";
            if (nivel5 == "") nivel5 = "0";


            EtiquetaNodo = IdUR+ " - " +ur;


            if (nivel5 != "00")
            {
                NombreNodoPadre = "nodo" + nivel1 + nivel2 + nivel3; ///+ nivel4;
                TreeNode[] NodoPadre = treeViewCatUR.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2 + nivel3 + nivel4 + nivel5;
                    NumImagen = 5;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = (nivel1+","+ nivel2 + "," + nivel3 + "," + nivel4+","+ nivel5) + ";" + IdUR +";"+ ur;
                }
            }

            else if (nivel4 != "00")
            {
                NombreNodoPadre = "nodo" + nivel1 + nivel2 + nivel3;
                TreeNode[] NodoPadre = treeViewCatUR.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2 + nivel3 + nivel4;
                    NumImagen = 4;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = (nivel1 + "," + nivel2 + "," + nivel3 + "," + nivel4) + ";" + IdUR + ";" + ur;
                }
            }

            else if (nivel3 != "0")
            {
                NombreNodoPadre = "nodo" + nivel1 + nivel2;
                TreeNode[] NodoPadre = treeViewCatUR.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2 + nivel3;
                    NumImagen = 3;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = (nivel1 + "," + nivel2 + "," + nivel3) + ";" + IdUR + ";" + ur;
                }
            }

            else if (nivel2 != "0")
            {
                NombreNodoPadre = "nodo" + nivel1;
                TreeNode[] NodoPadre = treeViewCatUR.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2;
                    NumImagen = 2;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = (nivel1 + "," + nivel2) + ";" + IdUR + ";" + ur;
                    NuevoNodo.Expand();
                }
            }
            else if (nivel1 != "0")
            {
                NombreNodo = "nodo" + nivel1.ToString();
                NumImagen = 1;
                TreeNode NuevoNodo = treeViewCatUR.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                NuevoNodo.Tag = (nivel1)+";"+ IdUR + ";" + ur;
                NuevoNodo.Expand();

            }
        }

        private void treeViewCatUR_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewCatUR.SelectedNode.Tag != null)
            {
                TxtNodoTag.Text = "";
                String[] TreeViewTag = treeViewCatUR.SelectedNode.Tag.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int elem = 0; elem < TreeViewTag.Length; elem++)
                {
                    if (TxtNodoTag.Text == "")
                    {
                        TxtNodoTag.Text = TreeViewTag[elem].ToString();
                        
                    }
                    else
                        TxtNodoTag.Text += "; " + TreeViewTag[elem].ToString();
                }
                /// Lleno con el primer elemento y el ultimo

                lblPadreURNueva.Text = TreeViewTag[1].ToString() + "- " + TreeViewTag[TreeViewTag.Length-1].ToString();
                txtId_URPadre.Text = TreeViewTag[1].ToString();
                txtURN.Text  = TreeViewTag[TreeViewTag.Length - 1].ToString();


                ///activo textos para longitud de nuevo nodo
                if (comando == "A")
                {
                    if (txtId_URPadre.TextLength < 4)
                    {
                        txtIdUr1.Enabled = true;
                        txtIdUr2.Enabled = false;
                        txtIdUr3.Enabled = false;
                    }
                    else if ((txtId_URPadre.TextLength > 4) && (txtId_URPadre.TextLength < 6))
                    {
                        txtIdUr1.Enabled = true;
                        txtIdUr2.Enabled = true;
                        txtIdUr3.Enabled = true;
                    }
                    else
                    {
                        txtIdUr1.Enabled = false;
                        txtIdUr2.Enabled = false;
                        txtIdUr3.Enabled = false;
                    }
                }
                else
                {
                    txtIdUr1.Enabled = false;
                    txtIdUr2.Enabled = false;
                    txtIdUr3.Enabled = false;
                }


                ///fin activar textos
            }
        }



        private void btnAgregarNuevaUR_Click(object sender, EventArgs e)
        {
            groupAgregarNuevaUR.Enabled = true;
            groupAgregarNuevaUR.Text = "Datos del UR Nueva: ";
            txtURN.Enabled = true;
            comando = "A";


            ///txtPadreURNueva.Text = TxtNodoTag.Text;
        }





        private void btnCancelar_Click(object sender, EventArgs e)
        {
            groupAgregarNuevaUR.Enabled = false ;
            ///groupAgregarNuevaUR.Text = "Datos de la UR: ";
            ///txtId_URPadre.Text = "";

            groupAgregarNuevaUR.Enabled = false;
            groupAgregarNuevaUR.Text = "Unidad Responsable: ";
            txtId_URPadre.Text = "";
            txtURN.Text = "";
            txtIdUr1.Text = "";
            txtIdUr2.Text = "";
            txtIdUr3.Text = "";
            txtIdUr1.Enabled = true;
            txtIdUr2.Enabled = true;
            txtIdUr3.Enabled = true;
            txtURN.Enabled = true;

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            groupAgregarNuevaUR.Enabled = false;
            groupAgregarNuevaUR.Text = "Datos de la UR: ";
            txtId_URPadre.Text = "";
            txtURN.Text = "";
        }
        public static bool EsNumero(object value)
        {
            try
            {
                int entero = Convert.ToInt32(value.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void txtIdUr1_Enter(object sender, EventArgs e)
        {
            txtIdUr1.SelectionStart = 0;
            txtIdUr1.SelectionLength = txtIdUr1.Text.Length;

        }

        private void txtIdUr2_Enter(object sender, EventArgs e)
        {
            txtIdUr2.SelectionStart = 0;
            txtIdUr2.SelectionLength = txtIdUr2.Text.Length;
        }


        private void txtIdUr3_Enter(object sender, EventArgs e)
        {
            txtIdUr3.SelectionStart = 0;
            txtIdUr3.SelectionLength = txtIdUr3.Text.Length;
        }


        private void txtIdUr1_Leave(object sender, EventArgs e)
        {
            if (txtIdUr1.Text.Trim() != "")
            {
                if (ClassFunciones.Si_esNumero(txtIdUr1.Text.Trim()) == false)
                {
                    MessageBox.Show("Favor de ingresar una una clave de UR válida");
                    txtIdUr1.Focus();
                    return;
                }
                if (Convert.ToInt32(txtIdUr1.Text) < 10)
                {
                    txtIdUr1.Text = "0" + Convert.ToInt32(txtIdUr1.Text);
                }
                if (Convert.ToInt32(txtIdUr1.Text) == 0)
                {
                    txtIdUr1.Text = "";
                }
            }
        }

        private void txtIdUr2_Leave(object sender, EventArgs e)
        {
            if (txtIdUr2.Text.Trim() != "")
            {
                if (ClassFunciones.Si_esNumero(txtIdUr2.Text.Trim()) == false)
                {
                    MessageBox.Show("Favor de ingresar una una clave de UR válida");
                    txtIdUr2.Focus();
                    return;
                }
                if (Convert.ToInt32(txtIdUr2.Text) < 10)
                {
                    txtIdUr2.Text = "0" + Convert.ToInt32(txtIdUr2.Text);
                }
                if (Convert.ToInt32(txtIdUr2.Text) == 0)
                {
                    txtIdUr2.Text = "";
                }
            }
        }

        private void txtIdUr3_Leave(object sender, EventArgs e)
        {
            if (txtIdUr3.Text.Trim() != "")
            {
                if (ClassFunciones.Si_esNumero(txtIdUr3.Text.Trim()) == false)
                {
                    MessageBox.Show("Favor de ingresar una una clave de UR válida");
                    txtIdUr3.Focus();
                    return;
                }
                if (Convert.ToInt32(txtIdUr3.Text) < 10)
                {
                    txtIdUr3.Text = "0" + Convert.ToInt32(txtIdUr3.Text);
                }
                if (Convert.ToInt32(txtIdUr3.Text) == 0)
                {
                    txtIdUr3.Text = "";
                }
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ///**********SECCION DE FILTROS DE VALIDACION************
            if (txtId_URPadre.Text.Trim() == "")
            {
                MessageBox.Show("Favor de ingresar una clave Padre de UR");
                txtId_URPadre.Focus();
                return;
            }
            if (txtURN.Text.Trim() == "")
            {
                MessageBox.Show("Favor de ingresar una descripción de UR");
                txtURN.Focus();
                return;
            }

            switch (comando)
            {
                case "A":

                    if (txtIdUr1.Text.Trim() == "")
                    {
                        MessageBox.Show("Favor de ingresar una una clave de UR");
                        txtIdUr1.Focus();
                        return;
                    }

                    ///***LLAMADA DE CLASE PARA GRABAR REGISTROS
                    InsertarUR();
                    break;
                case "M":
                    ///***LLAMADA DE CLASE PARA GRABAR REGISTROS
                    ModificarUR();
                    break;
                case "E":
                    ///***LLAMADA DE CLASE PARA GRABAR REGISTROS
                    EliminarUR();
                    break;
            }





            ///***MENSAJE DE CAPTURA MASIVA DE REGISTROS
            ///
            ///
            ///

            /// RESTABLECIMIENTO DE CONTROLES
            btnCancelar.PerformClick() ;

        }

        private void btnModificarUR_Click(object sender, EventArgs e)
        {
            groupAgregarNuevaUR.Enabled = true;
            ///groupAgregarNuevaUR.Text = "Datos del UR Nueva: ";
            comando = "M";
            txtIdUr1.Enabled = false;
            txtIdUr2.Enabled = false;
            txtIdUr3.Enabled = false;
            txtURN.Enabled = true;
        }

        private void btnEliminarUr_Click(object sender, EventArgs e)
        {
            groupAgregarNuevaUR.Enabled = true;
            comando = "E";
            txtIdUr1.Enabled = false;
            txtIdUr2.Enabled = false;
            txtIdUr3.Enabled = false;
            txtURN.Enabled = false;
        }

        private bool InsertarUR()
        {
            bool insertaregistro = true;
            bool Salirwhile = false;
            string paso;

            ///todas las variables para llenar registrio

            string CodigoUR = "";
            String CodigoNuevoNivel = "";


            string UR = txtURN.Text.ToString().Trim();
            int Nivel1 = 0;
            int Nivel2 = 0;
            int Nivel3 = 0;
            string Nivel4 = "00";
            string Nivel5 = "00";
            int niveles = 1;
            string cadena = TxtNodoTag.Text;

            if (txtIdUr1.Text.Trim() != "")
            {
                if (txtIdUr2.Text.Trim() != "")
                {
                    CodigoUR = txtId_URPadre.Text.Trim() + "." + txtIdUr1.Text.Trim() + "." + txtIdUr2.Text.Trim();
                    CodigoNuevoNivel = txtIdUr1.Text.Trim() + "." + txtIdUr2.Text.Trim();
                    if (txtIdUr3.Text.Trim() != "")
                    {
                        CodigoUR = CodigoUR + "." + txtIdUr3.Text.Trim();
                        CodigoNuevoNivel = CodigoNuevoNivel + "." + txtIdUr3.Text.Trim();
                    }
                }

                if ((txtIdUr2.Text.Trim() == "") && (txtIdUr3.Text.Trim() == ""))
                {
                    paso = Convert.ToInt32(txtIdUr1.Text).ToString();
                    CodigoUR = txtId_URPadre.Text.Trim() + "." + paso;
                    CodigoNuevoNivel = paso;
                }
            }




            ///******************procedimiento para evitar duplicidad de Id_UR
            ///Select * from CAT_UR Where ID_UR=@id_ur"

            string CadenabuscaID = "Select * from CAT_UR where ID_UR=@id_ur";
            SqlDataAdapter AdaptadorSql = new SqlDataAdapter(CadenabuscaID, ClassBaseDeDatos.MyConnectionDB);
            AdaptadorSql.SelectCommand.CommandType = CommandType.Text;
            AdaptadorSql.SelectCommand.Parameters.Add("@id_ur", SqlDbType.VarChar);
            AdaptadorSql.SelectCommand.Parameters["@id_ur"].Value = CodigoUR;

            DataSet dataset = new DataSet();
            AdaptadorSql.Fill(dataset, "query_CatalogoUR");
            DataTable Midatatable = dataset.Tables["query_CatalogoUR"];
            AdaptadorSql.SelectCommand.ExecuteNonQuery();

            if (Midatatable.Rows.Count > 0)
            {
                MessageBox.Show("Existe una UR con el mismo codigo de UR: " + CodigoUR);
                insertaregistro = false;
                return insertaregistro;
            }



            /// *****************procedimiento de guardar*****************
            /// declaracion del SqlCommand
            SqlCommand ComandoSQL;
            string NombreProcedimiento = "IngresarRegistrosCatalogoUR";


            ///PROCEDIMIENTO DE EXTRACION DE CODIGOS DE NIVELES DEL 1 AL 5

            while ((niveles < 6) && (Salirwhile == false))
            {
                for (int i = 0; i < cadena.Length; i++)
                {
                    if (cadena.Substring(i, 1).Equals(","))
                    {
                        ///MessageBox.Show("encontre ','");
                        niveles += 1;
                    }
                    else if (cadena.Substring(i, 1).Equals(";"))
                    {
                        Salirwhile = true;
                        break;
                    }
                    else
                    {
                        switch (niveles)
                        {
                            case 1:
                                Nivel1 = Convert.ToInt32(cadena.Substring(i, 1).Trim());
                                ///MessageBox.Show(cadena.Substring(i, 1));
                                break;
                            case 2:
                                Nivel2 = Convert.ToInt32(cadena.Substring(i, 1).Trim());
                                ///MessageBox.Show(cadena.Substring(i, 1));
                                break;
                            case 3:
                                Nivel3 = Convert.ToInt32(cadena.Substring(i, 1).Trim());
                                ///MessageBox.Show(cadena.Substring(i, 1));
                                break;
                            case 4:
                                Nivel4 = cadena.Substring(i, 1).Trim();
                                ///MessageBox.Show(cadena.Substring(i, 1));
                                break;
                            case 5:
                                Nivel5 = cadena.Substring(i, 1).Trim();
                                ///MessageBox.Show(cadena.Substring(i, 1));
                                break;
                        }
                    }
                }
            }

            ///ASIGNO EL SIGUIENTE A ASIGNAR EN LA TABLA
            switch (niveles)
            {
                case 1:
                    Nivel2 = Convert.ToInt32(CodigoNuevoNivel);
                    break;
                case 2:
                    Nivel3 = Convert.ToInt32(CodigoNuevoNivel);
                    break;
                case 3:
                    Nivel4 = CodigoNuevoNivel;
                    break;
                    /*case 4:
                        Nivel5 = CodigoUR;
                        break;*/

            }

            try
            {
                ///Enlazamos el CommandSql a ComandoSQL con la Base y Procedimiento
                ComandoSQL = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                ComandoSQL.CommandType = CommandType.StoredProcedure;

                ///Enviamos los parametros al procedimiento almacenado
                ComandoSQL.Parameters.Add("@CodigoUR", SqlDbType.VarChar);
                ComandoSQL.Parameters.Add("@Nivel1", SqlDbType.Int);
                ComandoSQL.Parameters.Add("@Nivel2", SqlDbType.Int);
                ComandoSQL.Parameters.Add("@Nivel3", SqlDbType.Int);
                ComandoSQL.Parameters.Add("@Nivel4", SqlDbType.VarChar);
                ComandoSQL.Parameters.Add("@Nivel5", SqlDbType.VarChar);
                ComandoSQL.Parameters.Add("@UR", SqlDbType.VarChar);

                ComandoSQL.Parameters["@CodigoUR"].Value = CodigoUR;
                ComandoSQL.Parameters["@Nivel1"].Value = Nivel1;
                ComandoSQL.Parameters["@Nivel2"].Value = Nivel2;
                ComandoSQL.Parameters["@Nivel3"].Value = Nivel3;
                ComandoSQL.Parameters["@Nivel4"].Value = Nivel4;
                ComandoSQL.Parameters["@Nivel5"].Value = Nivel5;
                ComandoSQL.Parameters["@UR"].Value = UR;

                ComandoSQL.ExecuteNonQuery();
                LlenarTreeCatUR();
            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al guardar el registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                insertaregistro = false;
            }
            finally
            {
                ComandoSQL = null;
            }
            return insertaregistro;
        }




        private bool ModificarUR()
        {
            bool modificarregistro = true;

            ///todas las variables para llenar registrio

            string CodigoUR = txtId_URPadre.Text.Trim();
            string UR = txtURN.Text.ToString().Trim();
      
            if (CodigoUR=="")
            {
                MessageBox.Show("El ID_UR no se encuentra seleccionado");
                modificarregistro  = false;
                return modificarregistro;
            }




            /// *****************procedimiento de guardar*****************
            /// declaracion del SqlCommand
            SqlCommand ComandoSQL;
            string NombreProcedimiento = "MODIFICAR_REGISTRO_CATALOGO_UR";

            try
            {
                ///Enlazamos el CommandSql a ComandoSQL con la Base y Procedimiento
                ComandoSQL = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                ComandoSQL.CommandType = CommandType.StoredProcedure;

                ///Enviamos los parametros al procedimiento almacenado
                ComandoSQL.Parameters.Add("@CodigoUR", SqlDbType.VarChar);
                ComandoSQL.Parameters.Add("@UR", SqlDbType.VarChar);

                ComandoSQL.Parameters["@CodigoUR"].Value = CodigoUR;
                ComandoSQL.Parameters["@UR"].Value = UR;

                ComandoSQL.ExecuteNonQuery();
                LlenarTreeCatUR();
            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al guardar el registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                modificarregistro = false;
            }
            finally
            {
                ComandoSQL = null;
            }
            return modificarregistro;



        }
        private bool EliminarUR()
        {
            bool eliminarregistro = true;
            ///DialogResult  Confirmacion;

            ///todas las variables para llenar registrio

            string CodigoUR = txtId_URPadre.Text.Trim();

            if (CodigoUR == "")
            {
                MessageBox.Show("El ID_UR no se encuentra seleccionado");
                eliminarregistro = false;
                return eliminarregistro;
            }

            if (MessageBox.Show(this, "Deseas Eliminar la UR: "+ CodigoUR + "?", "Confirmación de Nuevo Progtrama", MessageBoxButtons.YesNoCancel)== DialogResult.No)
            {
                eliminarregistro = false;
                return eliminarregistro;
            }


            /// *****************procedimiento de guardar*****************
            /// declaracion del SqlCommand
            SqlCommand ComandoSQL;
            string NombreProcedimiento = "ELIMINAR_REGISTRO_CATALOGO_UR";

            try
            {
                ///Enlazamos el CommandSql a ComandoSQL con la Base y Procedimiento
                ComandoSQL = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                ComandoSQL.CommandType = CommandType.StoredProcedure;

                ///Enviamos los parametros al procedimiento almacenado
                ComandoSQL.Parameters.Add("@CodigoUR", SqlDbType.VarChar);

                ComandoSQL.Parameters["@CodigoUR"].Value = CodigoUR;

                ComandoSQL.ExecuteNonQuery();
                LlenarTreeCatUR();
            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(this, sqlexception.Message.ToString(), "Ha ocurrido un error al eliminar el registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                eliminarregistro = false;
            }
            finally
            {
                ComandoSQL = null;
            }
            return eliminarregistro;



        }

        private void txtIdUr1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIdUr2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

