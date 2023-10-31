using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Interfaces
{
    public interface IRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        //void Update(T obj);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
