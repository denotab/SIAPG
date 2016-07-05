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
    public partial class FrmListaRequisiciones : Form
    {
        public static string comando = "";
        public static int Fila = 0;
        public static int filaSeleccionada = 0;
        public static int IdPedido = 0;

        public FrmListaRequisiciones()
        {
            InitializeComponent();
            cargarGridPedidos();
        }

        private void toolNuevoPedido_Click(object sender, EventArgs e)
        {

            FrmNuevaRequisicion FormNuevaRequisicion = new FrmNuevaRequisicion();
            FormNuevaRequisicion.Tag = "N";
            FormNuevaRequisicion.Show();

        }

        private void toolEditarPedido_Click(object sender, EventArgs e)
        {
            FrmNuevaRequisicion FormNuevaRequisicion = new FrmNuevaRequisicion();
            FormNuevaRequisicion.Tag = "S";
            FormNuevaRequisicion.Show();

        }

        private void toolEliminarPedido_Click(object sender, EventArgs e)
        {
            comando = "E";
        }

        private void toolPedidos_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void cargarGridPedidos()
        {


            string procedimientoAlmacenado = "CONSULTAR_OPERACIONES_DETALLE";
            SqlConnection sqlConnection1 = new SqlConnection(ClassFunciones.CadenaConexion);
            SqlCommand ComandoSQL = new SqlCommand();
            SqlDataReader SQLReader;


            ComandoSQL.CommandText = procedimientoAlmacenado;
            ComandoSQL.CommandType = CommandType.StoredProcedure;

            ComandoSQL.Parameters.Add("@tipo", SqlDbType.VarChar).Value = "REQUISICION";

            ComandoSQL.Connection = sqlConnection1;

            sqlConnection1.Open();
            SQLReader = ComandoSQL.ExecuteReader();

            while (SQLReader.Read())
            {
                Fila = GridPedidos.Rows.Add();
                GridPedidos.Rows[Fila].Cells["Folio"].Value = SQLReader.GetInt32(SQLReader.GetOrdinal("ID_OPERACIONES_DETALLE")).ToString();
                //GridPedidos.Rows[Fila].Cells["Tipo"].Value = SQLReader.GetInt32(reader.GetOrdinal("TIPO_OPERACIONES")).ToString();
                GridPedidos.Rows[Fila].Cells["UR"].Value = SQLReader.GetString(SQLReader.GetOrdinal("ID_UR")).ToString();
                GridPedidos.Rows[Fila].Cells["Descripcion"].Value = SQLReader.GetString(SQLReader.GetOrdinal("DESCRIPCION")).ToString();
                GridPedidos.Rows[Fila].Cells["Fecha"].Value = SQLReader.GetDateTime(SQLReader.GetOrdinal("FECHA")).ToString();
                GridPedidos.Rows[Fila].Cells["Proveedor"].Value = SQLReader.GetInt32 (SQLReader.GetOrdinal("ID_PROVEEDOR")).ToString();

            }

        }

        private void cargarGridPedidosIdOD(int IdOperacionesDetalle, string TipoOperacion)
        {


            string procedimientoAlmacenado = "CONSULTAR_OPERACIONES_DETALLE_IDOPERACIONESD";
            SqlConnection sqlConnection1 = new SqlConnection(ClassFunciones.CadenaConexion);
            SqlCommand ComandoSQL = new SqlCommand();
            SqlDataReader SQLReader;


            ComandoSQL.CommandText = procedimientoAlmacenado;
            ComandoSQL.CommandType = CommandType.StoredProcedure;
            
            ComandoSQL.Parameters.Add("@idoperacionesdetalle", SqlDbType.Int).Value = IdOperacionesDetalle;
            ComandoSQL.Parameters.Add("@tipooperacion", SqlDbType.VarChar).Value = TipoOperacion;

            ComandoSQL.Connection = sqlConnection1;

            sqlConnection1.Open();
            SQLReader = ComandoSQL.ExecuteReader();

            while (SQLReader.Read())
            {
                Fila = GridPedidos.Rows.Add();
                GridPedidos.Rows[Fila].Cells["Folio"].Value = SQLReader.GetInt32(SQLReader.GetOrdinal("ID_OPERACIONES_DETALLE")).ToString();
                //GridPedidos.Rows[Fila].Cells["Tipo"].Value = SQLReader.GetInt32(reader.GetOrdinal("TIPO_OPERACIONES")).ToString();
                GridPedidos.Rows[Fila].Cells["UR"].Value = SQLReader.GetString(SQLReader.GetOrdinal("ID_UR")).ToString();
                GridPedidos.Rows[Fila].Cells["Descripcion"].Value = SQLReader.GetString(SQLReader.GetOrdinal("DESCRIPCION")).ToString();
                GridPedidos.Rows[Fila].Cells["Fecha"].Value = SQLReader.GetDateTime(SQLReader.GetOrdinal("FECHA")).ToString();
                GridPedidos.Rows[Fila].Cells["Proveedor"].Value = SQLReader.GetInt32(SQLReader.GetOrdinal("ID_PROVEEDOR")).ToString();

            }

        }

        private void cargarGridPedidosD(string Descripcion, string TipoOperacion)
        {


            string procedimientoAlmacenado = "CONSULTAR_OPERACIONES_DETALLE_DESCRIPCION";
            SqlConnection sqlConnection1 = new SqlConnection(ClassFunciones.CadenaConexion);
            SqlCommand ComandoSQL = new SqlCommand();
            SqlDataReader SQLReader;

            Descripcion = "%" + Descripcion + "%";
            ComandoSQL.CommandText = procedimientoAlmacenado;
            ComandoSQL.CommandType = CommandType.StoredProcedure;

            ComandoSQL.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Descripcion;
            ComandoSQL.Parameters.Add("@tipooperacion", SqlDbType.VarChar).Value = TipoOperacion;


            ComandoSQL.Connection = sqlConnection1;

            sqlConnection1.Open();
            SQLReader = ComandoSQL.ExecuteReader();

            while (SQLReader.Read())
            {
                Fila = GridPedidos.Rows.Add();
                GridPedidos.Rows[Fila].Cells["Folio"].Value = SQLReader.GetInt32(SQLReader.GetOrdinal("ID_OPERACIONES_DETALLE")).ToString();
                //GridPedidos.Rows[Fila].Cells["Tipo"].Value = SQLReader.GetInt32(reader.GetOrdinal("TIPO_OPERACIONES")).ToString();
                GridPedidos.Rows[Fila].Cells["UR"].Value = SQLReader.GetString(SQLReader.GetOrdinal("ID_UR")).ToString();
                GridPedidos.Rows[Fila].Cells["Descripcion"].Value = SQLReader.GetString(SQLReader.GetOrdinal("DESCRIPCION")).ToString();
                GridPedidos.Rows[Fila].Cells["Fecha"].Value = SQLReader.GetDateTime(SQLReader.GetOrdinal("FECHA")).ToString();
                GridPedidos.Rows[Fila].Cells["Proveedor"].Value = SQLReader.GetInt32(SQLReader.GetOrdinal("ID_PROVEEDOR")).ToString();

            }

        }


        private void GridPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex > -1)
            {
                filaSeleccionada = GridPedidos.CurrentCell.RowIndex;
                IdPedido = Convert.ToInt32(GridPedidos.Rows[filaSeleccionada].Cells["Folio"].Value.ToString());
                ClassFunciones.IdPedidoSeleccionado = IdPedido;
                string fechaSTR = GridPedidos.Rows[filaSeleccionada].Cells["Fecha"].Value.ToString();
                string concepto = GridPedidos.Rows[filaSeleccionada].Cells["Descripcion"].Value.ToString();
                MostrarPedido(IdPedido, fechaSTR, concepto);

            }
        }

        private void MostrarPedido(int IdOperaciones_detalle, string fecha, string concepto)
        {
            FrmNuevaRequisicion FormNuevaRequisicion = new FrmNuevaRequisicion();
            int FilaPedido = 0;


            string procedimientoAlmacenado = "CONSULTAR_OPERACIONES_ID_OPERACIONES_DETALLE";
            SqlConnection sqlConnection2 = new SqlConnection(ClassFunciones.CadenaConexion);
            SqlCommand ComandoSQLmp = new SqlCommand();
            SqlDataReader SQLReadermp;


            ComandoSQLmp.CommandText = procedimientoAlmacenado;
            ComandoSQLmp.CommandType = CommandType.StoredProcedure;
            ComandoSQLmp.Parameters.Add("@idoperaciones_detalle", SqlDbType.Int).Value = IdOperaciones_detalle;
            ComandoSQLmp.Connection = sqlConnection2;

            sqlConnection2.Open();
            SQLReadermp = ComandoSQLmp.ExecuteReader();

            while (SQLReadermp.Read())
            {
                FilaPedido = FormNuevaRequisicion.gridPedido.Rows.Add();
                //Cargar encabezados del detalle

                FormNuevaRequisicion.txtFolio.Text = SQLReadermp.GetInt32(SQLReadermp.GetOrdinal("ID_OPERACIONES")).ToString();
                FormNuevaRequisicion.dateFecha.Value = SQLReadermp.GetDateTime(SQLReadermp.GetOrdinal("FECHA_MOVIMIENTO"));
                FormNuevaRequisicion.txtDescripcionP.Text = SQLReadermp.GetString(SQLReadermp.GetOrdinal("DESCRIPCION")).ToString(); ;

                //Cargar datos del grid detalle
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["idcog"].Value = SQLReadermp.GetString(SQLReadermp.GetOrdinal("ID_COG")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["cog"].Value = SQLReadermp.GetString(SQLReadermp.GetOrdinal("COG")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["descripcion"].Value = SQLReadermp.GetString(SQLReadermp.GetOrdinal("DESCRIPCION")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["unidad"].Value = SQLReadermp.GetString(SQLReadermp.GetOrdinal("UNIDAD")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["cantidad"].Value = SQLReadermp.GetSqlDouble (SQLReadermp.GetOrdinal("CANTIDAD")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["PU"].Value = SQLReadermp.GetSqlMoney(SQLReadermp.GetOrdinal("PU")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["monto"].Value = SQLReadermp.GetSqlMoney(SQLReadermp.GetOrdinal("MONTO")).ToString();
                ///FormOrdenCompra.gridPedido.Rows[FilaPedido].Cells["idunidad"].Value = SQLReader.GetString(SQLReader.GetOrdinal("ID_UNIDAD")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["idprograma"].Value = SQLReadermp.GetString(SQLReadermp.GetOrdinal("ID_PROGRAMA")).ToString();
                ///FormOrdenCompra.gridPedido.Rows[FilaPedido].Cells["idproveedor"].Value = SQLReader.GetString(SQLReader.GetOrdinal("ID_PROVEEDOR")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["estatus"].Value = SQLReadermp.GetInt32(SQLReadermp.GetOrdinal("STATUS_SALDO")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["idoi"].Value = SQLReadermp.GetString(SQLReadermp.GetOrdinal("ID_ORIGEN_INGRESO")).ToString();
                FormNuevaRequisicion.gridPedido.Rows[FilaPedido].Cells["idregistro"].Value = SQLReadermp.GetInt32(SQLReadermp.GetOrdinal("ID_REGISTRO")).ToString();
            }

            //FormOrdenCompra.MdiParent = this;

            FormNuevaRequisicion.Tag = "S";

            FormNuevaRequisicion.Show();
        }

        private void toolFolio_Click(object sender, EventArgs e)
        {

        }

        private void toolFolio_KeyPress(object sender, KeyPressEventArgs e)
        {



        }

        private void toolFolio_TextChanged(object sender, EventArgs e)
        {
            if (toolFolio.Text.ToString() == "*") 
            {
                GridPedidos.Rows.Clear();
                cargarGridPedidosIdOD(0, "REQUISICION");

            }
            else if (toolFolio.Text.ToString() != "")
            {
                GridPedidos.Rows.Clear();
                cargarGridPedidosIdOD(Convert.ToInt32(toolFolio.Text.ToString()), "REQUISICION");
            }

        }

        private void toolDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void toolDescripcion_TextChanged(object sender, EventArgs e)
        {
            if (toolDescripcion.Text.ToString() == "*")
            {
                GridPedidos.Rows.Clear();
                cargarGridPedidosD("", "REQUISICION");

            }
            else if (toolDescripcion.Text.ToString() != "")
            {
                GridPedidos.Rows.Clear();
                cargarGridPedidosD(toolDescripcion.Text.ToString(), "REQUISICION");
            }

        }
    }
}
