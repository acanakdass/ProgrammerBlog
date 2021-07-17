using Microsoft.AspNetCore.Http;
using ProgrammerBlog.Entities.ComplexTypes;
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
        Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null);
        IDataResult<ImageDeletedDto> Delete(string imageName);
    }
}
