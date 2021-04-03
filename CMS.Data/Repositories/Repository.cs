using CMS.Core.Interfaces;
using CMS.Data.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace CMS.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class, IEntity
    {
        private readonly AuthContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AuthContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null
            , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            , string includeProperties = "", bool trackByTheContext = true)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //for using ef eager loading
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (!trackByTheContext)
            {
                return query.AsNoTracking();
            }
            else
            {
                return query;
            }
        }

        public TEntity GetById(int id, bool trackByTheContext = true)
        {
            if (!trackByTheContext)
            {
                return _dbSet.AsNoTracking().SingleOrDefault(q => q.Id == id);
            }
            return _dbSet.SingleOrDefault(q => q.Id == id);
        }

        public bool Exists(int id)
        {
            return _dbSet.Any(q => q.Id == id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            Commit();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Commit();
        }

        public void Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            Commit();
        }

        public IEnumerable<TEntity> ExecQuery(string query, bool trackByTheContext = true
            , params object[] parameters)
        {
            var result = _dbSet.SqlQuery(query, parameters);
            if (!trackByTheContext)
                result.AsNoTracking().AsEnumerable();
            return result.AsEnumerable();
        }

        private void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                System.Diagnostics.Debug.WriteLine("exception message: {0}, inner exception message: {1}"
                    , ex.Message, ex.InnerException != null
                    ? ex.InnerException.Message : "[empty]");
                throw;
            }
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
