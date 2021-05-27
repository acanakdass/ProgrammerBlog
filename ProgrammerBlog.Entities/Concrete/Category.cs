using ProgrammerBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Entities.Concrete
{
    //entityBase'den türeyip Iemtity'i implement ediyor
    public class Category : IEntityRepository,IEntity 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
