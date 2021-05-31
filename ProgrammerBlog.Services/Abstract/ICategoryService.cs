using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<Category>> Get(int categoryId);
        Task<IDataResult<IList<Category>>> GetAll(int categoryId);
        Task<IDataResult<IList<Category>>> GetAllNonDeleted(int categoryId);
        Task<IResult> Add(CategoryAddDto categoryAddDto,string creatorName);
        Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifierName);
        Task<IResult> Delete(int categoryId,string modifierName); //make ısDeleted true
        Task<IResult> HardDelete(int categoryId);
    }
}
