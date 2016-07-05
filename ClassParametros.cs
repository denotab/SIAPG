using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions; 

namespace SIAPG
{
    class ClassParametros
    {

        public static double _PorcentajeEnero;
        public static double _PorcentajeFebrero;
        public static double _PorcentajeMarzo;
        public static double _PorcentajeAbril;
        public static double _PorcentajeMayo;
        public static double _PorcentajeJunio;
        public static double _PorcentajeJulio;
        public static double _PorcentajeAgosto;
        public static double _PorcentajeSeptiembre;
        public static double _PorcentajeOctubre;
        public static double _PorcentajeNoviembre;
        public static double _PorcentajeDiciembre;
        public static string _IDCOG;
        public static string _IDCOGCELDA;
        public static string _COGCELDA;
        public static bool _IngresoAlternativo;   // indica que el formulario se ejecuta para realizar una accion única. ().
        public static int _IdRowGrid;
        public static int _IdPoliza;
        public static bool _NuevaHojaDePresupuesto;
        public static bool _NuevaHojaDeIngreso;
        public static long _IdHojaPresupuesto;
        public static long _IdHojaIngreso;

        public static bool _ProcesoCorrecto;
        public static bool _ProcesoCancelado;
        public static bool _GuardarSobreHojaExistente;
        public static bool _GuardarComo;
        public static bool _CerrandoCargaPresupuestaria = false;
        public static bool _CerrandoCargaIngreso = false;

        public static string _RutaArchivoPresupuesto;
        public static string _NombreHojaPresupuesto;
        public static string _RutaArchivoIngreso;
        public static string _NombreHojaIngreso;
        public static bool _ProcesoEjecutado;


        public static IFormatProvider _MiRegionProvider = new CultureInfo("es-MX", false);

        private static string[] _ColumnasExcel = new string[] { "A", "B", "C", "D", "E", "F", "G","H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S","T", "U", "V", "W", "X", "Y", "Z"};


        public enum _Catalogo : Int32
        {
            TipoDeCuenta = 1,      
            ClasificacionDocumental = 2,
            UnidadResponsable_UR = 3,
            FuenteDeFinanciamiento_OR = 4, 
            Programa = 5, 
            ClasificacionObjetodelGasto_COG = 6, 
            Finalidad = 7, 
            Funcion = 8, 
            Subfuncion = 9,
            CuentaGenero = 10, 
            CuentaGrupo = 11,
            CuentaRubro = 12
        }

        public struct _PorcentajeCOG
        {
            public string IDCOG;
            public string ENERO;
            public string FEBRERO;
            public string MARZO;
            public string ABRIL;
            public string MAYO;
            public string JUNIO;
            public string JULIO;
            public string AGOSTO;
            public string SEPTIEMBRE;
            public string OCTUBRE;
            public string NOVIEMBRE;
            public string DICIEMBRE;
             
            public _PorcentajeCOG(string  _IDCOG, string _ENERO, string _FEBRERO, string _MARZO, string _ABRIL, string _MAYO, string _JUNIO, string _JULIO, string _AGOSTO, string _SEPTIEMBRE, string _OCTUBRE, string _NOVIEMBRE, string _DICIEMBRE)
            {
                IDCOG = _IDCOG;
                ENERO = _ENERO;
                FEBRERO = _FEBRERO;
                MARZO = _MARZO;
                ABRIL = _ABRIL;
                MAYO = _MAYO;
                JUNIO = _JUNIO;
                JULIO = _JULIO;
                AGOSTO = _AGOSTO;
                SEPTIEMBRE = _SEPTIEMBRE;
                OCTUBRE = _OCTUBRE;
                NOVIEMBRE = _NOVIEMBRE;
                DICIEMBRE = _DICIEMBRE;
            }
        }

        public List<_PorcentajeCOG> PORCENTAJECOG = new List<_PorcentajeCOG>();
        public List<int> IdRegistrosEliminados = new List<int>();

        public double PorcentajeEnero
        {
            get { return _PorcentajeEnero; }
            set { _PorcentajeEnero = value; }
        }
        public double PorcentajeFebrero
        {
            get { return _PorcentajeFebrero; }
            set { _PorcentajeFebrero = value; }
        }

        public double PorcentajeMarzo
        {
            get { return _PorcentajeMarzo; }
            set { _PorcentajeMarzo = value; }
        }

        public double PorcentajeAbril
        {
            get { return _PorcentajeAbril; }
            set { _PorcentajeAbril = value; }
        }

        public double PorcentajeMayo
        {
            get { return _PorcentajeMayo; }
            set { _PorcentajeMayo = value; }
        }

