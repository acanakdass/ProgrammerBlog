﻿using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> Get(int articleId);

        Task<IDataResult<ArticleListDto>> GetAll();

        Task<IDataResult<ArticleListDto>> GetAllNonDeleted();

        Task<IDataResult<ArticleListDto>> GetAllNonDeletedAndActive();
        
        Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId);

        Task<IResult> Add(ArticleAddDto articleAddDto, string creatorName);

        Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifierName);

        Task<IResult> Delete(int articleId, string modifierName); //make ısDeleted true
        Task<IResult> HardDelete(int articleId);
    }
}