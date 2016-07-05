using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAPG
{
    class ClassFunciones
    {
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

    }

}
