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

        public IDataResult<ImageDeletedDto> DeleteUserImage(string imageName)
        {
            
            var fileToDelete = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{imgFolder}/", imageName);
            if (System.IO.File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto
                {
                    Fullname = imageName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                System.IO.File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Success, imageDeletedDto);
            }
            else
            {
                return new DataResult<ImageDeletedDto>(ResultStatus.Error, "Böyle bir dosya bulunamadı", null);
            }
        }

        public async Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile imageFile, string folderName="userImages")
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

            var imageUploadedDto = new ImageUploadedDto
            {
                Fullname = $"{folderName}/{newFileName}",
                Oldname = oldfileName,
                Extension = fileExtension,
                Path = path,
                Size = imageFile.Length
            };
            return new DataResult<ImageUploadedDto>(
                ResultStatus.Success,
                $"{userName} adlı kullanıcı fotoğrafı başarıyla yüklendi",
                imageUploadedDto
                );
        }
    }
}
