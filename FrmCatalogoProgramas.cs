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
    public partial class FrmCatalogoProgramas : Form
    {
        readonly BackgroundWorker PausaBackGround = new BackgroundWorker();
        ClassBaseDeDatos ClaseBaseDatos = new ClassBaseDeDatos();
        Properties.Settings PropiedadesAPP = new Properties.Settings();

        public TreeNode MySelectedNode = new TreeNode();
        public TreeNode MySelectedNodeAnterior = new TreeNode();
        ///bool ModuloAgregarActivo = false;
        ///bool NuevoRegistro = false;

        int NodoSelectedTipoGasto = -1;
        int NodoSelectedId_UR = -1;
        ///int NodoSelectedFinalidad = -1;
        ///int NodoSelectedFuncion = -1;
        ///int NodoSelectedSubfuncion = -1;

        public FrmCatalogoProgramas()
        {
            InitializeComponent();
            PausaBackGround.DoWork += EventStartPausa;
            PausaBackGround.RunWorkerCompleted += EventStopPausa;

        }

        private void EventStartPausa(object sender, DoWorkEventArgs doworkeventargs)
        {
            Thread.Sleep(500);
        }

        private void EventStopPausa(object sender, RunWorkerCompletedEventArgs runworkercompletedeventargs)
        {
            ActivarProgressBar();
            CargarRegistros();
            LlenarComboTipoGasto();
            LlenarComboUR();
            DesactivarProgressBar();
        }

        private void ActivarProgressBar()
        {
            panelProgressBar.Visible = true;
            pictureBoxProgress.Enabled = true;
        }

        private void DesactivarProgressBar()
        {
            panelProgressBar.Visible = false;
            pictureBoxProgress.Enabled = false;
        }
        private void ConfigurarPanelProgressBar()
        {
            panelProgressBar.Parent = this;
            panelProgressBar.Visible = false;
            panelProgressBar.BringToFront();
            panelProgressBar.Left = (this.Width - panelProgressBar.Width) / 2;
            panelProgressBar.Top = (this.Height - panelProgressBar.Height) - 60;
            pictureBoxProgress.Enabled = false;
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            PausaBackGround.RunWorkerAsync();
            ConfigurarPanelProgressBar();
            btnAgregar.Enabled = false;
            groupNProgramas.Visible = false;
            groupComandos.Location= new  Point(565, 272);
            GroupPrograma.Height = 190;

        }

        private void CargarRegistros()
        {
            int TotalRegistros = 0;
            int RegistroActual = 0;
            string NombreProcedimiento = "CONSULTAR_CATALOGO_PROGRAMAS";
            SqlDataAdapter AdaptadorSQL = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
            AdaptadorSQL.SelectCommand.CommandType = CommandType.StoredProcedure ;
            /*AdaptadorSQL.SelectCommand.Parameters.Add("@Id_Tipo_Gasto", SqlDbType.Int);
            AdaptadorSQL.SelectCommand.Parameters.Add("@Id_UR", SqlDbType.VarChar);
            AdaptadorSQL.SelectCommand.Parameters.Add("@SoloElUltimoNivel", SqlDbType.Bit);
            AdaptadorSQL.SelectCommand.Parameters["@Id_Tipo_Gasto"].Value = -1;
            AdaptadorSQL.SelectCommand.Parameters["@Id_UR"].Value = "-1";
            AdaptadorSQL.SelectCommand.Parameters["@SoloElUltimoNivel"].Value = 0;
            */
            DataSet Midataset = new DataSet();
            AdaptadorSQL.Fill(Midataset, "PA_CAT_PROGRAMAS");
            DataTable Midatatable = Midataset.Tables["PA_CAT_PROGRAMAS"];


            string Id_Tipo_Gasto = "";
            string Id_UR = "0";
            

            if (Midatatable.Rows.Count >= 0)
            {
                TotalRegistros = Midatatable.Rows.Count;
                while (RegistroActual < TotalRegistros)
                {

                    DataRow Registro = Midatatable.Rows[RegistroActual];
                    Id_UR = Registro["ID_UR"].ToString().Trim();
                    if (Registro["ID_TIPO_GASTO"].ToString().Trim() != Id_Tipo_Gasto && (Id_UR == "0"))
                    {
                        Id_Tipo_Gasto = Registro["ID_TIPO_GASTO"].ToString().Trim();
                        InsertarNodoPrimero(Convert.ToInt32(Id_Tipo_Gasto),Registro["TIPO_GASTO"].ToString().Trim() , Registro["PROGRAMA"].ToString().Trim());
                       ///Id_Tipo_Gasto = "";
                        ///Id_UR = "0";
                    }
                    else 
                    ///if (Id_UR != "0")
                    {
                        Id_Tipo_Gasto = Registro["ID_TIPO_GASTO"].ToString().Trim();
                        Id_UR = Registro["ID_UR"].ToString().Trim();
                        InsertarNodoSecundario(Convert.ToInt32(Id_Tipo_Gasto), Registro["TIPO_GASTO"].ToString().Trim(), Id_UR, Registro["PROGRAMA"].ToString().Trim());
                        ///Id_UR = "0";
                    }

                    RegistroActual++;
                }

                treeViewProgramas.EndUpdate();
                treeViewProgramas.ExpandAll();
            }
        }

        private void InsertarNodoPrimero(int Id_Tipo_Gasto,string Tipo_Gasto, string Programa)
        {
            string IdNameNodoPrimario = "Programa" + Id_Tipo_Gasto.ToString();
            string EtiquetaNodo = Id_Tipo_Gasto.ToString() + ". " + Programa;
            int NumImage = 3;
            TreeNode NodeParent = treeViewProgramas.Nodes.Add(IdNameNodoPrimario, EtiquetaNodo, NumImage);
            NodeParent.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
            NodeParent.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
            NodeParent.Tag = Id_Tipo_Gasto.ToString()+ ";" + Programa;
        }

        private void InsertarNodoSecundario(int Id_Tipo_Gasto,string  Tipo_Gasto, string Id_UR, string Programa)
        {
            string IdNameNodoPrimario = "Programa" + Id_Tipo_Gasto.ToString();
            string IdNameNodoSecundario = "UR" + Id_Tipo_Gasto.ToString() + Id_UR;
            string EtiquetaNodo = Id_Tipo_Gasto.ToString() + "- " + Id_UR + "- " + Programa;

            int NumImage = 10;
            TreeNode[] IdNodoPrimario = treeViewProgramas.Nodes.Find(IdNameNodoPrimario, true);

            if (IdNodoPrimario.Length != 0)
            {
                TreeNode NodoPrimario = IdNodoPrimario[0];
                TreeNode IdNodoSegundo = NodoPrimario.Nodes.Add(IdNameNodoSecundario, EtiquetaNodo, NumImage);

                IdNodoSegundo.ForeColor = ColorTranslator.FromHtml(PropiedadesAPP.FrontColorNodoPrimero);
                IdNodoSegundo.BackColor = ColorTranslator.FromHtml(PropiedadesAPP.BackColorNodoPrimero);
                IdNodoSegundo.Tag = Id_Tipo_Gasto.ToString() + ". " + Tipo_Gasto + ";" + Id_UR + ". " + Programa;

            }
        }

        private void LlenarComboTipoGasto()
           
        {
            string NombreProcedimiento = "CONSULTAR_CATALOGO_TIPO_GASTO";
            SqlDataAdapter AdaptadorSQL;
            comboTipoGasto.DataSource = null;
            NodoSelectedTipoGasto = -1;
            NodoSelectedId_UR = 1;

            try
            {
                AdaptadorSQL = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                AdaptadorSQL.SelectCommand.CommandType = CommandType.StoredProcedure;
                ///sqldataadapter.SelectCommand.Parameters.Add("@CodigoFinalidad", SqlDbType.Int);
                ///sqldataadapter.SelectCommand.Parameters["@CodigoFinalidad"].Value = -1;
                DataSet Midataset = new DataSet();
                AdaptadorSQL.Fill(Midataset, "query_CONSULTA_TIPO_GASTO");
                DataTable Midatatable = Midataset.Tables["query_CONSULTA_TIPO_GASTO"];

                comboTipoGasto.Items.Clear();
                comboTipoGasto.DataSource = Midatatable;
                comboTipoGasto.ValueMember = Midatatable.Columns[0].ToString();
                comboTipoGasto.DisplayMember = Midatatable.Columns[2].ToString().Trim();

            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
            }
        }

        private void LlenarComboUR()
        {
            string NombreProcedimiento = "CONSULTAR_CATALOGO_UR_CONCATENADO";
            comboPrograma.DataSource = null;
            NodoSelectedTipoGasto = -1;
            NodoSelectedId_UR = 1;

            try
            {
                SqlDataAdapter AdaptadorSql = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                DataSet dataset = new DataSet();
                AdaptadorSql.Fill(dataset, "query_CatalogoUR");
                DataTable Midatatable = dataset.Tables["query_CatalogoUR"];

                comboPrograma.Items.Clear();
                comboPrograma.DataSource = Midatatable;
                comboPrograma.ValueMember = Midatatable.Columns[0].ToString();
                comboPrograma.DisplayMember = Midatatable.Columns[2].ToString().Trim();

            }
            catch (SqlException MyException)
            {
                MessageBox.Show(MyException.Message);
            }
        }


        private void Boton_Cargar_Click(object sender, EventArgs e)
        {
         
        }


        private void comboTipoGasto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboTipoGasto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                MessageBox.Show("aqui va procedimiento");
            }
        }

        private void treeViewProgramas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewProgramas.SelectedNode.Tag != null)
                {
                TxtNodoTag.Text = "";
                String[] treeViewTag = treeViewProgramas.SelectedNode.Tag.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int elem = 0; elem < treeViewTag.Length; elem++)
                {
                    if (TxtNodoTag.Text == "")
                        TxtNodoTag.Text = treeViewTag[elem].ToString();
                    else
                        TxtNodoTag.Text += "; " + treeViewTag[elem].ToString();
                }
                comboTipoGasto.Text = treeViewTag[0].ToString();
                comboPrograma.Text = treeViewTag[1].ToString();


                /*
                if (campos.Length == 1)
                {
                    NodoSelectedTipoGasto = Convert.ToInt32(campos[0].ToString());
                    comboBoxFinalidad.SelectedValue = NodoSelectedTipoGasto;
                    NodoSelectedFuncion = -1;
                    NodoSelectedSubfuncion = -1;
                    LlenarComboFuncion();
                    InhabilitarCaptura();
                    comboBoxFuncion.Text = "";
                    comboBoxSubFuncion.Text = "";
                    comboBoxSubFuncion.Enabled = false;

                }
                else if (campos.Length == 2)
                {

                    if (Convert.ToInt32(campos[0].ToString()) != NodoSelectedTipoGasto)
                    {
                        NodoSelectedFinalidad = Convert.ToInt32(campos[0].ToString());
                        comboBoxFinalidad.SelectedValue = NodoSelectedFinalidad;
                        LlenarComboFuncion();
                    }

                    NodoSelectedFuncion = Convert.ToInt32(campos[1].ToString());
                    comboBoxFuncion.SelectedValue = NodoSelectedFuncion;
                    NodoSelectedSubfuncion = -1;
                    LlenarComboSubFuncion();
                    comboBoxSubFuncion.Text = "";
                    InhabilitarCaptura();
                    comboBoxSubFuncion.Enabled = true;
                }
                else if (campos.Length == 3)
                {
                    if (Convert.ToInt32(campos[0].ToString()) != NodoSelectedFinalidad)
                    {
                        NodoSelectedFinalidad = Convert.ToInt32(campos[0].ToString());
                        comboBoxFinalidad.SelectedValue = NodoSelectedFinalidad;
                        NodoSelectedFuncion = -1;
                        NodoSelectedSubfuncion = -1;
                        LlenarComboFuncion();
                    }

                    if (Convert.ToInt32(campos[1].ToString()) != NodoSelectedFuncion)
                    {
                        LlenarComboFuncion();
                        NodoSelectedFuncion = Convert.ToInt32(campos[1].ToString());
                        comboBoxFuncion.SelectedValue = NodoSelectedFuncion;
                        LlenarComboSubFuncion();
                    }
                    NodoSelectedSubfuncion = Convert.ToInt32(campos[2].ToString());
                    comboBoxSubFuncion.SelectedValue = NodoSelectedSubfuncion;
                    LlencamposSubfuncion();
                    HabilitarCaptura();
                }
                /// Llenar aqui 
                ///Llenargridfuncionalidad();

                */
            }
       
        }

        private void comboTipoGasto_Leave(object sender, EventArgs e)
        {
            DialogResult agregarTP;
            if (comboTipoGasto.SelectedIndex.Equals(-1))
            {
                agregarTP = MessageBox.Show (this,"El Elemento escrito no pertenece a uno de la lista Existente. Deseas agregra un nuevo Tipo de Programa?","Confirmación de Nuevo Progtrama",MessageBoxButtons.YesNoCancel  );
                if (agregarTP== DialogResult.Yes)
                {

                }
                else
                {
                    MessageBox.Show("Favor de selecionar un elemento de la lista");
                    
                    comboTipoGasto.SelectedIndex = 0;
                    comboTipoGasto.Focus();
                }
            }
        }

        private void Boton_Cargar_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmCatalogoUR  FormCatalogoUR = new FrmCatalogoUR();
            FormCatalogoUR.ShowDialog();
        }

        private void btnAgregarNuevoPrograma_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = true ;
            groupNProgramas.Visible = true;
            groupComandos.Location = new Point(565, 434);
            GroupPrograma.Height = 272;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            groupNProgramas.Visible = false;
            groupComandos.Location = new Point(565, 272);
            GroupPrograma.Height = 190;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            groupNProgramas.Visible = false;
            groupComandos.Location = new Point(565, 272);
            GroupPrograma.Height = 190;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }
    }
}
