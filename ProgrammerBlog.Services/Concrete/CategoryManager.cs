using ProgrammerBlog.Data.Abstract;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Services.Abstract;
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

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string creatorName)
        {
            var categoryToAdd = new Category
            {
                Name = categoryAddDto.Name,
                Description = categoryAddDto.Description,
                Note = categoryAddDto.Note,
                IsActive = categoryAddDto.IsActive,
                CreaterName = creatorName,
                ModifierName = creatorName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = false
            };

            await _unitOfWork.Categories.AddAsync(categoryToAdd)
                .ContinueWith(t=>_unitOfWork.SaveAsync()); //save changes in db
            //continuewith: çok hızlı bir şekilde,beklemeden sıradaki aksiyona geçer
            
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklendi.");
        }

        public async Task<IResult> Delete(int categoryId, string modifierName)
        {
            var categoryToDelete = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (categoryToDelete != null)
            {
                categoryToDelete.IsDeleted = true;
                categoryToDelete.ModifierName = modifierName;
                categoryToDelete.ModifiedDate= DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(categoryToDelete).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{categoryToDelete.Name} adlı kategori başarıyla silindi.");
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<Category>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<Category>(ResultStatus.Success, category);
            }
            return new DataResult<Category>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<IList<Category>>> GetAll(int categoryId)
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles); //include articles
            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success, categories);
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Kategori bulunamadı", null); //data:null
        }

        public async Task<IDataResult<IList<Category>>> GetAllNonDeleted(int categoryId)
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            //            IsDeleted olmayan kategorileri makalaleri ile birlikte çek. 
            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success, categories);
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Kategori bulunamadı.", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var categoryToDelete = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (categoryToDelete != null)
            {
                await _unitOfWork.Categories.DeleteAsync(categoryToDelete).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{categoryToDelete.Name} adlı kategori kalıcı olarak silindi.");
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifierName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            if (category != null)
            {
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive= categoryUpdateDto.IsActive;
                category.IsDeleted= categoryUpdateDto.IsDeleted;
                category.ModifierName = modifierName;
                category.ModifiedDate = DateTime.Now;

                await _unitOfWork.Categories.UpdateAsync(category)
                    .ContinueWith(t=>_unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellendi.");
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);

        }
    }
}
