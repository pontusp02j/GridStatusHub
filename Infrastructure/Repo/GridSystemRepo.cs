using System.Data;
using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace GridStatusHub.Infra.Repo
{
    public class GridSystemRepo : BaseRepo<GridSystem>, IGridSystemRepo<GridSystem>
    {
        private readonly IBaseRepo<GridCell> _gridCellRepo;

        public GridSystemRepo(IDbConnection connection, ILogger<GridSystemRepo> logger, 
            IBaseRepo<GridCell> gridCellRepo) 
            : base(connection, logger)
        {
            _gridCellRepo = gridCellRepo;
        }

        public async Task<GridSystem> GetGridSystemWithCellsAsync(int id)
        {
            var gridSystem = await base.GetByIdAsync(id);

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
