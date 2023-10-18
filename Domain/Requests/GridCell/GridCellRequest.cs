namespace GridStatusHub.Domain.Requests.GridCell 
{
    public class GridCellRequest
    {
        public int? Id { get; set; }  
        public int GridSystemId { get; set; }  
        public int RowPosition { get; set; } 
        public int ColumnPosition { get; set; } 
        public string? ColorStatus { get; set; }
    }

}