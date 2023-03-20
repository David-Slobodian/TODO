using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TODO.Dal.Entities;

namespace TODO.Dal.Repos
{
    public interface IGenericRepository<T> where T: BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> DeleteById(Guid id);
        Task<bool> Update(T entity);
        Task<Guid> Add(T entity);
        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);
    }
}
