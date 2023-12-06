using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PaymentSystem.Core.DTOs
{
    public class MerchantUpdateRequestDto
    {
        [Required]
        public string BusinessIdNumber { get; set; }

        [Required]
        public string BusinessName { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        public string ContactSurname { get; set; }

        [Required]
        public DateTime DateOfEstablishment { get; set; }
    }
}
