using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;
using GridStatusHub.Domain.Requests.GridSystem;
using GridStatusHub.Domain.Responses.GridSystem;
using GridStatusHub.Domain.Utilities;

namespace GridStatusHub.Domain.HandlerRequests.Command  
{
    public class CreateGridSystemCommandHandler : ICreateGridSystemCommandHandler<GridSystemRequest, GridSystemResponse>
    {
        private readonly IGridSystemRepo<GridSystem> _gridSystemRepo;
        private readonly GridSystemIntegrityService _integrityService;

        public CreateGridSystemCommandHandler(IGridSystemRepo<GridSystem> gridSystemRepo,
            GridSystemIntegrityService integrityService
        )
        {
            _gridSystemRepo = gridSystemRepo;
            _integrityService = integrityService;
        }

        public async Task<GridSystemResponse> HandleAsync(GridSystemRequest request, CancellationToken cancellationToken)
        {
            bool isNameUnique = await _integrityService.IsNameUnique(request.Name, request.Id);
            GridSystemResponse response = new GridSystemResponse();
            
            if (!isNameUnique)
            {
                response.Message = "The GridSystem name is not unique.";
                return response;
            }

            var gridSystem = new GridSystem
            {
                Name = request.Name,
                EstablishmentDate = DateTime.Now
            };

            var gridSystemId = await _gridSystemRepo.InsertAsync(gridSystem);

            List<GridCell> cells = GenerateGridCellsLayout(gridSystemId);
            foreach(var cell in cells)
            {
                await _gridSystemRepo.InsertAsync(cell);
            }

            return response;
        }

        private List<GridCell> GenerateGridCellsLayout(int gridSystemId)
        {
            var cells = new List<GridCell>();
            for (int row = 1; row <= 5; row++)
            {
                for (int col = 1; col <= 5; col++)
                {
                    cells.Add(new GridCell
                    {
                        GridSystemId = gridSystemId,
                        RowPosition = row,
                        ColumnPosition = col,
                    });
                }
            }

            return cells;
        }
    }
}