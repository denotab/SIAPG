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
    public partial class FormClasificadorFuncionalSelected : Form
    {

        Properties.Settings PropiedadesAPP = new Properties.Settings();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassOpcionesSeleccionadas OpcionesSeleccionadas = new ClassOpcionesSeleccionadas();

        public FormClasificadorFuncionalSelected()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormClasificadorFuncionalSelected_Load(object sender, EventArgs e)
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
            LlenarGridFFS();
            this.Cursor = Cursors.Default;
        }

        private void LlenarGridFFS()
        {
            int TotalRegistros = 0;
            int RegistroActual = 0;
            string NombreProcedimiento = "ConsultarCatalogoFFS";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoFuncion", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoFuncion"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoSubfuncion", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoSubfuncion"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@SoloElUltimoNivel", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@SoloElUltimoNivel"].Value = PropiedadesAPP.CargarUnicamenteElUltimoNivel;
            
            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "CatalogoFFS");
            DataTable datatable = dataset.Tables["CatalogoFFS"];


            string IdFFS = "";
            string Finalidad = "";
            string Funcion = "";
            string SubFuncion = "";
            string FFS = "";

            if (datatable.Rows.Count > 0)
            {
                TotalRegistros = datatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {
                    DataRow registro = datatable.Rows[RegistroActual];
                    IdFFS = registro["ID_FFS"].ToString().Trim();
                    Finalidad = registro["COD_FINALIDAD"].ToString().Trim();
                    Funcion = registro["COD_FUNCION"].ToString().Trim();
                    SubFuncion = registro["COD_SUBFUNCION"].ToString().Trim();
                    FFS = registro["CLASIFICACION_FUNCIONAL"].ToString().Trim();
                    InsertarNodos(IdFFS, Finalidad, Funcion, SubFuncion, FFS);
                    RegistroActual++;

                }

                treeViewCatalogo.EndUpdate();
                //  treeViewConsulta.ExpandAll();
            }
            else
            {
                MessageBox.Show(this, "Cero registros encontrados; no se han cargado los catálogos de Finalidad, Funcion y Subfunción", "Atención.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Close();
            }
        }

        private void InsertarNodos(string IdFFS, string Finalidad, string Funcion, string Subfuncion,   string FFS)
        {
            string NombreNodo = "";
            string NombreNodoPadre = "";
            byte NumImagen = 0;
            string EtiquetaNodo = "";

            EtiquetaNodo = FFS;
            if (PropiedadesAPP.CargarUnicamenteElUltimoNivel)
            {
                NombreNodo = "nodo" + IdFFS.ToString();
                NumImagen = 3;
                TreeNode NuevoNodo = treeViewCatalogo.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                NuevoNodo.Tag = IdFFS;
            }
            else
            {

             if (Subfuncion != "0")
                {
                    NombreNodoPadre = "nodo" + Finalidad + Funcion;
                    TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                    if (NodoPadre.Length != 0)
                    {
                        NombreNodo = "nodo" + Finalidad + Funcion + Subfuncion;
                        NumImagen = 3;
                        TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                        TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                        NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                        NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                        NuevoNodo.Tag = IdFFS;
                    }
                }

                else if (Funcion != "0")
                {
                    NombreNodoPadre = "nodo" + Finalidad;
                    TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                    if (NodoPadre.Length != 0)
                    {
                        NombreNodo = "nodo" + Finalidad + Funcion;
                        NumImagen = 2;
                        TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                        TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                        NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                        NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                        NuevoNodo.Tag = IdFFS;
                        NuevoNodo.Expand();
                    }
                }
                else if (Finalidad != "0")
                {
                    NombreNodo = "nodo" + Finalidad.ToString();
                    NumImagen = 1;
                    TreeNode NuevoNodo = treeViewCatalogo.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                    NuevoNodo.Tag = IdFFS;
                    NuevoNodo.Expand();
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
            if (!PropiedadesAPP.CargarUnicamenteElUltimoNivel)
                AsignarElementosSeleccionados(treeViewCatalogo.Nodes[0]);
            else
                AsignarElementosSeleccionados();

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


        private void checkBoxUl_Click(object sender, EventArgs e)
        {

            DialogResult ConfirmarCambios = new DialogResult();
            string Alerta = "Desea reorganizar la vista de la estructura de arbol ?";

            ConfirmarCambios = MessageBox.Show(this, Alerta, "Se reorganizará la estructura", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            if (ConfirmarCambios == DialogResult.Yes)
            {
                if (PropiedadesAPP.CargarUnicamenteElUltimoNivel)
                    PropiedadesAPP.CargarUnicamenteElUltimoNivel = false;
                else
                    PropiedadesAPP.CargarUnicamenteElUltimoNivel = true;
                PropiedadesAPP.Save();
                treeViewCatalogo.Nodes.Clear();
                LlenarGridFFS();
            }
        }
    }
}
