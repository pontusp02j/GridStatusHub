using FastEndpoints;
using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Requests.GridSystem;
using GridStatusHub.Domain.Responses.GridSystem;

namespace GridStatusHub.Presentation.Endpoints
{
    public class UpdateGridSystemEndpoint : Endpoint<GridSystemRequest, GridSystemResponse>
    {
        private readonly IUpdateGridSystemCommandHandler<GridSystemRequest, GridSystemResponse> _updateHandler;

        public UpdateGridSystemEndpoint(IUpdateGridSystemCommandHandler<GridSystemRequest, GridSystemResponse> updateHandler)
        {
            _updateHandler = updateHandler;
        }

        public override void Configure()
        {
            Put("/gridsystem/Update");
            AllowAnonymous(); // TODO: Note: In a real-world scenario, secure the endpoints
        }

        public override async Task HandleAsync(GridSystemRequest request, CancellationToken c)
        {
            var response = await _updateHandler.HandleAsync(request, c);

            if (response != null && response.Id > 0)
            {
                await SendAsync(response, StatusCodes.Status200OK);
            }
            else
            {
                GridSystemResponse errorResponse = new GridSystemResponse
                {
                    Message = "Grid system update failed or not found.",
                };

                await SendAsync(errorResponse, StatusCodes.Status404NotFound);
            }
        }
    }
}
