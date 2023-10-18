
namespace GridStatusHub.Domain.Responses.GridCell 
{
    public class GridCellResponse
    {
        public int Id { get; set; } 
        public int GridSystemId { get; set; }  
        public int RowPosition { get; set; } 
        public int ColumnPosition { get; set; } 
        public string? ColorStatus { get; set; }
    }
}