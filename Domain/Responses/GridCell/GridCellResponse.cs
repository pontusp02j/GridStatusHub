
namespace GridStatusHub.Domain.Responses.GridCell 
{
    public class GridCellResponse
    {
        public int id { get; set; } 
        public int gridsystemid { get; set; }  
        public int rowposition { get; set; } 
        public int columnposition { get; set; } 
        public string? colorstatus { get; set; }
    }
}