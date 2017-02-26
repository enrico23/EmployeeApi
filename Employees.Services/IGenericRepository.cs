using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Services
{
    public interface IGenericRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = ""
            );

        T GetByID(object id);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Delete(T entity);
        void Edit(T entity);
        void Save();

        int Count();
        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);
        void ExecuteStoredProcedure(string query, params object[] parameters);

    }
}
