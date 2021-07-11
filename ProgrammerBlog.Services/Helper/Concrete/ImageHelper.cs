using Microsoft.AspNetCore.Http;
using ProgrammerBlog.Entities.ComplexTypes;
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
        private string userImagesFolder= "userImages";
        private string postImagesFolder= "postImages";

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

        public async Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null)
        {

            /* Eğer folderName değişkeni null gelir ise, o zaman resim tipine göre (ImageType) klasör adı ataması yapılır. */
            if (folderName == null)
            {
                folderName = imageType == ImageType.User ? userImagesFolder : postImagesFolder;
            }


            /* Eğer folderName değişkeni ile gelen klasör adı sistemimizde mevcut değilse, yeni bir klasör oluşturulur. */
            if (!Directory.Exists($"wwwroot/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"wwwroot/{imgFolder}/{folderName}");
            }

            /* Resimin yüklenme sırasındaki ilk adı oldFileName adlı değişkene atanır. */
            string oldfileName = Path.GetFileNameWithoutExtension(imageFile.FileName);  //file ismini uzantısı .jpg vs olmadan alır
            /* Resimin uzantısı fileExtension adlı değişkene atanır. */

            string fileExtension = Path.GetExtension(imageFile.FileName);
            DateTime datetime = DateTime.Now;

            /*
            // Parametre ile gelen değerler kullanılarak yeni bir resim adı oluşturulur.
            // Örn: AhmetAkdas_587_5_38_12_3_10_2020.png
            */
            string newFileName = $"{name}_{datetime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";

            /* Kendi parametrelerimiz ile sistemimize uygun yeni bir dosya yolu (path) oluşturulur. */
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{imgFolder}/{folderName}", newFileName);

            /* Sistemimiz için oluşturulan yeni dosya yoluna resim kopyalanır. */
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
            string message = imageType == ImageType.User ? $"{name} adlı kullanıcı fotoğrafı başarıyla yüklendi" : $"{name} adlı makale resmi başarıyla yüklendi";
            return new DataResult<ImageUploadedDto>(
                ResultStatus.Success,
                message,
                imageUploadedDto
                );
        }


    }
}
