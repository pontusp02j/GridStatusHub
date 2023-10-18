namespace GridStatusHub.Domain.Context {
    public interface IGridSystemRepo<GridSystem> : IBaseRepo<GridSystem> 
    {
        Task<GridSystem> GetGridSystemWithCellsAsync(int id);
    }
}