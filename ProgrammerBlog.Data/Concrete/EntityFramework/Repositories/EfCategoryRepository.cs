using Microsoft.EntityFrameworkCore;
using ProgrammerBlog.Data.Abstract;
using ProgrammerBlog.Data.Concrete.EntityFramework.Context;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Shared.Data.Concrete.EntityFramework;
using ProgrammerBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
        } 
        private ProgrammerBlogContext _programmerBlogContext { get { return _context as ProgrammerBlogContext; } }

        public async Task<Category> GetById(int categoryId)
        {
            return await _programmerBlogContext.Categories.Where(c => c.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}
