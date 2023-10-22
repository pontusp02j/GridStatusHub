using GridStatusHub.Domain.Requests.GridCell;

namespace GridStatusHub.Domain.Requests.GridSystem 
{
    public class GridSystemRequest
    {
        public int Id { get; set; }  
        public string Name { get; set; } = string.Empty; 
        public DateTime? EstablishmentDate { get; set; } 
        public ICollection<GridCellRequest>? GridCells { get; set; }
    }
} 