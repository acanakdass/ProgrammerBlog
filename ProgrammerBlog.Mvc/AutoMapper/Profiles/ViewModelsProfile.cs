using AutoMapper;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.AutoMapper.Profiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<ArticleAddViewModel, ArticleAddDto>();
            CreateMap<ArticleUpdateDto, ArticleUpdateViewModel>().ReverseMap();
            //CreateMap<ArticleUpdateViewModel, ArticleUpdateDto>();  ReverseMap sayesinde bu işlemi yazmamıza gerek kalmaz

        }
    }
}
