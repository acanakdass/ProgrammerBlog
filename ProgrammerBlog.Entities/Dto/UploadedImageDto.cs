using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Entities.Dto
{
    public class UploadedImageDto
    {
        public string Fullname{ get; set; }
        public string Oldname{ get; set; }
        public string Extension{ get; set; }
        public string Path{ get; set; }
        public string FolderName{ get; set; }
        public long Size{ get; set; }
    }
}
