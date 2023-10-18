using GridStatusHub.Domain.Requests.GridSystem;
using GridStatusHub.Domain.Responses.GridSystem;
using GridStatusHub.Domain.Context;
using FastEndpoints;

namespace GridStatusHub.Presentation.Endpoints
{
    public class GetGridSystemByIdEndpoint : Endpoint<GridSystemRequest, GridSystemResponse>
    {
        private readonly IGetGridSystemByIdQueryHandler _queryHandler;

        public GetGridSystemByIdEndpoint(IGetGridSystemByIdQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public override void Configure()
        {
            Get("/gridsystems/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GridSystemRequest r, CancellationToken c)
        {
            var gridSystem = await _queryHandler.HandleAsync(r.Id);
            if (gridSystem == null)
            {
                var notFoundResponse = new GridSystemResponse 
                {
                    Message = $"Grid system with ID {r.Id} not found."
                };
                await SendAsync(notFoundResponse);
            }
            else
            {
                await SendAsync(gridSystem);
            }
        }
    }
}
