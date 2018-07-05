using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleValidator
{
    [UserValidation]
    public class Customer
    {

        [Required(ErrorMessage = "{0} is a mandatory field")]
        [StringLength(maximumLength: 50, MinimumLength = 10,
        ErrorMessage = "The property {0} should have {1} maximum characters and {2} minimum characters")]
        public string Name { get; set; }

        [Range(typeof(DateTime), "01/01/1900", "01/01/2014",
   ErrorMessage = "Valid dates for the Property {0} between {1} and {2}")]
        public DateTime EntryDate { get; set; }

        [CustomValidation(typeof(CustomerWeekendValidation), nameof(CustomerWeekendValidation.WeekendValidate))]
        public DateTime EntryDate2 { get; set; }

        [ControlDateTime(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, ErrorMessage = "The {0} isn't valid")]
        public DateTime EntryDate3 { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int Age { get; set; }
        [MaxLength(2, ErrorMessage = "The property {0} doesn't have more than {1} elements")]
        public int[] ArrayInt { get; set; }

        [Compare("Customer.Password", ErrorMessage = "The fields Password and PasswordConfirmation should be equals")]
        public string PasswordConfirmation { get; set; }

        public void Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this, null, null);

            if (!Validator.TryValidateObject(this, context, results, true))
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
