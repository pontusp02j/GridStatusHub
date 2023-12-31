using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;
using GridStatusHub.Domain.Requests.GridSystem;
using GridStatusHub.Domain.Responses.GridCell;
using GridStatusHub.Domain.Responses.GridSystem;
using GridStatusHub.Domain.Service.Rules;
using GridStatusHub.Domain.Utilities;

namespace GridStatusHub.Domain.HandlerRequests.Command  
{
    public class UpdateGridSystemCommandHandler : IUpdateGridSystemCommandHandler<GridSystemRequest, GridSystemResponse>
    {
        private readonly IGridSystemRepo<GridSystem> _gridSystemRepo;
        private readonly GridCellColorService _colorService;
        private readonly GridSystemIntegrityService _integrityService;
        private readonly ValidationUtility _validationUtility;

        public UpdateGridSystemCommandHandler(IGridSystemRepo<GridSystem> gridSystemRepo, GridCellColorService colorService,
            GridSystemIntegrityService integrityService, ValidationUtility validationUtility
        )
        {
            _gridSystemRepo = gridSystemRepo;
            _colorService = colorService;
            _integrityService = integrityService;
            _validationUtility = validationUtility;
        }

        public async Task<GridSystemResponse> HandleAsync(GridSystemRequest request, CancellationToken cancellationToken)
        {
            if (request.GridCells != null && request.GridCells.Any())
            {
                return await UpdateCellColorOnly(request);
            }
            else
            {
                return await UpdateGridSystemWithoutCells(request);
            }
        }

        private async Task<GridSystemResponse> UpdateGridSystemWithoutCells(GridSystemRequest gridSystemData)
        {
            GridSystemResponse response = new GridSystemResponse();
            bool isNameUnique = await _integrityService.IsNameUnique(gridSystemData.Name, gridSystemData.Id);
            
            var validationResults = _validationUtility.ValidateRequest(gridSystemData);
            
            if (validationResults.Any() || !isNameUnique)
            {
                response.Message = "Error";
                return response;
            }

            var gridSystemToUpdate = await _gridSystemRepo.GetByIdAsync(gridSystemData.Id);
            if (gridSystemToUpdate != null)
            {
                gridSystemToUpdate.name = gridSystemData.Name;
                gridSystemToUpdate.establishmentdate = gridSystemData.EstablishmentDate ?? gridSystemToUpdate.establishmentdate;

                await _gridSystemRepo.UpdateGridSystemNameAsync(gridSystemToUpdate.id, gridSystemToUpdate.name);

                response.Id = gridSystemToUpdate.id;
                response.Name = gridSystemToUpdate.name;
                response.EstablishmentDate = gridSystemToUpdate.establishmentdate;
            }
            else
            {
                response.Message = "GridSystem not found.";
            }

            return response;
        }


        private async Task<GridSystemResponse> UpdateCellColorOnly(GridSystemRequest gridSystemData)
        {
            GridSystemResponse response = new GridSystemResponse();
                    
            var gridSystem = await _gridSystemRepo.GetGridSystemWithCellsAsync(gridSystemData.Id);

            if (gridSystem != null)
            {
                var gridCellRequest = gridSystemData.GridCells?.FirstOrDefault();

                var cellToUpdate = gridSystem.GridCells?.SingleOrDefault(cell => cell.id == gridCellRequest?.Id);
                        
                if (cellToUpdate != null)
                {
                    cellToUpdate.colorstatus = _colorService.GetNextStatus(cellToUpdate.colorstatus);
                    await _gridSystemRepo.UpdateAsync(cellToUpdate);
                            
                    var gridCellsResponseList = gridSystem.GridCells?.Select(cell => new GridCellResponse 
                    {
                        id = cell.id, 
                        colorstatus = cell.colorstatus
                    }).ToList();

                    response.Id = gridSystem.id;
                    response.Name = gridSystem.name;
                    response.EstablishmentDate = gridSystem.establishmentdate;
                    response.GridCells = gridCellsResponseList;
                }
            }
            else
            {
                response.Message = "Grid system not found.";
            }

            return response;
        }
    }
}