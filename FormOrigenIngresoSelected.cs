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
    public partial class FormOrigenIngresoSelected : Form
    {
        Properties.Settings PropiedadesAPP = new Properties.Settings();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassOpcionesSeleccionadas OpcionesSeleccionadas = new ClassOpcionesSeleccionadas();

        public FormOrigenIngresoSelected()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;


        }

        private void FormOrigenIngresoSelected_Load(object sender, EventArgs e)
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
            string NombreProcedimiento = "ConsultarCatalogoOrigenIngreso";
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

            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "CatalogoOI");
            DataTable datatable = dataset.Tables["CatalogoOI"];


            string CodigoNivel1 = "";
            string CodigoNivel2 = "";
            string CodigoNivel3 = "";
            string CodigoNivel4 = "";
            string NivelComplemento = ""; 
            string OrigenIngreso = "";
            string IdOI = "";

            if (datatable.Rows.Count >= 0)
            {
                TotalRegistros = datatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {


                    DataRow registro = datatable.Rows[RegistroActual];
                    CodigoNivel1 = registro["OI_NIVEL1"].ToString().Trim();
                    CodigoNivel2 = registro["OI_NIVEL2"].ToString().Trim();
                    CodigoNivel3 = registro["OI_NIVEL3"].ToString().Trim();
                    CodigoNivel4 = registro["OI_NIVEL4"].ToString().Trim();
                    NivelComplemento = registro["OI_NIVEL_COMPLEMENTO"].ToString().Trim();
                    OrigenIngreso = registro["ORIGEN_INGRESO"].ToString().Trim();
                    IdOI = registro["ID_ORIGEN_INGRESO"].ToString().Trim();
                    InsertarNodos(IdOI, CodigoNivel1, CodigoNivel2, CodigoNivel3, CodigoNivel4, NivelComplemento, OrigenIngreso);
                    RegistroActual++;
                }

                treeViewCatalogo.EndUpdate();
                //  treeViewConsulta.ExpandAll();
            }
        }
        private void InsertarNodos(string Codigo, string nivel1, string nivel2, string nivel3, string nivel4, string NivelComplemento, string OrigenIngreso)
        {
            string NombreNodo = "";
            string NombreNodoPadre = "";
            byte NumImagen = 0;
            string EtiquetaNodo = "";

            if (NivelComplemento == "") NivelComplemento = "0";
            if (nivel4 == "") nivel4 = "0";
            if (nivel3 == "") nivel3 = "0";
            if (nivel2 == "") nivel2 = "0";
            if (nivel1 == "") nivel1 = "0";


            EtiquetaNodo = OrigenIngreso;


            if (NivelComplemento != "0")
            {
                NombreNodoPadre = "nodo" + nivel1 + nivel2 + nivel4;
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

            else if (nivel4 != "0")
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

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
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

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            OpcionesSeleccionadas.Aplicar = true;
            if (!PropiedadesAPP.CargarUnicamenteElUltimoNivel)
                AsignarElementosSeleccionados(treeViewCatalogo.Nodes[0]);
            else
                AsignarElementosSeleccionados();

            OpcionesSeleccionadas.guardarlista = OpcionesSeleccionadas.RegistrosSeleccionados;

            this.Close();

        }

  
        private void AsignarElementosSeleccionados()
        {
            string Clave;
            string Descripcion;

            foreach (TreeNode node in treeViewCatalogo.Nodes)
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
