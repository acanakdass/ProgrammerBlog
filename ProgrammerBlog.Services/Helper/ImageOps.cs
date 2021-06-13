using Microsoft.AspNetCore.Http;
using ProgrammerBlog.Shared.Utilities.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Helper
{
    public static class ImageOps
    {


        public static async Task<string> ImageUpload(string userName, IFormFile imageFile)
        {
            //string fileName = Path.GetFileNameWithoutExtension(userAddDto.ImageFile.FileName);  //file ismini uzantısı .jpg vs olmadan alır
            string fileExtention = Path.GetExtension(imageFile.FileName);
            DateTime datetime = DateTime.Now;
            string fileName = $"{userName}_{datetime.FullDateAndTimeStringWithUnderscore()}{fileExtention}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\userImages", fileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName;

        }



        public static bool DeleteImage(string fileName)
        {
            fileName = "sdafsdffsd_323_35_45_20_13_6_2021.jpg";
            string fileToDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\userImages", fileName);
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            else
            {
                return false;
            }
        }





    }
}
