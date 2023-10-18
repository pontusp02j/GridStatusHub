using GridStatusHub.Domain.Responses.GridSystem;

namespace GridStatusHub.Domain.Context 
{
    public interface IGetGridSystemByIdQueryHandler
    {
        Task<GridSystemResponse> HandleAsync(int id);
    }
}
