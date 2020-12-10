using System;
using System.Collections.Generic;
using System.Text;
using Abouca.Application.Dto;
using Abouca.Domain.Information;
using Abouca.Domain.User;
using AutoMapper;

namespace Abouca.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<Information, InformationDeleteDto>().ReverseMap();
        }
    }
}
