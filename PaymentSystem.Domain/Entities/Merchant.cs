using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Domain.Entities
{
    public class Merchant
    {
        [Key]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Merchant Number can only contain numerical values.")]
        public long MerchantNumber { get; set; }

        [Required]
        public string BusinessIdNumber { get; set; }

        [Required]
        public string BusinessName { get; set; }

        [Required]
        public string ContactName { get; set; }

        public string AverageTransactionVolume { get; set; }

        [Required]
        public string ContactSurname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Establishment")]
        [DateLessThanAYearAgo(ErrorMessage = "Date must be more than 1 year ago.")]
        public DateTime DateOfEstablishment { get; set; }
    }

    public class DateLessThanAYearAgoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                // Check if the date is less than 1 year ago
                if (date >= DateTime.Now.AddYears(-1))
                {
                    return new ValidationResult("Date must be more than 1 year ago.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
