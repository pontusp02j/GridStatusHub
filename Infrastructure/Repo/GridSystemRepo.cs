using System.Data;
using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;

namespace GridStatusHub.Infra.Repo
{
    public class GridSystemRepo : IGridSystemRepo<GridSystem>
    {
        private readonly IBaseRepo<GridSystem> _gridSystemBaseRepo;
        private readonly IBaseRepo<GridCell> _gridCellRepo;

        public GridSystemRepo(IBaseRepo<GridSystem> gridSystemBaseRepo, IBaseRepo<GridCell> gridCellRepo)
        {
            _gridSystemBaseRepo = gridSystemBaseRepo;
            _gridCellRepo = gridCellRepo;
        }

        public async Task<GridSystem> GetGridSystemWithCellsAsync(int id)
        {
            var gridSystem = await _gridSystemBaseRepo.GetByIdAsync(id);

            var gridCells = (await GetAllGridCellsForGridSystemAsync(id)).ToList();
            gridSystem.GridCells = gridCells;

            return gridSystem;
        }

        private async Task<IEnumerable<GridCell>> GetAllGridCellsForGridSystemAsync(int gridSystemId)
        {
            var allGridCells = await _gridCellRepo.GetAllAsync();
            return allGridCells.Where(gc => gc.GridSystemId == gridSystemId);
        }
    }
}
