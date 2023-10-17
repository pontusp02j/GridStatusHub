using Dapper.Contrib.Extensions;

namespace GridStatusHub.Infra.Entities
{
    [Table("GridSystems")]
    public class GridSystem
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime EstablishmentDate { get; set; }
        public ICollection<GridCell>? GridCells { get; set; }
    }
}
