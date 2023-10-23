using GridStatusHub.Domain.Responses.GridSystem;
using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;
using System.Linq;
using GridStatusHub.Domain.Responses.GridCell;


namespace GridStatusHub.Domain.HandlerRequests.Query 
{
    public class GetAllGridSystemsQueryHandler : IGetAllGridSystemsQueryHandler
    {
        private readonly IGridSystemRepo<GridSystem> _reposGridSystem;

        public GetAllGridSystemsQueryHandler(IGridSystemRepo<GridSystem> reposGridSystem)
        {
            _reposGridSystem = reposGridSystem;
        }

        public async Task<IEnumerable<GridSystemResponse>> HandleAsync()
        {
            IEnumerable<GridSystem> gridSystems = await _reposGridSystem.GetAllGridSystemsAndCellsAsync();

            var gridSystemResponses = gridSystems.Select(gridSystem => new GridSystemResponse
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
            }).ToList();

            return gridSystemResponses;                
        }
    }
}
