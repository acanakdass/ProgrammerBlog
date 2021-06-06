﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

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
                Message = $"{ categoryAddDto.Name } adlı kategori başarıyla eklendi."
            };

            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklendi.", addedCategoryDto);
        }

        public async Task<IResult> Delete(int categoryId, string modifierName)
        {
            var categoryToDelete = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (categoryToDelete != null)
            {
                categoryToDelete.IsDeleted = true;
                categoryToDelete.ModifierName = modifierName;
                categoryToDelete.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(categoryToDelete);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, $"{categoryToDelete.Name} adlı kategori başarıyla silindi.");
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
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
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir kategori bulunamadı."
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
                Message = "Kategori bulunamadı"
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
                    ResultStatus = ResultStatus.Success
                };
                return new DataResult<CategoryListDto>(ResultStatus.Success, categoryListDto);
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori bulunamadı", null); //data:null
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
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori bulunamadı", null); //data:null
        }




        public async Task<IResult> HardDelete(int categoryId)
        {
            var categoryToDelete = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (categoryToDelete != null)
            {
                await _unitOfWork.Categories.DeleteAsync(categoryToDelete);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, $"{categoryToDelete.Name} adlı kategori kalıcı olarak silindi.");
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifierName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifierName = modifierName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);

            var updatedCategoryDto = new CategoryDto
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{ categoryUpdateDto.Name } adlı kategori başarıyla eklendi."
            };


            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellendi.", updatedCategoryDto);
        }
    }
}
