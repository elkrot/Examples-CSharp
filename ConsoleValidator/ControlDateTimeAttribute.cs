using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleValidator
{
    public class ControlDateTimeAttribute : ValidationAttribute
    {
        private DayOfWeek[] NotValidDays;
        private bool ThrowExcepcion;

        public ControlDateTimeAttribute(params DayOfWeek[] notValidDays)
        {
            ThrowExcepcion = false;
            NotValidDays = notValidDays;
        }

        public ControlDateTimeAttribute(bool throwExcepcion, params DayOfWeek[] notValidDays)
        {
            ThrowExcepcion = throwExcepcion;
            NotValidDays = notValidDays;
        }


        public override bool IsValid(object value)
        {
            DateTime fecha;

            if (!DateTime.TryParse(value.ToString(), out fecha))
            {
                if (ThrowExcepcion)
                    throw new ArgumentException(
                        "The ControlDateTimeAttribute, only validate DateTime Types.");
                else
                    return false;
            }

            return NotValidDays.Contains(fecha.DayOfWeek);
        }
    }
}
