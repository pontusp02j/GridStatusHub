using FastEndpoints;
using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Requests.GridSystem;
using GridStatusHub.Domain.Responses.GridSystem;


namespace GridStatusHub.Presentation.Endpoints
{
    public class CreateGridSystemEndpoint : Endpoint<GridSystemRequest, GridSystemResponse>
    {
        private readonly ICreateGridSystemCommandHandler<GridSystemRequest, GridSystemResponse> _createHandler;

        public CreateGridSystemEndpoint(ICreateGridSystemCommandHandler<GridSystemRequest, GridSystemResponse> createHandler)
        {
            _createHandler = createHandler;
        }

        public override void Configure()
        {
            Post("/gridsystem");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GridSystemRequest request, CancellationToken c)
        {
            var response = await _createHandler.HandleAsync(request, c);

            if (response != null && response.Id > 0)
            {
                await SendAsync(response, StatusCodes.Status201Created);
            }
            else
            {
                GridSystemResponse errorResponse = new GridSystemResponse
                {
                    Message = "Grid system creation failed.",
                };

                await SendAsync(errorResponse, StatusCodes.Status400BadRequest);
            }
        }
    }
}
