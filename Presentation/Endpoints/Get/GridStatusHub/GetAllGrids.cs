using GridStatusHub.Domain.Requests.GridSystem;
using GridStatusHub.Domain.Responses.GridSystem;
using GridStatusHub.Domain.Context;
using FastEndpoints;

namespace GridStatusHub.Presentation.Endpoints {
    public class GridSystemEndpoint : Endpoint<GridSystemRequest, List<GridSystemResponse>>
    {
        private readonly IGetAllGridSystemsQueryHandler _queryHandler;

        public GridSystemEndpoint(IGetAllGridSystemsQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public override void Configure()
        {
            Get("/gridsystems");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GridSystemRequest r, CancellationToken c)
        {
            var gridSystems = await _queryHandler.HandleAsync();
            await SendAsync(gridSystems.ToList());
        }
    }

}