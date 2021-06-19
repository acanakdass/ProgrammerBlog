using Microsoft.AspNetCore.Http;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Services.Helpers.Abstract;
using ProgrammerBlog.Shared.Utilities.Extentions;
using ProgrammerBlog.Shared.Utilities.Results.Abstract;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammerBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly string imgFolder = "img";

        public async Task<IDataResult<UploadedImageDto>> UploadUserImage(string userName, IFormFile imageFile, string folderName="userImages")
        {
            if (!Directory.Exists($"wwwroot/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"wwwroot/{imgFolder}/{folderName}");
             }
            string oldfileName = Path.GetFileNameWithoutExtension(imageFile.FileName);  //file ismini uzantısı .jpg vs olmadan alır
            string fileExtension = Path.GetExtension(imageFile.FileName);
            DateTime datetime = DateTime.Now;
            string newFileName = $"{userName}_{datetime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{imgFolder}/{folderName}", newFileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var imageUploadedDto = new UploadedImageDto
            {
                Fullname = $"{folderName}/{newFileName}",
                Oldname = oldfileName,
                Extension = fileExtension,
                Path = path,
                Size = imageFile.Length
            };
            return new DataResult<UploadedImageDto>(
                ResultStatus.Success,
                $"{userName} adlı kullanıcı fotoğrafı başarıyla yüklendi",
                imageUploadedDto
                );
        }
    }
}
