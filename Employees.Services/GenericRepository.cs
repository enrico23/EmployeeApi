using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Employees.Services
{
    public abstract class GenericRepository<C, T> : IGenericRepository<T>
        where T : class
        where C : DbContext, new()
    {

        private C _db = new C();
        public C Context
        {
            get { return _db; }
            set { _db = value; }
        }

        /// <summary>
        /// Generic Get - no filters
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _db.Set<T>();
            return query.ToList();
        }


        public virtual IEnumerable<T> Get(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = _db.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual T GetByID(object id)
        {
            return _db.Set<T>().Find(id);
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _db.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {

            _db.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {

            _db.SaveChanges();
        }

        public int Count()
        {
            return _db.Set<T>().Count();
        }


        public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            return _db.Set<T>().SqlQuery(query, parameters).ToList();
        }

        public void ExecuteStoredProcedure(string query, params object[] parameters)
        {

            _db.Database.ExecuteSqlCommand(query, parameters);
        }


        #region dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
                if (disposing)
                    _db.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
