using AutoMapper;
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
            /*CreateMap<RegistrationDTO, AppUser>()
                 .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email.ToLower()))
                 .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.Email.ToLower()));
            CreateMap<GetProfileDTO, AppUser>().ReverseMap();
            CreateMap<AppUser, GetUserDTO>().ReverseMap();*/
        }
    }
}
