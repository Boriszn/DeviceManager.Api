using DeviceManager.Api.Attributes.Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DeviceManager.Api.Helpers.Dapper
{
    /// <summary>
    /// This is a helper class to build dapper queries
    /// </summary>
    public static class QueryBuilderHelper
    {
        /// <summary>
        /// Builds generic dapper find query
        /// </summary>
        /// <typeparam name="T">Type of the model</typeparam>
        /// <returns></returns>
        public static string GetFindQuery<T>()
        {
            var keyProperty = GetPropertiesWithAttribute<T>(typeof(KeyAttribute)).FirstOrDefault();
            var tableName = GetTableName<T>();
            if (keyProperty == null)
                throw new KeyNotFoundException($"Key property not found for table {tableName}");
            return $"SELECT * FROM {tableName} WHERE {keyProperty} = @{keyProperty}";
        }

        /// <summary>
        /// Builds select query for the table.
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <returns>Select query</returns>
        public static string GetSelectQuery<T>() => $"SELECT * FROM {GetTableName<T>()}";

        /// <summary>
        /// Builds a insert query for the model
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <returns>Insert query</returns>
        public static string GetInsertQuery<T>()
        {
            var insertProperties = GetPropertiesWithAttribute<T>(typeof(DapperInsertAttribute));
            var insertQueryBuilder = new StringBuilder($"INSERT INTO {GetTableName<T>()}(");
            var insertPropertiesBuilder = new StringBuilder();
            bool appendComma = false;
            foreach (var property in insertProperties)
            {
                insertQueryBuilder.Append($"{(appendComma ? "," : string.Empty)} {property.Name}");
                insertPropertiesBuilder.Append($"{(appendComma ? "," : string.Empty)} @{property.Name}");
                appendComma = true;
            }
            insertQueryBuilder.Append($") VALUES({insertPropertiesBuilder.ToString()}); SELECT SCOPE_IDENTITY()");

            return insertQueryBuilder.ToString();
        }


        /// <summary>
        /// Gets the table based on model name. Assumes table name is model name followed by 's'
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <returns>Table name</returns>
        public static string GetTableName<T>() => $"{typeof(T).Name}s";

        /// <summary>
        /// Gets all properties on which <paramref name="attributeType"/> is defined. For eg. <see cref="DapperInsertAttribute"/>.
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="attributeType">Attribute type</param>
        /// <returns>Property list</returns>
        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(Type attributeType)
        {
            return typeof(T).GetProperties().Where(property => Attribute.IsDefined(property, attributeType)).OrderBy(x => x.Name);
        }
    }
}
