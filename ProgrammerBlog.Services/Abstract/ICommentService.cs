using ProgrammerBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<int>> Count();

        Task<IDataResult<int>> CountNonDeleteds();
    }
}
