using GridStatusHub.Domain.Responses.GridSystem;
using GridStatusHub.Domain.HandlerRequests.Query;

namespace GridStatusHub.Domain.Context 
{
    public interface IGetAllGridSystemsQueryHandler
    {
        Task<IEnumerable<GridSystemResponse>> HandleAsync();
    }
}
