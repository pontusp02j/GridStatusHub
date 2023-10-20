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

            var gridSystemResponse = new GridSystemResponse
            {
                Id = gridSystem.id,
                Name = gridSystem.name,
                EstablishmentDate = gridSystem.establishmentdate,
                GridCells = gridSystem.GridCells?.Select(gridCell => new GridCellResponse
                {
                    Id = gridCell.id,
                    GridSystemId = gridCell.gridsystemid,
                    RowPosition = gridCell.rowposition,
                    ColumnPosition = gridCell.columnposition,
                    ColorStatus = gridCell.colorstatus
                }).ToList()
            };

            return gridSystemResponse;                
        }
    }
}
