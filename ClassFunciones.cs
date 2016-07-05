using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Security.Cryptography;



namespace SIAPG
{

    
    class ClassFunciones
    {
        public static  string UsuarioActual;
        public static int IdHojaPresupuesto;
        public static int IdPedidoSeleccionado;
        public static string CadenaConexion=@"Data Source=SERVIDOR-PC\SIAPG;Initial Catalog=SIAPG; Integrated Security=True";
        
        public class UsuarioEntity
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string NombreLogin { get; set; }
            public string Password { get; set; }
            public int Id { get; set; }

        }

        public static bool Si_esNumero(object ObjectToTest)
        {
            if (ObjectToTest == null)
            {
                return false;
            }
            else
            {
                double OutValue;
                return double.TryParse(ObjectToTest.ToString().Trim(),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out OutValue);
            }
        }



    public static bool Autenticar(string usuario, string password)
        {
            string sql = @"SELECT COUNT(*)
                      FROM Usuarios
                      WHERE NombreLogin = @nombre AND Password = @password";
            

            ///using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            using(SqlConnection conn = new SqlConnection(@"Data Source=LAC\COMPAC;Initial Catalog=SIAPG;Integrated Security=True"))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@nombre", usuario);

                string hash = Helper.EncodePassword(string.Concat(usuario, password));
                command.Parameters.AddWithValue("@password", hash);

                int count = Convert.ToInt32 (command.ExecuteScalar());

                if (count == 0)
                {
                    
                    return false;
                }
                else
                {
                    UsuarioActual = usuario;
                    return true;
                }                    
            
            }

        }



        public static UsuarioEntity Insert(string nombre, string apellido, string nombreLogin, string password)
        {

            UsuarioEntity usuario = new UsuarioEntity();

            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.NombreLogin = nombreLogin;
            usuario.Password = password;

            return Insert(usuario);
        }

        public static UsuarioEntity Insert(UsuarioEntity usuario)
        {

            string sql = @"INSERT INTO Usuarios (
                           Nombre,
                           Apellido,
                           NombreLogin,
                           Password)
                      VALUES (
                            @Nombre, 
                            @Apellido, 
                            @NombreLogin,
                            @Password)
                    SELECT SCOPE_IDENTITY()";


            ///using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAC-PC\SIAPG;Initial Catalog=SIAPG;Integrated Security=True"))
            {

                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("Apellido", usuario.Apellido);
                command.Parameters.AddWithValue("NombreLogin", usuario.NombreLogin);

                string password = Helper.EncodePassword(string.Concat(usuario.NombreLogin, usuario.Password));
                command.Parameters.AddWithValue("Password", password);

                conn.Open();

                usuario.Id = Convert.ToInt32(command.ExecuteScalar());
                ///command.ExecuteNonQuery();
                return usuario;
            }
        }
        

        internal class Helper
        {
            public static string EncodePassword(string originalPassword)
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();

                byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
                byte[] hash = sha1.ComputeHash(inputBytes);

                return Convert.ToBase64String(hash);
            }
        }
        
    }

}
