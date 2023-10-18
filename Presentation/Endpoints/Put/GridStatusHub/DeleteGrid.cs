using FastEndpoints;
using GridStatusHub.Domain.Context;

namespace GridStatusHub.Presentation.Endpoints 
{
    public class DeleteGridSystemEndpoint : Endpoint<int, bool>
    {
        private readonly IDeleteGridSystemCommandHandler _deleteHandler;

        public DeleteGridSystemEndpoint(IDeleteGridSystemCommandHandler deleteHandler)
        {
            _deleteHandler = deleteHandler;
        }

        public override void Configure()
        {
            Delete("/gridsystem/Delete/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(int id, CancellationToken c)
        {
            bool result = await _deleteHandler.HandleAsync(id);
            
            await (result 
            ? SendAsync(true, StatusCodes.Status200OK) 
            : SendAsync(false, StatusCodes.Status404NotFound));
        }
    }
}
