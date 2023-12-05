using AutoMapper;
using PaymentSystem.Core.DTOs;
using PaymentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Utility
{
    public class PaymentSystemProfile : Profile
    {
        public PaymentSystemProfile()
        {
            CreateMap<CustomerRequestDto, Customer>();
            CreateMap<Customer, CustomerResponseDto>();
            CreateMap<CustomerEditRequestDto, Customer>();
        }
    }
}
