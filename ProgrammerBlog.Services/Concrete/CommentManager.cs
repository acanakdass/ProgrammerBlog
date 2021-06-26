using ProgrammerBlog.Data.Abstract;
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
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<int>> Count()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync();
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {

                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata oluştu!", -1);
            }
        }
        public async Task<IDataResult<int>> CountNonDeleteds()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync(cm=>!cm.IsDeleted);
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {

                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata oluştu!", -1);
            }
        }
    }
}
