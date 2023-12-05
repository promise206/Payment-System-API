using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PaymentSystem.Core.DTOs
{
    public class MerchantResponseDto
    {
        public long MerchantNumber { get; set; }

        public string BusinessIdNumber { get; set; }

        public string BusinessName { get; set; }

        public string ContactName { get; set; }

        public string AverageTransactionVolume { get; set; }

        public string ContactSurname { get; set; }

        public DateTime DateOfEstablishment { get; set; }
    }
}
