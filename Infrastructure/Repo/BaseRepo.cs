using System.Data;
using Dapper.Contrib.Extensions;
using GridStatusHub.Domain.Context;
using Microsoft.Extensions.Logging;


namespace GridStatusHub.Infra.Repo {
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected readonly IDbConnection _connection;
        protected readonly ILogger<BaseRepo<T>> _logger;

        public BaseRepo(IDbConnection connection, ILogger<BaseRepo<T>> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _connection.GetAllAsync<T>();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all entities of type {EntityType}.", typeof(T).Name);
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _connection.GetAsync<T>(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving an entity of type {EntityType} with ID {Id}.", typeof(T).Name, id);
                throw;
            }
        }

        public async Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                return await _connection.InsertAsync(entity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting an entity of type {EntityType}.", typeof(TEntity).Name);
                throw;
            }
        }

        public async Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                return await _connection.UpdateAsync(entity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating an entity of type {EntityType}.", typeof(T).Name);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if(entity == null) return false;
                return await _connection.DeleteAsync(entity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an entity of type {EntityType} with ID {Id}.", typeof(T).Name, id);
                throw;
            }
        }
    }
}
