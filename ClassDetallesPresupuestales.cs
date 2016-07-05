using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAPG
{
    class ClassDetallesPresupuestales
    {
        private static double _MontoTotalOrigen;
        private static double _MontoTotalImportado;
        private static double _MontoDiferencia;
        private static int _RegistrosImportados = 0;

        private static bool _RealizarAjustes;
        public double MontoTotalOrigen
        {
            get { return _MontoTotalOrigen; }
            set { _MontoTotalOrigen = value; }
        }
        public double MontoTotalImportado
        {
            get { return _MontoTotalImportado; }
            set { _MontoTotalImportado = value; }
        }

        public double MontoDiferencia
        {
            get { return _MontoDiferencia; }
            set { _MontoDiferencia = value; }
        }
        public bool RealizarAjustes
        {
            get { return _RealizarAjustes; }
            set { _RealizarAjustes = value;}
        }
        public int RegistrosImportados
        {
            get { return _RegistrosImportados; }
            set  { _RegistrosImportados = value; }

        }
    }
}
