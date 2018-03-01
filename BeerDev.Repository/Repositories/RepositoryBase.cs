using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BeerDev.Repository.Interfaces;

// One may thing, it is a just example of Web Api?
// In every solution we can perceive complexity as costs
// There are costs unavoidable and accidental so we have to chose our costs
// As I want to keep the Web Api independent I encapsulate the data management here,
// it is not a accidental complexity, It was a well thought decision to make the study flexible and independent
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
        // Usually I don't save the changes here.
        // It's beginning, ending  and disposal are not responsibility of the repository. 
        // Applying the separate of concerns and only responsibility principle of the SOLID
        // the good practice is to use unit of work for those functions.
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

        // As mention in the update method it not a good practice to save chance here
        // but for simplicity sake and because it is a complexity that we don't need in this study
        // the unit of work is not implemented.
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
