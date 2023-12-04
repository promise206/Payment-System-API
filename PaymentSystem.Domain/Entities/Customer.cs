using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Domain.Entities
{
    public class Customer : IdentityUser
    {
        public long Id { get; set; }

        [Required, StringLength(11, ErrorMessage = "National id must have a maximum length of 11", MinimumLength = 1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "National id can only contain numerical values.")]
        public string NationalId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string CustomerNumber { get; set; }

        public string TransactionHistory { get; set; }
    }
}
