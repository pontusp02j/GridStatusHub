namespace GridStatusHub.Domain.Entities
{
    public class GridSystem
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime EstablishmentDate { get; set; }
        public ICollection<GridCell>? GridCells { get; set; }
    }
}
