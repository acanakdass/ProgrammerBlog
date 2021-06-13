using AutoMapper;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();   //UserAddDto sınıfını User sınıfına çevir
        }
    }
}
