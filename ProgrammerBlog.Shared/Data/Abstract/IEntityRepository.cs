using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Shared.Entities.Abstract
{
    public interface IEntityRepository<Type> where Type:class,IEntity,new()
    {
        Task<Type> GetAsync(Expression<Func<Type, bool>> predicate, params Expression<Func<Type, object>>[] includeProperties);

        Task<IList<Type>> GetAllAsync(Expression<Func<Type, bool>> predicate =null, params Expression<Func<Type, object>>[] includeProperties);
        //predicate null verildi, null gelmez ise gelen bilgiye göre filtrelenecek

        Task AddAsync(Type entity);

        Task UpdateAsync(Type entity);

        Task DeleteAsync(Type entity);

        Task<bool> IsAnyAsync(Expression<Func<Type, bool>> predicate);

        Task<int> CountAsync(Expression<Func<Type, bool>> predicate);
    }
}
