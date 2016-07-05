using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKCONTPAQNGLib;

namespace SIAPG
{
    

    class classSDK
    {
        public static TSdkSesion objSDKSesion = new TSdkSesion();

        public static string empresa = string.Empty;
        public static int resultado;

        public static void iniciarSesion()
        {

           
        //Verificar la conexión con el SDK Contpaq
            if (objSDKSesion.conexionActiva == 0) //Si no se ha iniciado la sesión
            {
                objSDKSesion.iniciaConexion();
                }
            if (objSDKSesion.conexionActiva == 1 && objSDKSesion.ingresoUsuario == 0) //Si hay conexion pero no ha ingresado el usuario
            {
                objSDKSesion.firmaUsuario();
            }
            if (objSDKSesion.conexionActiva == 1 && objSDKSesion.ingresoUsuario == 1)  //Todo salió OK
            {
                //Abrir empresa
                abrirEmpresa();
            }
        } //fin método iniciarSesion

        public static void abrirEmpresa()
        {
 
            empresa = "ctPRUEBA";
            resultado = objSDKSesion.abreEmpresa(empresa);
            if (resultado == 0) //No se abrió la empresa
            {
                Console.WriteLine("Error: " + objSDKSesion.UltimoMsjError);
                Console.ReadLine();
                ///menu();
            }
            else //Abrió ok
            {
                Console.WriteLine("Se ha abierto correctamente la empresa: " + empresa);
                Console.ReadLine();
                ///menu();
            }
        }



    }

}
