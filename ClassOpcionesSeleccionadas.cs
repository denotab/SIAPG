using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAPG
{
    class ClassOpcionesSeleccionadas
    {
        static bool _Aplicar;
        static List<ListaSeleccionada> registros;
        public struct ListaSeleccionada
        {
            public string clave;
            public string descripcion;

            public ListaSeleccionada(string _clave, string _descripcion)
            {
                clave = _clave;
                descripcion = _descripcion;
            }
        }
        public List<ListaSeleccionada> RegistrosSeleccionados = new List<ListaSeleccionada>();

        public bool Aplicar
        {
            get { return _Aplicar; }
            set { _Aplicar = value; }
        }


        public List<ListaSeleccionada> guardarlista
        {
            get { return registros; }
            set { registros = value; }
        }

       
      
    }
}
