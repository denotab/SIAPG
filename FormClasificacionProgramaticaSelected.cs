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
    public partial class FormClasificacionProgramaticaSelected : Form
    {

        Properties.Settings PropiedadesAPP = new Properties.Settings();
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassOpcionesSeleccionadas OpcionesSeleccionadas = new ClassOpcionesSeleccionadas();


        public FormClasificacionProgramaticaSelected()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void FormClasificacionProgramaticaSelected_Load(object sender, EventArgs e)
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
            string NombreProcedimiento = "ConsultarCatalogoClasificacionProgramatica";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoProgramatico1", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoProgramatico1"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@CodigoProgramatico2", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@CodigoProgramatico2"].Value = -1;
            sqldataadapter.SelectCommand.Parameters.Add("@SoloElUltimoNivel", SqlDbType.Int);
            sqldataadapter.SelectCommand.Parameters["@SoloElUltimoNivel"].Value = PropiedadesAPP.CargarUnicamenteElUltimoNivel;

            DataSet dataset = new DataSet();
            sqldataadapter.Fill(dataset, "CatalogoClasificacionProgramatica");
            DataTable datatable = dataset.Tables["CatalogoClasificacionProgramatica"];


            string IdProgramatico = "";
            string ProgramaticoNivel1 = "";
            string ProgramaticoNivel2 = "";
            string Programatico = "";
            string IdentificadorProgramatico = "";
            string FFS = "";

            if (datatable.Rows.Count > 0)
            {
                TotalRegistros = datatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {
                    DataRow registro = datatable.Rows[RegistroActual];
                    IdProgramatico = registro["ID_PROGRAMATICO"].ToString().Trim();
                    ProgramaticoNivel1 = registro["PROGRAMATICO_NIVEL1"].ToString().Trim();
                    ProgramaticoNivel2 = registro["PROGRAMATICO_NIVEL2"].ToString().Trim();
                    Programatico = registro["PROGRAMATICO"].ToString().Trim();
                    IdentificadorProgramatico = registro["CLAVE_PROGRAMATICO"].ToString().Trim();
                    InsertarNodos(IdProgramatico, ProgramaticoNivel1, ProgramaticoNivel2, Programatico, IdentificadorProgramatico);
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

        private void InsertarNodos(string IdProgramatico, string ProgramaticoNivel1, string ProgramaticoNivel2, string Programatico, string IdentificadorProgramatico)
        {
            string NombreNodo = "";
            string NombreNodoPadre = "";
            byte NumImagen = 0;
            string EtiquetaNodo = "";

            EtiquetaNodo = IdentificadorProgramatico.ToString() + " " +  Programatico;
            if (PropiedadesAPP.CargarUnicamenteElUltimoNivel)
            {
                NombreNodo = "nodo" + IdProgramatico.ToString();
                NumImagen = 3;
                TreeNode NuevoNodo = treeViewCatalogo.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                NuevoNodo.Tag = IdProgramatico;
            }
            else if (ProgramaticoNivel2 != "0")
            {
                NombreNodoPadre = "nodo" + ProgramaticoNivel1;
                TreeNode[] NodoPadre = treeViewCatalogo.Nodes.Find(NombreNodoPadre, true);
                if (NodoPadre.Length != 0)
                {
                    NombreNodo = "nodo" + ProgramaticoNivel1 + ProgramaticoNivel2;
                    NumImagen = 2;
                    TreeNode RamaReferencia = NodoPadre[NodoPadre.Length - 1];
                    TreeNode NuevoNodo = RamaReferencia.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                    NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoTercero);
                    NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoTercero);
                    NuevoNodo.Tag = IdProgramatico;
                    NuevoNodo.Expand();
                }
            }
            else if (ProgramaticoNivel1 != "0")
            {
                NombreNodo = "nodo" + ProgramaticoNivel1.ToString();
                NumImagen = 1;
                TreeNode NuevoNodo = treeViewCatalogo.Nodes.Add(NombreNodo, EtiquetaNodo, NumImagen);
                NuevoNodo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                NuevoNodo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                NuevoNodo.Tag = IdProgramatico;
                NuevoNodo.Expand();
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

        private void treeViewCatalogo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }

            }

        }
    }
}
