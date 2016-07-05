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
    public partial class FormUnidadResponsableSelect : Form
    {
        Properties.Settings PropiedadesAPP = new Properties.Settings();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassOpcionesSeleccionadas OpcionesSeleccionadas = new ClassOpcionesSeleccionadas(); 

            
        public FormUnidadResponsableSelect()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormUnidadResponsableSelect_Load(object sender, EventArgs e)
        {
            PausaBackGround.RunWorkerAsync();
        }
        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {
            this.Cursor = Cursors.WaitCursor;
            LlenarGridUR();
            this.Cursor = Cursors.Default;
        }

        private void LlenarGridUR()
        {
            int TotalRegistros = 0;
            int RegistroActual = 0;
            ///string NombreProcedimiento = "ConsultarCatalogoUR";
            string NombreProcedimiento = "CONSULTAR_CATALOGO_UR";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoNivel1", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoNivel1"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoNivel2", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoNivel2"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoNivel3", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoNivel3"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoNivel4", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoNivel4"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoNivel5", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoNivel5"].Value = -1;

            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "CatalogoUR");
            DataTable datatable = dataset.Tables["CatalogoUR"];


            string CodigoNivel1 = "";
            string CodigoNivel2 = "";
            string CodigoNivel3 = "";
            string CodigoNivel4 = "";
            string CodigoNivel5 = "";
            string UR = "";
            string IdUR = "";

            if (datatable.Rows.Count >= 0)
            {
                TotalRegistros = datatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {


                    DataRow registro = datatable.Rows[RegistroActual];
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

                treeViewCatalogo.EndUpdate();
                //  treeViewConsulta.ExpandAll();
            }
        }
        private void InsertarNodos(string Codigo, string nivel1, string nivel2, string nivel3, string nivel4, string nivel5, string ur)
        {
            string NombreNodo = "";
            string NombreNodoPadre = "";
            byte NumImagen = 0;
            string EtiquetaNodo = "";

            if (nivel5 == "") nivel5 = "0";
            if (nivel4 == "") nivel4 = "0";
            if (nivel3 == "") nivel3 = "0";
            if (nivel2 == "") nivel2 = "0";
            if (nivel1 == "") nivel1 = "0";

     
            EtiquetaNodo = ur;

 
            if (nivel5 != "00")
            {
                NombreNodoPadre = "nodo" + nivel1 + nivel2 + nivel3 + nivel4;
                TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2 + nivel3 + nivel4 + nivel5;
                    NumImagen = 5;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length -1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = Codigo;
                }
            }

            else if (nivel4 != "00")
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
                NombreNodoPadre = "nodo" + nivel1 + nivel2 ;
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

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        private void treeViewCatalogo_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }

            }
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            OpcionesSeleccionadas.Aplicar = true; 
            AsignarElementosSeleccionados(treeViewCatalogo.Nodes[0]);
            OpcionesSeleccionadas.guardarlista = OpcionesSeleccionadas.RegistrosSeleccionados;

            this.Close(); 
        }

        private void AsignarElementosSeleccionados(TreeNode treeNode)
        {
            string Clave;
            string Descripcion;


            foreach (TreeNode node in treeNode.Nodes)
            {

                if (node.Nodes.Count > 0)
                {
                    this.AsignarElementosSeleccionados(node);
                }
                else
                {
                    if (node.Checked)
                    {
                        Clave = node.Tag.ToString();
                        Descripcion = node.Text.ToString();
                        OpcionesSeleccionadas.RegistrosSeleccionados.Add(new ClassOpcionesSeleccionadas.ListaSeleccionada(Clave, Descripcion));
                    }
                }

            }

        }

    }
}
