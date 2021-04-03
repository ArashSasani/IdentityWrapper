using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CMS.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool trackByTheContext = true);
        TEntity GetById(int id, bool trackByTheContext = true);
        bool Exists(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
        /// <summary>
        /// execute any query against specified entity type in the database
        /// </summary>
        /// <param name="query">sql query to execute</param>
        /// <param name="trackByTheContext">use or not using EF AsNoTracking method (for read-only scenarios)</param>
        /// <param name="parameters">any parameter values you supply will automatically be 
        /// converted to a DbParameter to protect against sql injection</param>
        /// <returns></returns>
        IEnumerable<TEntity> ExecQuery(string query, bool trackByTheContext = true
            , params object[] parameters);
    }
}
