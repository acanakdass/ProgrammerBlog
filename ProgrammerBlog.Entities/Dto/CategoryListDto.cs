using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Entities.Dto
{
    public class CategoryListDto: DtoGetBase
    {
        public IList<Category> Categories { get; set; }
    }
}
