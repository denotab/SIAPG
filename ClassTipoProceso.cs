using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAPG
{
    class ClassTipoProceso
    {
        public  static int _TipoProceso; 
        public enum ProcesoAejecutar : int
        {
            EjecutarCocentradoPorUr = 1,
            EjecutarConcentradoPorOrigenIngreso = 2,
            CuadroComparativoPresupuestoIngreso = 3,
            CuadroComparativoPresupuestoIngresoConAjustes = 4,    // Se utiliza para activar el button de ajuste (cuando se ha     importado un presupuesto desde excel con perdidas decimales)
            EjecutarConcentradoPorCOG = 5
        }

        public int TipoProceso
        {
            get { return _TipoProceso; }
            set { _TipoProceso = value;  }
        }
    }
}
