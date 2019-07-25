using Dapper;
using DeviceManager.Api.Attributes.Dapper;
using DeviceManager.Api.Helpers.Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Data.Management.Dapper
{
    /// <summary>
    /// Implementation of Generic Dapper repository
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class DapperRepository<T> : DapperRepositoryBase, IDapperRepository<T>
    {
        /// <summary>
        /// Initializes instance of transaction
        /// </summary>
        /// <param name="transaction"></param>
        public DapperRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        /// <inherit/>
        public async Task<Guid> AddAsync(T entity)
        {

            if (entity == null)
                throw new ArgumentNullException(nameof(T));

            var paramObject = new ExpandoObject();
            foreach(var property in QueryBuilderHelper.GetPropertiesWithAttribute<T>(typeof(DapperInsertAttribute)))
            {
                paramObject.TryAdd(property.Name, property.GetValue(entity));
            }
            
            return await Connection.ExecuteScalarAsync<Guid>(QueryBuilderHelper.GetInsertQuery<T>(), paramObject, Transaction);

        }
         
        /// <inherit/>
        public async Task<IList<T>> AllAsync(int page, int pageCount)
        {
            var records =  await Connection.QueryAsync<T>(QueryBuilderHelper.GetSelectQuery<T>(), transaction: Transaction);
            return records.ToList();
        }

        /// <inherit/>
        public async Task<T> FindAsync(Guid id)
        {
            var query = QueryBuilderHelper.GetFindQuery<T>();
            var paramObject = new ExpandoObject();
            paramObject.TryAdd(QueryBuilderHelper.GetPropertiesWithAttribute<T>(typeof(KeyAttribute)).FirstOrDefault().Name, id);
            var record = await Connection.QueryAsync<T>(query, param: paramObject, transaction: Transaction);
            return record.FirstOrDefault();
        }

        /// <inherit/>
        public void RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        /// <inherit/>
        public void RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inherit/>
        public void UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
