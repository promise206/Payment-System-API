﻿using System;
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
        public DateTime DateOfEstablishment { get; set; }
    }
}
