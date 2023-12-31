namespace GridStatusHub.Domain.Context {
    public interface IBaseRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> DeleteAsync(int id);
    }
}