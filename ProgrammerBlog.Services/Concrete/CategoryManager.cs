using AutoMapper;
using ProgrammerBlog.Data.Abstract;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Services.Abstract;
using ProgrammerBlog.Services.Utilities;
using ProgrammerBlog.Shared.Utilities.Results.Abstract;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammerBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /// <summary>
        /// Verilen CategoryAddDto nesnesi ve CreaterName parametresi ile yeni bit "Category" ekler
        /// </summary>
        /// <param name="categoryAddDto">CategoryAddDto tipinde eklenecek kategori</param>
        /// <param name="creatorName">String tipinde kategoriyi ekleyen kullanıcının ismi</param>
        /// <returns>"Category" ekleme işleminin başarılı olup olmadığı bilgisini(ResultStatus), duruma göre gerekli mesajı ve eklenen kategoriye ait "CategoryDto nesnesini döndürür.</returns>
        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string creatorName)
        {
            var categoryToAdd = _mapper.Map<Category>(categoryAddDto);
            categoryToAdd.CreaterName = creatorName;
            categoryToAdd.ModifierName = creatorName;

            var addedCategory = await _unitOfWork.Categories.AddAsync(categoryToAdd);
            await _unitOfWork.SaveAsync();

            var addedCategoryDto = new CategoryDto
            {
                Category = addedCategory,
                ResultStatus = ResultStatus.Success,
                Message = Messages.CategoryMessages.Added(categoryName: addedCategory.Name)
            };

            return new DataResult<CategoryDto>(ResultStatus.Success, Messages.CategoryMessages.Added(categoryName: addedCategory.Name), addedCategoryDto);
        }

        public async Task<IDataResult<int>> Count()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync();
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {

                return new DataResult<int>(ResultStatus.Error,"Beklenmeyen bir hata oluştu!",-1);
            }
        }


        public async Task<IDataResult<int>> CountNonDeleteds()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync(c=>!c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {

                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata oluştu!", -1);
            }
        }



        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifierName)
        {
            var categoryToDelete = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (categoryToDelete != null)
            {
                categoryToDelete.IsDeleted = true;
                categoryToDelete.ModifierName = modifierName;
                categoryToDelete.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(categoryToDelete);
                await _unitOfWork.SaveAsync();
                var deletedCategoryDto = new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.CategoryMessages.Deleted(categoryName: deletedCategory.Name)
                };

                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, Messages.CategoryMessages.Deleted(categoryName: deletedCategory.Name), deletedCategoryDto);
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.CategoryMessages.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.CategoryMessages.NotFound(isPlural: false)
            });
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                var categoryDto = new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                };
                return new DataResult<CategoryDto>(ResultStatus.Success, categoryDto);
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.CategoryMessages.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.CategoryMessages.NotFound(isPlural: false)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles); //include articles
            if (categories.Count > -1)
            {
                var categoryListDto = new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,

                };
                return new DataResult<CategoryListDto>(ResultStatus.Success, categoryListDto);
            }
            var ErrorDto = new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.CategoryMessages.NotFound(isPlural: true)
            };
            return new DataResult<CategoryListDto>(ResultStatus.Error, ErrorDto); //data:null
        }


        public async Task<IDataResult<CategoryListDto>> GetAllNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles); //include articles
            if (categories.Count > -1)
            {
                var categoryListDto = new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,

                };
                return new DataResult<CategoryListDto>(ResultStatus.Success, categoryListDto);
            }
            var ErrorDto = new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.CategoryMessages.NotFound(isPlural: true)
            };
            return new DataResult<CategoryListDto>(ResultStatus.Error, ErrorDto); //data:null
        }



        public async Task<IDataResult<CategoryListDto>> GetAllNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles); //include articles
            if (categories.Count > -1)
            {
                var categoryListDto = new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                };
                return new DataResult<CategoryListDto>(ResultStatus.Success, categoryListDto);
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.CategoryMessages.NotFound(isPlural: true), null); //data:null
        }

        /// <summary>
        /// Verilen Id parametresine ait CategoryUpdateDto nesnesini döndürür.
        /// </summary>
        /// <param name="categoryId"> Güncellenecek kategoriye ait Id bilgisi.</param>
        /// <returns>Asenkron olarak DataResult içerisinde CategoryUpdateDto döndürür.</returns>
        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var result = await _unitOfWork.Categories.IsAnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                //category nesnemizi categoryUpdateDto'ya çeviren automapper işlemi
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            return new DataResult<CategoryUpdateDto>(ResultStatus.Error, Messages.CategoryMessages.NotFound(isPlural: false), null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var categoryToDelete = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (categoryToDelete != null)
            {
                await _unitOfWork.Categories.DeleteAsync(categoryToDelete);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, Messages.CategoryMessages.HardDeleted(categoryName: categoryToDelete.Name));
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, Messages.CategoryMessages.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifierName)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category = _mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
            category.ModifierName = modifierName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);

            var updatedCategoryDto = new CategoryDto
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = Messages.CategoryMessages.Updated(categoryName: updatedCategory.Name)
            };


            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, Messages.CategoryMessages.Updated(categoryName: updatedCategory.Name), updatedCategoryDto);
        }
    }
}
