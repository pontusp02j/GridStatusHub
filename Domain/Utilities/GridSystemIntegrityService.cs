using GridStatusHub.Domain.Context;
using GridStatusHub.Domain.Entities;

namespace GridStatusHub.Domain.Utilities
{
    public class GridSystemIntegrityService
    {
        private readonly IGridSystemRepo<GridSystem> _gridSystemRepo;

        public GridSystemIntegrityService(IGridSystemRepo<GridSystem> gridSystemRepo)
        {
            _gridSystemRepo = gridSystemRepo;
        }

        public async Task<bool> IsNameUnique(string name, int? currentId = null)
        {
            var existingGridSystemWithName = await _gridSystemRepo.GetGridSystemByNameAsync(name);
            
            if (existingGridSystemWithName == null) 
            {
                return true;
            }

            // If checking during an update operation, the name can be the same for the current item
            if (currentId.HasValue && existingGridSystemWithName.id == currentId.Value) 
            {
                return true;
            }

            return false;
        }
    }

}