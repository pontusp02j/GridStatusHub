using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;

namespace GridStatusHub.Domain.HandlerRequests.Command 
{
    public class DeleteGridSystemCommandHandler : IDeleteGridSystemCommandHandler
    {
        private readonly IGridSystemRepo<GridSystem> _reposGridSystem;

        public DeleteGridSystemCommandHandler(IGridSystemRepo<GridSystem> reposGridSystem)
        {
            _reposGridSystem = reposGridSystem;
        }

        public async Task<bool> HandleAsync(int id)
        {
            return await _reposGridSystem.SoftDeleteGridSystemByIdAsync(id);                
        }
    }
}
