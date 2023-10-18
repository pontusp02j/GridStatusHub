using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;
using GridStatusHub.Domain.Requests.GridSystem;
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

        public UpdateGridSystemCommandHandler(IGridSystemRepo<GridSystem> gridSystemRepo, GridCellColorService colorService,
            GridSystemIntegrityService integrityService
        )
        {
            _gridSystemRepo = gridSystemRepo;
            _colorService = colorService;
            _integrityService = integrityService;
        }

        public async Task<GridSystemResponse> HandleAsync(GridSystemRequest request, CancellationToken cancellationToken)
        {
            return request.GridCells == null || !request.GridCells.Any()
                ? await UpdateGridSystemWithoutCells(request)
                : await UpdateGridCell(request);
        }

        private async Task<GridSystemResponse> UpdateGridSystemWithoutCells(GridSystemRequest gridSystemData)
        {
            GridSystemResponse response = new GridSystemResponse();

            bool isNameUnique = await _integrityService.IsNameUnique(gridSystemData.Name, gridSystemData.Id);
            
            if (!isNameUnique)
            {
                response.Message = "The GridSystem name is not unique.";
                return response;
            }

            var gridSystemToUpdate = await _gridSystemRepo.GetByIdAsync(gridSystemData.Id);
            if (gridSystemToUpdate != null)
            {
                gridSystemToUpdate.Name = gridSystemData.Name;
                gridSystemToUpdate.EstablishmentDate = gridSystemData.EstablishmentDate ?? gridSystemToUpdate.EstablishmentDate;

                await _gridSystemRepo.UpdateAsync(gridSystemToUpdate);

                response.Id = gridSystemToUpdate.Id;
                response.Name = gridSystemToUpdate.Name;
                response.EstablishmentDate = gridSystemToUpdate.EstablishmentDate;
            }

            return response;
        }

        private async Task<GridSystemResponse> UpdateGridCell(GridSystemRequest gridSystemData)
        {
            GridSystemResponse response = new GridSystemResponse();

            var gridCellRequest = gridSystemData.GridCells?.FirstOrDefault();
            
            if (gridCellRequest == null) 
            {
                return response;  
            }

            var gridSystemToUpdate = await _gridSystemRepo.GetGridSystemWithCellsAsync(gridSystemData.Id);

            if (gridSystemToUpdate != null && gridSystemToUpdate.GridCells != null)
            {
                var cellToUpdate = gridSystemToUpdate.GridCells.FirstOrDefault(cell => cell.Id == gridCellRequest.Id);

                if (cellToUpdate != null)
                {
                    cellToUpdate.GridSystemId = gridCellRequest.GridSystemId;
                    cellToUpdate.RowPosition = gridCellRequest.RowPosition;
                    cellToUpdate.ColumnPosition = gridCellRequest.ColumnPosition;

                    cellToUpdate.ColorStatus = _colorService.GetNextStatus(cellToUpdate.ColorStatus);

                    await _gridSystemRepo.UpdateAsync(gridSystemToUpdate);
                }
            }

            return response;
        }

    }

}