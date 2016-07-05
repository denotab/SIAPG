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
    class ClassPedidos
    {
        public static decimal saldo;
        public static decimal saldoResto;
        public static OrdenCompra FormPedidos = new OrdenCompra();
        public static int TienePresupuesto(string idcog, string idur, int idprograma, decimal monto, DateTime fecha)
        {
            int tieneP = 0;
            string NombreProcedimiento = "CONSULTA_SI_TIENE_PRESUPUESTO";
            ///decimal saldoResto =0;
            saldoResto = 0;
            try
            {
                SqlDataAdapter AdaptadorSql = new SqlDataAdapter(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                AdaptadorSql.SelectCommand.CommandType = CommandType.StoredProcedure;
                AdaptadorSql.SelectCommand.Parameters.Add("@idCOG", SqlDbType.VarChar);
                AdaptadorSql.SelectCommand.Parameters.Add("@idUR", SqlDbType.VarChar);
                AdaptadorSql.SelectCommand.Parameters.Add("@idPrograma", SqlDbType.Int);
                ///AdaptadorSql.SelectCommand.Parameters.Add("@Programa", SqlDbType.VarChar);
                AdaptadorSql.SelectCommand.Parameters["@idCOG"].Value = idcog;
                AdaptadorSql.SelectCommand.Parameters["@idUR"].Value = idur;
                AdaptadorSql.SelectCommand.Parameters["@idPrograma"].Value = idprograma;
                DataSet dataset = new DataSet();
                AdaptadorSql.Fill(dataset, "query_SI_TIENE_PRESUPUESTO");
                DataTable Midatatable = dataset.Tables["query_SI_TIENE_PRESUPUESTO"];

                int numRows = Midatatable.Rows.Count;
                if (numRows > 0)
                {
                    DateTime Ahora = fecha;
                    ///decimal saldo = 0;
                    saldo = 0;
                    decimal ejercido;

                    switch (Ahora.Month)
                    {
                        case 1:
                            ///***COMPARAR SALDO CON MOVIMIENTO
                            if (Midatatable.Rows[0]["ENERO_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["ENERO_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["ENERO"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["FEBRERO"]) + Convert.ToDecimal(Midatatable.Rows[0]["MARZO"]) + Convert.ToDecimal(Midatatable.Rows[0]["ABRIL"]) + Convert.ToDecimal(Midatatable.Rows[0]["MAYO"]) + Convert.ToDecimal(Midatatable.Rows[0]["JUNIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["JULIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["AGOSTO"]) + Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]) ;
                            break;
                        case 2:
                            if (Midatatable.Rows[0]["FEBRERO_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["FEBRERO_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["FEBRERO"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["MARZO"]) + Convert.ToDecimal(Midatatable.Rows[0]["ABRIL"]) + Convert.ToDecimal(Midatatable.Rows[0]["MAYO"]) + Convert.ToDecimal(Midatatable.Rows[0]["JUNIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["JULIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["AGOSTO"]) + Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 3:
                            if (Midatatable.Rows[0]["MARZO_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["MARZO_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["MARZO"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["ABRIL"]) + Convert.ToDecimal(Midatatable.Rows[0]["MAYO"]) + Convert.ToDecimal(Midatatable.Rows[0]["JUNIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["JULIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["AGOSTO"]) + Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 4:
                            if (Midatatable.Rows[0]["ABRIL_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["ABRIL_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["ABRIL"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["MAYO"]) + Convert.ToDecimal(Midatatable.Rows[0]["JUNIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["JULIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["AGOSTO"]) + Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 5:
                            if (Midatatable.Rows[0]["MAYO_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["MAYO_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["MAYO"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["JUNIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["JULIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["AGOSTO"]) + Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 6:
                            if (Midatatable.Rows[0]["JUNIO_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows [0]["JUNIO_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["JUNIO"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["JULIO"]) + Convert.ToDecimal(Midatatable.Rows[0]["AGOSTO"]) + Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 7:
                            if (Midatatable.Rows[0]["JULIO_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["JULIO_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["JULIO"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["AGOSTO"]) + Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 8:
                            if (Midatatable.Rows[0]["AGOSTO_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["AGOSTO_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["AGOSTO"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 9:
                            if (Midatatable.Rows[0]["SEPTIEMBRE_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["SEPTIEMBRE"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 10:
                            if (Midatatable.Rows[0]["OCTUBRE_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["OCTUBRE"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) + Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 11:
                            if (Midatatable.Rows[0]["NOVIEMBRE_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["NOVIEMBRE"]) - ejercido;
                            saldoResto = Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]);
                            break;
                        case 12:
                            if (Midatatable.Rows[0]["DICIEMBRE_EJERCIDO"].ToString() == "")
                            {
                                ejercido = 0;
                            }
                            else
                            {
                                ejercido = Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE_EJERCIDO"]);
                            }
                            saldo = Convert.ToDecimal(Midatatable.Rows[0]["DICIEMBRE"]) - ejercido;
                            saldoResto = 0;
                            break;
                    }



                    if (monto < saldo)
                    {
                        tieneP = 1;
                    }
                    else if (monto<(saldo+saldoResto))
                    {
                        tieneP = 2;
                        return tieneP;
                    }
                    else 
                    {
                        tieneP = 3;
                        MessageBox.Show("Excede el presupuesto anual");
                        return tieneP;
                    }


                }


            }
            catch (SqlException MyException)
            {
                tieneP = 0;
                MessageBox.Show(MyException.Message);
                return tieneP;
            }

            return tieneP;
        }



        public static int ObtenerIDOperaciones(String tabla)
        {
            int id = 0;
            SqlCommand ComandoSQLIdo;
            string NombreProcedimiento = "OBTENER_ID_TABLA";
            try
            {
                ///Enlazamos el CommandSql a ComandoSQL con la Base y Procedimiento
                ComandoSQLIdo = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                ComandoSQLIdo.CommandType = CommandType.StoredProcedure;
                ///Enviamos los parametros al procedimiento almacenado
                ComandoSQLIdo.Parameters.Add("@tabla", SqlDbType.VarChar);
                ComandoSQLIdo.Parameters["@tabla"].Value = tabla;
                id = Convert.ToInt32(ComandoSQLIdo.ExecuteScalar());

            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(sqlexception.Message.ToString(), "Ha ocurrido un error para obtener el id de la tabla: " + tabla, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ///id=0;
            }
            finally
            {
                ComandoSQLIdo = null;
            }

            return id;
        }

        public static int ObtenerIDTabla(String tabla)
        {
            int id = 0;
            SqlCommand ComandoSQL;
            string NombreProcedimiento = "OBTENER_ID_TABLA";
            try
            {
                ///Enlazamos el CommandSql a ComandoSQL con la Base y Procedimiento
                ComandoSQL = new SqlCommand(NombreProcedimiento, ClassBaseDeDatos.MyConnectionDB);
                ComandoSQL.CommandType = CommandType.StoredProcedure;
                ///Enviamos los parametros al procedimiento almacenado
                ComandoSQL.Parameters.Add("@tabla", SqlDbType.VarChar);
                ComandoSQL.Parameters["@tabla"].Value = tabla;
                id = Convert.ToInt32(ComandoSQL.ExecuteScalar());

            }
            catch (SqlException sqlexception)
            {
                MessageBox.Show(sqlexception.Message.ToString(), "Ha ocurrido un error para obtener el id de la tabla: " + tabla, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ///id=0;
            }
            finally
            {
                ComandoSQL = null;
            }

            return id;
        }











    }


}
