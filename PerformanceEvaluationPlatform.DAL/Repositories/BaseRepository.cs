using Dapper;
using DapperParameters;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly DatabaseOptions _databaseOptions;
        protected readonly PepDbContext DbContext;

        protected BaseRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
        {
            DbContext = dbContext;
            _databaseOptions = databaseOptions?.Value ?? throw new ArgumentNullException(nameof(databaseOptions));
            if (databaseOptions.Value.SqlConnectionString is null)
            {
                throw new ArgumentNullException(nameof(databaseOptions.Value.SqlConnectionString));
            }
        }
        protected async Task<IList<TResult>> ExecuteSp<TResult>(string spName, object parameters)
        {
            using (IDbConnection dbConnection = new SqlConnection(_databaseOptions.SqlConnectionString))
            {
                var mappedParameters = MapParameters(parameters);
                var result = await dbConnection.QueryAsync<TResult>(spName, mappedParameters, commandType: CommandType.StoredProcedure);
                return result.AsList();
            }
        }

        protected async Task<IList<TResult>> ExecuteSp<TFirst, TSecond, TThird, TResult>(string spName, object parameters, Func<TFirst, TSecond, TThird, TResult> resultMappingFunc, string splitOn)
        {
            using (IDbConnection dbConnection = new SqlConnection(_databaseOptions.SqlConnectionString))
            {
                var mappedParameters = MapParameters(parameters);
                var result = await dbConnection.QueryAsync<TFirst, TSecond, TThird, TResult>(spName, resultMappingFunc, mappedParameters, splitOn: splitOn, commandType: CommandType.StoredProcedure);
                return result.AsList();
            }
        }

        protected async Task<TEntity> Get<TEntity>(int id) where TEntity: class
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        protected async Task Create<TEntity>(TEntity entity)
        {
            await DbContext.AddAsync(entity);
        }

        public Task SaveChanges()
        {
            return DbContext.SaveChangesAsync();
        }
        protected void Delete<TEntity>(TEntity entity)
        {
            DbContext.Remove(entity);
        }

        private DynamicParameters MapParameters(object model) 
        {
            var parameters = new DynamicParameters();
            var allProperties = model.GetType().GetProperties().ToList();            
            foreach (var property in allProperties)
            {
                if (IsIntCollection(property))
                {
                    var intListTableTypeParameter = ConvertToIntListTableType((property.GetValue(model, null) as ICollection<int>));
                    parameters.AddTable(property.Name, "[IntList]", intListTableTypeParameter);
                }
                else
                {
                    parameters.Add(property.Name, property.GetValue(model, null));
                }
            }
            return parameters;
        }

        private bool IsIntCollection(PropertyInfo property) 
        {
            return property.PropertyType.IsAssignableFrom(typeof(ICollection<int>));
        }

        private ICollection<IntListTableType> ConvertToIntListTableType(ICollection<int> items) 
        {
            var intListItems = new List<IntListTableType>();

            if (items is null)
            {
                return intListItems;
            }

            foreach (var item in items)
            {
                intListItems.Add(new IntListTableType { Id = item });
            }
            return intListItems;
        }
    }
}
