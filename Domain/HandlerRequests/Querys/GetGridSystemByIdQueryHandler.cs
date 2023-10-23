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
                    id = gridCell.id,
                    gridsystemid = gridCell.gridsystemid,
                    rowposition = gridCell.rowposition,
                    columnposition = gridCell.columnposition,
                    colorstatus = gridCell.colorstatus
                }).ToList()
            };

            return gridSystemResponse;                
        }
    }
}
