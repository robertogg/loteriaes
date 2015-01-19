using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaES.Core
{
    public interface IRepository<T> where T:Entity
    {
        IQueryable<T> GetAll();
        T Get(int id);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void PublishEvents(T entity);
    }
}
