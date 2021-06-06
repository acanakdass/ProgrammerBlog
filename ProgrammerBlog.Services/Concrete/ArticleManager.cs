using AutoMapper;
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
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            //articleId idsine sahip makaleyi, user ve category bilgisi ile dbden getir.
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId, a => a.User, a => a.Category);
            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, "Böyle bir makale bulunamadı.", null);

        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, a => a.Category, a => a.User);
            if (articles.Count > -1)
            {
                var articleListDto = new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                };
                return new DataResult<ArticleListDto>(ResultStatus.Success, articleListDto);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makale Bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.IsAnyAsync(c => c.Id == categoryId);
            if (result)
            {

                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
                if (articles.Count > -1)
                {
                    var articleListDto = new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    };
                    return new DataResult<ArticleListDto>(ResultStatus.Success, articleListDto);
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Makale Bulunamadı", null);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Böyle bir kategori Bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllNonDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, a => a.Category, a => a.User);
            if (articles.Count > -1)
            {
                var articleListDto = new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                };
                return new DataResult<ArticleListDto>(ResultStatus.Success, articleListDto);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makale Bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllNonDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, a => a.Category, a => a.User);
            if (articles.Count > -1)
            {
                var articleListDto = new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                };
                return new DataResult<ArticleListDto>(ResultStatus.Success, articleListDto);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makale Bulunamadı", null);
        }

        public async Task<IResult> Add(ArticleAddDto articleAddDto, string creatorName)
        {
            //Article dönecek bir map
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreaterName = creatorName;
            article.ModifierName = creatorName;
            article.UserId = 1;

            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{articleAddDto.Title} başlıklı makale başarıyla eklendi.");
        }

        public async Task<IResult> Delete(int articleId, string modifierName)
        {
            var result = await _unitOfWork.Articles.IsAnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                article.ModifierName = modifierName;
                article.IsDeleted = true;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla silindi.");
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makale Bulunamadı", null);

        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.IsAnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();


                return new Result(ResultStatus.Success, $"{article.Title} başlıklı makale başarıyla kalıcı olarak silindi.");
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makale Bulunamadı", null);

        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifierName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifierName = modifierName;
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, $"{articleUpdateDto.Title} başlıklı makale başarıyla güncellendi.");
        }
    }
}
