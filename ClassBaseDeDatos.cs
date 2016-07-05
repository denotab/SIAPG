using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; 

namespace SIAPG
{
    class ClassBaseDeDatos
    {

        public static SqlConnection MyConnectionDB;
        public static Boolean _ConexionEstablecida;
        public static string _ErrorDeConexion = "";
 

        private static int idusuario; 

        public void ConectarBaseDeDatos()
        {
            Properties.Settings LogginBd = new Properties.Settings();
            string MyConection;
            _ConexionEstablecida = false;
            _ErrorDeConexion = "";
            bool exito = true;

            try
            {
                MyConection = "User ID = " + LogginBd.Login_Usuario + "; Password=" + LogginBd.Login_Contraseña+ ";" +
                    "Initial Catalog=" + LogginBd.Login_Bd + ";" + " Data Source = " + LogginBd.Login_Servidor;

                MyConnectionDB = new SqlConnection(MyConection);
                MyConnectionDB.Open();
                _ConexionEstablecida = true;
                
            }
            catch (SqlException  ExceptionSql)
            {
                _ErrorDeConexion = ExceptionSql.ToString();
            }            
        }

        public bool ConexionEstablecida
        {
            get { return _ConexionEstablecida; }
        }

        public string ErrorDeConexion
        {
            get { return _ErrorDeConexion; }
        }




    }
}
