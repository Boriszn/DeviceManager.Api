using System;
using System.Collections.Generic;
using System.Data;

namespace DeviceManager.Api.Data.Management.Dapper
{
    /// <summary>
    /// Dapper Unit of work for maintaining the transaction
    /// </summary>
    public class DapperUnitOfWork : IDapperUnitOfWork, IDisposable
    {
        private IDbConnection connection;
        private IDbTransaction transaction;
        private bool disposed;

        /// <summary>
        /// The repositories
        /// </summary>
        private Dictionary<Type, object> repositories;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionFactory">The connection factory.</param>
        public DapperUnitOfWork(IConnectionFactory connectionFactory)
        {
            this.connection = connectionFactory.Connection;
        }

        /// <inheritdoc />
        public void BeginTransaction()
        {
            connection.Open();
            transaction = connection.BeginTransaction();
        }

        /// <inheritdoc />
        public IDapperRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!this.repositories.ContainsKey(type))
            {
                this.repositories[type] = new DapperRepository<TEntity>(transaction);
            }

            return (IDapperRepository<TEntity>)this.repositories[type];
        }

        /// <inheritdoc />
        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
                transaction = connection.BeginTransaction();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }
                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }
                disposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ~DapperUnitOfWork()
        {
            Dispose(false);
        }
    }
}
