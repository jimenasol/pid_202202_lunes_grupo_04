﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class Extensiones
    {

        public static int ToInt(this object obj)
        {
            int entero = 0;
            int.TryParse(obj.ToString(), out entero);
            return entero;
        }

        public static decimal ToDecimal(this object obj)
        {
            decimal numero = 0;
            decimal.TryParse(obj.ToString(), out numero);
            return numero;
        }

        public static DateTime? ToDateTime(this object obj)
        {
            DateTime fecha = new DateTime();
            DateTime.TryParse(obj.ToString(), out fecha);
            if (fecha == DateTime.MinValue) return null;
            else return fecha;
        }


        public static bool ToBool(this object obj)
        {
            bool boleano = new bool();
            bool.TryParse(obj.ToString(), out boleano);
            return boleano;
        }

    }
}
