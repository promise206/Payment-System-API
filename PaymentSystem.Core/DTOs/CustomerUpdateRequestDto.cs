using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.DTOs
{
    public class CustomerUpdateRequestDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string CustomerNumber { get; set; }

        public string TransactionHistory { get; set; }
    }
}
