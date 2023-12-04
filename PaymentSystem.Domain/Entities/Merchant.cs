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
        [Required]
        public string MerchantNumber { get; set; }

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
        public string DateOfEstablishment { get; set; }
    }
}
