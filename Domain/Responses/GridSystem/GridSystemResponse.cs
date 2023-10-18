using GridStatusHub.Domain.Responses.GridCell;

namespace GridStatusHub.Domain.Responses.GridSystem 
{
    public class GridSystemResponse
    {
        public int Id { get; set; }  
        public string? Name { get; set; } 
        public DateTime EstablishmentDate { get; set; } 
        public ICollection<GridCellResponse>? GridCells { get; set; }
        public string? Message {get; set;}
    }
}