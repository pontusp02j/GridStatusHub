using GridStatusHub.Domain.Entities;

namespace GridStatusHub.Domain.Context {
    public interface IGridSystemRepo<GridSystem> : IBaseRepo<GridSystem> 
    {
        Task<IEnumerable<GridSystem>> GetAllGridSystemsAndCellsAsync();
        Task<GridSystem> GetGridSystemWithCellsAsync(int id);
        Task<GridSystem> GetGridSystemByNameAsync(string name);
        Task<bool> UpdateGridSystemNameAsync(int id, string newName);
        Task<(int, List<GridCell>)> InsertGridAndCellsAsync(GridSystem gridSystem, List<GridCell> cells);
        Task<bool> SoftDeleteGridSystemByIdAsync(int id);
    }
}