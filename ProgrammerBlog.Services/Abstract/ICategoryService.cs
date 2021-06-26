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

        /// <summary>
        /// Verilen Id parametresine ait CategoryUpdateDto nesnesini döndürür.
        /// </summary>
        /// <param name="categoryId"> Güncellenecek kategoriye ait Id bilgisi.</param>
        /// <returns>Asenkron olarak DataResult içerisinde CategoryUpdateDto döndürür.</returns>
        Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId);

        Task<IDataResult<CategoryListDto>> GetAll();

        Task<IDataResult<CategoryListDto>> GetAllNonDeleted();

        Task<IDataResult<CategoryListDto>> GetAllNonDeletedAndActive();


        /// <summary>
        /// Verilen CategoryAddDto nesnesi ve CreaterName parametresi ile yeni bit "Category" ekler
        /// </summary>
        /// <param name="categoryAddDto">CategoryAddDto tipinde eklenecek kategori</param>
        /// <param name="creatorName">String tipinde kategoriyi ekleyen kullanıcının ismi</param>
        /// <returns>"Category" ekleme işleminin başarılı olup olmadığı bilgisini(ResultStatus), duruma göre gerekli mesajı ve eklenen kategoriye ait "CategoryDto nesnesini döndürür.</returns>
        Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string creatorName);

        Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifierName);

        Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifierName); //make ısDeleted true

        Task<IResult> HardDelete(int categoryId);

        Task<IDataResult<int>> Count();

        Task<IDataResult<int>> CountNonDeleteds();

    }
}
