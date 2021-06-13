using ProgrammerBlog.Shared.Utilities.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Shared.Utilities.Helpers
{
    public static class ImageDelete
    {

        public static async Task<bool> DeleteImage(string fileName)
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
