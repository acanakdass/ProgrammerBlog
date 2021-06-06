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
        Task<IDataResult<CategoryDto>> Get(int categoryId);

        Task<IDataResult<CategoryListDto>> GetAll();

        Task<IDataResult<CategoryListDto>> GetAllNonDeleted();

        Task<IDataResult<CategoryListDto>> GetAllNonDeletedAndActive();

        Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string creatorName);

        Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifierName);

        Task<IResult> Delete(int categoryId, string modifierName); //make ısDeleted true

        Task<IResult> HardDelete(int categoryId);
    }
}
