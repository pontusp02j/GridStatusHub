using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;
using GridStatusHub.Domain.Requests.GridSystem;
using GridStatusHub.Domain.Responses.GridCell;
using GridStatusHub.Domain.Responses.GridSystem;
using GridStatusHub.Domain.Utilities;

namespace GridStatusHub.Domain.HandlerRequests.Command  
{
    public class CreateGridSystemCommandHandler : ICreateGridSystemCommandHandler<GridSystemRequest, GridSystemResponse>
    {
        private readonly IGridSystemRepo<GridSystem> _gridSystemRepo;
        private readonly GridSystemIntegrityService _integrityService;
        private readonly ValidationUtility _validationUtility;

        public CreateGridSystemCommandHandler(IGridSystemRepo<GridSystem> gridSystemRepo,
            GridSystemIntegrityService integrityService, ValidationUtility validationUtility
        )
        {
            _gridSystemRepo = gridSystemRepo;
            _integrityService = integrityService;
            _validationUtility = validationUtility;
        }

        public async Task<GridSystemResponse> HandleAsync(GridSystemRequest request, CancellationToken cancellationToken)
        {
            bool isNameUnique = await _integrityService.IsNameUnique(request.Name, request.Id);
            GridSystemResponse response = new GridSystemResponse();
            
            var validationResults = _validationUtility.ValidateRequest(request);
            if (validationResults.Any())
            {
                return response;
            }

            if (!isNameUnique)
            {
                return response;
            }

            var gridSystem = new GridSystem
            {
                name = request.Name,
                establishmentdate = DateTime.Now
            };

            List<GridCell> cells = GenerateGridCellsLayout();

            (var id, var updatedCells) = await _gridSystemRepo.InsertGridAndCellsAsync(gridSystem, cells);

            response.Id = id;
            response.Name = gridSystem.name;
            response.GridCells = updatedCells.Select(cell => ConvertPropertyService.CopyPropertiesFrom<GridCell, GridCellResponse>(cell)).ToList();    
            
            return response;
        }

        private List<GridCell> GenerateGridCellsLayout()
        {
            var cells = new List<GridCell>();
            for (int row = 1; row <= 5; row++)
            {
                for (int col = 1; col <= 5; col++)
                {
                    cells.Add(new GridCell
                    {
                        rowposition = row,
                        columnposition = col,
                    });
                }
            }

            return cells;
        }
    }
}