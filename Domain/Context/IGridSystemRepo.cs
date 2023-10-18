namespace GridStatusHub.Domain.Context {
    public interface IGridSystemRepo<GridSystem> : IBaseRepo<GridSystem> 
    {
        Task<IEnumerable<GridSystem>> GetAllGridSystemsAndCellsAsync();
        Task<GridSystem> GetGridSystemWithCellsAsync(int id);
    }
}