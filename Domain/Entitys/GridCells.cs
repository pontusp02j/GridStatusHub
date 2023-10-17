namespace GridStatusHub.Domain.Entities
{
    public class GridCell
    {
        public int Id { get; set; }

        public int GridSystemId { get; set; }

        public int RowPosition { get; set; }
        public int ColumnPosition { get; set; }

        public string? ColorStatus { get; set; }
    }
}