        public double PorcentajeJunio
        {
            get { return _PorcentajeJunio; }
            set { _PorcentajeJunio = value; }
        }

        public double PorcentajeJulio
        {
            get { return _PorcentajeJulio; }
            set { _PorcentajeJulio = value; }
        }

        public double PorcentajeAgosto
        {
            get { return _PorcentajeAgosto; }
            set { _PorcentajeAgosto = value; }
        }

        public double PorcentajeSeptiembre
        {
            get { return _PorcentajeSeptiembre; }
            set { _PorcentajeSeptiembre = value; }
        }

        public double PorcentajeOctubre
        {
            get { return _PorcentajeOctubre; }
            set { _PorcentajeOctubre = value; }
        }

        public double PorcentajeNoviembre
        {
            get { return _PorcentajeNoviembre; }
            set { _PorcentajeNoviembre = value; }
        }

        public double PorcentajeDiciembre
        {
            get { return _PorcentajeDiciembre; }
            set { _PorcentajeDiciembre = value; }
        }

        public string IDCOC
        {
            get { return _IDCOG; }
            set { _IDCOG = value; }
        }

        public string IDCOGCELDA
        {
            get { return _IDCOGCELDA; }
            set { _IDCOGCELDA = value; }

        }

        public string COGCELDA
        {
            get { return _COGCELDA; }
            set { _COGCELDA = value; }

        }

        public bool IngresoAlternativo
        {
            get { return _IngresoAlternativo; }
            set { _IngresoAlternativo = value; }

        }

        public int IdRowGrid
        {
            get { return _IdRowGrid; }
            set { _IdRowGrid = value; }
        }
        public  bool IsNumeric(object ObjectToTest)
        {
            if (ObjectToTest == null)
            {
                return false;
            }
            else
            {
                Regex SignoMonedamoneda = new Regex(";,$%");

              //  StringBuilder NueValue = new StringBuilder(SignoMonedamoneda.Replace(ObjectToTest.ToString().Trim(), ""));

                
                double OutValue;
                return double.TryParse(SignoMonedamoneda.Replace(ObjectToTest.ToString().Trim(), ""),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out OutValue);
            }
        }

        public bool NuevaHojaDePresupuesto
        {
            get { return _NuevaHojaDePresupuesto; }
            set { _NuevaHojaDePresupuesto = value; }
        }

        public long  IdHojaPresupuesto
        {
            get { return _IdHojaPresupuesto; }
            set { _IdHojaPresupuesto = value; }
        }


        public bool NuevaHojaDeIngreso
        {
            get { return _NuevaHojaDeIngreso; }
            set { _NuevaHojaDeIngreso = value; }
        }

        public long IdHojaIngreso
        {
            get { return _IdHojaIngreso; }
            set { _IdHojaIngreso = value; }
        }
        public  bool ProcesoCorrecto
        {
            get { return _ProcesoCorrecto; }
            set { _ProcesoCorrecto = value; }
        }

        public bool ProcesoCancelado
        {
            get { return _ProcesoCancelado; }
            set { _ProcesoCancelado = value; }
        }

        public bool GuardarSobreHojaExistente
        {  
            get { return  _GuardarSobreHojaExistente; }
            set { _GuardarSobreHojaExistente = value; }
        }

        public bool GuardarComo
        {  
            get { return _GuardarComo; }
            set { _GuardarComo = value; }
        }

        public bool CerrandoCargaPresupuestaria
        {
            get { return _CerrandoCargaPresupuestaria; }
            set { _CerrandoCargaPresupuestaria = value; }
        }

        public bool CerrandoCargaIngreso
        {
            get { return _CerrandoCargaIngreso; }
            set { _CerrandoCargaIngreso = value; }
        }
        public string NombreHojaPresupuesto
        {
            get { return _NombreHojaPresupuesto; }
            set { _NombreHojaPresupuesto = value; }

        }

        public string NombreHojaIngreso
        {
            get { return _NombreHojaIngreso; }
            set { _NombreHojaIngreso = value; }

        }

        public string RutaArchivoPresupuesto
        {
            get { return _RutaArchivoPresupuesto; }
            set { _RutaArchivoPresupuesto = value; }

        }

        public string RutaArchivoIngreso
        {
            get { return _RutaArchivoIngreso; }
            set { _RutaArchivoIngreso = value; }

        }
        public bool ProcesoEjecutado
        { 
            get { return _ProcesoEjecutado; }
            set { _ProcesoEjecutado = value;  }
        }

        public IFormatProvider MiRegionProvider
        {
            get { return _MiRegionProvider; }
        }

        public string[] ColumnasExcel
        {
            get { return _ColumnasExcel; }
        }
    }
}
