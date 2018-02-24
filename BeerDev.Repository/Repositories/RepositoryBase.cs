using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BeerDev.Repository.Interfaces;

namespace BeerDev.Repository.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T :class
    {
        protected readonly BeerDbContext Db;
        protected IDbSet<T> DbSet;

        public RepositoryBase()
        {
            Db = new BeerDbContext();
            DbSet = Db.Set<T>();
        }

        public void Add(T obj)
        {
            DbSet.Add(obj);
            Db.SaveChanges();
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public ICollection<T> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public ICollection<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return GetIncludes(includes).ToList();
        }

        public void Update(T obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).FirstOrDefault();
        }

        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return GetIncludes(includes).FirstOrDefault(predicate);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return GetIncludes(includes).Where(predicate).ToList();
        }

        public void Delete(T obj)
        {
            DbSet.Remove(obj);
            Db.SaveChanges();
        }
        private IQueryable<T> GetIncludes(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet.AsNoTracking();
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query;
        }
    }
}
