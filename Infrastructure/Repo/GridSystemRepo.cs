using System.Data;
using Dapper;
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

        public async Task<GridSystem> GetGridSystemByNameAsync(string name)
        {
            var parameters = new { Name = name };
            
            const string query = @"
                SELECT * 
                FROM gridsystems 
                WHERE Name = @Name 
                AND establishmentDate IS NOT NULL 
                AND establishmentDate <> '0001-01-01T00:00:00'";
            
            return await _connection.QuerySingleOrDefaultAsync<GridSystem>(query, parameters);
        }

        public async Task<IEnumerable<GridSystem>> GetAllGridSystemsAndCellsAsync()
        {
            List<GridSystem> gridSystems = (await base.GetAllAsync()).ToList();

            List<GridCell> allGridCells = (await _gridCellRepo.GetAllAsync()).ToList();

            return gridSystems = gridSystems.Select(gridSystem => {
                gridSystem.GridCells = allGridCells.Where(gc => gc.gridsystemid == gridSystem.id).ToList();
                return gridSystem;
            }).ToList();
        }

        public async Task<GridSystem> GetGridSystemWithCellsAsync(int id)
        {
            var gridSystem = await base.GetByIdAsync(id);

            var gridCells = (await GetAllGridCellsForGridSystemAsync(id)).ToList();
            gridSystem.GridCells = gridCells;

            return gridSystem;
        }

        public async Task<bool> UpdateGridSystemNameAsync(int id, string newName)
        {
            var parameters = new { Id = id, Name = newName };
            const string query = "UPDATE gridsystems SET Name = @Name WHERE Id = @Id";

            var affectedRows = await _connection.ExecuteAsync(query, parameters);
            return affectedRows > 0;
        }
        
        private async Task<IEnumerable<GridCell>> GetAllGridCellsForGridSystemAsync(int gridSystemId)
        {
            var allGridCells = await _gridCellRepo.GetAllAsync();
            return allGridCells.Where(gc => gc.gridsystemid == gridSystemId);
        }

        public async Task<int> InsertGridAndCellsAsync(GridSystem gridSystem, List<GridCell> cells)
        {
            const string gridInsertQuery = "INSERT INTO gridsystems (Name, EstablishmentDate) VALUES (@Name, @EstablishmentDate) RETURNING id";
            int gridSystemId = await _connection.ExecuteScalarAsync<int>(gridInsertQuery, new { gridSystem.name, gridSystem.establishmentdate });

            foreach(var cell in cells) 
            {
                cell.gridsystemid = gridSystemId;
                const string cellInsertQuery = "INSERT INTO gridcells (gridsystemid, rowposition, columnposition) VALUES (@gridsystemid, @rowposition, @columnposition)";
                await _connection.ExecuteAsync(cellInsertQuery, cell);
            }

            return gridSystemId;
        }

        public async Task<bool> SoftDeleteGridSystemByIdAsync(int id)
        {
            var parameters = new { Id = id };
            const string query = "UPDATE gridsystems SET establishmentdate = null WHERE Id = @Id";

            var affectedRows = await _connection.ExecuteAsync(query, parameters);
            
            return affectedRows > 0;
        }
    }
}
