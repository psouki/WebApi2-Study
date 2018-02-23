using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BeerDev.Repository.Interfaces
{
    public interface IRepositoryBase<T> where T : class 
    {
        void Add(T obj);
        T GetById(int id);
        ICollection<T> GetAll();
        ICollection<T> GetAll(params Expression<Func<T, object>>[] includes);
        void Update(T obj);
        T Get(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includess);
        void Complete();
    }
}
