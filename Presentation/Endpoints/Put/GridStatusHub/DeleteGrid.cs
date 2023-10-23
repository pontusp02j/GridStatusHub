using FastEndpoints;
using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Requests.GridSystem;
using GridStatusHub.Domain.Responses.GridSystem;

namespace GridStatusHub.Presentation.Endpoints 
{
    public class DeleteGridSystemEndpoint : Endpoint<GridSystemRequest, GridSystemResponse>
    {
        private readonly IDeleteGridSystemCommandHandler _deleteHandler;

        public DeleteGridSystemEndpoint(IDeleteGridSystemCommandHandler deleteHandler)
        {
            _deleteHandler = deleteHandler;
        }

        public override void Configure()
        {
            Delete("/gridsystems/delete/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GridSystemRequest request, CancellationToken c)
        {
            bool result = await _deleteHandler.HandleAsync(request.Id);
            
            await (result 
            ? SendOkAsync() 
            : SendErrorsAsync(StatusCodes.Status400BadRequest));
        }
    }
}
