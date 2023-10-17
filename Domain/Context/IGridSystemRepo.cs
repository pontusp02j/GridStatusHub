namespace GridStatusHub.Domain.Context {
    public interface IGridSystemRepo<GridSystem>
    {
        Task<GridSystem> GetGridSystemWithCellsAsync(int id);
    }
}