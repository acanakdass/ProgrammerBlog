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
            return new DataResult<ArticleDto>(ResultStatus.Error, Messages.ArticleMessages.NotFound(isPlural:false), null);

        }

        public async Task<IDataResult<int>> Count()
        {
            var articlesCount = await _unitOfWork.Articles.CountAsync();
            if (articlesCount> -1)
            {
                return new DataResult<int>(ResultStatus.Success, articlesCount);
            }
            else
            {

                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata oluştu!", -1);
            }
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
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.ArticleMessages.NotFound(isPlural: true), null);
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
                return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.ArticleMessages.NotFound(isPlural: true), null);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.CategoryMessages.NotFound(isPlural: false), null);
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
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.ArticleMessages.NotFound(isPlural: true), null);
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
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.ArticleMessages.NotFound(isPlural: true), null);
        }


        /// <summary>
        /// Verilen Id parametresine ait ArticleUpdateDto nesnesini DataResult içerisinde döndürür.
        /// </summary>
        /// <param name="articleId"> Güncellenecek makaleye ait Id bilgisi.</param>
        /// <returns>Asenkron olarak DataResult içerisinde ArticleUpdateDto döndürür.</returns>
        public async Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDto(int articleId)
        {
            var result = await _unitOfWork.Articles.IsAnyAsync(c => c.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(c => c.Id == articleId);
                //article nesnemizi ArticleUpdateDto'ya çeviren automapper işlemi
                var articleUpdateDto = _mapper.Map<ArticleUpdateDto>(article);
                return new DataResult<ArticleUpdateDto>(ResultStatus.Success, articleUpdateDto);
            }
            return new DataResult<ArticleUpdateDto>(ResultStatus.Error, Messages.ArticleMessages.NotFound(isPlural: false), null);
        }


        public async Task<IResult> Add(ArticleAddDto articleAddDto, string creatorName,int userId)
        {
            //Article dönecek bir map
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreaterName = creatorName;
            article.ModifierName = creatorName;
            article.UserId = userId;

            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.ArticleMessages.Added(articleTitle: article.Title));
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

                return new Result(ResultStatus.Success, Messages.ArticleMessages.Deleted(articleTitle: article.Title));
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.ArticleMessages.NotFound(isPlural: false), null);

        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.IsAnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();


                return new Result(ResultStatus.Success, Messages.ArticleMessages.HardDeleted(articleTitle: article.Title));
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.ArticleMessages.NotFound(isPlural: false), null);

        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifierName)
        {
            var oldArticle = await _unitOfWork.Articles.GetAsync(a => a.Id == articleUpdateDto.Id);

            var article = _mapper.Map<ArticleUpdateDto,Article>(articleUpdateDto,oldArticle);  //articleUpdateDto 'da olmayan verileri oldArticle'dakiler ile doldur.
            article.ModifierName = modifierName;
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, Messages.ArticleMessages.Updated(articleTitle:article.Title));
        }

        public async Task<IDataResult<int>> CountNonDeleteds()
        {
            var articlesCount = await _unitOfWork.Articles.CountAsync(a=>!a.IsDeleted);
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, articlesCount);
            }
            else
            {

                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata oluştu!", -1);
            }
        }
    }
}
