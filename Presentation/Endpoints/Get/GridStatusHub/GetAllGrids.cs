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
            var gridSystems = await _queryHandler.HandleAsync() ?? Enumerable.Empty<GridSystemResponse>();
            var gridSystemList = gridSystems.ToList();

            if (gridSystemList.Count == 0)
            {
                var notFoundResponse = new GridSystemResponse 
                {
                    Message = "No grid systems found." 
                };
                await SendAsync(new List<GridSystemResponse> { notFoundResponse });
            }
            else
            {
                await SendAsync(gridSystemList);
            }
        }
    }

}