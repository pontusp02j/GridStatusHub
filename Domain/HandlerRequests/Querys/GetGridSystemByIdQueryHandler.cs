using GridStatusHub.Domain.Responses.GridSystem;
using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;
using GridStatusHub.Domain.Responses.GridCell;

namespace GridStatusHub.Domain.HandlerRequests.Query
{
    public class GetGridSystemByIdQueryHandler : IGetGridSystemByIdQueryHandler
    {
        private readonly IGridSystemRepo<GridSystem> _reposGridSystem;

        public GetGridSystemByIdQueryHandler(IGridSystemRepo<GridSystem> reposGridSystem)
        {
            _reposGridSystem = reposGridSystem;
        }

        public async Task<GridSystemResponse> HandleAsync(int id)
        {
            var gridSystem = await _reposGridSystem.GetGridSystemWithCellsAsync(id);
            if(gridSystem == null) return null;

            var gridSystemResponse = new GridSystemResponse
            {
                Id = gridSystem.Id,
                Name = gridSystem.Name,
                EstablishmentDate = gridSystem.EstablishmentDate,
                GridCells = gridSystem.GridCells?.Select(gridCell => new GridCellResponse
                {
                    Id = gridCell.Id,
                    GridSystemId = gridCell.GridSystemId,
                    RowPosition = gridCell.RowPosition,
                    ColumnPosition = gridCell.ColumnPosition,
                    ColorStatus = gridCell.ColorStatus
                }).ToList()
            };

            return gridSystemResponse;                
        }
    }
}
