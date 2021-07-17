using AutoMapper;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleAddDto, Article>()
                .ForMember(destinationMember => destinationMember.CreatedDate, options => options.MapFrom(x => DateTime.Now));  //ArticleAddDto'yu Article'a dönüştür.
            CreateMap<ArticleUpdateDto, Article>()
                .ForMember(destinationMember => destinationMember.ModifiedDate, options => options.MapFrom(x => DateTime.Now)); //ArticleUpdateDto'yu Article'a dönüştür.
            CreateMap<Article, ArticleUpdateDto>();
        }
    }
}
