using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            var customer = new Customer();
     //    var result =   customer.ValidateObject();

            customer.Validate();
            Validate(customer);

            Console.ReadKey();
        }

        public static void Validate(Customer user)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
                Console.WriteLine("Пользователь прошел валидацию");
        }
    }
}
