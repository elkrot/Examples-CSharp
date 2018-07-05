using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleValidator
{
    public class UserValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Customer user = value as Customer;
                this.ErrorMessage = "Имя  должно быть Alice и возраст одновременно  должен быть равен 33";
                return false;

        }
    }
}
