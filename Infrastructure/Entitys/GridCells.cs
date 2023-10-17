using Dapper.Contrib.Extensions;

namespace GridStatusHub.Infra.Entities
{

    [Table("GridCells")]
    public class GridCell
    {
        [Key]
        public int Id { get; set; }

        public int GridSystemId { get; set; }

        public int RowPosition { get; set; }
        public int ColumnPosition { get; set; }

        public string? ColorStatus { get; set; }
    }
}
