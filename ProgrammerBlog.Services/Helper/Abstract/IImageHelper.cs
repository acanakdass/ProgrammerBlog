using Microsoft.AspNetCore.Http;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<UploadedImageDto>> UploadUserImage(string userName, IFormFile imageFile, string folderName = "userImages");
    }
}
