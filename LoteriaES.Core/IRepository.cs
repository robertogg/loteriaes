using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaES.Core
{
    public interface IRepository<T> where T:class
    {
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
