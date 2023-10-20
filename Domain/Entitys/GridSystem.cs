namespace GridStatusHub.Domain.Entities
{
    public class GridSystem
    {
        public int id { get; set; }

        public string? name { get; set; }

        public DateTime establishmentdate { get; set; }
        public ICollection<GridCell>? GridCells { get; set; }
    }
}
