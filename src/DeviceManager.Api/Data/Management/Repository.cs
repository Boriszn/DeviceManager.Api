using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeviceManager.Api.Data.Management
{
    /// <summary>
    /// Generic repository, contains CRUD operation of EF entity
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class Repository<T> : IRepository<T>
        where T : class
    {
        /// <summary>
        /// EF data base context
        /// </summary>
        private readonly IDbContext context;

        /// <summary>
        /// Used to query and save instances of
        /// </summary>
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(IDbContext context)
        {
            this.context = context;

            this.dbSet = context.Set<T>();
        }

        /// <inheritdoc />
        public virtual EntityState Add(T entity)
        {
            return this.dbSet.Add(entity).State;
        }

        /// <inheritdoc />
        public T Get<TKey>(TKey id)
        {
            return this.dbSet.Find(id);
        }

        /// <inheritdoc/>
        public T Get<TKey, TProperty>(TKey id, Expression<Func<T, TProperty>> navigationPropertyPath) where TProperty : class
        {
            var entity = this.dbSet.Find(id);
            if (entity == null) return null;
            this.context.Entry(entity).Reference(navigationPropertyPath).Load();
            return entity;
        }

        /// <inheritdoc />
        public async Task<T> GetAsync<TKey>(TKey id)
        {
            return await this.dbSet.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<T> GetAsync<TKey, TProperty>(TKey id, Expression<Func<T, TProperty>> navigationPropertyPath) where TProperty : class
        {
            var entity = await this.dbSet.FindAsync(id);
            if (entity == null) return null;
            await this.context.Entry(entity).Reference(navigationPropertyPath).LoadAsync();
            return entity;
       }

        /// <inheritdoc />
        public T Get(params object[] keyValues)
        {
            return this.dbSet.Find(keyValues);
        }

        /// <inheritdoc />
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Where(predicate);
        }

        /// <inheritdoc />
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string include)
        {
            return this.FindBy(predicate).Include(include);
        }

        /// <inheritdoc />
        public IQueryable<T> GetAll()
        {
            return this.dbSet;
        }

        /// <inheritdoc />
        public IQueryable<T> GetAll(int page, int pageCount)
        {
            var pageSize = (page - 1) * pageCount;

            return this.dbSet.Skip(pageSize).Take(pageCount);
        }

        /// <inheritdoc />
        public IQueryable<T> GetAll<TProperty>(int page, int pageCount, Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            var pageSize = (page - 1) * pageCount;

            return this.dbSet.Include(navigationPropertyPath).Skip(pageSize).Take(pageCount);
        }

        /// <inheritdoc />
        public IQueryable<T> GetAll(string include)
        {
            return this.dbSet.Include(include);
        }

        /// <inheritdoc />
        public IQueryable<T> FromSql(string query, params object[] parameters)
        {
            return this.dbSet.FromSql(query, parameters);
        }

        /// <inheritdoc />
        public IQueryable<T> GetAll(string include, string include2)
        {
            return this.dbSet.Include(include).Include(include2);
        }

        /// <inheritdoc />
        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Any(predicate);
        }

        /// <inheritdoc />
        public IQueryable<T> FromSql(string sqlQuery)
        {
            return this.dbSet.FromSql(sqlQuery);
        }

        /// <inheritdoc />
        public EntityState Delete(T entity)
        {
            return this.dbSet.Remove(entity).State;
        }

        /// <inheritdoc />
        public virtual EntityState Update(T entity)
        {
            return this.dbSet.Update(entity).State;
        }
    }
}
