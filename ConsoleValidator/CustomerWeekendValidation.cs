using System;
using System.ComponentModel.DataAnnotations;

namespace ConsoleValidator
{
    public class CustomerWeekendValidation
    {
        public static ValidationResult WeekendValidate(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday
                ? new ValidationResult("The wekeend days aren't valid")
                : ValidationResult.Success;
        }
    }
}
