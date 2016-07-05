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

    public partial class FrmCatalogoCog : Form
    {
        Properties.Settings PropiedadesAPP = new Properties.Settings();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassOpcionesSeleccionadas OpcionesSeleccionadas = new ClassOpcionesSeleccionadas();


        public FrmCatalogoCog()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;
                    


    }

        private void FrmCatalogoCog_Load(object sender, EventArgs e)
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
            string NombreProcedimiento = "CONSULTA_CATALOGO_COG_235";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            /*sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@COGNivel1", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@COGNivel1"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@COGNivel2", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@COGNivel2"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@COGNivel3", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@COGNivel3"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@COG_COMPLEMENTO", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@COG_COMPLEMENTO"].Value = -1;
            */

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
                    InsertarNodos(IdCOG, CodigoNivel1, CodigoNivel2, CodigoNivel3, CodigoNivel4,  COG);
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
            ///byte NumImagen = 0;
            string EtiquetaNodo = "";

            if (nivel4 == "") nivel4 = "0";
            if (nivel3 == "") nivel3 = "0";
            if (nivel2 == "") nivel2 = "0";
            if (nivel1 == "") nivel1 = "0";


            EtiquetaNodo = Codigo+ ". "+COG;



            if (nivel4 != "0")
            {
                NombreNodoPadre = "nodo" + nivel1 + nivel2 + nivel3;
                TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2 + nivel3 + nivel4;
                    ///NumImagen = 4;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = Codigo+";" + COG ;
                }
            }

            else if (nivel3 != "0")
            {
                NombreNodoPadre = "nodo" + nivel1 + nivel2;
                TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2 + nivel3;
                    ///NumImagen = 3;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = Codigo + ";" + COG;
                }
            }

            else if (nivel2 != "0")
            {
                NombreNodoPadre = "nodo" + nivel1;
                TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + nivel1 + nivel2;
                    ///NumImagen = 2;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = Codigo + ";" + COG;
                    ///NuevoNodo.Expand();
                }
            }
            else if (nivel1 != "0")
            {
                NombreNodo = "nodo" + nivel1.ToString();
                ///NumImagen = 1;
                TreeNode NuevoNodo = treeViewCatalogo.Nodes.Add(NombreNodo, EtiquetaNodo);
                NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                NuevoNodo.Tag = Codigo + ";" + COG;
                ///NuevoNodo.Expand();

            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
        }

        private void treeViewCatalogo_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            /*foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
            */
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            ///OpcionesSeleccionadas.Aplicar = true;
            ///AsignarElementosSeleccionados(treeViewCatalogo.Nodes[0]);
            ///OpcionesSeleccionadas.guardarlista = OpcionesSeleccionadas.RegistrosSeleccionados;

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

        private void treeViewCatalogo_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            if (treeViewCatalogo.SelectedNode.Tag != null)
            {
                ///FrmPedidos.nuevoCog[0] = "";
                String[] treeViewTag = treeViewCatalogo.SelectedNode.Tag.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int elem = 0; elem < treeViewTag.Length; elem++)
                {
                    ///if (FrmPedidos.nuevoCog[elem] == "")
                        OrdenCompra.nuevoCog[elem] = treeViewTag[elem].ToString();
                    ///else
                        ///FrmPedidos.nuevoCog[elem] += "; " + treeViewTag[elem].ToString();
                }
            }
        }





        private void button1_Click(object sender, EventArgs e)
        {


        }
    }
}



    
